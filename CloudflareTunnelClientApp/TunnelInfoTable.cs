using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CloudflareTunnelBindApp
{
    public enum PROTOCOL
    {
        TCP,
        RDP,
        SSH,
        SMB,
    }

    public class TunnelInfoTable : DataTable
    {
        public struct TunnelInfo
        {
            public PROTOCOL Protocol;
            public string Hostname;
            public string LocalBindUrl;
            public ushort Port;
        }

        public TunnelInfoTable(string name = "TunnelInfoTable1") : base(name)
        {
            
            DataColumn protocolCol = new DataColumn()
            {
                ColumnName = "Protocol",
                DefaultValue = PROTOCOL.TCP,
                DataType = typeof(PROTOCOL),
                AllowDBNull = false
            };
            DataColumn hostnameCol = new DataColumn()
            {
                ColumnName = "Hostname",
                DataType = typeof(string)
            };
            DataColumn urlCol = new DataColumn()
            {
                ColumnName = "Local Bind Url",
                DefaultValue = "localhost",
                DataType = typeof(string),
                AllowDBNull = false
            };
            DataColumn portCol = new DataColumn()
            {
                ColumnName = "Port",
                DefaultValue = 80,
                DataType = typeof(ushort),
                AllowDBNull = false,
            };

            Columns.Add(protocolCol);
            Columns.Add(hostnameCol);
            Columns.Add(urlCol);
            Columns.Add(portCol);

            PrimaryKey = new DataColumn[] { urlCol, portCol };

            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

            bool result = Load(FilePath);
        }

        public List<TunnelInfo> GetTunnelInfos()
        {
            List<TunnelInfo> tunnelInfos = new List<TunnelInfo>();
            foreach (DataRow row in Rows)
            {
                TunnelInfo info = new TunnelInfo {
                    Protocol = (PROTOCOL)row["Protocol"],
                    Hostname = (string)row["Hostname"],
                    LocalBindUrl = (string)row["Local Bind Url"],
                    Port = (ushort)row["Port"]
                };
                tunnelInfos.Add(info);
            }

            return tunnelInfos;
        }

        public string FilePath {
            get {
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string appPath = Path.Combine(basePath, "CloudflareTunnelBindApp");
                return Path.Combine(appPath, "tunnels.xml");
            }
        }

        public bool Save()
        {
            this.AcceptChanges();
            //MessageBox.Show("Save to " + FilePath);
            try
            {
                var serializer = new XmlSerializer(typeof(List<TunnelInfo>));
                using (var writer = new StreamWriter(FilePath))
                {
                    serializer.Serialize(writer, GetTunnelInfos());
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Translate(LanguageManager.TranslationKey.SaveErrorInTunnelIInfos)
                    + Environment.NewLine + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool Load(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            var serializer = new XmlSerializer(typeof(List<TunnelInfo>));
            try
            {
                using (var reader = new System.IO.StreamReader(filePath))
                {
                    var tunnelInfos = (List<TunnelInfo>)serializer.Deserialize(reader);
                    foreach (var info in tunnelInfos)
                    {
                        var newRow = this.NewRow();
                        newRow["Protocol"] = info.Protocol;
                        newRow["Hostname"] = info.Hostname;
                        newRow["Local Bind Url"] = info.LocalBindUrl;
                        newRow["Port"] = info.Port;
                        this.Rows.Add(newRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Translate(LanguageManager.TranslationKey.LoadErrorInTunnelInfos), 
                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Rows.Clear();
                return false;
            }

            return true;
        }

        /*
        public PROTOCOL Protocol { get; set; }
        public string Hostname { get; set; }
        public string LocalBindUrl { get; set; }
        public string port { get; set; }
        */
    }
}
