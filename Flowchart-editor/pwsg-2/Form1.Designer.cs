﻿namespace pwsg_2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.CycleBlockButton = new System.Windows.Forms.Button();
            this.operationBlockButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.trashButton = new System.Windows.Forms.Button();
            this.InputBlockButton = new System.Windows.Forms.Button();
            this.stopBlockButton = new System.Windows.Forms.Button();
            this.decisionBlockButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.CycleBlockButton, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 8);
            this.tableLayoutPanel6.Controls.Add(this.operationBlockButton, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.button2, 0, 7);
            this.tableLayoutPanel6.Controls.Add(this.textBox1, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this.trashButton, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.InputBlockButton, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.stopBlockButton, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.decisionBlockButton, 0, 2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // CycleBlockButton
            // 
            this.CycleBlockButton.BackColor = System.Drawing.SystemColors.Control;
            this.CycleBlockButton.BackgroundImage = global::pwsg_2.Properties.Resources.ЦИКЛ;
            resources.ApplyResources(this.CycleBlockButton, "CycleBlockButton");
            this.CycleBlockButton.Name = "CycleBlockButton";
            this.CycleBlockButton.UseVisualStyleBackColor = false;
            this.CycleBlockButton.Click += new System.EventHandler(this.CycleBlockButton_Click);
            // 
            // operationBlockButton
            // 
            this.operationBlockButton.BackColor = System.Drawing.SystemColors.Control;
            this.operationBlockButton.BackgroundImage = global::pwsg_2.Properties.Resources.ДЕЙСТВИЕ;
            resources.ApplyResources(this.operationBlockButton, "operationBlockButton");
            this.operationBlockButton.Name = "operationBlockButton";
            this.operationBlockButton.UseVisualStyleBackColor = false;
            this.operationBlockButton.Click += new System.EventHandler(this.operationBlockButton_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::pwsg_2.Properties.Resources.СОХРАНИТЬ;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // trashButton
            // 
            this.trashButton.BackColor = System.Drawing.SystemColors.Control;
            this.trashButton.BackgroundImage = global::pwsg_2.Properties.Resources.УДАЛИТЬ;
            resources.ApplyResources(this.trashButton, "trashButton");
            this.trashButton.Name = "trashButton";
            this.trashButton.UseVisualStyleBackColor = false;
            this.trashButton.Click += new System.EventHandler(this.trashButton_Click);
            // 
            // InputBlockButton
            // 
            this.InputBlockButton.BackColor = System.Drawing.SystemColors.Control;
            this.InputBlockButton.BackgroundImage = global::pwsg_2.Properties.Resources.Ввод_вывод;
            resources.ApplyResources(this.InputBlockButton, "InputBlockButton");
            this.InputBlockButton.Name = "InputBlockButton";
            this.InputBlockButton.UseVisualStyleBackColor = false;
            this.InputBlockButton.Click += new System.EventHandler(this.InputBlockButton_Click);
            // 
            // stopBlockButton
            // 
            this.stopBlockButton.BackColor = System.Drawing.SystemColors.Control;
            this.stopBlockButton.BackgroundImage = global::pwsg_2.Properties.Resources.КОНЕЦ1;
            resources.ApplyResources(this.stopBlockButton, "stopBlockButton");
            this.stopBlockButton.Name = "stopBlockButton";
            this.stopBlockButton.UseVisualStyleBackColor = false;
            this.stopBlockButton.Click += new System.EventHandler(this.stopBlockButton_Click);
            // 
            // decisionBlockButton
            // 
            this.decisionBlockButton.BackColor = System.Drawing.SystemColors.Control;
            this.decisionBlockButton.BackgroundImage = global::pwsg_2.Properties.Resources.ЛОГИЧЕСКОЕ_УСЛОВИЕ;
            resources.ApplyResources(this.decisionBlockButton, "decisionBlockButton");
            this.decisionBlockButton.Name = "decisionBlockButton";
            this.decisionBlockButton.UseVisualStyleBackColor = false;
            this.decisionBlockButton.Click += new System.EventHandler(this.decisionBlockButton_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Name = "panel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button trashButton;
        private System.Windows.Forms.Button stopBlockButton;
        private System.Windows.Forms.Button decisionBlockButton;
        private System.Windows.Forms.Button operationBlockButton;
        private System.Windows.Forms.Button InputBlockButton;
        private System.Windows.Forms.Button CycleBlockButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
