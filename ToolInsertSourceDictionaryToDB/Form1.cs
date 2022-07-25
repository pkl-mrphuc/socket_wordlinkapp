using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolInsertSourceDictionaryToDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _connectionString = "Data Source=DESKTOP-DBGAO2D;Initial Catalog=WordLinkApp;User ID=phuclk;Password=Ak.123456;Encrypt=True;TrustServerCertificate=True;";
        private string _filePath = "E:\\MYPROJECT\\ASP.NETCoreApp\\WordLinkApp\\ToolInsertSourceDictionaryToDB\\Source\\words.txt";
        private SqlConnection _sqlConnection;
        private void btnRun_Click(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                string sql = "insert into [Dictionary](Text, Source) values(@text, @source)";
                using (StreamReader file = new StreamReader(_filePath))
                {
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        DictionaryItem item = JsonConvert.DeserializeObject<DictionaryItem>(ln);
                        if (!string.IsNullOrEmpty(item.text))
                        {
                            string source = item.source.ElementAt(0);
                            int result = _sqlConnection.Execute(sql, new { item.text, source });
                            if (result > 0)
                            {
                                this.Invoke(new Action(() => {
                                    lbCounter.Text = counter.ToString();
                                }));
                                counter++;
                            }
                        }

                    }
                    file.Close();
                }

                _sqlConnection.Close();
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Hoàn thành");
        }
    }

    public class DictionaryItem
    {
        public string text { get; set; }
        public List<string> source { get; set; }
    }
}
