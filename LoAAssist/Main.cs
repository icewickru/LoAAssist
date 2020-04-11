using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessHelper = LoAAssist.Helper.Process;
using ProcessState = LoAAssist.Helper.ProcessState;
using ReadMemoryHelper = LoAAssist.Helper.Memory.Read;
using LoAAssist.Render;
using LoAAssist.Helper.GameInfo;

namespace LoAAssist
{
    public partial class Main : Form
    {
        protected App app;

        public Main() {
            // Instantiating of classes
            ProcessState processState = new ProcessState();
            ProcessHelper processHelper = new ProcessHelper(processState);
            ReadMemoryHelper readMemoryHelper = new ReadMemoryHelper(
                processHelper
            );
            GameInfo gameInfo = new GameInfo();
            GameInfoObserver gameInfoObserver = new GameInfoObserver(
                readMemoryHelper, 
                gameInfo
            );
            Renderer renderer = new Renderer(
                this,
                processHelper,
                gameInfo
            );

            this.app = new App(
                this,
                processHelper,
                readMemoryHelper,
                renderer,
                gameInfoObserver
            );

            // Runs app
            this.app.run();
            InitializeComponent();
        }

        public TextBox getTextBox()
        {
            return this.textBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.app.appTick(sender, e);
        }
    }
}
