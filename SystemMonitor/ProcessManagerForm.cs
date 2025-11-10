using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SystemMonitor.Services;

namespace SystemMonitor
{
    public partial class ProcessManagerForm : Form
    {
        private ProcessMonitorService? processMonitor;
        private System.Windows.Forms.Timer updateTimer;
        
        public ProcessManagerForm(ProcessMonitorService? procMonitor)
        {
            InitializeComponent();
            
            processMonitor = procMonitor;
            
            // Setup category filter
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
            
            // Set up timer
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 2000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            
            UpdateProcessInfo();
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
        
        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            UpdateProcessInfo();
        }
        
        private void UpdateProcessInfo()
        {
            if (processMonitor == null) return;
            
            try
            {
                var currentProcesses = processMonitor.GetAllProcesses();
                
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating process info: {ex.Message}");
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
            updateTimer?.Stop();
            base.OnFormClosing(e);
        }
    }
}
