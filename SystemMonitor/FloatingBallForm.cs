using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SystemMonitor.Services;

namespace SystemMonitor
{
    public partial class FloatingBallForm : Form
    {
        private SystemMonitorService? systemMonitor;
        private ProcessMonitorService? processMonitor;
        private OptimizationService? optimizationService;
        
        private Point lastPoint;
        private bool isDragging = false;
        
        private const int BallSize = 150;
        private System.Windows.Forms.Timer? updateTimer;
        
        private float cpuUsage = 0;
        private float memoryUsage = 0;
        private float networkSpeed = 0;
        
        public FloatingBallForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }
        
        private void InitializeCustomComponents()
        {
            // Set up the form as a circular, transparent ball
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Lime;
            this.TransparencyKey = Color.Lime;
            this.Size = new Size(BallSize, BallSize);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - BallSize - 20, 20);
            this.TopMost = true;
            this.ShowInTaskbar = false;
            
            // Create circular region
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, BallSize, BallSize);
            this.Region = new Region(path);
            
            // Enable double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                         ControlStyles.AllPaintingInWmPaint | 
                         ControlStyles.UserPaint, true);
            
            // Set up timer
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1000;
            updateTimer.Tick += UpdateTimer_Tick;
            
            // Initialize services
            try
            {
                systemMonitor = new SystemMonitorService();
                processMonitor = new ProcessMonitorService();
                optimizationService = new OptimizationService();
                updateTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            // Set up context menu
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("查看详情", null, (s, e) => ShowDetails());
            contextMenu.Items.Add("一键优化", null, (s, e) => PerformOptimization());
            contextMenu.Items.Add("进程管理", null, (s, e) => ShowProcessManager());
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("退出", null, (s, e) => Application.Exit());
            this.ContextMenuStrip = contextMenu;
        }
        
        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            if (systemMonitor != null)
            {
                cpuUsage = systemMonitor.GetCpuUsage();
                memoryUsage = systemMonitor.GetMemoryUsagePercent();
                
                var downloadSpeed = systemMonitor.GetNetworkDownloadSpeed();
                var uploadSpeed = systemMonitor.GetNetworkUploadSpeed();
                networkSpeed = (downloadSpeed + uploadSpeed) / (1024 * 1024); // Convert to MB/s
                
                this.Invalidate(); // Trigger repaint
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            
            // Draw semi-transparent background gradient
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, BallSize, BallSize);
                
                // Create gradient based on CPU usage (green to red)
                Color color1 = GetColorForUsage(cpuUsage);
                Color color2 = Color.FromArgb(150, color1);
                
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(200, color1);
                    brush.SurroundColors = new Color[] { Color.FromArgb(100, color2) };
                    g.FillEllipse(brush, 0, 0, BallSize, BallSize);
                }
                
                // Draw outer ring
                using (Pen pen = new Pen(Color.FromArgb(180, Color.White), 2))
                {
                    g.DrawEllipse(pen, 2, 2, BallSize - 4, BallSize - 4);
                }
            }
            
            // Draw CPU usage in center
            using (Font font = new Font("Microsoft YaHei", 18, FontStyle.Bold))
            {
                string cpuText = $"{cpuUsage:F0}%";
                SizeF textSize = g.MeasureString(cpuText, font);
                g.DrawString(cpuText, font, Brushes.White, 
                    (BallSize - textSize.Width) / 2, 
                    (BallSize - textSize.Height) / 2 - 15);
            }
            
            // Draw CPU label
            using (Font font = new Font("Microsoft YaHei", 9))
            {
                string label = "CPU";
                SizeF textSize = g.MeasureString(label, font);
                g.DrawString(label, font, Brushes.White, 
                    (BallSize - textSize.Width) / 2, 
                    BallSize / 2 + 5);
            }
            
            // Draw memory usage at bottom
            using (Font font = new Font("Microsoft YaHei", 8))
            {
                string memText = $"内存: {memoryUsage:F0}%";
                SizeF textSize = g.MeasureString(memText, font);
                g.DrawString(memText, font, Brushes.White, 
                    (BallSize - textSize.Width) / 2, 
                    BallSize - 25);
            }
        }
        
        private Color GetColorForUsage(float usage)
        {
            if (usage < 30)
                return Color.FromArgb(76, 175, 80); // Green
            else if (usage < 60)
                return Color.FromArgb(255, 193, 7); // Yellow
            else if (usage < 80)
                return Color.FromArgb(255, 152, 0); // Orange
            else
                return Color.FromArgb(244, 67, 54); // Red
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastPoint = e.Location;
            }
        }
        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isDragging)
            {
                this.Location = new Point(
                    this.Location.X + e.X - lastPoint.X,
                    this.Location.Y + e.Y - lastPoint.Y);
            }
        }
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isDragging = false;
        }
        
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            ShowDetails();
        }
        
        private void ShowDetails()
        {
            var detailsForm = new DetailsForm(systemMonitor, processMonitor, optimizationService);
            detailsForm.Show();
        }
        
        private void PerformOptimization()
        {
            if (optimizationService == null) return;
            
            try
            {
                var result = optimizationService.QuickOptimize();
                MessageBox.Show(result.Message, "优化结果", 
                    MessageBoxButtons.OK, 
                    result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"优化失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ShowProcessManager()
        {
            var processForm = new ProcessManagerForm(processMonitor);
            processForm.Show();
        }
        
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            updateTimer?.Stop();
            systemMonitor?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
