using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysDiag = System.Diagnostics;

namespace LoAAssist.Helper
{
    public class ProcessState
    {
        protected SysDiag.Process instance;
        protected bool processIsActive = false;

        /// <summary>
        /// Process instance
        /// </summary>
        public SysDiag.Process Instance
        {
            get { return instance; }
            set {
                if (value == null) {
                    processIsActive = false;
                }
                instance = value; 
            }
        }

        /// <summary>
        /// Is process active
        /// </summary>
        public bool IsActive
        {
            get { return processIsActive; }
            set { processIsActive = value; }
        }

        /// <summary>
        /// Is process runned
        /// </summary>
        public bool IsRun
        {
            get { return instance != null; }
        }
    }
}
