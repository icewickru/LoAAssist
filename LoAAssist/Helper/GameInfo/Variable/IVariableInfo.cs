using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoAAssist.Helper.GameInfo.Variable
{
    public interface IVariableInfo
    {
        /// <summary>
        /// Get variable ID
        /// </summary>
        /// <returns></returns>
        string getId();

        /// <summary>
        /// Get variable value
        /// </summary>
        /// <returns></returns>
        string getValue();
    }
}
