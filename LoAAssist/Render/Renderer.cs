using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessHelper = LoAAssist.Helper.Process;
using LoAAssist.Helper.GameInfo;

namespace LoAAssist.Render
{
    public class Renderer
    {
        protected Main form;
        protected ProcessHelper processHelper;
        protected GameInfo gameInfo;

        public Renderer(
            Main _form,
            ProcessHelper _processHelper,
            GameInfo _gameInfo
        ) {
            this.form = _form;
            this.processHelper = _processHelper;
            this.gameInfo = _gameInfo;
        }

        /// <summary>
        /// Execute rendering of data
        /// </summary>
        public void execute()
        {
            this.form.getTextBox().Text = "";
            this.form.getTextBox().Text += "Is runned: " + this.processHelper.getProcessState().IsRun.ToString() + "\r\n";
            this.form.getTextBox().Text += "Is active: " + this.processHelper.getProcessState().IsActive.ToString() + "\r\n";
            foreach (KeyValuePair<string, string> variable in gameInfo.getVariables())
            {
                this.form.getTextBox().Text += variable.Key + ": " + variable.Value + "\r\n";
            }
        }
    }
}
