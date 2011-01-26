﻿using System;
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
        // name
        public string name;
        // info
        public string info;
        // item display on inventory
        public Texture2D icon;
        // item display on ground
        public Texture2D drop;
        // link texture when equip
        public string link;
        // blend color
        public Color color;
        // item type
        // 0 : equipment
        // 1 : use
        // 2 : keys
        // 3 : other
        public int type;
        // list of option
        public List<ItemOps> option = new List<ItemOps>();

        public Item(Texture2D _icon, Texture2D _drop, string _link, int _type)
        {
            icon = _icon;
            drop = _drop;
            link = _link;
            type = _type;

            name = "No Name";
            info = "No Info";
            color = Color.White;
        }

        public Item(Texture2D _icon, Texture2D _drop, string _link, int _type,List<ItemOps> _option)
        {
            icon = _icon;
            drop = _drop;
            link = _link;
            type = _type;
            option = _option;

            name = "No Name";
            info = "No Info";
            color = Color.White;
        }
    }
}