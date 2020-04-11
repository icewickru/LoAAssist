using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ReadMemoryHelper = LoAAssist.Helper.Memory.Read;

namespace LoAAssist.Helper.GameInfo.Variable
{
    class CoordinateY : IVariableInfo
    {
        //public const Int64 ADDRESS = 0x100209930;
        public const Int64 ADDRESS = 0x100209940;

        protected ReadMemoryHelper readMemoryHelper;

        public CoordinateY(
            ReadMemoryHelper _readMemoryHelper
        )
        {
            this.readMemoryHelper = _readMemoryHelper;
        }

        public string getValue()
        {
            return Math.Round(this.readMemoryHelper.readFloat(ADDRESS), 2).ToString();
        }

        public string getId()
        {
            return "coordinate_y";
        }
    }
}
