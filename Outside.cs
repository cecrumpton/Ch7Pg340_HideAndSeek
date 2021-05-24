using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch7Pg340_HideAndSeek
{
    class Outside : Location
    {
        private bool hot;

        public override string Description
        {
            get
            {
                string newDescription = base.Description;
                if (this.hot)
                    newDescription += " It's very hot here.";
                return newDescription;
            }
        }

        public Outside(string name, bool hot) : base (name)
        {
            this.hot = hot;
        }
    }
}
