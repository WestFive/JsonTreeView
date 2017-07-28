namespace TreeTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TreeGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TextGroup = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLane = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDisConnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.radioReal = new System.Windows.Forms.RadioButton();
            this.radioSimulation = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textLaneName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLaneCode = new System.Windows.Forms.ComboBox();
            this.MessageHubBox = new System.Windows.Forms.GroupBox();
            this.TextServerURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabQueue = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.logGroupBox = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabLane.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.MessageHubBox.SuspendLayout();
            this.tabQueue.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeGroupBox
            // 
            this.TreeGroupBox.Location = new System.Drawing.Point(3, 3);
            this.TreeGroupBox.Name = "TreeGroupBox";
            this.TreeGroupBox.Size = new System.Drawing.Size(265, 485);
            this.TreeGroupBox.TabIndex = 0;
            this.TreeGroupBox.TabStop = false;
            this.TreeGroupBox.Text = "Tree";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.38527F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.61473F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 284F));
            this.tableLayoutPanel1.Controls.Add(this.TreeGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TextGroup, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(990, 491);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(274, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(428, 485);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // TextGroup
            // 
            this.TextGroup.Location = new System.Drawing.Point(708, 3);
            this.TextGroup.Name = "TextGroup";
            this.TextGroup.Size = new System.Drawing.Size(278, 485);
            this.TextGroup.TabIndex = 1;
            this.TextGroup.TabStop = false;
            this.TextGroup.Text = "textGroup";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLane);
            this.tabControl1.Controls.Add(this.tabQueue);
            this.tabControl1.Location = new System.Drawing.Point(286, 509);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(713, 139);
            this.tabControl1.TabIndex = 1;
            // 
            // tabLane
            // 
            this.tabLane.Controls.Add(this.groupBox2);
            this.tabLane.Controls.Add(this.groupBox1);
            this.tabLane.Controls.Add(this.MessageHubBox);
            this.tabLane.Location = new System.Drawing.Point(4, 22);
            this.tabLane.Name = "tabLane";
            this.tabLane.Padding = new System.Windows.Forms.Padding(3);
            this.tabLane.Size = new System.Drawing.Size(705, 113);
            this.tabLane.TabIndex = 0;
            this.tabLane.Text = "Lane";
            this.tabLane.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.buttonDisConnect);
            this.groupBox2.Controls.Add(this.buttonConnect);
            this.groupBox2.Controls.Add(this.radioReal);
            this.groupBox2.Controls.Add(this.radioSimulation);
            this.groupBox2.Location = new System.Drawing.Point(431, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 104);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode&Connection";
            // 
            // buttonDisConnect
            // 
            this.buttonDisConnect.Location = new System.Drawing.Point(180, 58);
            this.buttonDisConnect.Name = "buttonDisConnect";
            this.buttonDisConnect.Size = new System.Drawing.Size(82, 38);
            this.buttonDisConnect.TabIndex = 3;
            this.buttonDisConnect.Text = "DisConnect";
            this.buttonDisConnect.UseVisualStyleBackColor = true;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(180, 15);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(82, 40);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // radioReal
            // 
            this.radioReal.AutoSize = true;
            this.radioReal.Location = new System.Drawing.Point(6, 46);
            this.radioReal.Name = "radioReal";
            this.radioReal.Size = new System.Drawing.Size(71, 16);
            this.radioReal.TabIndex = 1;
            this.radioReal.TabStop = true;
            this.radioReal.Text = "RealMode";
            this.radioReal.UseVisualStyleBackColor = true;
            // 
            // radioSimulation
            // 
            this.radioSimulation.AutoSize = true;
            this.radioSimulation.Location = new System.Drawing.Point(6, 24);
            this.radioSimulation.Name = "radioSimulation";
            this.radioSimulation.Size = new System.Drawing.Size(107, 16);
            this.radioSimulation.TabIndex = 0;
            this.radioSimulation.TabStop = true;
            this.radioSimulation.Text = "SimulationMode";
            this.radioSimulation.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textLaneName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxLaneCode);
            this.groupBox1.Location = new System.Drawing.Point(215, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 101);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lane";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "LaneName";
            // 
            // textLaneName
            // 
            this.textLaneName.Location = new System.Drawing.Point(15, 58);
            this.textLaneName.Name = "textLaneName";
            this.textLaneName.Size = new System.Drawing.Size(172, 21);
            this.textLaneName.TabIndex = 2;
            this.textLaneName.Text = "CN-RUILI-JICP1-L01";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lane_Code";
            // 
            // comboBoxLaneCode
            // 
            this.comboBoxLaneCode.FormattingEnabled = true;
            this.comboBoxLaneCode.Items.AddRange(new object[] {
            "CN-RUILI-JICP1-L01",
            "CN-RUILI-JICP1-L02",
            "CN-RUILI-JICP1-L03",
            "CN-RUILI-JICP1-L04",
            "CN-RUILI-JICP1-L05"});
            this.comboBoxLaneCode.Location = new System.Drawing.Point(6, 20);
            this.comboBoxLaneCode.Name = "comboBoxLaneCode";
            this.comboBoxLaneCode.Size = new System.Drawing.Size(198, 20);
            this.comboBoxLaneCode.TabIndex = 0;
            this.comboBoxLaneCode.Text = "CN-RUILI-JICP1-L01";
            // 
            // MessageHubBox
            // 
            this.MessageHubBox.Controls.Add(this.TextServerURL);
            this.MessageHubBox.Controls.Add(this.label1);
            this.MessageHubBox.Location = new System.Drawing.Point(6, 6);
            this.MessageHubBox.Name = "MessageHubBox";
            this.MessageHubBox.Size = new System.Drawing.Size(203, 101);
            this.MessageHubBox.TabIndex = 2;
            this.MessageHubBox.TabStop = false;
            this.MessageHubBox.Text = "MessageHub";
            // 
            // TextServerURL
            // 
            this.TextServerURL.Location = new System.Drawing.Point(17, 40);
            this.TextServerURL.Name = "TextServerURL";
            this.TextServerURL.Size = new System.Drawing.Size(172, 21);
            this.TextServerURL.TabIndex = 0;
            this.TextServerURL.Text = "http://localhost:5000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server URL";
            // 
            // tabQueue
            // 
            this.tabQueue.Controls.Add(this.groupBox5);
            this.tabQueue.Controls.Add(this.groupBox4);
            this.tabQueue.Controls.Add(this.groupBox3);
            this.tabQueue.Location = new System.Drawing.Point(4, 22);
            this.tabQueue.Name = "tabQueue";
            this.tabQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tabQueue.Size = new System.Drawing.Size(705, 113);
            this.tabQueue.TabIndex = 1;
            this.tabQueue.Text = "Queue";
            this.tabQueue.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(242, 104);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "LocalCache";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 78);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Queue";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // logGroupBox
            // 
            this.logGroupBox.Location = new System.Drawing.Point(12, 509);
            this.logGroupBox.Name = "logGroupBox";
            this.logGroupBox.Size = new System.Drawing.Size(268, 135);
            this.logGroupBox.TabIndex = 2;
            this.logGroupBox.TabStop = false;
            this.logGroupBox.Text = "log";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(142, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 78);
            this.button2.TabIndex = 1;
            this.button2.Text = "Remove Queue";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Location = new System.Drawing.Point(251, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(148, 104);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ServerCache";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(26, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 78);
            this.button3.TabIndex = 2;
            this.button3.Text = "Clear Queue";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Location = new System.Drawing.Point(405, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(294, 104);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SelectQueue";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(282, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "QueueCode";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 47);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(99, 51);
            this.button4.TabIndex = 2;
            this.button4.Text = "Active";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(189, 47);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 51);
            this.button5.TabIndex = 3;
            this.button5.Text = "Push";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(92, 57);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(82, 40);
            this.button6.TabIndex = 4;
            this.button6.Text = "Push";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 662);
            this.Controls.Add(this.logGroupBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabLane.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.MessageHubBox.ResumeLayout(false);
            this.MessageHubBox.PerformLayout();
            this.tabQueue.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox TreeGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox TextGroup;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLane;
        private System.Windows.Forms.TabPage tabQueue;
        private System.Windows.Forms.GroupBox logGroupBox;
        private System.Windows.Forms.GroupBox MessageHubBox;
        private System.Windows.Forms.TextBox TextServerURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLaneCode;
        private System.Windows.Forms.TextBox textLaneName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDisConnect;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.RadioButton radioReal;
        private System.Windows.Forms.RadioButton radioSimulation;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

