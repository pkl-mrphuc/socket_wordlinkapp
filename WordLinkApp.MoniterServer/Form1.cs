using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp.Server;

namespace WordLinkApp.MoniterServer
{
    public partial class frmMonitor : Form
    {
        Form _monitor;
        public frmMonitor()
        {
            InitializeComponent();
            _server = new WebSocketServer("ws://127.0.0.1:1011");
            _monitor = this;
            _server.AddWebSocketService<Echo>("/Echo", () => new Echo(ref _monitor));
            _server.AddWebSocketService<EchoAll>("/EchoAll", () => new EchoAll(ref _monitor));
        }

        WebSocketServer _server;
        const string SERVER = "Server";

        private void btnStart_Click(object sender, EventArgs e)
        {
            _server.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            AddMessageToListMessage(SERVER, $"Server is {GetStatusIsListenningServer()} listenning  at {inpHost.Text}:{inpPort.Text}");
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            
            LoadViewLstMessage();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _server.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            AddMessageToListMessage(SERVER, $"Server is {GetStatusIsListenningServer()} listenning  at {inpHost.Text}:{inpPort.Text}");

        }

        private string GetStatusIsListenningServer()
        {
            return _server.IsListening ? "" : "not";
        }

        private void LoadViewLstMessage()
        {
            lstMessage.View = View.Details;
        }

        private void AddMessageToListMessage(string sender, string message)
        {
            ListViewItem mess = new ListViewItem(sender);
            ListViewItem.ListViewSubItem messData = new ListViewItem.ListViewSubItem(mess, message);
            mess.SubItems.Add(messData);
            lstMessage.Items.Add(mess);
        }
    }
}
