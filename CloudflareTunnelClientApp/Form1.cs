using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudflareTunnelBindApp
{
    public partial class Form1 : Form
    {
        private CmdProcessManager processManager;

        // 프로그램의 실제 종료 여부를 확인하는 플래그
        private bool isExiting = false;

        public Form1()
        {
            InitializeComponent();
            processManager = new CmdProcessManager(cmdOutput);
            processManager.AllProcessesStatusUpdate += UpdateExecuteUIState;
            InstallPackage();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 폼 로드 시 초기 UI 상태 업데이트
            UpdateExecuteUIState();
        }

        // 폼을 닫을 때 발생하는 이벤트
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // '프로그램 종료' 메뉴를 통해 닫는 경우가 아니라면
            if (!isExiting && e.CloseReason == CloseReason.UserClosing)
            {
                // 이벤트 취소 후 폼을 숨기고 트레이 아이콘을 표시
                e.Cancel = true;
                this.Hide();
                notifyIcon1.Visible = true;
            }
        }

        // 트레이 아이콘 더블 클릭 시 폼을 다시 표시
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        // '시작' 클릭
        private void startItem_Click(object sender, EventArgs e)
        {
            StartProcess();
        }

        // '중단' 클릭
        private void stopItem_Click(object sender, EventArgs e)
        {
            StopProcess();
        }

        // '프로그램 종료' 클릭
        private void exitItem_Click(object sender, EventArgs e)
        {
            isExiting = true; // 실제 종료 플래그 설정
            StopProcess();    // 진행 중인 프로세스 정리
            Application.Exit(); // 애플리케이션 완전 종료
        }


        private void koreanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LanguageManager.SetLanguage(LanguageManager.Language.Korean);
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LanguageManager.SetLanguage(LanguageManager.Language.English);
        }

        /// <summary>
        /// 백그라운드 프로세스를 시작합니다.
        /// </summary>
        private void StartProcess()
        {
            tunnelInfoTable.Save();
            processManager.Start(tunnelInfoTable.GetTunnelInfos());
        }

        /// <summary>
        /// 백그라운드 프로세스를 중단합니다.
        /// </summary>
        private void StopProcess()
        {
            processManager.StopProcesses();
        }


        private void InstallPackage()
        {
            string result = "";

            using (Process checkPackageProcess = new Process())
            {
                checkPackageProcess.StartInfo.FileName = "cmd.exe";
                checkPackageProcess.StartInfo.Arguments = "/c winget list --id Cloudflare.cloudflared";
                checkPackageProcess.StartInfo.CreateNoWindow = true;
                checkPackageProcess.StartInfo.UseShellExecute = false;
                checkPackageProcess.StartInfo.RedirectStandardOutput = true;
                checkPackageProcess.Start();
                
                result = checkPackageProcess.StandardOutput.ReadToEnd();
                checkPackageProcess.WaitForExit();
                checkPackageProcess.Close();
            }

            int resultCount = result.Count(c => c == '\n');
            if (resultCount > 1)
                return;

            MessageBox.Show(LanguageManager.Translate(LanguageManager.TranslationKey.CloudflaredNotInstalled),
                "Installation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            DialogResult letInstallresult = 
                MessageBox.Show(LanguageManager.Translate(LanguageManager.TranslationKey.LetInstallCloudflared),
                "Install cloudflared", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (letInstallresult == DialogResult.No)
                return;

            using (Process installProcess = new Process())
            {
                installProcess.StartInfo.FileName = "cmd.exe";
                installProcess.StartInfo.Arguments = "/c winget install --id Cloudflare.cloudflared";
                installProcess.StartInfo.CreateNoWindow = false;
                installProcess.StartInfo.UseShellExecute = true;
                installProcess.StartInfo.Verb = "runas"; // 관리자 권한으로 실행
                installProcess.Start();
                installProcess.WaitForExit();
            }

            MessageBox.Show(LanguageManager.Translate(LanguageManager.TranslationKey.CloudflaredInstallComplete), 
                "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 현재 프로세스 상태에 따라 UI 컨트롤(버튼, 메뉴)의 활성화 상태를 업데이트합니다.
        /// </summary>
        private void UpdateExecuteUIState()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateExecuteUIState));
                return;
            }

            bool isProcessRunning = !processManager.IsStoppedAllProcesses;

            // 메인 폼 버튼 상태 업데이트
            btnStart.Enabled = !isProcessRunning;
            btnStop.Enabled = isProcessRunning;

            // 트레이 메뉴 아이템 상태 업데이트
            startItem.Enabled = !isProcessRunning;
            stopItem.Enabled = isProcessRunning;

            // 메뉴 스트립 버튼 상태 업데이트
            startToolStripMenuItem.Enabled = !isProcessRunning;
            stopToolStripMenuItem.Enabled = isProcessRunning;
        }

        public void UpdateUI()
        {
            btnStart.Text = LanguageManager.Translate(LanguageManager.TranslationKey.Start);
            btnStop.Text = LanguageManager.Translate(LanguageManager.TranslationKey.Stop);
            startToolStripMenuItem.Text = LanguageManager.Translate(LanguageManager.TranslationKey.Start);
            stopToolStripMenuItem.Text = LanguageManager.Translate(LanguageManager.TranslationKey.Stop);
            exitToolStripMenuItem.Text = LanguageManager.Translate(LanguageManager.TranslationKey.Exit);
            fileToolStripMenuItem.Text = LanguageManager.Translate(LanguageManager.TranslationKey.File);
            languageToolStripMenuItem.Text = LanguageManager.Translate(LanguageManager.TranslationKey.Language);
            cmdPage.Text = LanguageManager.Translate(LanguageManager.TranslationKey.CmdOutput);
            tunnelListPage.Text = LanguageManager.Translate(LanguageManager.TranslationKey.TunnelList);
            koreanToolStripMenuItem.Text = LanguageManager.Translate(LanguageManager.TranslationKey.Korean);
            englishToolStripMenuItem.Text = LanguageManager.Translate(LanguageManager.TranslationKey.English);
        }

        private const string DATA_ERROR_MSG =
@"데이터 입력이 올바르지 않습니다. 다음 사항을 확인해주세요:
- Protocol 입력은 TCP, RDP, SSH, SMB 중 하나여야 합니다.

- Hostname은 공백일 수 없습니다.
- LocalBindUrl은 공백일 수 없습니다.
- Port는 0 ~ 65535 숫자만 입력 가능합니다.

- 동일한 Hostname, Port 번호는 중복 입력할 수 없습니다.
";

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(DATA_ERROR_MSG + Environment.NewLine);
        }

    }
}
