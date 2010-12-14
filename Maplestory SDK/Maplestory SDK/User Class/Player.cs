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
    class Player : Character
    {
        /// <summary>
        /// Đường dẫn file xml chứa thông tin nhân vật
        /// </summary>
        string XmlLocation;
        /// <summary>
        /// Body
        /// </summary>
        int[] Body; // thân
        int[] LHand; // tay trái
        int[] RHand; // tay phải
        int[] Head; // đầu
        int[] Face; // mặt
        int[] Hair; // tóc
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
        /// <summary>
        /// khởi tạo
        /// </summary>
        /// <param name="path">lấy đường dẫn file xml chứa info</param>
        public Player(string path)
        {
            this.XmlLocation = path;
        }

        public Player(string path, int hp, int maxhp, int mp, int maxmp,
                        int chari, int maxchari, int crit, int droprate,
                        int mgatk, int mgdef, int mgatkspeed, int pysatk,
                        int pysdef, int pysatkspeed, int str, int agi, int luk, int itel)
        {
            this.XmlLocation = path;
        }

        /// <summary>
        /// đặt giá trị vị trí cho phần thân
        /// </summary>
        /// <param name="x">vị trí x</param>
        /// <param name="y">vị trí y</param>
        /// <param name="idpic">Id của ảnh hiển thị cho nhân vật</param>
        public void Body_Pos(int x, int y, int idpic)
        {
            Body = new int[] { x, y, idpic };
        }
        /// <summary>
        /// lấy giá trị vị trí cho phần thân
        /// </summary>
        /// <returns></returns>
        public int[] Body_Pos()
        {
            return Body;
        }
    }
}
