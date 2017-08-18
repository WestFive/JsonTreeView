using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json.Linq;
using DevExpress.XtraBars;
using JsonTree;
using HubClient;
using System.IO.Ports;

namespace Simulator
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        static JsonTree.JsonTree tree = new JsonTree.JsonTree();
        private static dynamic WorkingQueue;
        private static dynamic Lane;
        private static HubClient.HubClient hubclient;
        private static List<string> NotAllow = new List<string> { "lane", "message_content", "ip_devices", "com_devices", "apps", "device", "queue" };

        private void Form1_Load(object sender, EventArgs e)
        {
            string jasonstr = File.ReadAllText(Application.StartupPath + "/conf/lane.json");
            Lane = JsonConvert.DeserializeObject<dynamic>(jasonstr);
            tree = new JsonTree.JsonTree();
            if (tree.GetRootFromJsonStr(0, jasonstr, JsonTree.JsonTree.Flag.OnlyObject))
            {
                LaneNodeView.Nodes.Add(tree.TreeNode);
                LaneNodeView.SelectedNode = tree.TreeNode;
                
            }
            QueueFormInit();
            LaneNodeView.NodeMouseDoubleClick += LaneNodeView_NodeMouseDoubleClick;
            QueueNodeView.NodeMouseDoubleClick += QueueNodeView_NodeMouseDoubleClick;
            navigationBarItem1.Select();
            QueueCombox.SelectedValueChanged += QueueCombox_SelectedValueChanged;
        }
        private void QueueCombox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (QueueCombox.Items.Count != 0)
            {
                WorkingQueue = QueueDic[QueueCombox.SelectedItem.ToString()];
                ActiveQueue();
            }
            else
            {
                MessageBox.Show("请勿选择空项");
            }
        }
        private void QueueNodeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeView_NodeMouseDoubleClick(sender, e, QueueRichTextBox, QueueFlowLayoutPanel2);
        }
        #region Queue窗体实现
        FlowLayoutPanel QueuePanel = new FlowLayoutPanel();
        Panel queuapanel1 = new Panel();
        Panel queuepanel2 = new Panel();
        Panel queuepanel3 = new Panel();
        TreeView QueueNodeView = new TreeView();
        System.Windows.Forms.ComboBox QueueCombox = new System.Windows.Forms.ComboBox();
        FlowLayoutPanel QueueFlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        FlowLayoutPanel QueueFlowLayoutPanel2 = new FlowLayoutPanel();
        RichTextBox QueueRichTextBox = new RichTextBox();
        private void QueueFormInit()
        {
            this.panelControl1.Controls.Add(this.QueuePanel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 147);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1014, 543);
            this.panelControl1.TabIndex = 13;
            // 
            // LanePanel
            // 
            this.QueuePanel.Controls.Add(this.queuapanel1);
            this.QueuePanel.Controls.Add(this.queuepanel2);
            this.QueuePanel.Controls.Add(this.queuepanel3);
            this.QueuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueuePanel.Location = new System.Drawing.Point(2, 2);
            this.QueuePanel.Name = "queuePanel";
            this.QueuePanel.Size = new System.Drawing.Size(1010, 539);
            this.QueuePanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.queuapanel1.Controls.Add(this.QueueFlowLayoutPanel1);
            this.queuapanel1.Location = new System.Drawing.Point(3, 3);
            this.queuapanel1.Name = "panel1";
            this.queuapanel1.Size = new System.Drawing.Size(209, 532);
            this.queuapanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.QueueFlowLayoutPanel1.Controls.Add(this.QueueCombox);
            this.QueueFlowLayoutPanel1.Controls.Add(this.QueueNodeView);
            this.QueueFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueueFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.QueueFlowLayoutPanel1.Name = "QueueFlowLayoutPanel1";
            this.QueueFlowLayoutPanel1.Size = new System.Drawing.Size(209, 532);
            this.QueueFlowLayoutPanel1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.QueueCombox.FormattingEnabled = true;
            this.QueueCombox.Location = new System.Drawing.Point(3, 3);
            this.QueueCombox.Name = "comboBox1";
            this.QueueCombox.Size = new System.Drawing.Size(203, 22);
            this.QueueCombox.TabIndex = 0;
            // 
            // LaneNodeView
            // 
            this.QueueNodeView.Location = new System.Drawing.Point(3, 31);
            this.QueueNodeView.Name = "LaneNodeView";
            this.QueueNodeView.Size = new System.Drawing.Size(203, 532);
            this.QueueNodeView.TabIndex = 0;
            // 




            #region Cancel
            //// panel1
            //// 
            //this.queuapanel1.Controls.Add(this.QueueNodeView);
            //this.queuapanel1.Location = new System.Drawing.Point(3, 3);
            //this.queuapanel1.Name = "queuepanel1";
            //this.queuapanel1.Size = new System.Drawing.Size(209, 410);
            //this.queuapanel1.TabIndex = 0;
            //// 
            //// LaneNodeView
            //// 
            //this.QueueNodeView.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.QueueNodeView.Location = new System.Drawing.Point(0, 0);
            //this.QueueNodeView.Name = "QueueNodeView";
            //this.QueueNodeView.Size = new System.Drawing.Size(209, 410);
            //this.QueueNodeView.TabIndex = 0; 
            #endregion
            // 
            // panel2
            // 
            this.queuepanel2.Controls.Add(this.QueueFlowLayoutPanel2);
            this.queuepanel2.Location = new System.Drawing.Point(218, 3);
            this.queuepanel2.Name = "queuepanel2";
            this.queuepanel2.Size = new System.Drawing.Size(333, 533);
            this.queuepanel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.queuepanel3.Controls.Add(this.QueueRichTextBox);
            this.queuepanel3.Location = new System.Drawing.Point(557, 3);
            this.queuepanel3.Name = "queuepanel3";
            this.queuepanel3.Size = new System.Drawing.Size(450, 532);
            this.queuepanel3.TabIndex = 2;
            // 
            // LaneflowLayoutPanel1
            // 
            this.QueueFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueueFlowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.QueueFlowLayoutPanel2.Name = "QueueFlowLayoutPanel1";
            this.QueueFlowLayoutPanel2.Size = new System.Drawing.Size(333, 529);
            this.QueueFlowLayoutPanel2.TabIndex = 0;
            this.QueueFlowLayoutPanel2.AutoScroll = true;
            // 
            // LanerichTextBox
            // 
            this.QueueRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueueRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.QueueRichTextBox.Name = "QueuerichTextBox";
            this.QueueRichTextBox.Size = new System.Drawing.Size(443, 529);
            this.QueueRichTextBox.TabIndex = 0;
            this.QueueRichTextBox.Text = "";
        }
        #endregion
        private void LaneNodeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeView_NodeMouseDoubleClick(sender, e, LanerichTextBox, LaneflowLayoutPanel1);
        }
        /// <summary>
        /// 追加在quque控件上
        /// </summary>
        /// <param name="list"></param>
        private void AppendToQueueMenu(string value)
        {
            BarStaticItem st = new BarStaticItem();
            st.Caption = value.ToString();
            QueueCombox.Items.Add((object)value);
            QueueCombox.SelectedItem = value;
        }
        /// <summary>
        /// 追加C面
        /// </summary>
        private void LanemessageAppendLog(string temp)
        {
            LanerichTextBox.Text = DataHanding.MessageEncoder.ConvertJsonString(temp);
        }
        /// <summary>
        /// Queue綁定到queuerichC
        /// </summary>
        private void QueuemessageAppendLog(string temp)
        {
            QueueRichTextBox.Text = DataHanding.MessageEncoder.ConvertJsonString(temp);
        }
        private void NodeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e, RichTextBox richTextBox, FlowLayoutPanel panel)
        {

            dynamic root = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(Lane));
            string switchkey = this.LaneNodeView.SelectedNode.Text.ToString();
            string fullPath = LaneNodeView.SelectedNode.FullPath;
            List<string> counts = fullPath.Split("\\".ToArray()).ToList();
            counts.Remove(counts[0]);
            int no = 1;
            dynamic temp = root;
            for (int i = 0; i < counts.Count; i++)
            {

                if (Int32.TryParse(counts[i], out no))
                {
                    temp = JsonConvert.DeserializeObject<dynamic[]>(JsonConvert.SerializeObject(temp))[no];
                }
                else
                {
                    temp = temp[counts[i]];
                }
            }
            string builder = JsonConvert.SerializeObject(temp);
            LanemessageAppendLog(JsonConvert.SerializeObject(temp));
            richTextBox.Text = DataHanding.MessageEncoder.ConvertJsonString(builder);
            Dictionary<LabelControl, Control> dic = new Dictionary<LabelControl, Control>();
            List<SimpleButton> methodButtons = new List<SimpleButton>();
            JToken jtok = JToken.FromObject(JsonConvert.DeserializeObject<object>(builder));

            Dictionary<string, dynamic> keyvalue = new Dictionary<string, object>();
            Dictionary<string, dynamic> buttondic = new Dictionary<string, object>();
            switch (jtok.Type)
            {
                case JTokenType.Array:
                    object[] obj = jtok.ToArray();
                    for (int i = 0; i < obj.Length; i++)
                    {
                        keyvalue.Add(e.Node.Text + i.ToString(), obj[i]);
                    }
                    break;
                default:
                    if (jtok["property"] != null)
                    {
                        keyvalue = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(jtok["property"]));
                        if (jtok["method"] != null)
                        {
                            dynamic[] methods = JsonConvert.DeserializeObject<dynamic[]>(JsonConvert.SerializeObject(jtok["method"]));
                            foreach (var item in methods)
                            {
                                buttondic.Add((string)item.method_name, item);
                            }

                           // buttondic = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(JsonConvert.SerializeObject(jtok["method"]));
                        }
                    }
                    else
                    {
                        keyvalue = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(jtok));
                    }
                    break;
            }
            //if (e.Node.Text == "method")
            //{
            //    foreach (var item in keyvalue)
            //    {
            //        dic.Add(new LabelControl { Text = item.Key }, new SimpleButton { Text = item.Value.ToString(), Tag = item.Key, Name = no.ToString(), Width = 310 });
            //        dic.LastOrDefault().Value.Click += Value_Click;
            //    }
                
            //}
            //else
            //{
                foreach (var item in keyvalue)
                {
                    dic.Add(new LabelControl { Text = item.Key }, new TextBox { Text = item.Value.ToString(), Tag = item.Key, Name = no.ToString(), Width = 310 });
                }
                if (buttondic.Count > 0)
                {
                    foreach (var item in buttondic)
                    {
                        methodButtons.Add( new SimpleButton { Text = item.Value.display_name.ToString(), Tag = item.Value, Name = no.ToString(), Width = 310 });
                       
                    }

                }
            //}
            switch (LanePanel.Visible)
            {
                case true:
                    LaneflowLayoutPanel1.Controls.Clear();
                    LaneflowLayoutPanel1.AutoScroll = true;
                    break;
                case false:
                    QueueFlowLayoutPanel2.Controls.Clear();
                    QueueFlowLayoutPanel2.AutoScroll = true;
                    break;
            }
            foreach (var item in dic)
            {
                panel.Controls.Add(item.Key); item.Key.Show();
                panel.Controls.Add(item.Value); item.Value.Show();
                item.Value.TextChanged += Value_TextChanged;
                if (NotAllow.Count(x => x == item.Key.Text) > 0)
                {
                    ((TextBox)item.Value).ReadOnly = true;
                }
            }
            foreach (var item in methodButtons)
            {
                panel.Controls.Add(item);item.Show();
                 item.Click += Value_Click;
            }
        }

        /// <summary>
        /// ValueClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Value_Click(object sender, EventArgs e)
        {

            //"classCcrCamera" 

            Camera.CamerController cm = new Camera.CamerController();
            switch (((SimpleButton)sender).Text)
            {
                case "PLCMethodInit":
                    PLC_DLL.PLC.Init();
                    PLC_DLL.PLC.SerialPort.DataReceived += SerialPort_DataReceived;
                    break;
                case "PLCMethodSend":

                    break;
                case "CameraMethodInit":
                    cm.InitSDK();
                    cm.Login("10.1.1.200", "37777", "admin", "admin");
                    break;
                case "CameraMethodMoveTo1":
                    cm.ConvertTo("1");
                    break;
                case "CameraMethodMoveTo2":
                    cm.ConvertTo("2");
                    break;
                case "SnopMethodSend":
                    cm.GetImage();
                    break;
            }
            //MessageBox.Show(((SimpleButton)sender).Text);
        }
        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {



            byte[] buffer = new byte[((SerialPort)sender).BytesToRead];
            ((SerialPort)sender).Read(buffer, 0, buffer.Length);
            //MessageBox.Show(System.Text.Encoding.Default.GetString(buffer));
            UpdateLane("revice", System.Text.Encoding.Default.GetString(buffer), "root\\message_content\\lane\\device\\com_devices\\PLC\\property");
            buffer = null;
        }
        private void Value_TextChanged(object sender, EventArgs e)
        {
            string key = ((TextBox)sender).Tag.ToString();
            string value = ((TextBox)sender).Text.ToString();
            string intseed = ((TextBox)sender).Name.ToString();
            if (LaneNodeView.Visible == true)
            {
                UpdateLane(key, value, intseed);
            }
            else
            {
                UpdateQueue(key, value, intseed);
            }
        }
        private void UpdateQueue(string key, string value, string intseed)
        {
            Invoke(new MethodInvoker(() =>
            {
                dynamic root = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(WorkingQueue));
                string switchkey = this.QueueNodeView.SelectedNode.Text.ToString();
                string fullPath = QueueNodeView.SelectedNode.FullPath;
                List<string> counts = fullPath.Split("\\".ToArray()).ToList();
                counts.Remove(counts[0]);
                dynamic temp = root;
                for (int i = 0; i < counts.Count; i++)
                {
                    temp = temp[counts[i]];
                }
                temp[key] = value;
                QueuemessageAppendLog(JsonConvert.SerializeObject(temp));
                WorkingQueue = root;
            }));

        }
        //private void UpdateLane(string key, string value)
        //{
        //    Invoke(new MethodInvoker(() =>
        //    {
        //        dynamic root = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(Lane));
        //        string switchkey = this.LaneNodeView.SelectedNode.Text.ToString();
        //        string fullPath = LaneNodeView.SelectedNode.FullPath;
        //        List<string> counts = fullPath.Split("\\".ToArray()).ToList();
        //        counts.Remove(counts[0]);
        //        dynamic temp = root;
        //        for (int i = 0; i < counts.Count; i++)
        //        {
        //            int no = 1;
        //            if (Int32.TryParse(counts[i], out no))
        //            {
        //                temp = JsonConvert.DeserializeObject<dynamic[]>(JsonConvert.SerializeObject(temp))[no];
        //            }
        //            else
        //            {
        //                temp = temp[counts[i]];
        //            }
        //        }
        //        temp[key] = value;
        //        LanemessageAppendLog(JsonConvert.SerializeObject(temp));
        //        Lane = root;
        //    }));
        //}
        private void UpdateLane(string key, string value, string FullPath = null)
        {
            Invoke(new MethodInvoker(() =>
            {
                dynamic root = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(Lane));
                string switchkey = this.LaneNodeView.SelectedNode.Text.ToString();
                if (FullPath == null)
                {
                    FullPath = LaneNodeView.SelectedNode.FullPath;
                }
                List<string> counts = FullPath.Split("\\".ToArray()).ToList();
                counts.Remove(counts[0]);
                dynamic temp = root;
                for (int i = 0; i < counts.Count; i++)
                {
                    int no = 1;
                    if (Int32.TryParse(counts[i], out no))
                    {
                        temp = JsonConvert.DeserializeObject<dynamic[]>(JsonConvert.SerializeObject(temp))[no];
                    }
                    else
                    {
                        temp = temp[counts[i]];
                    }
                }
                temp[key] = value;
                LanemessageAppendLog(JsonConvert.SerializeObject(temp));
                Lane = root;
            }));
        }




        private void BtnConnect_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch ((bool)ModeSelect.EditValue)
            {
                case true://type realmode
                    break;
                case false://type simulator
                    hubclient = new HubClient.HubClient(EditMessageAddress.EditValue.ToString(), "messagehub", new Dictionary<string, string> { { "Name", LaneCodeItem.EditValue.ToString() }, { "Type", "Lane" } });
                    hubclient.reciveStatus += Hubclient_reciveStatus;
                    hubclient.reciveMessage += Hubclient_reciveMessage;
                    hubclient.reciveHubError += Hubclient_reciveHubError;
                    hubclient.HubInit();
                    hubclient.Change(LaneCodeItem.Caption, DataHanding.MessageEncoder.EncodingLaneMessage(Lane, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.RecipientType.ALL));
                    break;
            }
        }
        private void Hubclient_reciveHubError(string str)
        {
            barEditLog.EditValue = str;
        }
        private void Hubclient_reciveMessage(string str)
        {
            barEditLog.EditValue = str;
        }
        private void Hubclient_reciveStatus(string str)
        {
            barEditLog.EditValue = str;
        }
        private void barButtonPush_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (hubclient != null)
            {
                hubclient.Change(LaneCodeItem.EditValue.ToString(), DataHanding.MessageEncoder.EncodingLaneMessage(Lane, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.RecipientType.ALL));
            }
            else
            {
                MessageBox.Show("无法推送，暂未与消息服务建立连接");
            }
        }
        public static Dictionary<string, dynamic> QueueDic = new Dictionary<string, dynamic>();
        /// <summary>
        /// AddQueue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonAddQueue_ItemClick(object sender, ItemClickEventArgs e)
        {
            LanePanel.Visible = false;
            dynamic queue = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Application.StartupPath + "/conf/queue.json"));
            queue = JsonConvert.DeserializeObject<dynamic>(DataHanding.MessageEncoder.EncodingQueueMessage(queue, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.QueueAction.create));
            if (hubclient != null)
            {
                hubclient.Change(LaneCodeItem.EditValue.ToString(), JsonConvert.SerializeObject(queue));
            }
            QueueDic.Add((string)queue.message_content.queue_code, queue);
            AppendToQueueMenu((string)queue.message_content.queue_code);

            ActiveQueue();
        }
        private void barButtonRemoveQueue_ItemClick(object sender, ItemClickEventArgs e)
        {
            string send = DataHanding.MessageEncoder.EncodingQueueMessage(WorkingQueue, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.QueueAction.delete);
            if (hubclient != null)
            {
                hubclient.Change(LaneCodeItem.EditValue.ToString(), send);
            }
            QueueCombox.Items.Remove(QueueCombox.SelectedItem.ToString());
            QueueDic.Remove((string)WorkingQueue.message_content.queue_code);
            if (QueueCombox.Items.Count == 0)
            {
                QueueCombox.Text = "";
                QueueFlowLayoutPanel2.Controls.Clear();
                QueueRichTextBox.Text = "";
                return;
            }
            else
            {
                QueueCombox.SelectedItem = QueueCombox.Items[0];
                WorkingQueue = QueueDic[QueueCombox.SelectedItem.ToString()];
                ActiveQueue();
            }
        }
        private void ActiveQueue()
        {
            try
            {

                WorkingQueue = QueueDic[QueueCombox.SelectedItem.ToString()];
                //LaneNodeView.Nodes.Clear();
                QueueNodeView.Nodes.Clear();
                QueueFlowLayoutPanel2.Controls.Clear();
                if (tree.GetRootFromJsonStr(0, WorkingQueue.ToString(), JsonTree.JsonTree.Flag.OnlyObject))
                {
                    QueueNodeView.Nodes.Add(tree.TreeNode);
                    QueueNodeView.SelectedNode = tree.TreeNode;

                }

                QueueRichTextBox.Text = WorkingQueue["message_content"].ToString();
                LanePanel.Visible = false;
                QueuePanel.Visible = true;
                navigationBarItem2.Select();
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show("作业数据已空" + ex.ToString());
            }
        }
        /// <summary>
        /// QueuePush
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (hubclient != null)
            {
                string send = DataHanding.MessageEncoder.EncodingQueueMessage(WorkingQueue, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.QueueAction.update);
                hubclient.Change(LaneCodeItem.EditValue.ToString(), send);
            }
        }
        private void officeNavigationBar_ItemClick_1(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Lane":
                    LanePanel.Visible = true;
                    tree.GetRootFromJsonStr(0, JsonConvert.SerializeObject(Lane), JsonTree.JsonTree.Flag.OnlyObject);
                    break;
                case "Queue":
                    LanePanel.Visible = false;
                    tree.GetRootFromJsonStr(0, JsonConvert.SerializeObject(WorkingQueue), JsonTree.JsonTree.Flag.OnlyObject);
                    break;
            }
        }
        private void barButtonClearQueue_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (hubclient != null)
            {

                hubclient.hub.Invoke("clear");
            }
        }
    }
}