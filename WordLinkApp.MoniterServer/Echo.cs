using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;
using WordLinkApp.SocketModel;

namespace WordLinkApp.MoniterServer
{
    public class Echo : WebSocketBehavior
    {
        private Form _monitor;
        public Echo(ref Form form)
        {
            _monitor = form;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                if (e.Data != null)
                {
                    MessageData message = JsonConvert.DeserializeObject<MessageData>(e.Data);
                    if (message != null)
                    {
                        AddMessageDataToMonitor(message);
                        SendMessageData(e.Data, message.ReceiverId);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SendMessageData(string orgMessage, string receiverId)
        {
            string id = Sessions.Sessions.ToList().Where(item => item.Context.QueryString["username"] == receiverId).FirstOrDefault().ID;
            if(id != null)
            {
                Sessions.SendTo(orgMessage, id);
            }
        }

        protected override void OnOpen()
        {
            AddSessionContextToMonitor();
        }

        protected override void OnClose(CloseEventArgs e)
        {
            RemoveSessionContextFromMonitor();
        }

        private void RemoveSessionContextFromMonitor()
        {
            _monitor.Invoke(new Action(() => {
                if (_monitor != null)
                {
                    ListView lstSocket = _monitor.Controls["lstSocket"] as ListView;
                    ListView lstMessage = _monitor.Controls["lstMessage"] as ListView;
                    if (lstSocket != null)
                    {
                        RemoveSessionItemFromList(lstSocket);
                        AddMessageToListMessage(lstMessage, GetUserNameByQueryString(), "Ngắt kết nối");
                    }
                }
            }));
        }

        private string GetUserNameByQueryString()
        {
            string user = this.Context.QueryString["username"];
            return string.IsNullOrEmpty(user) ? this.ID : user;
        }

        private void AddSessionContextToMonitor()
        {
            _monitor.Invoke(new Action(() => {
                if (_monitor != null)
                {
                    ListView lstSocket = _monitor.Controls["lstSocket"] as ListView;
                    ListView lstMessage = _monitor.Controls["lstMessage"] as ListView;
                    if (lstSocket != null && lstMessage != null)
                    {
                        AddSessionItemToList(lstSocket);
                        AddMessageToListMessage(lstMessage, GetUserNameByQueryString(), "Mở kết nối thành công");
                    }
                }
            }));
        }

        private void AddMessageDataToMonitor(MessageData message)
        {
            _monitor.Invoke(new Action(() => {
                if (_monitor != null)
                {
                    ListView lstMessage = _monitor.Controls["lstMessage"] as ListView;
                    if (lstMessage != null)
                    {
                        string writeLog = string.Empty;
                        switch (message.Key)
                        {
                            case 100:
                                writeLog = $"{message.SenderId} want to join room of {message.ReceiverId}";
                                break;
                            case 103:
                                writeLog = $"{message.SenderId} accepted join game of {message.ReceiverId}";
                                break;
                            case 104:
                                writeLog = $"{message.SenderId} want to load room game of {message.ReceiverId}";
                                break;
                            case 106:
                                writeLog = $"{message.SenderId} want to set boss for {message.ReceiverId}";
                                break;
                            case 107:
                                writeLog = $"{message.SenderId} started game with {message.ReceiverId}";
                                break;
                            case 108:
                                writeLog = $"{message.SenderId} send answer to {message.ReceiverId}";
                                break;
                        }
                        AddMessageToListMessage(lstMessage, message.SenderId, writeLog);
                    }
                }
            }));
        }

        private void AddMessageToListMessage(ListView lstMessage, string sender, string message)
        {
            ListViewItem mess = new ListViewItem(sender);
            ListViewItem.ListViewSubItem messData = new ListViewItem.ListViewSubItem(mess, message);
            mess.SubItems.Add(messData);
            lstMessage.Items.Add(mess);
        }

        private void AddSessionItemToList(ListView lstSocket)
        {
            ListViewItem socket = new ListViewItem(GetUserNameByQueryString());
            lstSocket.Items.Add(socket);
        }

        private void RemoveSessionItemFromList(ListView lstSocket)
        {
            string user = GetUserNameByQueryString();
            for (int i = 0; i < lstSocket.Items.Count; i++)
            {
                if(lstSocket.Items[i].Text == user)
                {
                    lstSocket.Items.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
