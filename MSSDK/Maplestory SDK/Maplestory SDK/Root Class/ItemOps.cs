using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maplestory_SDK.Root_Class
{
    class ItemOps
    {
        //string detail of option
        // e.g : "Add 10 agi"
        public string text;
        // name of attribute
        // e.g : "Agi"
        public string name;
        // attribute add
        // e.g : 10
        public int add;

        public ItemOps(string _text,string _name, int _add)
        {
            text = _text;
            name = _name;
            add = _add;
        }
    }
}
