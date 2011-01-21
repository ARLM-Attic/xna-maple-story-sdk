using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TomShane.Neoforce.Controls;

namespace Maplestory_SDK.Root_Class
{
    class Inventory 
    {
        // game
        Game game;
        // manager
        Manager manager;
        // provide maximum slot available
        public int slot = 36;
        // texture of slot
        Texture2D textureSlot;
        // contain list item of character
        public Item[] listItem;

        public ImageBox[] listImagebox;

        // interface declare
        Window win;

        KeyboardState keyState;
        KeyboardState oldState;

        /// <summary>
        /// Constructor
        /// </summary>
        public Inventory(Game _game,Manager _manager)
        {
            game = _game;
            textureSlot = game.Content.Load<Texture2D>("Temp\\Slot");

            listItem = new Item[slot];
            listImagebox = new ImageBox[slot];

            manager = _manager;
            CreateInterface();
        }

        public void SHOW()
        {

        }

        public void HIDE()
        {

        }

        /// <summary>
        /// Add item into inventory
        /// </summary>
        public void AddItem()
        {

        }

        public void Update()
        {
            keyState = Keyboard.GetState();
            // update input
            if (KeypressTest(Keys.I))
            {
                if (win.Visible)
                    win.Visible = false;
                else
                    win.Visible = true;
            }
            oldState = keyState;
        }

        /// <summary>
        /// Use for get a single key press
        /// </summary>
        /// <param name="theKey"></param>
        /// <returns>true or false</returns>
        private bool KeypressTest(Keys theKey)
        {
            if (keyState.IsKeyUp(theKey) && oldState.IsKeyDown(theKey))
                return true;

            return false;
        }

        public void Draw()
        {

        }

        /// <summary>
        /// khởi tạo giao diện
        /// </summary>
        void CreateInterface()
        {

            win = new Window(manager);
            win.Init();
            win.Center();
            win.Height = 228;
            win.Width = 207;
            win.Text = "Inventory";
            win.AutoScroll = false;
            win.Resizable = false;
            win.IconVisible = false;
            win.ResizeEnd += new TomShane.Neoforce.Controls.EventHandler(win_ResizeEnd);
            win.Visible = false;

            manager.Add(win);

            int y = -1;
            int x = 0;

            for (int i = 0; i < slot; i++)
            {
                if (x % 6 == 0)
                {
                    y++;
                    x = 0;
                }
                    
                ImageBox imgslot = new ImageBox(manager);
                imgslot.Init();
                imgslot.Left = x * 32;
                imgslot.Top = y * 32;
                imgslot.Parent = win;
                imgslot.Image = textureSlot;

                listImagebox[x] = imgslot;

                x++;
            }
        }

        void win_ResizeEnd(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            win.Text = win.Width.ToString() + " : " + win.Height.ToString();
        }

    }
}
