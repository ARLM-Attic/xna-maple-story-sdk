using Maplestory_SDK.Root_Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maplestory_SDK.User_Class
{
    internal class Player
    {
        /// <summary>
        /// Equipment
        /// </summary>

        public string AttackType = "1"; // any weapon have 3 type of attack, and it will change automatic :D
        // i think it maybe same in maplestory.
        // hope some one tweak code in here :)) lol
        public string WeaponType = "Hand"; // default = hand
        public string Weapon; //  vũ khí
        public string Shield; // lá chắn
        public string Pant; // quần
        public string Shoe; // giày
        public string Armor; // áo
        public string Glove; // găng tay
        public string Hat; // mũ
        public string Cape; // áo choàng
        public string Acc; // áo choàng
        public string Skin; // áo choàng
        public string Face; // áo choàng
        public string Hair; // áo choàng
        /// <summary>
        /// thuộc tính của nhân vật
        ////////////////////////////
        public string name;
        public string titlename;
        int[] stat;
        /// <summary>
        /// thuộc tính cơ bản
        /// </summary>
        public int str;
        public int agi;
        public int luk;
        public int mag;
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

        public Character player;
        Run main;

        /// <summary>
        /// Create Player
        /// </summary>
        /// <param name="Main">main</param>
        /// <param name="skin">skin of player</param>
        /// <param name="face">face of player</param>
        /// <param name="hair">hair of player</param>
        /// <param name="Stat">list of stat</param>
        /// <param name="name">name of player</param>
        public Player(Run Main, string skin, string face, string hair, int[] Stat, string name, string title)
        {
            // set body
            Skin = skin;
            Face = face;
            Hair = hair;
            // create new character
            player = new Character(Main, skin, face, hair);
            player.WeaponType = WeaponType;
            player.AttackType = AttackType;
            main = Main;
            // set character atribute
            titlename = title;
            this.name = name;
            //stat
            if (Stat.Length == 19) stat = Stat;
            AddStat();
        }

        /// <summary>
        /// Add stat
        /// </summary>
        private void AddStat()
        {
            if (stat != null)
            {
                str = stat[0];
                agi = stat[1];
                luk = stat[2];
                mag = stat[3];
                mgatk = stat[4];
                mgdef = stat[5];
                mgatkspeed = stat[6];
                pysatk = stat[7];
                pysdef = stat[8];
                pysatkspeed = stat[9];
                hp = stat[10];
                maxhp = stat[11];
                mp = stat[12];
                maxmp = stat[13];
                chari = stat[14];
                maxchari = stat[15];
                crit = stat[16];
                droprate = stat[17];
                exprate = stat[18];
            }
        }

        /// <summary>
        /// use for update movement and other
        /// </summary>
        public void Update()
        {
            // movement
            player.KeyInput();
            player.Move();
            // equipment
            player.Weapon = Weapon;
            player.Shield = Shield;
            player.Armor = Armor;
            player.Pant = Pant;
            player.Shoe = Shoe;
            player.Hat = Hat;
            player.Acc = Acc;
            player.Glove = Glove;
            player.Cape = Cape;
            // body gadget
            player.Skin = Skin;
            player.Face = Face;
            player.Hair = Hair;
            // other
            player.WeaponType = WeaponType;
            player.AttackType = AttackType;
            // check attack time count
            player.CheckAttacktimecount();
        }

        /// <summary>
        /// use for update Texture
        /// </summary>
        public void Draw(SpriteBatch spritebatch)
        {
            player.Draw(spritebatch);
            spritebatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), name, new Vector2(player.get_X() - 5, player.get_Y() + 30), Color.Black);
        }
    }
}