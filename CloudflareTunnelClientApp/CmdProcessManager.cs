using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudflareTunnelBindApp
{
    using TranslationKey = LanguageManager.TranslationKey;
    internal class CmdProcessManager
    {
        private readonly struct ProcessInfo
        {
            public ProcessInfo(Process process, TunnelInfoTable.TunnelInfo info)
            {
                ProcessObject = process;
                this.info = info;
            }

            public Process ProcessObject { get; }
            public TunnelInfoTable.TunnelInfo info { get; }
        }

        // 관리할 백그라운드 프로세스 객체
        private Dictionary<int, ProcessInfo> backgroundProcesses;

        private TextBox cmdOutput;

        public delegate void AllProcessesStoppedEventHandler();
        public event AllProcessesStoppedEventHandler AllProcessesStatusUpdate;

        public bool IsStoppedAllProcesses { get; private set; } = true;

        public CmdProcessManager(TextBox outputBox)
        {
            backgroundProcesses = new Dictionary<int, ProcessInfo>();
            cmdOutput = outputBox;
        }

        public void Start(List<TunnelInfoTable.TunnelInfo> tunnelInfos)
        {

            if (!IsStoppedAllProcesses)
            {
                MessageBox.Show(LanguageManager.Translate(TranslationKey.ProcessAlreadyRunning), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(tunnelInfos.Count == 0)
            {
                MessageBox.Show(LanguageManager.Translate(TranslationKey.NoTunnelInfo), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cmdOutput.Text = "";

            foreach (var info in tunnelInfos)
            {
                Process backgroundProcess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,   // 표준 입력을 리디렉션하여 명령어를 보낼 수 있도록 설정
                    UseShellExecute = false,        // Shell을 사용하지 않아야 리디렉션 가능
                    CreateNoWindow = true,         // 콘솔 창을 숨김
                    RedirectStandardOutput = true,
                    RedirectStandardError = true    // 표준 출력을 리디렉션하여 출력 내용을 읽을 수 있도록 설정
                };

                string userPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
                startInfo.EnvironmentVariables["PATH"] = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);

                backgroundProcess.StartInfo = startInfo;
                backgroundProcess.EnableRaisingEvents = true; // 프로세스 종료 이벤트 활성화
                //backgroundProcess.Exited += BackgroundProcess_Exited; // 프로세스가 스스로 종료되었을 때의 이벤트 핸들러 연결

                backgroundProcess.OutputDataReceived += BackgroundProcess_OutputDataReceived;
                backgroundProcess.ErrorDataReceived += BackgroundProcess_OutputDataReceived;

                backgroundProcess.Start();
                System.Threading.Thread.Sleep(100); // 프로세스가 완전히 시작될 시간을 잠시 대기

                backgroundProcesses.Add(
                    backgroundProcess.Id, 
                    new ProcessInfo(backgroundProcess, info));

                // 비동기 출력 읽기를 시작합니다. 이제부터 OutputDataReceived 이벤트가 활성화됩니다.
                backgroundProcess.BeginOutputReadLine();
                backgroundProcess.BeginErrorReadLine();

                string command = $"cloudflared access {info.Protocol.ToString().ToLower()} --url {info.LocalBindUrl}:{info.Port} --hostname {info.Hostname}";

                // cmd에 명령어를 전송합니다.
                backgroundProcess.StandardInput.WriteLine(command);

                /*
                // cmd에 명령어들을 순차적으로 전송합니다.
                backgroundProcess.StandardInput.WriteLine("waitfor /t 5 placehold");
                backgroundProcess.StandardInput.WriteLine("echo 모든 작업 완료");
                backgroundProcess.StandardInput.WriteLine("exit"); // 프로세스 종료
                */

                backgroundProcess.StandardInput.Close();
            }

            UpdateProcessesStatus();
        }

        /// <summary>
        /// 프로세스가 스스로 종료되었을 때 UI 상태를 업데이트합니다.
        /// </summary> 
        /*
        private void BackgroundProcess_Exited(object sender, EventArgs e)
        {
            Process exitedProcess = sender as Process;
            
            backgroundProcesses.Remove(exitedProcess.Id);

            UpdateProcessesStatus();
        }
        */

        /// <summary>
        /// 백그라운드 프로세스를 중단합니다.
        /// </summary>
        public void StopProcesses()
        {
            if (IsStoppedAllProcesses)
            {
                UpdateProcessesStatus();
                return;
            }

            foreach (var backgroundProcess in backgroundProcesses.Values.ToList())
            {
                int pid = backgroundProcess.ProcessObject.Id;
                KillProcessAndChild(pid);

                if (!backgroundProcess.ProcessObject.HasExited)
                {
                    backgroundProcess.ProcessObject.Kill();
                }

                /*
                backgroundProcess.CloseMainWindow();
                // Ctrl+C 이벤트 전송 시도
                if (backgroundProcess.WaitForExit(3000))
                {
                    // 프로세스가 정상적으로 종료됨
                    Console.WriteLine($"프로세스 ID {backgroundProcess.Id}, {backgroundProcess.SessionId}가 정상적으로 종료됨.");
                }
                else
                {
                    // 프로세스가 여전히 실행 중이면 강제 종료
                    Console.WriteLine($"프로세스 ID {backgroundProcess.Id}, {backgroundProcess.SessionId}가 응답하지 않아 강제 종료 시도.");
                    backgroundProcess.Kill(true);
                    backgroundProcess.Dispose();
                }
                */

                PrintResultOfCommandInCmd(pid, LanguageManager.Translate(TranslationKey.ProcessTermination));
            }

            backgroundProcesses.Clear();
            UpdateProcessesStatus();
        }

        private void KillProcessAndChild(int pid)
        {
            // taskkill 명령어를 사용하여 프로세스와 자식 프로세스를 모두 종료합니다.
            ProcessStartInfo psi = new ProcessStartInfo("taskkill", $"/F /T /PID {pid}")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process proc = new Process { StartInfo = psi };

            proc.Start();
            System.Threading.Thread.Sleep(100);

            proc.WaitForExit(3000);

            if (!proc.HasExited)
            {
                proc.Kill();
            }

        }

        private void UpdateProcessesStatus()
        {
            if (backgroundProcesses.Count == 0)
            {
                IsStoppedAllProcesses = true;
            }
            else
            {
                IsStoppedAllProcesses = false;
            }
            AllProcessesStatusUpdate?.Invoke();
        }

        public void BackgroundProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // e.Data에 cmd의 출력 한 줄이 담겨 넘어옵니다. (null이 아닐 때만 처리)
            if (!string.IsNullOrEmpty(e.Data))
            {
                cmdOutput.Invoke(new Action(() => 
                {
                    cmdOutput.AppendText(e.Data + Environment.NewLine);
                }));
                if (e.Data.Length < 21 + 1) return;
                if (e.Data.Substring(21).StartsWith("INF Start Websocket listener host="))
                {
                    Process currentProcess = sender as Process;
                    PrintResultOfCommandInCmd(currentProcess.Id, LanguageManager.Translate(TranslationKey.TunnelRunning));
                }
                else if(!e.Data.StartsWith("ERR Error on Websocket listener error") 
                    && e.Data.StartsWith("failed to start forwarding server"))
                {
                    Process currentProcess = sender as Process;
                    PrintResultOfCommandInCmd(currentProcess.Id, LanguageManager.Translate(TranslationKey.TunnelBindFailed), 
                        LanguageManager.Translate(TranslationKey.MayExistAnotherProcessWaitingOnSamePort));
                }
            }
        }
        
        private void PrintResultOfCommandInCmd(int processId, string result, string note = "")
        {
            if (!note.Equals("")) note += Environment.NewLine;
            TunnelInfoTable.TunnelInfo info = backgroundProcesses[processId].info;
            string print = $"{result} - {info.Protocol}://{info.Hostname} -> {info.LocalBindUrl}:{info.Port}" +
                Environment.NewLine + Environment.NewLine + note;

            cmdOutput.Invoke(new Action(() =>
            {
                cmdOutput.AppendText(Environment.NewLine + print + Environment.NewLine);
            }));
        }
        
    }
}