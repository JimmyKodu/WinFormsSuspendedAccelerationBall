using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SystemMonitor.Services;

namespace SystemMonitor
{
    public partial class MainForm : Form
    {
        private SystemMonitorService? systemMonitor;
        private ProcessMonitorService? processMonitor;
        private OptimizationService? optimizationService;
        private List<ProcessInfo> currentProcesses = new List<ProcessInfo>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialize services
                systemMonitor = new SystemMonitorService();
                processMonitor = new ProcessMonitorService();
                optimizationService = new OptimizationService();

                // Setup process category filter
                cmbCategory.Items.Add("全部");
                cmbCategory.Items.Add("系统进程");
                cmbCategory.Items.Add("浏览器");
                cmbCategory.Items.Add("开发工具");
                cmbCategory.Items.Add("媒体应用");
                cmbCategory.Items.Add("办公软件");
                cmbCategory.Items.Add("游戏");
                cmbCategory.Items.Add("其他应用");
                cmbCategory.SelectedIndex = 0;

                // Setup data grid
                SetupProcessGrid();

                // Start monitoring
                updateTimer.Start();
                
                lblStatus.Text = "监控中...";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupProcessGrid()
        {
            dataGridProcesses.Columns.Clear();
            dataGridProcesses.Columns.Add("ProcessId", "进程ID");
            dataGridProcesses.Columns.Add("ProcessName", "进程名称");
            dataGridProcesses.Columns.Add("Category", "分类");
            dataGridProcesses.Columns.Add("MemoryUsageMB", "内存 (MB)");
            dataGridProcesses.Columns.Add("CpuUsagePercent", "CPU (%)");
            dataGridProcesses.Columns.Add("ThreadCount", "线程数");

            dataGridProcesses.Columns["ProcessId"].Width = 80;
            dataGridProcesses.Columns["ProcessName"].Width = 150;
            dataGridProcesses.Columns["Category"].Width = 100;
            dataGridProcesses.Columns["MemoryUsageMB"].Width = 100;
            dataGridProcesses.Columns["CpuUsagePercent"].Width = 80;
            dataGridProcesses.Columns["ThreadCount"].Width = 80;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab == tabSystem)
                {
                    UpdateSystemInfo();
                }
                else if (tabControl.SelectedTab == tabProcess)
                {
                    UpdateProcessInfo();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Update error: {ex.Message}");
            }
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
                Debug.WriteLine($"Error updating system info: {ex.Message}");
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

        private void UpdateProcessInfo()
        {
            if (processMonitor == null) return;

            try
            {
                currentProcesses = processMonitor.GetAllProcesses();
                
                // Filter by category if needed
                var selectedCategory = cmbCategory.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedCategory) && selectedCategory != "全部")
                {
                    currentProcesses = currentProcesses.Where(p => p.Category == selectedCategory).ToList();
                }

                // Update grid
                dataGridProcesses.Rows.Clear();
                foreach (var proc in currentProcesses)
                {
                    dataGridProcesses.Rows.Add(
                        proc.ProcessId,
                        proc.ProcessName,
                        proc.Category,
                        proc.MemoryUsageMB,
                        proc.CpuUsagePercent,
                        proc.ThreadCount
                    );
                }

                lblStatus.Text = $"显示 {dataGridProcesses.Rows.Count} 个进程";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating process info: {ex.Message}");
            }
        }

        private void BtnOptimize_Click(object sender, EventArgs e)
        {
            if (optimizationService == null) return;

            try
            {
                btnOptimize.Enabled = false;
                lblStatus.Text = "优化中...";
                Application.DoEvents();

                var result = optimizationService.QuickOptimize();
                
                MessageBox.Show(result.Message, "优化结果", 
                    MessageBoxButtons.OK, 
                    result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                lblStatus.Text = "优化完成";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"优化失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "优化失败";
            }
            finally
            {
                btnOptimize.Enabled = true;
            }
        }

        private void BtnRefreshProcesses_Click(object sender, EventArgs e)
        {
            UpdateProcessInfo();
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProcessInfo();
        }

        private void BtnKillProcess_Click(object sender, EventArgs e)
        {
            if (processMonitor == null || dataGridProcesses.SelectedRows.Count == 0)
                return;

            try
            {
                var selectedRow = dataGridProcesses.SelectedRows[0];
                var processId = Convert.ToInt32(selectedRow.Cells["ProcessId"].Value);
                var processName = selectedRow.Cells["ProcessName"].Value.ToString();

                var result = MessageBox.Show(
                    $"确定要结束进程 '{processName}' (PID: {processId}) 吗？",
                    "确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (processMonitor.KillProcess(processId))
                    {
                        MessageBox.Show("进程已结束", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateProcessInfo();
                    }
                    else
                    {
                        MessageBox.Show("无法结束该进程", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"结束进程失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSetPriority_Click(object sender, EventArgs e)
        {
            if (processMonitor == null || dataGridProcesses.SelectedRows.Count == 0)
                return;

            try
            {
                var selectedRow = dataGridProcesses.SelectedRows[0];
                var processId = Convert.ToInt32(selectedRow.Cells["ProcessId"].Value);
                var processName = selectedRow.Cells["ProcessName"].Value.ToString();

                // Show priority selection dialog
                using var priorityForm = new Form
                {
                    Text = "选择优先级",
                    Width = 300,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterParent,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var lblInfo = new Label
                {
                    Text = $"进程: {processName}",
                    Left = 20,
                    Top = 20,
                    Width = 250
                };

                var cmbPriority = new ComboBox
                {
                    Left = 20,
                    Top = 50,
                    Width = 240,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbPriority.Items.AddRange(new object[] 
                {
                    "实时",
                    "高",
                    "高于正常",
                    "正常",
                    "低于正常",
                    "低"
                });
                cmbPriority.SelectedIndex = 3; // Normal

                var btnOk = new Button
                {
                    Text = "确定",
                    Left = 100,
                    Top = 100,
                    DialogResult = DialogResult.OK
                };

                priorityForm.Controls.Add(lblInfo);
                priorityForm.Controls.Add(cmbPriority);
                priorityForm.Controls.Add(btnOk);
                priorityForm.AcceptButton = btnOk;

                if (priorityForm.ShowDialog() == DialogResult.OK)
                {
                    ProcessPriorityClass priority = cmbPriority.SelectedIndex switch
                    {
                        0 => ProcessPriorityClass.RealTime,
                        1 => ProcessPriorityClass.High,
                        2 => ProcessPriorityClass.AboveNormal,
                        3 => ProcessPriorityClass.Normal,
                        4 => ProcessPriorityClass.BelowNormal,
                        5 => ProcessPriorityClass.Idle,
                        _ => ProcessPriorityClass.Normal
                    };

                    if (processMonitor.SetProcessPriority(processId, priority))
                    {
                        MessageBox.Show("优先级已更改", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("无法更改该进程的优先级", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更改优先级失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            updateTimer.Stop();
            systemMonitor?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
