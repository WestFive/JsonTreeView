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
            this.tabQueue = new System.Windows.Forms.TabPage();
            this.tabLane = new System.Windows.Forms.TabPage();
            this.logGroupBox = new System.Windows.Forms.GroupBox();
            this.QueueButtonSubmit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabQueue.SuspendLayout();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 283F));
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(429, 485);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // TextGroup
            // 
            this.TextGroup.Location = new System.Drawing.Point(709, 3);
            this.TextGroup.Name = "TextGroup";
            this.TextGroup.Size = new System.Drawing.Size(278, 485);
            this.TextGroup.TabIndex = 1;
            this.TextGroup.TabStop = false;
            this.TextGroup.Text = "textGroup";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabQueue);
            this.tabControl1.Controls.Add(this.tabLane);
            this.tabControl1.Location = new System.Drawing.Point(286, 509);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(713, 139);
            this.tabControl1.TabIndex = 1;
            // 
            // tabQueue
            // 
            this.tabQueue.Controls.Add(this.QueueButtonSubmit);
            this.tabQueue.Location = new System.Drawing.Point(4, 22);
            this.tabQueue.Name = "tabQueue";
            this.tabQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tabQueue.Size = new System.Drawing.Size(705, 113);
            this.tabQueue.TabIndex = 0;
            this.tabQueue.Text = "Queue";
            this.tabQueue.UseVisualStyleBackColor = true;
            // 
            // tabLane
            // 
            this.tabLane.Location = new System.Drawing.Point(4, 22);
            this.tabLane.Name = "tabLane";
            this.tabLane.Padding = new System.Windows.Forms.Padding(3);
            this.tabLane.Size = new System.Drawing.Size(705, 113);
            this.tabLane.TabIndex = 1;
            this.tabLane.Text = "Lane";
            this.tabLane.UseVisualStyleBackColor = true;
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
            // QueueButtonSubmit
            // 
            this.QueueButtonSubmit.Location = new System.Drawing.Point(589, 84);
            this.QueueButtonSubmit.Name = "QueueButtonSubmit";
            this.QueueButtonSubmit.Size = new System.Drawing.Size(75, 23);
            this.QueueButtonSubmit.TabIndex = 0;
            this.QueueButtonSubmit.Text = "提交修改";
            this.QueueButtonSubmit.UseVisualStyleBackColor = true;
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
            this.tabQueue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox TreeGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox TextGroup;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabQueue;
        private System.Windows.Forms.TabPage tabLane;
        private System.Windows.Forms.GroupBox logGroupBox;
        private System.Windows.Forms.Button QueueButtonSubmit;
    }
}

