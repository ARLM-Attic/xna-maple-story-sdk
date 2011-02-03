using Maplestory_SDK.Root_Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;

namespace Maplestory_SDK.User_Class
{
    internal class Player
    {
        /// <summary>
        /// Equipment
        /// </summary>
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
        public string titlename = "";
        public int level = 1;
        public int exp = 0;
        // base exp up per level and is the first lvl
        public int expbase = 15;
        // expstep need larger than 1
        public int expstep = 2;
        // expnextlevel = expbase x expstep + (level - 1) x expbase x expstep
        public int expnextlevel = 0;
        public int gold = 1000;
        // Main attribute
        public int str = 5;
        public int agi = 5;
        public int luk = 5;
        public int mag = 5;
        // default
        public int mgatk = 5;
        public int mgdef = 5;
        public int mgatkspeed = 5;
        public int pysatk = 5;
        public int pysdef = 5;
        public int pysatkspeed = 5;
        public int hp = 30;
        public int maxhp = 30;
        public int mp = 15;
        public int maxmp = 15;
        public int chari = 30;
        public int maxchari = 30;

        public float crit = .3f;
        public float droprate = 0f;
        public float exprate = 0f;

        public float av_fire = 0;
        public float av_air = 0;
        public float av_lighting = 0;
        public float av_light = 0;
        public float av_wind = 0;
        public float av_water = 0;
        public float av_earth = 0;
        public float av_shadow = 0;
        //
        public int APoint = 10; // attribute point
        public int SPoint = 0; // skill point

        public PlayerBase player;
        Run main;

        public Inventory inventory;
        Status status;

        /// <summary>
        /// Create Player
        /// </summary>
        /// <param name="Main">main</param>
        /// <param name="skin">skin of player</param>
        /// <param name="face">face of player</param>
        /// <param name="hair">hair of player</param>
        /// <param name="Stat">list of stat</param>
        /// <param name="name">name of player</param>
        public Player(Run Main, Manager _manager, string skin, string face, string hair, string name)
        {
            // set body
            Skin = skin;
            Face = face;
            Hair = hair;
            // create new character
            player = new PlayerBase(Main, skin, face, hair);
            //player.WeaponType = WeaponType;
            main = Main;
            // set character attribute
            this.name = name;
            expnextlevel = expbase * expstep + (level - 1) * expbase * expstep;
            // initialize for player
            inventory = new Inventory(Main, _manager);
            status = new Status(this, _manager);

            // add some test item into inventory
            // load content
            // this detail below must put into a XML file in item folder
            Texture2D temp1 = main.Content.Load<Texture2D>("Items\\Armor\\0001\\icon_info");
            Texture2D temp2 = main.Content.Load<Texture2D>("Items\\Armor\\0001\\iconRaw_info");
            Item testitem = new Item(temp1, temp2, "Items\\Armor\\0001\\", 0);
            testitem.name = "T-Shirt";
            testitem.info = "This is a test\nitem.";
            // add item into inventory
            inventory.AddItem(testitem, 1);
            // not complete yet
            // demo add option to item
            // e.g : Agi + 5
            //         Atk + 10
            // -> testitem.option.Add(new ItemOps("Add 10 Agi","Agi",10));
        }

        public void Initialize()
        {
        }

        /// <summary>
        /// use for update movement and other
        /// </summary>
        public void Update(Map _map, SpriteBatch spriteBatch)
        {
            // movement
            player.KeyInput();
            player.Update(_map, spriteBatch);
            player.Animation();
            inventory.Update();
            status.Update();
            //// equipment
            //player.Weapon = Weapon;
            //player.Shield = Shield;
            //player.Armor = Armor;
            //player.Pant = Pant;
            //player.Shoe = Shoe;
            //player.Hat = Hat;
            //player.Acc = Acc;
            //player.Glove = Glove;
            //player.Cape = Cape;
            // body gadget
            player.Skin = Skin;
            player.Face = Face;
            player.Hair = Hair;
            //// other
            player.WeaponType = WeaponType;
            //// check attack time count
            //player.CheckAttacktimecount();
        }

        /// <summary>
        /// use for update Texture
        /// </summary>
        public void Draw(SpriteBatch spritebatch, GameTime gametime)
        {
            player.Draw(spritebatch);
            inventory.Draw(spritebatch);
            status.Draw(spritebatch);
            //spritebatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), name, new Vector2(player.get_X() - 5, player.get_Y() + 30), Color.Black);
        }
    }
}