namespace SystemMonitor
{
    partial class DetailsForm
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
            this.groupCpu = new System.Windows.Forms.GroupBox();
            this.progressCpu = new System.Windows.Forms.ProgressBar();
            this.lblCpuUsage = new System.Windows.Forms.Label();
            this.groupMemory = new System.Windows.Forms.GroupBox();
            this.progressMemory = new System.Windows.Forms.ProgressBar();
            this.lblMemoryUsage = new System.Windows.Forms.Label();
            this.groupTemp = new System.Windows.Forms.GroupBox();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.groupNetwork = new System.Windows.Forms.GroupBox();
            this.lblUploadSpeed = new System.Windows.Forms.Label();
            this.lblDownloadSpeed = new System.Windows.Forms.Label();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.btnProcessManager = new System.Windows.Forms.Button();
            this.groupCpu.SuspendLayout();
            this.groupMemory.SuspendLayout();
            this.groupTemp.SuspendLayout();
            this.groupNetwork.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupCpu
            // 
            this.groupCpu.Controls.Add(this.progressCpu);
            this.groupCpu.Controls.Add(this.lblCpuUsage);
            this.groupCpu.Location = new System.Drawing.Point(20, 20);
            this.groupCpu.Name = "groupCpu";
            this.groupCpu.Size = new System.Drawing.Size(350, 120);
            this.groupCpu.TabIndex = 0;
            this.groupCpu.TabStop = false;
            this.groupCpu.Text = "CPU占用";
            // 
            // progressCpu
            // 
            this.progressCpu.Location = new System.Drawing.Point(20, 70);
            this.progressCpu.Name = "progressCpu";
            this.progressCpu.Size = new System.Drawing.Size(310, 30);
            this.progressCpu.TabIndex = 1;
            // 
            // lblCpuUsage
            // 
            this.lblCpuUsage.AutoSize = true;
            this.lblCpuUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblCpuUsage.Location = new System.Drawing.Point(20, 35);
            this.lblCpuUsage.Name = "lblCpuUsage";
            this.lblCpuUsage.Size = new System.Drawing.Size(52, 29);
            this.lblCpuUsage.TabIndex = 0;
            this.lblCpuUsage.Text = "0%";
            // 
            // groupMemory
            // 
            this.groupMemory.Controls.Add(this.progressMemory);
            this.groupMemory.Controls.Add(this.lblMemoryUsage);
            this.groupMemory.Location = new System.Drawing.Point(20, 160);
            this.groupMemory.Name = "groupMemory";
            this.groupMemory.Size = new System.Drawing.Size(350, 120);
            this.groupMemory.TabIndex = 1;
            this.groupMemory.TabStop = false;
            this.groupMemory.Text = "内存占用";
            // 
            // progressMemory
            // 
            this.progressMemory.Location = new System.Drawing.Point(20, 70);
            this.progressMemory.Name = "progressMemory";
            this.progressMemory.Size = new System.Drawing.Size(310, 30);
            this.progressMemory.TabIndex = 1;
            // 
            // lblMemoryUsage
            // 
            this.lblMemoryUsage.AutoSize = true;
            this.lblMemoryUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblMemoryUsage.Location = new System.Drawing.Point(20, 35);
            this.lblMemoryUsage.Name = "lblMemoryUsage";
            this.lblMemoryUsage.Size = new System.Drawing.Size(39, 24);
            this.lblMemoryUsage.TabIndex = 0;
            this.lblMemoryUsage.Text = "0%";
            // 
            // groupTemp
            // 
            this.groupTemp.Controls.Add(this.lblTemperature);
            this.groupTemp.Location = new System.Drawing.Point(20, 300);
            this.groupTemp.Name = "groupTemp";
            this.groupTemp.Size = new System.Drawing.Size(350, 80);
            this.groupTemp.TabIndex = 2;
            this.groupTemp.TabStop = false;
            this.groupTemp.Text = "硬件温度";
            // 
            // lblTemperature
            // 
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblTemperature.Location = new System.Drawing.Point(20, 35);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(104, 25);
            this.lblTemperature.TabIndex = 0;
            this.lblTemperature.Text = "CPU: N/A";
            // 
            // groupNetwork
            // 
            this.groupNetwork.Controls.Add(this.lblUploadSpeed);
            this.groupNetwork.Controls.Add(this.lblDownloadSpeed);
            this.groupNetwork.Location = new System.Drawing.Point(20, 400);
            this.groupNetwork.Name = "groupNetwork";
            this.groupNetwork.Size = new System.Drawing.Size(350, 100);
            this.groupNetwork.TabIndex = 3;
            this.groupNetwork.TabStop = false;
            this.groupNetwork.Text = "网络速度";
            // 
            // lblUploadSpeed
            // 
            this.lblUploadSpeed.AutoSize = true;
            this.lblUploadSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUploadSpeed.Location = new System.Drawing.Point(20, 60);
            this.lblUploadSpeed.Name = "lblUploadSpeed";
            this.lblUploadSpeed.Size = new System.Drawing.Size(109, 20);
            this.lblUploadSpeed.TabIndex = 1;
            this.lblUploadSpeed.Text = "上传: 0 KB/s";
            // 
            // lblDownloadSpeed
            // 
            this.lblDownloadSpeed.AutoSize = true;
            this.lblDownloadSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDownloadSpeed.Location = new System.Drawing.Point(20, 30);
            this.lblDownloadSpeed.Name = "lblDownloadSpeed";
            this.lblDownloadSpeed.Size = new System.Drawing.Size(109, 20);
            this.lblDownloadSpeed.TabIndex = 0;
            this.lblDownloadSpeed.Text = "下载: 0 KB/s";
            // 
            // btnOptimize
            // 
            this.btnOptimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnOptimize.Location = new System.Drawing.Point(40, 520);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(150, 45);
            this.btnOptimize.TabIndex = 4;
            this.btnOptimize.Text = "一键优化";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.BtnOptimize_Click);
            // 
            // btnProcessManager
            // 
            this.btnProcessManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnProcessManager.Location = new System.Drawing.Point(200, 520);
            this.btnProcessManager.Name = "btnProcessManager";
            this.btnProcessManager.Size = new System.Drawing.Size(150, 45);
            this.btnProcessManager.TabIndex = 5;
            this.btnProcessManager.Text = "进程管理";
            this.btnProcessManager.UseVisualStyleBackColor = true;
            this.btnProcessManager.Click += new System.EventHandler(this.BtnProcessManager_Click);
            // 
            // DetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 584);
            this.Controls.Add(this.btnProcessManager);
            this.Controls.Add(this.btnOptimize);
            this.Controls.Add(this.groupNetwork);
            this.Controls.Add(this.groupTemp);
            this.Controls.Add(this.groupMemory);
            this.Controls.Add(this.groupCpu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统监控详情";
            this.groupCpu.ResumeLayout(false);
            this.groupCpu.PerformLayout();
            this.groupMemory.ResumeLayout(false);
            this.groupMemory.PerformLayout();
            this.groupTemp.ResumeLayout(false);
            this.groupTemp.PerformLayout();
            this.groupNetwork.ResumeLayout(false);
            this.groupNetwork.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupCpu;
        private System.Windows.Forms.ProgressBar progressCpu;
        private System.Windows.Forms.Label lblCpuUsage;
        private System.Windows.Forms.GroupBox groupMemory;
        private System.Windows.Forms.ProgressBar progressMemory;
        private System.Windows.Forms.Label lblMemoryUsage;
        private System.Windows.Forms.GroupBox groupTemp;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.GroupBox groupNetwork;
        private System.Windows.Forms.Label lblUploadSpeed;
        private System.Windows.Forms.Label lblDownloadSpeed;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.Button btnProcessManager;
    }
}
