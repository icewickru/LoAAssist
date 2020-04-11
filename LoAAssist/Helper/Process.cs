using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SysDiag = System.Diagnostics;

namespace LoAAssist.Helper
{
    public class Process
    {
        public const string PROCESS_NAME = "Legends of Aria";
        public const int OBSERVING_INTERVAL = 500;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        protected ProcessState processState;

        public Process(
            ProcessState _processState
        ) {
            this.processState = _processState;
        }

        /// <summary>
        /// Get process state
        /// </summary>
        /// <returns></returns>
        public ProcessState getProcessState()
        {
            return this.processState;
        }

        /// <summary>
        /// Observe game process and saving current state of process
        /// </summary>
        public void observeProcess()
        {
            IntPtr hWindow = FindWindow(null, PROCESS_NAME);
            bool isRun = (hWindow != IntPtr.Zero);
            if (!isRun)
            {
                this.processState.Instance = null;
                this.processState.IsActive = false;
                return;
            }

            // Get process ID by process handle
            int pid;
            GetWindowThreadProcessId(hWindow, out pid);
            SysDiag.Process process = SysDiag.Process.GetProcessById(pid);

            this.processState.Instance = process;
            if (process != null && process.ProcessName == PROCESS_NAME) {
                this.processState.IsActive = (process.ProcessName == this.getActiveProcessName());
            } else {
                this.processState.IsActive = false;
            }
        }

        /// <summary>
        /// Get name of active process
        /// </summary>
        /// <returns>Name of active process</returns>
        public string getActiveProcessName()
        {
            // Get active window handle
            IntPtr activeWindowHandle = GetForegroundWindow();

            // Get active window process ID
            int pid;
            GetWindowThreadProcessId(activeWindowHandle, out pid);

            // Get active process
            SysDiag.Process activeProcess = SysDiag.Process.GetProcessById(pid);
            activeWindowHandle = IntPtr.Zero;
            return activeProcess.ProcessName;
        }
    }
}
