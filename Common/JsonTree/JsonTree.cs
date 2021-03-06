﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JsonTree
{
    public class JsonTree
    {
        #region 废弃 
        //public TreeView TreeView = null;
        //public delegate void TreeNodeClick(string res);
        //public event TreeNodeClick NodeClick;
        //public delegate void TreeNodeDoubleClick(Dictionary<string, object> dic);
        //public event TreeNodeDoubleClick NodeDoubleClick;

        //public delegate void NodeJasonNeedBeEncode();
        //public event NodeJasonNeedBeEncode needbeencode;

        //public delegate void TriggerRootNode(string str);
        //public event TriggerRootNode BindRichText;
        //public List<string> JsonRootList = new List<string>();
        //public dynamic nowJson;
        #endregion
        public Dictionary<string, object> JasonKeyValue = new Dictionary<string, object>();
        public JsonTreeNode TreeNode { get; set; }

        /// <summary>
        /// 将关系数据列表 转化为 树状图
        /// </summary>
        /// <param name="parentId">根节点</param>
        /// <param name="relation">关系数据</param>
        /// <returns></returns>
        public Node GetRoot(int parentId, List<Mapping> mapping)
        {
            var root = new Node();
            root.Key = parentId;

            var groups = mapping.Select(x => new Node { Key = x.ChildID, Left = x.ParentID }).GroupBy(x => x.Left);
            var child = groups.FirstOrDefault(x => x.Key == parentId);

            if (child == null)
            {
                return null;
            }
            else
            {
                var children = child.ToList();
                if (children.Count > 0)
                {
                    var dict = groups.ToDictionary(g => g.Key, g => g.ToList());
                    for (int i = 0; i < children.Count; i++)
                    {
                        this.AddChild(children[i], dict);
                    }
                }

                root.Children = children;
            }

            return root;
        }
        //public void UpdateJasonObject(string key, string value)
        //{
        //    Dictionary<string, object> father = null;
        //    foreach (var item in JsonRootList)
        //    {
        //        if (key == item)
        //        {
        //            JasonKeyValue[item] = value;
        //        }
        //        else
        //        {
        //            Dictionary<string, object> tempdic = JsonConvert.DeserializeObject<Dictionary<string, object>>(JasonKeyValue[item]);
        //            if (tempdic.Count(x => x.Key == key) > 0)
        //            {
        //                tempdic[key] = value;//二层循环判断。
        //            }
        //            JasonKeyValue[item] = JsonConvert.SerializeObject(tempdic);

        //            for (int i = 0; i < JsonRootList.Count - 1; i++)
        //            {
        //                father = JsonConvert.DeserializeObject<Dictionary<string, object>>(JasonKeyValue[JsonRootList[i + 1]]);
        //                string fatherName = JsonRootList[i + 1];
        //                dynamic child = JsonConvert.DeserializeObject<dynamic>(JasonKeyValue[JsonRootList[i]]);

        //                father[JsonRootList[i]] = child;
        //                JasonKeyValue[fatherName] = JsonConvert.SerializeObject(father);

        //            }
        //        }
        //    }
        //    File.WriteAllText(Application.StartupPath + "/Out.json", JsonConvert.SerializeObject(father));
        //    nowJson = JsonConvert.SerializeObject(father);
        //    this.needbeencode?.Invoke();
        //    this.NodeClick?.Invoke(JsonConvert.SerializeObject(JasonKeyValue[key].ToString()));
        //}
        /// <summary>
        /// 从JSON字符串中获取TreeView节点数
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="jsonstr"></param>
        /// <param name="flag"></param>
        public bool GetRootFromJsonStr(int parentId, string jsonstr, Flag flag)
        {
            try
            {
                JasonKeyValue.Clear();

                JObject jobject = JObject.Parse(jsonstr);
                var rootNode = new JsonTreeNode(NodeType.Object, "root");
                //TreeView tw = new TreeView();
                //tw.Nodes.Add(rootNode);
                //tw.SelectedNode = rootNode;
                rootNode.ImageKey = rootNode.NodeType.ToString();
                rootNode.SelectedImageKey = rootNode.ImageKey;
                LoadObject(jobject, rootNode, flag);
                rootNode.Expand();
                TreeNode = rootNode;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //TreeView = tw;
            //TreeView.NodeMouseDoubleClick += TreeView_NodeMouseDoubleClick;
            //TreeView.NodeMouseClick += TreeView_NodeMouseClick;
        }
        #region 废弃
        //private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    //MessageBox.Show(e.Node.Text);
        //    string para = e.Node.Text;
        //    //JObject jobj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Application.StartupPath + "/example.json"));
        //    if (JasonKeyValue.Count(x => x.Key == para) > 0)
        //    {
        //        this.NodeClick?.Invoke(ConvertJsonString(JasonKeyValue[para].ToString()));
        //    }
        //}
        //private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    //MessageBox.Show(e.Node.Text);
        //    string para = e.Node.Text;
        //    BindRichText?.Invoke(e.Node.Text);
        //    JObject jobj = nowJson;
        //    //Dictionary<string, string> jobjDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(jobj));
        //    if (JasonKeyValue.Count(x => x.Key == para) > 0)
        //    {
        //        //this.NodeDoubleClick.Invoke(jobjDic);
        //        dynamic obj = JsonConvert.DeserializeObject<dynamic>(JasonKeyValue[para]);
        //        JToken jto = JToken.FromObject(obj);
        //        if (jto.Type == JTokenType.Object)
        //        {
        //            Dictionary<string, object> dicc = JsonConvert.DeserializeObject<Dictionary<string, object>>(JasonKeyValue[para]);
        //            this.NodeDoubleClick?.Invoke(dicc);
        //        }
        //        else if (jto.Type == JTokenType.Array)
        //        {
        //            Dictionary<string, object>[] dicc = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(JasonKeyValue[para]);
        //            Dictionary<string, object> newdicc = new Dictionary<string, object>();
        //            foreach (var item in dicc)
        //            {
        //                foreach (var items in item)
        //                {
        //                    if (newdicc.Count(x => x.Key == items.Key) == 0)
        //                    {
        //                        newdicc.Add(items.Key, items.Value);
        //                    }

        //                }

        //            }

        //        }
        //    }

        //}
        #endregion

        public enum Flag
        {
            All,
            OnlyObject
        }
        private void LoadObject(JObject jobject, JsonTreeNode rootNode, Flag flag)
        {

            foreach (var item in jobject)
            {
                if (item.Key != "property" && item.Key != "method")
                {
                    AddNode(rootNode, item.Key, item.Value, flag);
                }
                switch (flag)
                {
                    case Flag.OnlyObject:
                        if (JasonKeyValue.Count(x => x.Key == item.Key) == 0)
                        {
                            if (JToken.FromObject((object)item.Value).Type == JTokenType.Object || JToken.FromObject((object)item.Value).Type == JTokenType.Array)
                            {
                                JasonKeyValue.Add(item.Key, item.Value);
                            }
                        }
                        break;
                    case Flag.All:
                        if (JasonKeyValue.Count(x => x.Key == item.Key) == 0)
                        {
                            JasonKeyValue.Add(item.Key, item.Value);
                        }
                        break;


                }

            }

        }
        private void AddNode(JsonTreeNode parentNode, string property, JToken item, Flag flag)
        {
            JsonTreeNode node = null;
            if (flag == Flag.All)
            {
                node = JsonTreeNodeCreator.CreateNode(property, item);
                parentNode.Nodes.Add(node);
            }
            else
            {
                if (item.Type == JTokenType.Object || item.Type == JTokenType.Array)
                {
                    node = JsonTreeNodeCreator.CreateNode(property, item);
                    parentNode.Nodes.Add(node);

                }
            }
            if (item.Type == JTokenType.Array)
            {
                LoadArray(item, node, flag);
            }

            if (item.Type == JTokenType.Object)
            {
                LoadObject(item as JObject, node, flag);
            }
        }
        private void LoadArray(JToken item, JsonTreeNode node, Flag flag)
        {
            int i = 0;
            foreach (var childitem in item)
            {

                AddNode(node, i++.ToString(), childitem, flag);
                //JasonKeyValue.Add(seed, childitem);
            }
        }
        public void AddChild(Node child, IDictionary<int, List<Node>> mapping)
        {
            if (mapping != null && mapping.ContainsKey(child.Key))
            {
                child.Children = mapping[child.Key].ToList();
                for (int i = 0; i < child.Children.Count; i++)
                {
                    this.AddChild(child.Children[i], mapping);
                }
            }
        }
        public TreeView PrintTree(Node node, TreeView tview)
        {
            tview.Nodes.Add(node.Key.ToString());
            if (node.Children == null)
            {
                return null;
            }

            foreach (Node child in node.Children)
            {
                PrintTree(child, tview);
            }
            return tview;
        }
        public bool IsAContainB(int a, int b, IList<Mapping> mapping)
        {
            var result = IsAContainBTop(a, b, mapping);

            if (result)
            {
                return true;
            }

            result = IsAContainBBottom(a, b, mapping);
            if (result)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// A 是否包含 B
        /// </summary>
        /// <param name="a">Parent</param>
        /// <param name="b">Child</param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public bool IsAContainBTop(int a, int b, IList<Mapping> mapping)
        {
            // 排除自循环
            if (a == b)
            {
                return true;
            }

            var getBList = mapping.Where(x => x.ChildID == b).ToList();
            if (getBList.Count == 0)
            {
                // go to bottom jquery
                return false;
            }

            var result = false;
            foreach (var getB in getBList)
            {
                if (getB.ParentID == a)
                {
                    return true;
                }
                else
                {
                    // 向上查找
                    result = this.IsAContainBTop(a, getB.ParentID, mapping);
                }

                if (result)
                {
                    return true;
                }
            }

            return result;
        }
        public bool IsAContainBBottom(int a, int b, IList<Mapping> mapping)
        {
            // 排除自循环
            if (a == b)
            {
                return true;
            }

            var getBList = mapping.Where(x => x.ParentID == b).ToList();
            if (getBList.Count == 0)
            {
                return false;
            }

            var result = false;
            foreach (var getB in getBList)
            {
                if (getB.ChildID == a)
                {
                    return true;
                }
                else
                {
                    // 向下查找
                    result = this.IsAContainBBottom(a, getB.ChildID, mapping);
                }

                if (result)
                {
                    return true;
                }
            }

            return result;
        }
    }
    /// <summary>
    /// 关系数据结构
    /// parent --{Node}--children
    /// </summary>
    public class Mapping
    {
        public int ParentID { get; set; }

        public int ChildID { get; set; }
    }
    /// <summary>
    /// 节点数据结构
    /// {p1,p2,p3,...} -- [Node] -- {c1,c2,c3,...}
    /// </summary>
    public class Node
    {
        public int Left { get; set; }

        public int Key { get; set; }

        public List<Node> Children { get; set; }
    }
}
