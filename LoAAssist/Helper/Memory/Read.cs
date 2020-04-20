using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using LoAAssist.Helper;
using ProcessHelper = LoAAssist.Helper.Process;

namespace LoAAssist.Helper.Memory
{
    public class Read
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr pHandle, IntPtr Address, byte[] Buffer, int Size, IntPtr NumberofBytesRead);

        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr OpenProcess(
                    int dwDesiredAccess,
                    IntPtr bInheritHandle,
                    IntPtr dwProcessId
                    );

        protected const int PROCESS_VM_READ = 0x10;

        // Names of observing modules
        protected static string[] OBSERVE_MODULES = { 
            "UnityPlayer.dll"
        };

        // Addresses of observing modules
        protected Dictionary<string, Int64> moduleAddress = new Dictionary<string, Int64> { };

        protected ProcessHelper processHelper;

        public Read(
            ProcessHelper _processHelper
        ) {
            this.processHelper = _processHelper;
        }

        /// <summary>
        /// Get address of module of process
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="dllName"></param>
        /// <returns></returns>
        public Int64 GetModuleAddress(String processName, String dllName)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessesByName(processName).FirstOrDefault();
            foreach (System.Diagnostics.ProcessModule pm in p.Modules)
                if (pm.ModuleName.Equals(dllName))
                    return (Int64)pm.BaseAddress;
            return 0;
        }

        /// <summary>
        /// Read 4 bytes of memory by address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public byte[] readMemory(Int64 address, Int32 sizeToRead)
        {
            string processName = ProcessHelper.PROCESS_NAME;
            if (
                this.processHelper.getProcessState().Instance == null || 
                this.processHelper.getProcessState().Instance.ProcessName != processName
            ) {
                return null;
            }

            int processId = this.processHelper.getProcessState().Instance.Id;
            IntPtr handle = OpenProcess(PROCESS_VM_READ, IntPtr.Zero, new IntPtr(processId));
            byte[] value = new byte[sizeToRead];
            var res = ReadProcessMemory(handle, new IntPtr(address), value, sizeToRead, IntPtr.Zero);
            if (!res) {
                return null;
            }
            
            return value;
        }

        /// <summary>
        /// Read and convert memory bytes to float value
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public float readFloat(Int64 address)
        {
            byte[] bytes = this.readMemory(address, sizeof(float));
            if (bytes == null) {
                return Single.NaN;
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// Read and convert memory bytes to double value
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public double readDouble(Int64 address)
        {
            byte[] bytes = this.readMemory(address, sizeof(double));
            if (bytes == null)
            {
                return Double.NaN;
            }
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// Read and convert memory bytes to Int32 value
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Int32 readInt32(Int64 address)
        {
            byte[] bytes = this.readMemory(address, sizeof(Int32));
            if (bytes == null)
            {
                return 0;
            }
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Read and convert memory bytes to Int64 value
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Int64 readInt64(Int64 address)
        {
            byte[] bytes = this.readMemory(address, sizeof(Int64));
            if (bytes == null)
            {
                return 0;
            }
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// Read chain of pointers and return value as array of bytes
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="offsets"></param>
        /// <param name="valueSize"></param>
        /// <returns></returns>
        public byte[] readPointerChain(Int64 baseAddress, Int64[] offsets, Int32 valueSize = 4)
        {
            if (offsets.Length == 0) {
                return this.readMemory(baseAddress, valueSize);
            }

            Int64 address = baseAddress;
            for (int i = 0; i < offsets.Length; i++) {
                address = this.readInt64(address);
                address += offsets[i];
            }

            return this.readMemory(address, valueSize);
        }

        /// <summary>
        /// Get module address of LoA process
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public Int64 getModuleAddress(string moduleName)
        {
            if (
                this.moduleAddress.ContainsKey(moduleName) &&
                this.moduleAddress[moduleName] != 0
            )
            {
                return this.moduleAddress[moduleName];
            }

            foreach (string module in OBSERVE_MODULES)
            {
                if (
                    !this.moduleAddress.ContainsKey(module) ||
                    this.moduleAddress[module] == 0
                )
                {
                    this.moduleAddress[module] = this.GetModuleAddress(ProcessHelper.PROCESS_NAME, module);
                }
            }

            return this.moduleAddress[moduleName];
        }
    }
}

