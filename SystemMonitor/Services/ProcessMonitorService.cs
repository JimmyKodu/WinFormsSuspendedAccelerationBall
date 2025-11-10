using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

namespace SystemMonitor.Services
{
    /// <summary>
    /// Represents information about a running process
    /// </summary>
    public class ProcessInfo
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public long MemoryUsageMB { get; set; }
        public double CpuUsagePercent { get; set; }
        public int ThreadCount { get; set; }
        public DateTime StartTime { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }

    /// <summary>
    /// Service for monitoring and managing processes
    /// </summary>
    public class ProcessMonitorService
    {
        private Dictionary<int, DateTime> lastCpuTimes = new Dictionary<int, DateTime>();
        private Dictionary<int, TimeSpan> lastProcessorTimes = new Dictionary<int, TimeSpan>();

        /// <summary>
        /// Gets all running processes with detailed information
        /// </summary>
        public List<ProcessInfo> GetAllProcesses()
        {
            var processes = new List<ProcessInfo>();
            
            try
            {
                foreach (var process in Process.GetProcesses())
                {
                    try
                    {
                        var processInfo = new ProcessInfo
                        {
                            ProcessId = process.Id,
                            ProcessName = process.ProcessName,
                            Category = CategorizeProcess(process.ProcessName),
                            MemoryUsageMB = process.WorkingSet64 / (1024 * 1024),
                            ThreadCount = process.Threads.Count,
                            CpuUsagePercent = CalculateCpuUsage(process)
                        };

                        try
                        {
                            processInfo.StartTime = process.StartTime;
                            processInfo.FilePath = process.MainModule?.FileName ?? string.Empty;
                        }
                        catch
                        {
                            // Some processes may not allow access to these properties
                            processInfo.StartTime = DateTime.MinValue;
                            processInfo.FilePath = string.Empty;
                        }

                        processes.Add(processInfo);
                    }
                    catch
                    {
                        // Skip processes we can't access
                    }
                    finally
                    {
                        process.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting processes: {ex.Message}");
            }

            return processes.OrderByDescending(p => p.MemoryUsageMB).ToList();
        }

        /// <summary>
        /// Categorizes a process based on its name and characteristics
        /// </summary>
        private string CategorizeProcess(string processName)
        {
            var name = processName.ToLower();

            // System processes
            if (name.Contains("system") || name.Contains("service") || name.Contains("svchost") ||
                name.Contains("dwm") || name.Contains("csrss") || name.Contains("lsass") ||
                name.Contains("smss") || name.Contains("winlogon") || name.Contains("wininit"))
            {
                return "系统进程"; // System Process
            }

            // Browsers
            if (name.Contains("chrome") || name.Contains("firefox") || name.Contains("edge") ||
                name.Contains("browser") || name.Contains("opera") || name.Contains("safari"))
            {
                return "浏览器"; // Browser
            }

            // Development tools
            if (name.Contains("devenv") || name.Contains("code") || name.Contains("studio") ||
                name.Contains("git") || name.Contains("visual"))
            {
                return "开发工具"; // Development Tools
            }

            // Media applications
            if (name.Contains("player") || name.Contains("music") || name.Contains("video") ||
                name.Contains("spotify") || name.Contains("vlc"))
            {
                return "媒体应用"; // Media Applications
            }

            // Office applications
            if (name.Contains("word") || name.Contains("excel") || name.Contains("powerpoint") ||
                name.Contains("outlook") || name.Contains("onenote") || name.Contains("office"))
            {
                return "办公软件"; // Office Applications
            }

            // Games
            if (name.Contains("game") || name.Contains("steam") || name.Contains("epic"))
            {
                return "游戏"; // Games
            }

            return "其他应用"; // Other Applications
        }

        /// <summary>
        /// Calculates CPU usage for a specific process
        /// </summary>
        private double CalculateCpuUsage(Process process)
        {
            try
            {
                var currentTime = DateTime.Now;
                var currentProcessorTime = process.TotalProcessorTime;

                if (lastCpuTimes.ContainsKey(process.Id) && lastProcessorTimes.ContainsKey(process.Id))
                {
                    var timeDiff = (currentTime - lastCpuTimes[process.Id]).TotalMilliseconds;
                    var processorTimeDiff = (currentProcessorTime - lastProcessorTimes[process.Id]).TotalMilliseconds;

                    if (timeDiff > 0)
                    {
                        var cpuUsage = (processorTimeDiff / (timeDiff * Environment.ProcessorCount)) * 100;
                        lastCpuTimes[process.Id] = currentTime;
                        lastProcessorTimes[process.Id] = currentProcessorTime;
                        return Math.Round(cpuUsage, 2);
                    }
                }

                lastCpuTimes[process.Id] = currentTime;
                lastProcessorTimes[process.Id] = currentProcessorTime;
            }
            catch
            {
                // CPU usage calculation failed
            }

            return 0;
        }

        /// <summary>
        /// Terminates a process by its ID
        /// </summary>
        public bool KillProcess(int processId)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                process.Kill();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error killing process: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Sets the priority of a process
        /// </summary>
        public bool SetProcessPriority(int processId, ProcessPriorityClass priority)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                process.PriorityClass = priority;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error setting process priority: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets processes grouped by category
        /// </summary>
        public Dictionary<string, List<ProcessInfo>> GetProcessesByCategory()
        {
            var allProcesses = GetAllProcesses();
            return allProcesses.GroupBy(p => p.Category)
                              .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Gets high resource consuming processes
        /// </summary>
        public List<ProcessInfo> GetHighResourceProcesses(int topCount = 10)
        {
            var allProcesses = GetAllProcesses();
            return allProcesses.OrderByDescending(p => p.MemoryUsageMB + (p.CpuUsagePercent * 10))
                              .Take(topCount)
                              .ToList();
        }
    }
}
