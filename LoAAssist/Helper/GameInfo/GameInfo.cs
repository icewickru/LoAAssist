using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoAAssist.Helper.GameInfo
{
    public class GameInfo
    {
        protected Dictionary<string, string> variables = new Dictionary<string, string>();

        /// <summary>
        /// Get all variables
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> getVariables()
        {
            return this.variables;
        }

        /// <summary>
        /// Get variable value by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getVariable(string id)
        {
            try {
                return this.variables[id];
            } catch {
                return "";
            }
        }

        /// <summary>
        /// Set value to variable with defined id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void setVariable(string id, string value)
        {
            this.variables[id] = value;
        }
    }
}
