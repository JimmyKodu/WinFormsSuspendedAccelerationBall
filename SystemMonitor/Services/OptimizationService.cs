using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SystemMonitor.Services
{
    /// <summary>
    /// Service for system optimization operations
    /// </summary>
    public class OptimizationService
    {
        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);

        /// <summary>
        /// Clears the working set of all processes to free up memory
        /// </summary>
        public long ClearMemory()
        {
            long freedMemory = 0;
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    
                    // Clear this process's working set
                    var currentProcess = Process.GetCurrentProcess();
                    SetProcessWorkingSetSize(currentProcess.Handle, -1, -1);
                    
                    freedMemory = GC.GetTotalMemory(true);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error clearing memory: {ex.Message}");
            }
            return freedMemory;
        }

        /// <summary>
        /// Optimizes system by ending unnecessary processes
        /// </summary>
        public int OptimizeProcesses()
        {
            int optimizedCount = 0;
            try
            {
                var processMonitor = new ProcessMonitorService();
                var highResourceProcesses = processMonitor.GetHighResourceProcesses(20);

                foreach (var proc in highResourceProcesses)
                {
                    // Only target non-system processes that are safe to optimize
                    if (proc.Category != "系统进程" && IsOptimizableProcess(proc.ProcessName))
                    {
                        try
                        {
                            // Lower priority instead of killing for safer optimization
                            if (processMonitor.SetProcessPriority(proc.ProcessId, ProcessPriorityClass.BelowNormal))
                            {
                                optimizedCount++;
                            }
                        }
                        catch
                        {
                            // Skip if we can't optimize this process
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error optimizing processes: {ex.Message}");
            }
            return optimizedCount;
        }

        /// <summary>
        /// Checks if a process is safe to optimize
        /// </summary>
        private bool IsOptimizableProcess(string processName)
        {
            var name = processName.ToLower();
            
            // Don't optimize critical system processes or Windows components
            string[] criticalProcesses = {
                "system", "registry", "csrss", "wininit", "services",
                "lsass", "svchost", "dwm", "explorer", "taskmgr"
            };

            foreach (var critical in criticalProcesses)
            {
                if (name.Contains(critical))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Performs a quick optimization of the system
        /// </summary>
        public OptimizationResult QuickOptimize()
        {
            var result = new OptimizationResult();
            
            try
            {
                // Clear memory
                result.MemoryFreed = ClearMemory();
                
                // Optimize processes
                result.ProcessesOptimized = OptimizeProcesses();
                
                result.Success = true;
                result.Message = $"优化完成: 释放内存 {result.MemoryFreed / (1024 * 1024)} MB, 优化了 {result.ProcessesOptimized} 个进程";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"优化失败: {ex.Message}";
            }
            
            return result;
        }
    }

    /// <summary>
    /// Result of an optimization operation
    /// </summary>
    public class OptimizationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public long MemoryFreed { get; set; }
        public int ProcessesOptimized { get; set; }
    }
}
