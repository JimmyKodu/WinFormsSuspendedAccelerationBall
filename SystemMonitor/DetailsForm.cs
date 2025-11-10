using System;
using System.Windows.Forms;
using SystemMonitor.Services;

namespace SystemMonitor
{
    public partial class DetailsForm : Form
    {
        private SystemMonitorService? systemMonitor;
        private ProcessMonitorService? processMonitor;
        private OptimizationService? optimizationService;
        private System.Windows.Forms.Timer updateTimer;
        
        public DetailsForm(SystemMonitorService? sysMonitor, ProcessMonitorService? procMonitor, OptimizationService? optService)
        {
            InitializeComponent();
            
            systemMonitor = sysMonitor;
            processMonitor = procMonitor;
            optimizationService = optService;
            
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }
        
        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            UpdateSystemInfo();
        }
        
        private void UpdateSystemInfo()
        {
            if (systemMonitor == null) return;
            
            try
            {
                // Update CPU
                var cpuUsage = systemMonitor.GetCpuUsage();
                lblCpuUsage.Text = $"{cpuUsage:F1}%";
                progressCpu.Value = Math.Min((int)cpuUsage, 100);
                
                // Update Memory
                var memoryPercent = systemMonitor.GetMemoryUsagePercent();
                var totalMemory = systemMonitor.GetTotalMemoryMB();
                var availableMemory = systemMonitor.GetAvailableMemoryMB();
                var usedMemory = totalMemory - availableMemory;
                lblMemoryUsage.Text = $"{memoryPercent:F1}% ({usedMemory:N0} / {totalMemory:N0} MB)";
                progressMemory.Value = Math.Min((int)memoryPercent, 100);
                
                // Update Temperature
                var temp = systemMonitor.GetCpuTemperature();
                if (temp > 0)
                {
                    lblTemperature.Text = $"CPU: {temp:F1}°C";
                }
                else
                {
                    lblTemperature.Text = "CPU: 不可用";
                }
                
                // Update Network
                var downloadSpeed = systemMonitor.GetNetworkDownloadSpeed();
                var uploadSpeed = systemMonitor.GetNetworkUploadSpeed();
                lblDownloadSpeed.Text = $"下载: {FormatSpeed(downloadSpeed)}";
                lblUploadSpeed.Text = $"上传: {FormatSpeed(uploadSpeed)}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating system info: {ex.Message}");
            }
        }
        
        private string FormatSpeed(float bytesPerSecond)
        {
            if (bytesPerSecond < 1024)
                return $"{bytesPerSecond:F0} B/s";
            else if (bytesPerSecond < 1024 * 1024)
                return $"{bytesPerSecond / 1024:F2} KB/s";
            else
                return $"{bytesPerSecond / (1024 * 1024):F2} MB/s";
        }
        
        private void BtnOptimize_Click(object sender, EventArgs e)
        {
            if (optimizationService == null) return;
            
            try
            {
                btnOptimize.Enabled = false;
                Application.DoEvents();
                
                var result = optimizationService.QuickOptimize();
                
                MessageBox.Show(result.Message, "优化结果", 
                    MessageBoxButtons.OK, 
                    result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"优化失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnOptimize.Enabled = true;
            }
        }
        
        private void BtnProcessManager_Click(object sender, EventArgs e)
        {
            var processForm = new ProcessManagerForm(processMonitor);
            processForm.Show();
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            updateTimer?.Stop();
            base.OnFormClosing(e);
        }
    }
}
