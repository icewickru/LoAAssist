using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoAAssist.Helper.GameInfo.Variable;
using ReadMemoryHelper = LoAAssist.Helper.Memory.Read;

namespace LoAAssist.Helper.GameInfo
{
    public class GameInfoObserver
    {
        protected GameInfo gameInfo;
        protected ReadMemoryHelper readMemoryHelper;

        public GameInfoObserver(
            ReadMemoryHelper _readMemoryHelper,
            GameInfo _gameInfo
        ) {
            this.readMemoryHelper = _readMemoryHelper;
            this.gameInfo = _gameInfo;
        }

        public List<IVariableInfo> getVariablesInfo()
        {
            return new List<IVariableInfo> {
                new CoordinateX(this.readMemoryHelper),
                new CoordinateY(this.readMemoryHelper)
                // <HINT> Add here any classes that implements IVariableInfo interface
                // to search and save values of variables every app tick
            };
        }

        public void execute()
        { 
            foreach(IVariableInfo variableInfo in this.getVariablesInfo())
            {
                gameInfo.setVariable(variableInfo.getId(), variableInfo.getValue());
            }
        }
    }
}
