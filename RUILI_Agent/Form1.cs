﻿using JsonTree;
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
        JsonTree.JsonTree tree = new JsonTree.JsonTree();
        List<string> NotAllow;
        private void Form1_Load(object sender, EventArgs e)
        {


            string jasonstr = File.ReadAllText(Application.StartupPath + "/conf/lane.json");
            tree = new JsonTree.JsonTree();
            if (tree.GetRootFromJsonStr(0, jasonstr, JsonTree.JsonTree.Flag.OnlyObject))
            {
                NodeView.Nodes.Add(tree.TreeNode);
                NodeView.SelectedNode = tree.TreeNode;
            }
            NotAllow = new List<string>();
            NotAllow.Add("lane");
            NotAllow.Add("message_content");
            NotAllow.Add("ip_devices");
            NotAllow.Add("com_devices");
            NotAllow.Add("apps");
            NotAllow.Add("device");
        }

        private void messageAppendLog()
        {
            string builder = string.Empty;
            int result = -1;
            if (int.TryParse(NodeView.SelectedNode.Text, out result))
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
                builder = tree.JasonKeyValue[e.Node.Text].ToString();
            }
            richTextBox1.Text = DataHanding.MessageEncoder.ConvertJsonString(builder);
            Dictionary<Label, TextBox> dic = new Dictionary<Label, TextBox>();
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
                dic.Add(new Label { Text = item.Key }, new TextBox { Text = item.Value.ToString(), Tag = item.Key, Name = result--.ToString(), });
            }
            flowLayoutPanel1.Controls.Clear();
            foreach (var item in dic)
            {
                flowLayoutPanel1.Controls.Add(item.Key); item.Key.Show();
                flowLayoutPanel1.Controls.Add(item.Value); item.Value.Show();
                item.Value.TextChanged += Value_TextChanged;
                if(NotAllow.Count(x=>x==item.Key.Text)>0)
                {
                    item.Value.Visible = false;
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
            if (NodeView.SelectedNode.Text == "apps" || NodeView.SelectedNode.Text == "device" || NodeView.SelectedNode.Text == "message_content" || NodeView.SelectedNode.Text == "ip_devices" || NodeView.SelectedNode.Text == "com_device")
            {
                MessageBox.Show("根元素和集合不允许被修改");
                return;
            }
            string key = ((TextBox)sender).Tag.ToString();
            string value = ((TextBox)sender).Text.ToString();
            string intseed = ((TextBox)sender).Name.ToString();

            UpdateLane(key, value, intseed);

        }

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
                            //root.lane.apps[Int32.Parse(intseed)][key] = value;
                            MessageBox.Show("不允许修改集合");
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
            File.WriteAllText(Application.StartupPath + "/test.json", tree.JasonKeyValue["message_content"].ToString());
        }


    }
}
