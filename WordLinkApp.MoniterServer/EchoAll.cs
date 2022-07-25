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
    public class EchoAll : WebSocketBehavior
    {
        private Form _monitor;
        public EchoAll(ref Form form)
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
                        Sessions.Broadcast(e.Data);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AddMessageDataToMonitor(MessageData message)
        {
            _monitor.Invoke(new Action(() => {
                if (_monitor != null)
                {
                    ListView lstMessage = _monitor.Controls["lstMessage"] as ListView;
                    if (lstMessage != null)
                    {
                        string writingLog = string.Empty;
                        switch (message.Key)
                        {
                            case 101:
                                writingLog = "Send to everyone with message: Create room";
                                break;
                            case 102:
                                writingLog = "Send to everyone with message: Delete room";
                                break;
                            case 105:
                                writingLog = "Send to everyone with message: Load home screen";
                                break;
                        }
                        AddMessageToListMessage(lstMessage, message.SenderId, writingLog);
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
    }
}
