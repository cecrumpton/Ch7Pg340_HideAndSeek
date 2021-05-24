using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch7Pg340_HideAndSeek
{
    class OutsideWithHidingPlace : Outside, IHidingPlace
    {
        public string HidingPlace { get; private set; }

        public override string Description { get { return base.Description + " Someone could hide " + HidingPlace + "."; } }

        public OutsideWithHidingPlace(string name, bool hot, string hidingPlace) : base (name, hot)
        {
            HidingPlace = hidingPlace;
        }
    }
}
