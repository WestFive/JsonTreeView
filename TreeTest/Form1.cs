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


            //tree.JsonRootList.Reverse();
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
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Dock = DockStyle.Left;
        }
    }
}
