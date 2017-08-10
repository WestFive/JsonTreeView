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

namespace Simulator
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        static JsonTree.JsonTree tree = new JsonTree.JsonTree();
        private static dynamic NowWorkingJason;
        private static dynamic Lane;
        private static HubClient.HubClient hubclient;
        private static List<string> NotAllow = new List<string> { "lane", "message_content", "ip_devices", "com_devices", "apps", "device", "queue" };

        //private static TreeView NodeView = new TreeView();




        //FlowLayoutPanel QueuePanel = new FlowLayoutPanel { Dock = DockStyle.Fill };
        //Panel QueueNodePanel = new Panel { Dock = DockStyle.Fill };
        //FlowLayoutPanel QueueLayoutPanel = new FlowLayoutPanel { Dock = DockStyle.Fill };
        //Panel QueueRichPanel = new Panel { Dock = DockStyle.Fill };
        //TreeView QueueNodeView = new TreeView { Dock = DockStyle.Fill };
        //RichTextBox QueueRich = new RichTextBox { Dock = DockStyle.Fill };


        private void Form1_Load(object sender, EventArgs e)
        {
            //DataInit();
            string jasonstr = File.ReadAllText(Application.StartupPath + "/conf/lane.json");
            NowWorkingJason = JsonConvert.DeserializeObject<dynamic>(jasonstr);
            Lane = NowWorkingJason;
            tree = new JsonTree.JsonTree();
            if (tree.GetRootFromJsonStr(0, jasonstr, JsonTree.JsonTree.Flag.OnlyObject))
            {
                LaneNodeView.Nodes.Add(tree.TreeNode);
                LaneNodeView.SelectedNode = tree.TreeNode;
                // NodeView.DataSource = tree.TreeNode;
            }
            //navBarGroup1.Controls.Add(NodeView);
            //navBarGroup1.ControlContainer.Controls.Add(NodeView);
            //NodeView.Show();
            QueueFormInit();
            //QueuePanel.Visible = false;

            LaneNodeView.NodeMouseDoubleClick += LaneNodeView_NodeMouseDoubleClick;
            //LanePanel.Visible = false;

            QueueNodeView.NodeMouseDoubleClick += QueueNodeView_NodeMouseDoubleClick;

        }

        private void QueueNodeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeView_NodeMouseDoubleClick(sender, e, QueueRichTextBox, QueueFlowLayoutPanel1);
        }





        #region Queue窗体实现
        FlowLayoutPanel QueuePanel = new FlowLayoutPanel();
        Panel queuapanel1 = new Panel();
        Panel queuepanel2 = new Panel();
        Panel queuepanel3 = new Panel();
        TreeView QueueNodeView = new TreeView();
        FlowLayoutPanel QueueFlowLayoutPanel1 = new FlowLayoutPanel();
        RichTextBox QueueRichTextBox = new RichTextBox();
        private void QueueFormInit()
        {
            this.panelControl1.Controls.Add(this.QueuePanel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 147);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(928, 421);
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
            this.QueuePanel.Size = new System.Drawing.Size(924, 417);
            this.QueuePanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.queuapanel1.Controls.Add(this.QueueNodeView);
            this.queuapanel1.Location = new System.Drawing.Point(3, 3);
            this.queuapanel1.Name = "queuepanel1";
            this.queuapanel1.Size = new System.Drawing.Size(209, 410);
            this.queuapanel1.TabIndex = 0;
            // 
            // LaneNodeView
            // 
            this.QueueNodeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueueNodeView.Location = new System.Drawing.Point(0, 0);
            this.QueueNodeView.Name = "QueueNodeView";
            this.QueueNodeView.Size = new System.Drawing.Size(209, 410);
            this.QueueNodeView.TabIndex = 0;
            // 
            // panel2
            // 
            this.queuepanel2.Controls.Add(this.QueueFlowLayoutPanel1);
            this.queuepanel2.Location = new System.Drawing.Point(218, 3);
            this.queuepanel2.Name = "queuepanel2";
            this.queuepanel2.Size = new System.Drawing.Size(333, 410);
            this.queuepanel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.queuepanel3.Controls.Add(this.QueueRichTextBox);
            this.queuepanel3.Location = new System.Drawing.Point(557, 3);
            this.queuepanel3.Name = "queuepanel3";
            this.queuepanel3.Size = new System.Drawing.Size(364, 410);
            this.queuepanel3.TabIndex = 2;
            // 
            // LaneflowLayoutPanel1
            // 
            this.QueueFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueueFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.QueueFlowLayoutPanel1.Name = "QueueFlowLayoutPanel1";
            this.QueueFlowLayoutPanel1.Size = new System.Drawing.Size(333, 410);
            this.QueueFlowLayoutPanel1.TabIndex = 0;
            // 
            // LanerichTextBox
            // 
            this.QueueRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueueRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.QueueRichTextBox.Name = "QueuerichTextBox";
            this.QueueRichTextBox.Size = new System.Drawing.Size(364, 410);
            this.QueueRichTextBox.TabIndex = 0;
            this.QueueRichTextBox.Text = "";
        }
        #endregion
        //private void QueueFormInit()
        //{
        //    panelControl1.Controls.Add(QueuePanel);
        //    panelControl1.Click += PanelControl1_Click;
        //    QueuePanel.Controls.Add(QueueNodePanel);
        //    QueuePanel.Controls.Add(QueueLayoutPanel);
        //    QueuePanel.Controls.Add(QueueRichPanel);        
        //    QueueRichPanel.Controls.Add(QueueRich);          
        //    QueueNodePanel.Controls.Add(QueueNodeView);
        //    QueueNodePanel.Show();
        //    QueueLayoutPanel.Show();
        //    QueueRichPanel.Show();
        //    QueueRich.Show();
        //    QueueNodeView.Show();
        //    QueuePanel.Show();
        //}

        private void PanelControl1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HEHE");
        }

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
            barEditQueues.LinksPersistInfo.Add(
            new DevExpress.XtraBars.LinkPersistInfo(st));
            barEditQueues.ItemLinks.Add(st);
            BarWorkItems.Add(st);
            st.ItemClick += St_ItemClick;
            barEditQueues.Caption = st.Caption;

        }
        List<BarStaticItem> BarWorkItems = new List<BarStaticItem>();

        private void RemoveFormQueueMenu(string value)
        {
            #region 弃用
            ////BarStaticItem st = BarWorkItems.FirstOrDefault(x => x.Caption == value);
            ////barEditQueues.LinksPersistInfo.Remove(
            ////new DevExpress.XtraBars.LinkPersistInfo(st));
            //barEditQueues.LinksPersistInfo.RemoveAt(0);
            ////BarWorkItems.Remove(st); 
            #endregion
            BarWorkItems.Remove(BarWorkItems.FirstOrDefault(x => x.Caption == value));
            barEditQueues.ItemLinks.Clear();
            barEditQueues.LinksPersistInfo.Clear();


            foreach (var item in BarWorkItems)
            {

                barEditQueues.LinksPersistInfo.Add(new LinkPersistInfo(item));

                item.ItemClick += St_ItemClick;
                barEditQueues.Caption = item.Caption;
            }
        }

        private void St_ItemClick(object sender, ItemClickEventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                barEditQueues.Caption = e.Item.Caption;
            }));

        }


        /// <summary>
        /// 追加C面
        /// </summary>
        private void messageAppendLog()
        {
            string builder = string.Empty;
            int result = -1;
            if (int.TryParse(LaneNodeView.SelectedNode.Text, out result))
            {
                object[] obj = JsonConvert.DeserializeObject<object[]>(JsonConvert.SerializeObject(tree.JasonKeyValue[LaneNodeView.SelectedNode.Parent.Text]));
                builder = JsonConvert.SerializeObject(obj[result]);
            }
            else
            {
                builder = tree.JasonKeyValue[LaneNodeView.SelectedNode.Text].ToString();
            }
            LanerichTextBox.Text = DataHanding.MessageEncoder.ConvertJsonString(builder);
        }

        private void NodeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e, RichTextBox richTextBox, FlowLayoutPanel panel)
        {
            string builder = string.Empty;
            int result = -1;
            if (int.TryParse(e.Node.Text, out result))
            {
                object[] obj = JsonConvert.DeserializeObject<object[]>(JsonConvert.SerializeObject(tree.JasonKeyValue[e.Node.Parent.Text]));

                builder = JsonConvert.SerializeObject(obj[result]);
            }
            else
            {
                try
                {
                    builder = tree.JasonKeyValue[e.Node.Text].ToString();
                }
                catch (KeyNotFoundException)
                {
                    builder = tree.JasonKeyValue["message_content"].ToString();
                }
            }
            richTextBox.Text = DataHanding.MessageEncoder.ConvertJsonString(builder);
            Dictionary<LabelControl, TextEdit> dic = new Dictionary<LabelControl, TextEdit>();
            JToken jtok = JToken.FromObject(JsonConvert.DeserializeObject<object>(builder));
            Dictionary<string, object> keyvalue;
            switch (jtok.Type)
            {
                case JTokenType.Array:
                    object[] obj = jtok.ToArray();
                    keyvalue = new Dictionary<string, object>();
                    for (int i = 0; i < obj.Length; i++)
                    {
                        keyvalue.Add(e.Node.Text + i.ToString(), obj[i]);
                    }
                    break;
                default:
                    keyvalue = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(jtok));
                    break;
            }
            foreach (var item in keyvalue)
            {
                dic.Add(new LabelControl { Text = item.Key }, new TextEdit { Text = item.Value.ToString(), Tag = item.Key, Name = result.ToString(), Width = 400 });
            }
            LaneflowLayoutPanel1.Controls.Clear();
            foreach (var item in dic)
            {
                panel.Controls.Add(item.Key); item.Key.Show();
                panel.Controls.Add(item.Value); item.Value.Show();
                item.Value.TextChanged += Value_TextChanged;
                if (NotAllow.Count(x => x == item.Key.Text) > 0)
                {
                    //item.Value.Text = "Object";
                    item.Value.ReadOnly = true;
                }
            }
        }

        private void Value_TextChanged(object sender, EventArgs e)
        {
            string key = ((TextEdit)sender).Tag.ToString();
            string value = ((TextEdit)sender).Text.ToString();
            string intseed = ((TextEdit)sender).Name.ToString();

            string Type = NowWorkingJason.message_type.ToString();
            switch (Type)
            {
                case "lane":
                    UpdateLane(key, value, intseed);
                    break;
                case "queue":
                    UpdateQueue(key, value, intseed);
                    break;
            }

        }

        private void UpdateQueue(string key, string value, string intseed)
        {
            dynamic root = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(tree.JasonKeyValue["message_content"]));
            string switchkey = this.LaneNodeView.SelectedNode.Text.ToString();
            switch (switchkey)
            {
                case "queue":
                    root.queue[key] = value;
                    break;
                case "message_content":
                    root[key] = value;
                    break;

            }
            tree.JasonKeyValue["queue"] = root.queue;
            tree.JasonKeyValue["message_content"] = root;
            messageAppendLog();
            NowWorkingJason.message_content = root;
        }

        private void UpdateLane(string key, string value, string intseed)
        {

            dynamic root = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(tree.JasonKeyValue["message_content"]));
            string switchkey = this.LaneNodeView.SelectedNode.Text.ToString();
            switch (switchkey)
            {
                case "lane"://device or apps
                    root.lane[key] = value;
                    break;
                case "device"://ip_devices or comdevices    
                    root.lane.device[key] = value;
                    break;
                case "ip_devices":
                    root.lane.device.ip_devices[Convert.ToInt32(key.Substring(key.Length - 1, 1))] = value;
                    break;
                case "com_devices":
                    root.lane.device.com_devices[Convert.ToInt32(key.Substring(key.Length - 1, 1))] = value;
                    break;
                case "apps"://array
                    //root.lane.apps[Convert.ToInt32(key.Substring(key.Length - 1, 1))] = value;
                    MessageBox.Show("不允许修改集合");
                    break;
                default:
                    switch (this.LaneNodeView.SelectedNode.Parent.Text.ToString())
                    {
                        case "ip_devices":
                            root.lane.device.ip_devices[Int32.Parse(intseed)][key] = value;
                            break;
                        case "com_devices":
                            root.lane.device.com_devices[Int32.Parse(intseed)][key] = value;
                            break;
                        case "apps":
                            root.lane.apps[Int32.Parse(intseed)][key] = value;
                            //MessageBox.Show("不允许修改集合");
                            break;
                        default:
                            break;
                    }
                    break;

            }
            tree.JasonKeyValue["lane"] = root.lane;
            tree.JasonKeyValue["message_content"] = root;
            tree.JasonKeyValue["ip_devices"] = root.lane.device.ip_devices;
            tree.JasonKeyValue["com_devices"] = root.lane.device.com_devices;
            tree.JasonKeyValue["apps"] = root.lane.apps;
            tree.JasonKeyValue["device"] = root.lane.device;
            messageAppendLog();
            NowWorkingJason.message_content.lane = root.lane;
            Lane = NowWorkingJason;
            File.WriteAllText(Application.StartupPath + "/test.json", tree.JasonKeyValue["message_content"].ToString());
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
                    hubclient.Change(LaneCodeItem.Caption, DataHanding.MessageEncoder.EncodingLaneMessage(NowWorkingJason, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.RecipientType.ALL));
                    break;
            }

            //if((bool)ModeSelect)
            //{
            //    MessageBox.Show("123");
            //}
            //ModeSelect.
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
                hubclient.Change(LaneCodeItem.EditValue.ToString(), DataHanding.MessageEncoder.EncodingLaneMessage(NowWorkingJason, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.RecipientType.ALL));
            }
            else
            {
                MessageBox.Show("无法推送，暂未与消息服务建立连接");
            }
        }



        private void barEditQueues_ItemClick(object sender, ItemClickEventArgs e)
        {



        }
        public static Dictionary<string, dynamic> QueueDic = new Dictionary<string, dynamic>();
        /// <summary>
        /// AddQueue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonAddQueue_ItemClick(object sender, ItemClickEventArgs e)
        {
            dynamic queue = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Application.StartupPath + "/conf/queue.json"));
            queue = JsonConvert.DeserializeObject<dynamic>(DataHanding.MessageEncoder.EncodingQueueMessage(queue, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.QueueAction.create));
            if (hubclient != null)
            {
                hubclient.Change(LaneCodeItem.EditValue.ToString(), JsonConvert.SerializeObject(queue));
            }
            QueueDic.Add((string)queue.message_content.queue_code, queue);
            AppendToQueueMenu((string)queue.message_content.queue_code);


        }

        private void barButtonRemoveQueue_ItemClick(object sender, ItemClickEventArgs e)
        {
            //comboBox1.SelectedItem.ToString();

            string send = DataHanding.MessageEncoder.EncodingQueueMessage(NowWorkingJason, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.QueueAction.delete);
            if (hubclient != null)
            {
                hubclient.Change(LaneCodeItem.EditValue.ToString(), send);
            }
            //comboBox1.Items.Remove(comboBox1.Items[comboBox1.SelectedIndex]);
            RemoveFormQueueMenu(barEditQueues.Caption);
            QueueDic.Remove((string)NowWorkingJason.message_content.queue_code);
            if (barEditQueues.LinksPersistInfo.Count != 0)
            {

                NowWorkingJason = QueueDic[barEditQueues.Caption];
            }
            else
            {
                NowWorkingJason = Lane;
            }
        }

        private void barButtonClearQueue_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonActive_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                
                NowWorkingJason = QueueDic[barEditQueues.Caption];
                //LaneNodeView.Nodes.Clear();
                QueueNodeView.Nodes.Clear();
                if (tree.GetRootFromJsonStr(0, NowWorkingJason.ToString(), JsonTree.JsonTree.Flag.OnlyObject))
                {
                    QueueNodeView.Nodes.Add(tree.TreeNode);
                    QueueNodeView.SelectedNode = tree.TreeNode;
                }
                LanerichTextBox.Text = NowWorkingJason["message_content"].ToString();

                LanePanel.Visible = false;
                QueuePanel.Visible = true;
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
                string send = DataHanding.MessageEncoder.EncodingQueueMessage(NowWorkingJason, LaneCodeItem.EditValue.ToString(), editLane_Name.EditValue.ToString(), DataHanding.MessageEncoder.QueueAction.update);
                hubclient.Change(LaneCodeItem.EditValue.ToString(), send);
            }
        }

        private void officeNavigationBar_ItemClick(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {

        }

        private void officeNavigationBar_ItemClick_1(object sender, DevExpress.XtraBars.Navigation.NavigationBarItemEventArgs e)
        {
            switch(e.Item.Text)
            {
                case "Lane":
                    //QueuePanel.Visible = false;
                    LanePanel.Visible = true;
                    break;
                case "Queue":
                   // QueuePanel.Visible = true;
                    LanePanel.Visible = false;
                    break;
            }
        }


        //private void barSubItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{

        //}

        //private void barHeaderItem2_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    barSubItem1.Caption = e.Item.Caption;
        //}
    }
}