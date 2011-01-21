using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maplestory_SDK.Root_Class
{
    internal class Item
    {
        // item display on inventory
        public Texture2D icon;
        // item display on ground
        public Texture2D drop;
        // list of option
        public List<ItemOps> option = new List<ItemOps>();
    }
}