using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Xml;
using Maplestory_SDK.Root_Class;

namespace Maplestory_SDK.User_Class
{
    class Player
    {
        /// <summary>
        /// Equipment
        /// </summary>
        int[] Weapon; //  vũ khí
        int[] Shield; // lá chắn
        int[] Pant; // quần
        int[] Shoes; // giày
        int[] Coat; // áo
        int[] Grover; // găng tay
        int[] Hat; // mũ
        int[] Cape; // áo choàng
        /// <summary>
        /// thuộc tính của nhân vật
        ////////////////////////////
        public string name;
        /// <summary>
        /// thuộc tính cơ bản
        /// </summary>
        public int str;
        public int agi;
        public int luk;
        public int itel;
        /// <summary>
        /// thuộc tính nâng cao
        /// </summary>
        public int mgatk;
        public int mgdef;
        public int mgatkspeed;
        public int pysatk;
        public int pysdef;
        public int pysatkspeed;
        /// <summary>
        /// thuộc tính mềm
        /// </summary>
        public int hp;
        public int maxhp;
        public int mp;
        public int maxmp;
        public int chari;
        public int maxchari;
        /// <summary>
        /// thuộc tính bổ xung
        /// </summary>
        public int crit;
        public int droprate;
        public int exprate;
    }
}
