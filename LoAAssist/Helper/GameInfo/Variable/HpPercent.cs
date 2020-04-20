using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ReadMemoryHelper = LoAAssist.Helper.Memory.Read;
using ProcessHelper = LoAAssist.Helper.Process;

namespace LoAAssist.Helper.GameInfo.Variable
{
    public class HpPercent : IVariableInfo
    {
        public const Int64 BASE_ADDRESS = 0x0185A750;

        protected ReadMemoryHelper readMemoryHelper;

        public HpPercent(
            ReadMemoryHelper _readMemoryHelper
        ) {
            this.readMemoryHelper = _readMemoryHelper;
        }

        public string getValue()
        {
            Int64 unityPlayer_dll = this.readMemoryHelper.getModuleAddress("UnityPlayer.dll");
            byte[] bytes = this.readMemoryHelper.readPointerChain(
                unityPlayer_dll + BASE_ADDRESS, 
                new Int64[] { 0xF20, 0xC10, 0x368, 0x270, 0x100 }, 
            8);
            return BitConverter.ToSingle(bytes, 0).ToString();
        }

        public string getId()
        {
            return "hp_percent";
        }
    }
}
