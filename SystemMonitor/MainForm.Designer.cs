namespace SystemMonitor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSystem = new System.Windows.Forms.TabPage();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.groupNetwork = new System.Windows.Forms.GroupBox();
            this.lblUploadSpeed = new System.Windows.Forms.Label();
            this.lblDownloadSpeed = new System.Windows.Forms.Label();
            this.groupTemp = new System.Windows.Forms.GroupBox();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.groupMemory = new System.Windows.Forms.GroupBox();
            this.progressMemory = new System.Windows.Forms.ProgressBar();
            this.lblMemoryUsage = new System.Windows.Forms.Label();
            this.groupCpu = new System.Windows.Forms.GroupBox();
            this.progressCpu = new System.Windows.Forms.ProgressBar();
            this.lblCpuUsage = new System.Windows.Forms.Label();
            this.tabProcess = new System.Windows.Forms.TabPage();
            this.btnRefreshProcesses = new System.Windows.Forms.Button();
            this.btnKillProcess = new System.Windows.Forms.Button();
            this.btnSetPriority = new System.Windows.Forms.Button();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.dataGridProcesses = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl.SuspendLayout();
            this.tabSystem.SuspendLayout();
            this.groupNetwork.SuspendLayout();
            this.groupTemp.SuspendLayout();
            this.groupMemory.SuspendLayout();
            this.groupCpu.SuspendLayout();
            this.tabProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProcesses)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSystem);
            this.tabControl.Controls.Add(this.tabProcess);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(984, 561);
            this.tabControl.TabIndex = 0;
            // 
            // tabSystem
            // 
            this.tabSystem.Controls.Add(this.btnOptimize);
            this.tabSystem.Controls.Add(this.groupNetwork);
            this.tabSystem.Controls.Add(this.groupTemp);
            this.tabSystem.Controls.Add(this.groupMemory);
            this.tabSystem.Controls.Add(this.groupCpu);
            this.tabSystem.Location = new System.Drawing.Point(4, 29);
            this.tabSystem.Name = "tabSystem";
            this.tabSystem.Padding = new System.Windows.Forms.Padding(3);
            this.tabSystem.Size = new System.Drawing.Size(976, 528);
            this.tabSystem.TabIndex = 0;
            this.tabSystem.Text = "系统监控";
            this.tabSystem.UseVisualStyleBackColor = true;
            // 
            // btnOptimize
            // 
            this.btnOptimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnOptimize.Location = new System.Drawing.Point(350, 450);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(250, 50);
            this.btnOptimize.TabIndex = 4;
            this.btnOptimize.Text = "一键优化";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.BtnOptimize_Click);
            // 
            // groupNetwork
            // 
            this.groupNetwork.Controls.Add(this.lblUploadSpeed);
            this.groupNetwork.Controls.Add(this.lblDownloadSpeed);
            this.groupNetwork.Location = new System.Drawing.Point(500, 220);
            this.groupNetwork.Name = "groupNetwork";
            this.groupNetwork.Size = new System.Drawing.Size(450, 200);
            this.groupNetwork.TabIndex = 3;
            this.groupNetwork.TabStop = false;
            this.groupNetwork.Text = "网络速度";
            // 
            // lblUploadSpeed
            // 
            this.lblUploadSpeed.AutoSize = true;
            this.lblUploadSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblUploadSpeed.Location = new System.Drawing.Point(20, 100);
            this.lblUploadSpeed.Name = "lblUploadSpeed";
            this.lblUploadSpeed.Size = new System.Drawing.Size(150, 25);
            this.lblUploadSpeed.TabIndex = 1;
            this.lblUploadSpeed.Text = "上传: 0 KB/s";
            // 
            // lblDownloadSpeed
            // 
            this.lblDownloadSpeed.AutoSize = true;
            this.lblDownloadSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblDownloadSpeed.Location = new System.Drawing.Point(20, 50);
            this.lblDownloadSpeed.Name = "lblDownloadSpeed";
            this.lblDownloadSpeed.Size = new System.Drawing.Size(150, 25);
            this.lblDownloadSpeed.TabIndex = 0;
            this.lblDownloadSpeed.Text = "下载: 0 KB/s";
            // 
            // groupTemp
            // 
            this.groupTemp.Controls.Add(this.lblTemperature);
            this.groupTemp.Location = new System.Drawing.Point(500, 20);
            this.groupTemp.Name = "groupTemp";
            this.groupTemp.Size = new System.Drawing.Size(450, 180);
            this.groupTemp.TabIndex = 2;
            this.groupTemp.TabStop = false;
            this.groupTemp.Text = "硬件温度";
            // 
            // lblTemperature
            // 
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblTemperature.Location = new System.Drawing.Point(20, 70);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(200, 31);
            this.lblTemperature.TabIndex = 0;
            this.lblTemperature.Text = "CPU: N/A";
            // 
            // groupMemory
            // 
            this.groupMemory.Controls.Add(this.progressMemory);
            this.groupMemory.Controls.Add(this.lblMemoryUsage);
            this.groupMemory.Location = new System.Drawing.Point(20, 220);
            this.groupMemory.Name = "groupMemory";
            this.groupMemory.Size = new System.Drawing.Size(450, 200);
            this.groupMemory.TabIndex = 1;
            this.groupMemory.TabStop = false;
            this.groupMemory.Text = "内存占用";
            // 
            // progressMemory
            // 
            this.progressMemory.Location = new System.Drawing.Point(20, 120);
            this.progressMemory.Name = "progressMemory";
            this.progressMemory.Size = new System.Drawing.Size(400, 40);
            this.progressMemory.TabIndex = 1;
            // 
            // lblMemoryUsage
            // 
            this.lblMemoryUsage.AutoSize = true;
            this.lblMemoryUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblMemoryUsage.Location = new System.Drawing.Point(20, 60);
            this.lblMemoryUsage.Name = "lblMemoryUsage";
            this.lblMemoryUsage.Size = new System.Drawing.Size(80, 31);
            this.lblMemoryUsage.TabIndex = 0;
            this.lblMemoryUsage.Text = "0%";
            // 
            // groupCpu
            // 
            this.groupCpu.Controls.Add(this.progressCpu);
            this.groupCpu.Controls.Add(this.lblCpuUsage);
            this.groupCpu.Location = new System.Drawing.Point(20, 20);
            this.groupCpu.Name = "groupCpu";
            this.groupCpu.Size = new System.Drawing.Size(450, 180);
            this.groupCpu.TabIndex = 0;
            this.groupCpu.TabStop = false;
            this.groupCpu.Text = "CPU占用";
            // 
            // progressCpu
            // 
            this.progressCpu.Location = new System.Drawing.Point(20, 110);
            this.progressCpu.Name = "progressCpu";
            this.progressCpu.Size = new System.Drawing.Size(400, 40);
            this.progressCpu.TabIndex = 1;
            // 
            // lblCpuUsage
            // 
            this.lblCpuUsage.AutoSize = true;
            this.lblCpuUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblCpuUsage.Location = new System.Drawing.Point(20, 50);
            this.lblCpuUsage.Name = "lblCpuUsage";
            this.lblCpuUsage.Size = new System.Drawing.Size(80, 31);
            this.lblCpuUsage.TabIndex = 0;
            this.lblCpuUsage.Text = "0%";
            // 
            // tabProcess
            // 
            this.tabProcess.Controls.Add(this.btnRefreshProcesses);
            this.tabProcess.Controls.Add(this.btnKillProcess);
            this.tabProcess.Controls.Add(this.btnSetPriority);
            this.tabProcess.Controls.Add(this.cmbCategory);
            this.tabProcess.Controls.Add(this.lblCategory);
            this.tabProcess.Controls.Add(this.dataGridProcesses);
            this.tabProcess.Location = new System.Drawing.Point(4, 29);
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcess.Size = new System.Drawing.Size(976, 528);
            this.tabProcess.TabIndex = 1;
            this.tabProcess.Text = "进程监控";
            this.tabProcess.UseVisualStyleBackColor = true;
            // 
            // btnRefreshProcesses
            // 
            this.btnRefreshProcesses.Location = new System.Drawing.Point(850, 20);
            this.btnRefreshProcesses.Name = "btnRefreshProcesses";
            this.btnRefreshProcesses.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshProcesses.TabIndex = 5;
            this.btnRefreshProcesses.Text = "刷新";
            this.btnRefreshProcesses.UseVisualStyleBackColor = true;
            this.btnRefreshProcesses.Click += new System.EventHandler(this.BtnRefreshProcesses_Click);
            // 
            // btnKillProcess
            // 
            this.btnKillProcess.Location = new System.Drawing.Point(650, 20);
            this.btnKillProcess.Name = "btnKillProcess";
            this.btnKillProcess.Size = new System.Drawing.Size(120, 30);
            this.btnKillProcess.TabIndex = 4;
            this.btnKillProcess.Text = "结束进程";
            this.btnKillProcess.UseVisualStyleBackColor = true;
            this.btnKillProcess.Click += new System.EventHandler(this.BtnKillProcess_Click);
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
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(100, 22);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 28);
            this.cmbCategory.TabIndex = 2;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.CmbCategory_SelectedIndexChanged);
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
            this.dataGridProcesses.Size = new System.Drawing.Size(930, 430);
            this.dataGridProcesses.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 561);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(984, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "就绪";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 583);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Name = "MainForm";
            this.Text = "系统性能监控与优化";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabSystem.ResumeLayout(false);
            this.groupNetwork.ResumeLayout(false);
            this.groupNetwork.PerformLayout();
            this.groupTemp.ResumeLayout(false);
            this.groupTemp.PerformLayout();
            this.groupMemory.ResumeLayout(false);
            this.groupMemory.PerformLayout();
            this.groupCpu.ResumeLayout(false);
            this.groupCpu.PerformLayout();
            this.tabProcess.ResumeLayout(false);
            this.tabProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProcesses)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSystem;
        private System.Windows.Forms.TabPage tabProcess;
        private System.Windows.Forms.GroupBox groupCpu;
        private System.Windows.Forms.Label lblCpuUsage;
        private System.Windows.Forms.ProgressBar progressCpu;
        private System.Windows.Forms.GroupBox groupMemory;
        private System.Windows.Forms.ProgressBar progressMemory;
        private System.Windows.Forms.Label lblMemoryUsage;
        private System.Windows.Forms.GroupBox groupTemp;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.GroupBox groupNetwork;
        private System.Windows.Forms.Label lblUploadSpeed;
        private System.Windows.Forms.Label lblDownloadSpeed;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.DataGridView dataGridProcesses;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Button btnSetPriority;
        private System.Windows.Forms.Button btnKillProcess;
        private System.Windows.Forms.Button btnRefreshProcesses;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}
