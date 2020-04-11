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

        public const int PROCESS_VM_READ = 0x10;

        protected ProcessHelper processHelper;

        public Read(
            ProcessHelper _processHelper
        ) {
            this.processHelper = _processHelper;
        }

        /// <summary>
        /// Read 4 bytes of memory by address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public byte[] readMemory(Int64 address)
        {
            string processName = ProcessHelper.PROCESS_NAME;
            if (
                this.processHelper.getProcessState().Instance == null || 
                this.processHelper.getProcessState().Instance.ProcessName != processName
            ) {
                return null;
            }

            int processId = this.processHelper.getProcessState().Instance.Id;
            IntPtr handle = OpenProcess(PROCESS_VM_READ, IntPtr.Zero, new IntPtr(processId)); // note: use the id
            byte[] value = new byte[4];
            var res = ReadProcessMemory(handle, new IntPtr(address), value, sizeof(int), IntPtr.Zero); // 100209928 // 0x209928 // new IntPtr(0x100209930) // new IntPtr(0x100209928)
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
        public double readFloat(Int64 address)
        {
            byte[] bytes = this.readMemory(address);
            if (bytes == null) {
                return Double.NaN;
            }
            return BitConverter.ToSingle(bytes, 0);
        }

    }
}

