using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessHelper = LoAAssist.Helper.Process;
using ReadMemoryHelper = LoAAssist.Helper.Memory.Read;
using LoAAssist.Render;
using LoAAssist.Helper.GameInfo;

namespace LoAAssist
{
    public class App
    {
        protected ProcessHelper processHelper;
        protected ReadMemoryHelper readMemoryHelper;
        protected Main form;
        protected Renderer renderer;
        protected GameInfoObserver gameInfoObserver;

        public App(
            Main _form,
            ProcessHelper _processHelper,
            ReadMemoryHelper _readMemoryHelper,
            Renderer _renderer,
            GameInfoObserver _gameInfoObserver
        ) {
            this.form = _form;
            this.processHelper = _processHelper;
            this.readMemoryHelper = _readMemoryHelper;
            this.renderer = _renderer;
            this.gameInfoObserver = _gameInfoObserver;
        }

        /// <summary>
        /// Runs the application
        /// </summary>
        public void run()
        {
            var timer = this.startObserving();
        }

        /// <summary>
        /// Starts observing game process
        /// </summary>
        /// <returns></returns>
        public System.Windows.Forms.Timer startObserving()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = ProcessHelper.OBSERVING_INTERVAL;
            timer.Tick += new EventHandler(appTick);
            timer.Start();
            return timer;
        }

        /// <summary>
        /// Observing tick handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void appTick(object sender, EventArgs e)
        {
            // Tracking game process state
            this.processHelper.observeProcess();
            // Tracking values of variables in memory
            this.gameInfoObserver.execute();
            // Execute data rendering
            this.renderer.execute();
        }
    }
}
