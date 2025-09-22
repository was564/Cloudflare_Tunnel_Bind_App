using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudflareTunnelBindApp
{
    public static class LanguageManager
    {

        public enum Language
        {
            NotSet,
            Korean,
            English
        }

        public enum TranslationKey
        {
            Start,
            Stop,
            Exit,
            TunnelList,
            File,
            Language,
            Korean,
            English,
            CmdOutput,
            ProcessAlreadyRunning,
            NoTunnelInfo,
            ProcessTermination,
            TunnelRunning,
            TunnelBindFailed,
            MayExistAnotherProcessWaitingOnSamePort,
            CloudflaredNotInstalled,
            LetInstallCloudflared,
            CloudflaredInstallComplete,
            SaveErrorInTunnelIInfos,
            LoadErrorInTunnelInfos,
            DuplicateExecution,
        }

        static LanguageManager()
        {
            translations = new Dictionary<Language, Dictionary<TranslationKey, string>>()
            {
            { Language.Korean, new Dictionary<TranslationKey, string>()
            {
                { TranslationKey.Start, "시작" },
                { TranslationKey.Stop, "중단" },
                { TranslationKey.Exit, "종료" },
                { TranslationKey.TunnelList, "터널 리스트" },
                { TranslationKey.File, "파일" },
                { TranslationKey.Language, "언어(Language)" },
                { TranslationKey.Korean, "한국어(Korean)" },
                { TranslationKey.English, "영어" },
                { TranslationKey.CmdOutput, "Cmd 출력" },
                { TranslationKey.ProcessAlreadyRunning, "프로세스가 이미 실행 중입니다." },
                { TranslationKey.NoTunnelInfo, "터널 리스트의 정보를 다시 확인해주세요" },
                { TranslationKey.ProcessTermination, "프로세스 종료" },
                { TranslationKey.TunnelRunning, "터널 실행 중" },
                { TranslationKey.TunnelBindFailed, "터널 Bind 실패" },
                { TranslationKey.MayExistAnotherProcessWaitingOnSamePort, "같은 포트에서 대기 중인 다른 프로세스가 있을 수 있습니다." },
                { TranslationKey.CloudflaredNotInstalled, "cloudflared 패키지가 설치되어 있지 않습니다." },
                { TranslationKey.LetInstallCloudflared, "cloudflared 패키지를 설치하시겠습니까?" },
                { TranslationKey.CloudflaredInstallComplete, "cloudflared 설치가 완료되었습니다." },
                { TranslationKey.SaveErrorInTunnelIInfos, "터널 정보를 불러오는 중에 오류가 발생하였습니다." },
                { TranslationKey.LoadErrorInTunnelInfos, "터널 정보가 훼손되었습니다." },
                { TranslationKey.DuplicateExecution, "프로그램이 이미 실행 중입니다." },
            } },
            { Language.English, new Dictionary<TranslationKey, string>()
            {
                { TranslationKey.Start, "Start" },
                { TranslationKey.Stop, "Stop" },
                { TranslationKey.Exit, "Exit" },
                { TranslationKey.TunnelList, "Tunnel List" },
                { TranslationKey.File, "File" },
                { TranslationKey.Language, "Language" },
                { TranslationKey.Korean, "Korean" },
                { TranslationKey.English, "English" },
                { TranslationKey.CmdOutput, "Cmd Output" },
                { TranslationKey.ProcessAlreadyRunning, "Process is already running." },
                { TranslationKey.NoTunnelInfo, "Please check the information in the tunnel list again." },
                { TranslationKey.ProcessTermination, "Process Termination" },
                { TranslationKey.TunnelRunning, "Tunnel Running" },
                { TranslationKey.TunnelBindFailed, "Tunnel Bind Failed" },
                { TranslationKey.MayExistAnotherProcessWaitingOnSamePort, "There may be another process waiting on the same port." },
                { TranslationKey.CloudflaredNotInstalled, "The cloudflared package is not installed." },
                { TranslationKey.LetInstallCloudflared, "Would you like to install the cloudflared package?" },
                { TranslationKey.CloudflaredInstallComplete, "cloudflared installation is complete." },
                { TranslationKey.SaveErrorInTunnelIInfos, "An error occurred while saving tunnel information." },
                { TranslationKey.LoadErrorInTunnelInfos, "The tunnel information is corrupted." },
                { TranslationKey.DuplicateExecution, "The program is already running." },
            } }
            };
        }


        private static ToolStripMenuItem koreanMenuItem;
        private static ToolStripMenuItem englishMenuItem;
        private static Form1 mainForm;

        public static Language CurrentLanguage { get; private set; } = Language.NotSet;

        private static readonly Dictionary<Language, Dictionary<TranslationKey, string>> translations;

        public static void Initialize(Form1 form, ToolStripMenuItem koreanItem, ToolStripMenuItem englishItem)
        {
            mainForm = form;
            koreanMenuItem = koreanItem;
            englishMenuItem = englishItem;
            SetLanguage(Language.Korean);
            mainForm.UpdateUI();
        }

        public static void SetLanguage(Language lang)
        {
            if (CurrentLanguage == lang) return;

            CurrentLanguage = lang;
            koreanMenuItem.Checked = (lang == Language.Korean);
            englishMenuItem.Checked = (lang == Language.English);
            mainForm.UpdateUI();
        }

        public static string Translate(TranslationKey key)
        {
            if (CurrentLanguage == Language.NotSet)
                return key.ToString();
            if (translations.TryGetValue(CurrentLanguage, out var langDict))
            {
                if (langDict.TryGetValue(key, out var translated))
                {
                    return translated;
                }
            }
            return key.ToString();
        }
    }
}
