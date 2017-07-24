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
        private void Form1_Load(object sender, EventArgs e)
        {


            string jsonstr = File.ReadAllText(Application.StartupPath + "/example.json");
            tree = new JsonTree.JsonTree();
            tree.GetRootFromJsonStr(0, jsonstr, JsonTree.JsonTree.Flag.OnlyObject);
            TreeView tw = tree.TreeView;
            this.Controls.Add(tw);
            tw.Width = 200;
            tw.Height = 600;
            tw.Show();
            richTextBox1.Text = tw.SelectedNode.Text;
            ///注册鼠标点击事件
            tree.NodeClick += Tree_NodeClick;
        }

        private void Tree_NodeClick(string res)
        {
            richTextBox1.Text = res;
        }

    }
}
