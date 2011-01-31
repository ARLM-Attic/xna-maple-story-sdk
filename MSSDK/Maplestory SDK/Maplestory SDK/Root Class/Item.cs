using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public List<List<object>> stringdetails = new List<List<object>>();

        public Item(Texture2D _icon, Texture2D _drop, string _link, int _type)
        {
            icon = _icon;
            drop = _drop;
            link = _link;
            type = _type;

            name = "No Name";
            info = "No Info";
            color = Color.Black;
        }

        public Item(Texture2D _icon, Texture2D _drop, string _link, int _type, List<ItemOps> _option)
        {
            icon = _icon;
            drop = _drop;
            link = _link;
            type = _type;
            option = _option;

            name = "No Name";
            info = "No Info";
            color = Color.Black;
        }

        public Item(Texture2D _icon, Texture2D _drop, string _link, int _type, List<ItemOps> _option, Color _color)
        {
            icon = _icon;
            drop = _drop;
            link = _link;
            type = _type;
            option = _option;

            name = "No Name";
            info = "No Info";
            color = _color;
        }

        public Item(Texture2D _icon, Texture2D _drop, string _link, int _type, Color _color)
        {
            icon = _icon;
            drop = _drop;
            link = _link;
            type = _type;

            name = "No Name";
            info = "No Info";
            color = _color;
        }

        public void LoadItem(string name)
        {
            // TODO: sử dụng để lấy thông tin 1 item bất kì từ data xml
        }

        public void LoadItem(int id)
        {
            // TODO: sử dụng để lấy thông tin 1 item bất kì từ data xml
        }
    }
}