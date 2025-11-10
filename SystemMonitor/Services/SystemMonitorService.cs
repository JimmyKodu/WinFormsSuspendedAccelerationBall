using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace SystemMonitor.Services
{
    /// <summary>
    /// Service for monitoring system performance metrics
    /// </summary>
    public class SystemMonitorService
    {
        private PerformanceCounter? cpuCounter;
        private PerformanceCounter? ramCounter;
        private PerformanceCounter? networkSentCounter;
        private PerformanceCounter? networkReceivedCounter;

        public SystemMonitorService()
        {
            InitializeCounters();
        }

        private void InitializeCounters()
        {
            try
            {
                // CPU counter
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                
                // RAM counter
                ramCounter = new PerformanceCounter("Memory", "Available MBytes");
                
                // Network counters - using the first available network interface
                var category = new PerformanceCounterCategory("Network Interface");
                var instances = category.GetInstanceNames();
                if (instances.Length > 0)
                {
                    var firstInstance = instances[0];
                    networkSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", firstInstance);
                    networkReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", firstInstance);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing counters: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the current CPU usage percentage
        /// </summary>
        public float GetCpuUsage()
        {
            try
            {
                return cpuCounter?.NextValue() ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the available memory in MB
        /// </summary>
        public float GetAvailableMemoryMB()
        {
            try
            {
                return ramCounter?.NextValue() ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the total physical memory in MB
        /// </summary>
        public long GetTotalMemoryMB()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    using var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return Convert.ToInt64(obj["TotalPhysicalMemory"]) / (1024 * 1024);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting total memory: {ex.Message}");
            }
            return 0;
        }

        /// <summary>
        /// Gets the memory usage percentage
        /// </summary>
        public float GetMemoryUsagePercent()
        {
            try
            {
                var total = GetTotalMemoryMB();
                var available = GetAvailableMemoryMB();
                if (total > 0)
                {
                    return ((total - available) / total) * 100;
                }
            }
            catch
            {
                // Fall through
            }
            return 0;
        }

        /// <summary>
        /// Gets the network download speed in bytes/sec
        /// </summary>
        public float GetNetworkDownloadSpeed()
        {
            try
            {
                return networkReceivedCounter?.NextValue() ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the network upload speed in bytes/sec
        /// </summary>
        public float GetNetworkUploadSpeed()
        {
            try
            {
                return networkSentCounter?.NextValue() ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the CPU temperature (if available)
        /// Note: Temperature reading requires specific hardware support and may not work on all systems
        /// </summary>
        public float GetCpuTemperature()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    using var searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        var temp = Convert.ToDouble(obj["CurrentTemperature"]);
                        // Convert from tenths of Kelvin to Celsius
                        return (float)((temp - 2732) / 10.0);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Temperature reading not supported: {ex.Message}");
            }
            return 0;
        }

        /// <summary>
        /// Disposes performance counters
        /// </summary>
        public void Dispose()
        {
            cpuCounter?.Dispose();
            ramCounter?.Dispose();
            networkSentCounter?.Dispose();
            networkReceivedCounter?.Dispose();
        }
    }
}
