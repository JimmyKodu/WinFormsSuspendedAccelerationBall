namespace SystemMonitor
{
    partial class ProcessManagerForm
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
            this.dataGridProcesses = new System.Windows.Forms.DataGridView();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.btnSetPriority = new System.Windows.Forms.Button();
            this.btnKillProcess = new System.Windows.Forms.Button();
            this.btnRefreshProcesses = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProcesses)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridProcesses
            // 
            this.dataGridProcesses.AllowUserToAddRows = false;
            this.dataGridProcesses.AllowUserToDeleteRows = false;
            this.dataGridProcesses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProcesses.Location = new System.Drawing.Point(20, 70);
            this.dataGridProcesses.MultiSelect = false;
            this.dataGridProcesses.Name = "dataGridProcesses";
            this.dataGridProcesses.ReadOnly = true;
            this.dataGridProcesses.RowHeadersWidth = 51;
            this.dataGridProcesses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridProcesses.Size = new System.Drawing.Size(860, 450);
            this.dataGridProcesses.TabIndex = 0;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(20, 25);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(82, 20);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "进程分类:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(110, 22);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 28);
            this.cmbCategory.TabIndex = 2;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.CmbCategory_SelectedIndexChanged);
            // 
            // btnSetPriority
            // 
            this.btnSetPriority.Location = new System.Drawing.Point(500, 20);
            this.btnSetPriority.Name = "btnSetPriority";
            this.btnSetPriority.Size = new System.Drawing.Size(120, 30);
            this.btnSetPriority.TabIndex = 3;
            this.btnSetPriority.Text = "调整优先级";
            this.btnSetPriority.UseVisualStyleBackColor = true;
            this.btnSetPriority.Click += new System.EventHandler(this.BtnSetPriority_Click);
            // 
            // btnKillProcess
            // 
            this.btnKillProcess.Location = new System.Drawing.Point(640, 20);
            this.btnKillProcess.Name = "btnKillProcess";
            this.btnKillProcess.Size = new System.Drawing.Size(120, 30);
            this.btnKillProcess.TabIndex = 4;
            this.btnKillProcess.Text = "结束进程";
            this.btnKillProcess.UseVisualStyleBackColor = true;
            this.btnKillProcess.Click += new System.EventHandler(this.BtnKillProcess_Click);
            // 
            // btnRefreshProcesses
            // 
            this.btnRefreshProcesses.Location = new System.Drawing.Point(780, 20);
            this.btnRefreshProcesses.Name = "btnRefreshProcesses";
            this.btnRefreshProcesses.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshProcesses.TabIndex = 5;
            this.btnRefreshProcesses.Text = "刷新";
            this.btnRefreshProcesses.UseVisualStyleBackColor = true;
            this.btnRefreshProcesses.Click += new System.EventHandler(this.BtnRefreshProcesses_Click);
            // 
            // ProcessManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 540);
            this.Controls.Add(this.btnRefreshProcesses);
            this.Controls.Add(this.btnKillProcess);
            this.Controls.Add(this.btnSetPriority);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.dataGridProcesses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ProcessManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "进程管理";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProcesses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridProcesses;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Button btnSetPriority;
        private System.Windows.Forms.Button btnKillProcess;
        private System.Windows.Forms.Button btnRefreshProcesses;
    }
}
