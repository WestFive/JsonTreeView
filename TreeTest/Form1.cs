using JsonTree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        JsonTree.JsonTree tree;
        RichTextBox richTextBox1;
        RichTextBox richlog;
        public Dictionary<Label, TextBox> ControllerDic = new Dictionary<Label, TextBox>();
        private void Form1_Load(object sender, EventArgs e)
        {
            #region LogGrpupBox
            richlog = new RichTextBox();
            BindToFatherControler(logGroupBox, richlog);

            #endregion

            string jsonstr = File.ReadAllText(Application.StartupPath + "/example.json");
            tree = new JsonTree.JsonTree();
            tree.GetRootFromJsonStr(0, jsonstr, JsonTree.JsonTree.Flag.OnlyObject);
            TreeView tw = tree.TreeView;
            BindToFatherControler(TreeGroupBox, tw);
            ///实例化richtextBox
            richTextBox1 = new RichTextBox();
            BindToFatherControler(TextGroup, richTextBox1);
            richTextBox1.Text = tw.SelectedNode.Text;
            ///注册鼠标点击事件
            tree.NodeClick += Tree_NodeClick;
            tree.NodeDoubleClick += Tree_NodeDoubleClick;

            tree.nowJson = jsonstr;

            tree.needbeencode += Tree_needbeencode;
        }

        private void Tree_needbeencode()
        {
            tree.nowJson = HubClient.Common.MessageEncoder.EncodingLaneMessage(tree.nowJson.ToString(), HubClient.Common.MessageEncoder.RecipientType.ALL);
        }

        static int pianyizhi = 0;
        private void Tree_NodeDoubleClick(Dictionary<string, object> dic)
        {
            flowLayoutPanel1.Controls.Clear();
            ControllerDic.Clear();

            foreach (var item in dic)
            {
                ControllerDic.Add(new Label { Text = item.Key }, new TextBox { Text = item.Value.ToString() });
            }
            foreach (var item in ControllerDic)
            {
                BindToFatherControlerAutoLoaction(flowLayoutPanel1, item.Key, pianyizhi);
                BindToFatherControlerAutoLoaction(flowLayoutPanel1, item.Value, pianyizhi);
                item.Value.Tag = item.Key.Text;
                //pianyizhi += 10;

            }
        }

        private void Tree_NodeClick(string res)
        {
            richTextBox1.Text = res;
        }

        /// <summary>
        /// 子控件在父控件中停靠
        /// </summary>
        /// <param name="father"></param>
        /// <param name="child"></param>
        private void BindToFatherControler(Control father, Control child)
        {
            child.Dock = DockStyle.Fill;
            father.Controls.Add(child);
            child.Show();
        }
        private void BindToFatherControlerAutoLoaction(Control father, Control child, int AutoLocation)
        {
            child.Location = new Point(0, AutoLocation + 10);
            if (child is TextBox)
            {
                child.TextChanged += Child_TextChanged;
            }
            father.Controls.Add(child);
            child.Show();


        }

        private void Child_TextChanged(object sender, EventArgs e)
        {
            string tag = (((TextBox)sender).Tag.ToString());
            if (tree.JasonKeyValue.Count(x => x.Key == ((TextBox)sender).Tag.ToString()) > 0)
            {
                //MessageBox.Show(tree.JasonKeyValue[tag]);
                tree.UpdateJasonObject(tag, ((TextBox)sender).Text);

            }
            try
            {
                richTextBox1.Text = JsonTree.JsonTree.ConvertJsonString(tree.JasonKeyValue[tree.TreeView.SelectedNode.Text]);
            }
            catch (Exception ex)
            {

            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Dock = DockStyle.Left;
        }
        HubClient.HubClient hub;

        public dynamic lane = File.ReadAllText(Application.StartupPath + "/lane.json");
        
        /// <summary>
        /// 连接消息服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            hub = new HubClient.HubClient(TextServerURL.Text, "messagehub", new Dictionary<string, string> { { "Type", "Client" }, { "Name", comboBoxLaneCode.SelectedItem.ToString() } });
            hub.reciveStatus += Hub_reciveStatus;
            hub.reciveHubError += Hub_reciveHubError;
            hub.reciveMessage += Hub_reciveMessage;
            hub.HubInit();          
            tree.GetRootFromJsonStr(0, lane, JsonTree.JsonTree.Flag.OnlyObject);

            SendLaneMessge();
        }

        #region 追加到日志

        private void Hub_reciveMessage(string str)
        {
            AppendLogStatus(str);
        }

        private void Hub_reciveHubError(string str)
        {
            AppendLogStatus(str);
        }

        private void Hub_reciveStatus(string str)
        {
            AppendLogStatus(str);
        }

        private void AppendLogStatus(string text)
        {
            Invoke(new MethodInvoker(() =>
            {
                richlog.AppendText(text + "\r\n");
            }));

        }
        #endregion



        private void SendLaneMessge()
        {
            JsonConvert.DeserializeObject<dynamic>(tree.nowJson).message_content.lane.lane_code = comboBoxLaneCode.SelectedItem.ToString();
            JsonConvert.DeserializeObject<dynamic>(tree.nowJson).message_content.lane.lane_name = textLaneName.Text;
            string besend = JsonConvert.SerializeObject(tree.nowJson);
            hub.Change(comboBoxLaneCode.SelectedItem.ToString(), besend);
        }

  
        private void button6_Click(object sender, EventArgs e)
        {
            if (hub != null)
            {
                if (tree.nowJson != null)
                {
                    SendLaneMessge();
                }
                else
                {
                    MessageBox.Show("请先修改再推送");
                }
            }
        }
    }
}
