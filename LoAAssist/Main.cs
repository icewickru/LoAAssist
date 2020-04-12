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
using LoAAssist.DI;

namespace LoAAssist
{
    public partial class Main : Form
    {
        protected App app;

        public Main() {
            // Instantiating of classes
            ObjectManager objectManager = new ObjectManager();
            objectManager.set(typeof(Main), this);
            this.app = (App)objectManager.get(typeof(App));

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = ((CheckBox)sender).Checked;
        }
    }
}
