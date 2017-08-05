using DevExpress.XtraEditors;
using HubClient;
using JsonTree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RUILI_Agent
{
    public partial class Form1 : Form
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
        private void Form1_Load(object sender, EventArgs e)
        {
            string jasonstr = File.ReadAllText(Application.StartupPath + "/conf/lane.json");
            NowWorkingJason = JsonConvert.DeserializeObject<dynamic>(jasonstr);
            Lane = NowWorkingJason;
            tree = new JsonTree.JsonTree();
            if (tree.GetRootFromJsonStr(0, jasonstr, JsonTree.JsonTree.Flag.OnlyObject))
            {
                NodeView.Nodes.Add(tree.TreeNode);
                NodeView.SelectedNode = tree.TreeNode;
               // NodeView.DataSource = tree.TreeNode;
            }

        }

        /// <summary>
        /// 追加C面
        /// </summary>
        private void messageAppendLog()
        {
            string builder = string.Empty;
            int result = -1;
            if (int.TryParse(NodeView.SelectedNode.Text , out result))
            {
                object[] obj = JsonConvert.DeserializeObject<object[]>(JsonConvert.SerializeObject(tree.JasonKeyValue[NodeView.SelectedNode.Parent.Text]));
                builder = JsonConvert.SerializeObject(obj[result]);
            }
            else
            {
                builder = tree.JasonKeyValue[NodeView.SelectedNode.Text].ToString();
            }
            richTextBox1.Text = DataHanding.MessageEncoder.ConvertJsonString(builder);
        }
        /// <summary>
        /// 鼠标双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            //MessageBox.Show(this.NodeView.SelectedNode.Text);
            //MessageBox.Show(this.NodeView.SelectedNode.Parent.Text);
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
            richTextBox1.Text = DataHanding.MessageEncoder.ConvertJsonString(builder);
            Dictionary<LabelControl,TextEdit> dic = new Dictionary<LabelControl,TextEdit>();
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
            flowLayoutPanel1.Controls.Clear();
            foreach (var item in dic)
            {
                flowLayoutPanel1.Controls.Add(item.Key); item.Key.Show();
                flowLayoutPanel1.Controls.Add(item.Value); item.Value.Show();
                item.Value.TextChanged += Value_TextChanged;
                if (NotAllow.Count(x => x == item.Key.Text) > 0)
                {
                    //item.Value.Text = "Object";
                    item.Value.ReadOnly = true;
                }
            }
             



        }
        /// <summary>
        /// B面TextChang事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Value_TextChanged(object sender, EventArgs e)
        {
            #region 弃用
            //string value = ((TextBox)sender).Tag.ToString();
            ////MessageBox.Show(value);

            //Dictionary<string, object> Root = new Dictionary<string, object>();
            //foreach (var item in tree.JasonKeyValue)
            //{
            //    Root.Add(item.Key, item.Value);

            //}
            //foreach (var item in tree.JasonKeyValue)
            //{
            //    switch (JToken.FromObject(item.Value).Type)
            //    {
            //        case JTokenType.Array:
            //            dynamic[] obj = JToken.FromObject(item.Value).ToArray();
            //            Dictionary<string, object> keyvalue = new Dictionary<string, object>();
            //            for (int i = 0; i < obj.Length; i++)
            //            {
            //                keyvalue.Add(item.Key + i.ToString(), obj[i]);
            //            }
            //            if (keyvalue.Count(x => x.Key == value) > 0)
            //            {
            //                keyvalue[value] = ((TextBox)sender).Text;
            //                for (int i = 0; i < keyvalue.Count; i++)
            //                {
            //                    obj[i] = JToken.FromObject(keyvalue[item.Key + i]);
            //                }
            //            }
            //            Root[item.Key] = JsonConvert.SerializeObject(obj);
            //            break;
            //        case JTokenType.Object:
            //            if (LastDoublieClick != null)
            //            {
            //                if (tree.JasonKeyValue.Count(x => x.Key == item.Key) == 0)//表示在字典中没有
            //                {
            //                    Dictionary<string, object> beupdatedic = JsonConvert.DeserializeObject<Dictionary<string, object>>(Root[LastDoublieClick].ToString());
            //                    beupdatedic[value] = ((TextBox)(sender)).Text;
            //                    Root[LastDoublieClick] = JsonConvert.SerializeObject(beupdatedic);
            //                    richTextBox1.Text = DataHanding.MessageEncoder.ConvertJsonString(Root[LastDoublieClick].ToString());
            //                }
            //                else
            //                {
            //                    Dictionary<string, object> tempdicc = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(item.Value));
            //                    if (tempdicc.Count(x => x.Key == value) > 0)
            //                    {
            //                        tempdicc[value] = ((TextBox)(sender)).Text;
            //                    }
            //                    else
            //                    {
            //                        Root[value] = ((TextBox)(sender)).Text;
            //                    }
            //                    richTextBox1.Text = DataHanding.MessageEncoder.ConvertJsonString(Root[item.Key].ToString());
            //                }
            //            }

            //            break;
            //    }

            //    tree.JasonKeyValue = Root;

            //}

            #endregion
            //if (NodeView.SelectedNode.Text == "apps" || NodeView.SelectedNode.Text == "device" || NodeView.SelectedNode.Text == "message_content" || NodeView.SelectedNode.Text == "ip_devices" || NodeView.SelectedNode.Text == "com_device")
            //{
            //    MessageBox.Show("根元素和集合不允许被修改");
            //    return;
            //}
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
        /// <summary>
        /// BC联动更新queue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="intseed"></param>
        private void UpdateQueue(string key, string value, string intseed)
        {
            dynamic root = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(tree.JasonKeyValue["message_content"]));
            string switchkey = this.NodeView.SelectedNode.Text.ToString();
            switch (switchkey)
            {
                case "queue"://device or apps
                    root.queue[key] = value;
                    break;
                case "message_content":
                    root[key] = value;
                    break;
                    //case "device"://ip_devices or comdevices    
                    //    root.lane.device[key] = value;
                    //    break;
                    //case "ip_devices":
                    //    root.lane.device.ip_devices[Convert.ToInt32(key.Substring(key.Length - 1, 1))] = value;
                    //    break;
                    //case "com_devices":
                    //    root.lane.device.com_devices[Convert.ToInt32(key.Substring(key.Length - 1, 1))] = value;
                    //    break;
                    //case "apps"://array
                    //    //root.lane.apps[Convert.ToInt32(key.Substring(key.Length - 1, 1))] = value;
                    //    MessageBox.Show("不允许修改集合");
                    //    break;
                    //default:
                    //    switch (this.NodeView.SelectedNode.Parent.Text.ToString())
                    //    {
                    //        case "ip_devices":
                    //            root.lane.device.ip_devices[Int32.Parse(intseed)][key] = value;
                    //            break;
                    //        case "com_devices":
                    //            root.lane.device.com_devices[Int32.Parse(intseed)][key] = value;
                    //            break;
                    //        case "apps":
                    //            root.lane.apps[Int32.Parse(intseed)][key] = value;
                    //            //MessageBox.Show("不允许修改集合");
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //    break;

            }
            tree.JasonKeyValue["queue"] = root.queue;
            tree.JasonKeyValue["message_content"] = root;
            //tree.JasonKeyValue["ip_devices"] = root.lane.device.ip_devices;
            //tree.JasonKeyValue["com_devices"] = root.lane.device.com_devices;
            //tree.JasonKeyValue["apps"] = root.lane.apps;
            //tree.JasonKeyValue["device"] = root.lane.device;
            messageAppendLog();
            NowWorkingJason.message_content = root;
            //File.WriteAllText(Application.StartupPath + "/test.json", tree.JasonKeyValue["message_content"].ToString());
        }
        /// <summary>
        /// BC联动更新lane
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="intseed"></param>
        private void UpdateLane(string key, string value, string intseed)
        {
            dynamic root = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(tree.JasonKeyValue["message_content"]));
            string switchkey = this.NodeView.SelectedNode.Text.ToString();
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
                    switch (this.NodeView.SelectedNode.Parent.Text.ToString())
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


        /// <summary>
        /// 连接消息服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (radioSimulation.Checked == true)
            {
                hubclient = new HubClient.HubClient(TextServerURL.Text, "messagehub", new Dictionary<string, string> { { "Name", textLaneName.Text }, { "Type", "Lane" } });
                hubclient.reciveStatus += Hubclient_reciveStatus;
                hubclient.reciveMessage += Hubclient_reciveMessage;
                hubclient.reciveHubError += Hubclient_reciveHubError;
                hubclient.HubInit();
                hubclient.Change(comboBoxLaneCode.SelectedItem.ToString(), DataHanding.MessageEncoder.EncodingLaneMessage(NowWorkingJason, comboBoxLaneCode.SelectedItem.ToString(), textLaneName.Text, DataHanding.MessageEncoder.RecipientType.ALL));

            }
            else if (radioReal.Checked == true)
            {

            }

        }

        /// <summary>
        /// 追加到日志
        /// </summary>
        /// <param name="str"></param>
        private void AppendLog(string str)
        {
            Invoke(new MethodInvoker(() =>
            {
                richTextBox2.AppendText(str + "\r\n");
            }));
        }

        private void Hubclient_reciveHubError(string str)
        {
            AppendLog(str);
        }

        private void Hubclient_reciveMessage(string str)
        {
            AppendLog(str);
        }

        private void Hubclient_reciveStatus(string str)
        {
            AppendLog(str);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hubclient.Change(comboBoxLaneCode.SelectedItem.ToString(), DataHanding.MessageEncoder.EncodingLaneMessage(NowWorkingJason, comboBoxLaneCode.SelectedItem.ToString(), textLaneName.Text, DataHanding.MessageEncoder.RecipientType.ALL));
        }


        public static Dictionary<string, dynamic> QueueDic = new Dictionary<string, dynamic>();

        /// <summary>
        /// 增加一票作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dynamic queue = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Application.StartupPath + "/conf/queue.json"));
            queue = JsonConvert.DeserializeObject<dynamic>(DataHanding.MessageEncoder.EncodingQueueMessage(queue, comboBoxLaneCode.SelectedItem.ToString(), textLaneName.Text, DataHanding.MessageEncoder.QueueAction.create));
            if (hubclient != null)
            {
                hubclient.Change(comboBoxLaneCode.Text, JsonConvert.SerializeObject(queue));
            }
            QueueDic.Add((string)queue.message_content.queue_code, queue);
            comboBox1.Items.Add((string)queue.message_content.queue_code);
            comboBox1.SelectedItem = (string)queue.message_content.queue_code;


        }

        /// <summary>
        /// 显示当前作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            NowWorkingJason = QueueDic[comboBox1.SelectedItem.ToString()];
            NodeView.Nodes.Clear();
            if (tree.GetRootFromJsonStr(0, NowWorkingJason.ToString(), JsonTree.JsonTree.Flag.OnlyObject))
            {
                NodeView.Nodes.Add(tree.TreeNode);
                NodeView.SelectedNode = tree.TreeNode;
            }
            richTextBox1.Text = NowWorkingJason["message_content"].ToString();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabLane")
            {
                NowWorkingJason = Lane;
                NodeView.Nodes.Clear();
                if (tree.GetRootFromJsonStr(0, NowWorkingJason.ToString(), JsonTree.JsonTree.Flag.OnlyObject))
                {
                    NodeView.Nodes.Add(tree.TreeNode);
                    NodeView.SelectedNode = tree.TreeNode;
                }
                richTextBox1.Text = Lane["message_content"].ToString();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            comboBox1.SelectedItem.ToString();

            string send = DataHanding.MessageEncoder.EncodingQueueMessage(NowWorkingJason, comboBoxLaneCode.SelectedItem.ToString(), textLaneName.Text, DataHanding.MessageEncoder.QueueAction.delete);
            if (hubclient != null)
            {
                hubclient.Change(comboBoxLaneCode.Text.ToString(), send);
            }
            comboBox1.Items.Remove(comboBox1.Items[comboBox1.SelectedIndex]);
            QueueDic.Remove((string)NowWorkingJason.message_content.queue_code);
            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
                NowWorkingJason = QueueDic[comboBox1.SelectedItem.ToString()];
            }
            else
            {
                NowWorkingJason = Lane;
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (hubclient != null)
            {
                string send = DataHanding.MessageEncoder.EncodingQueueMessage(NowWorkingJason, comboBoxLaneCode.SelectedItem.ToString(), textLaneName.Text, DataHanding.MessageEncoder.QueueAction.update);
                hubclient.Change(comboBoxLaneCode.Text.ToString(), send);
            }
        }
    }
}
