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
        // maximum slot available
        public int slot = 36;
        // texture of slot
        Texture2D textureSlot;
        // contain list item of character
        public List<Item[]> listItem = new List<Item[]>();

        public List<ImageBox[]> listImagebox = new List<ImageBox[]>();

        // interface declare
        Window win;
        TabControl tab;
        // item detail layout
        GroupPanel gp;
        Label lbdetail;


        KeyboardState keyState;
        KeyboardState oldState;

        /// <summary>
        /// Constructor
        /// </summary>
        public Inventory(Game _game,Manager _manager)
        {
            game = _game;
            textureSlot = game.Content.Load<Texture2D>("Temp\\Slot");

            // khởi tạo danh sách item
            for (int i = 0; i < 4; i++)
            {
                listItem.Add(new Item[slot]);
            }


            manager = _manager;
            // tạo giao diện
            CreateInterface();
        }

        /// <summary>
        /// Add item into inventory
        /// </summary>
        public bool AddItem(Item item)
        {
            // biến xác định đã thêm item
            bool isadded = false;
            for (int i = 0; i < slot; i++)
            {
                if (listItem[item.type][i] == null)
                {
                    // đã thêm item
                    isadded = true;
                    // thêm item vào danh sách item
                    listItem[item.type][i] = item;
                    // thay ảnh hiển thị trong inventory bằng ảnh của item
                    listImagebox[item.type][i].Image = item.icon;
                    // thoát vòng lặp
                    break;
                }
            }
            // nếu ko thêm được item
            if (!isadded)
            {
                // trả về thông báo hòm đồ full
            }
            return isadded; // trả về giá trị có add được item hay ko
        }

        public void Update()
        {
            // gán keystate bằng trạng thái của bàn phím
            keyState = Keyboard.GetState();
            // nếu nút I được nhấn
            if (KeypressTest(Keys.I))
            {
                if (win.Visible) // nếu hồm đồ đang hiển thị thì ẩn đi
                    win.Visible = false;
                else // còn không thì hiển thị
                    win.Visible = true;
            }
            oldState = keyState;

            ////////////////////////////////////

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ismoveitem)
            {
                int x = Mouse.GetState().X;
                int y = Mouse.GetState().Y;

                spriteBatch.Draw(tempp, new Vector2(x, y), Color.White);
            }
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

        /// <summary>
        /// khởi tạo giao diện
        /// </summary>
        void CreateInterface()
        {

            win = new Window(manager);
            win.Init();
            win.Center();
            win.Height = 260;
            win.Width = 224;
            win.Text = "Inventory";
            win.AutoScroll = false;
            win.Resizable = false;
            win.IconVisible = false;
            win.Visible = false;
            manager.Add(win);

            gp = new GroupPanel(manager);
            gp.Init();
            gp.Alpha = 210;
            gp.Text = "Thông tin";
            gp.Width = 100;
            gp.Height = 100;
            gp.Top = 0;
            gp.Left = 0;
            gp.Visible = false;

            manager.Add(gp);

            lbdetail = new Label(manager);
            lbdetail.Init();
            lbdetail.Parent = gp;
            lbdetail.Top = 0;
            lbdetail.Left = 0;
            lbdetail.Width = 100;
            lbdetail.Height = 70;
            lbdetail.Alignment = Alignment.TopLeft;
            lbdetail.Text = "info";



            tab = new TabControl(manager);
            tab.Init();
            tab.Top = 0;
            tab.Left = 0;
            tab.Width = 211;
            tab.Height = 254 - 32;
            tab.AddPage();
            tab.TabPages[0].Text = "Equipments";
            tab.TabPages[0].BackColor = Color.White;
            tab.AddPage();
            tab.TabPages[1].Text = "Use";
            tab.TabPages[1].BackColor = Color.White;
            tab.AddPage();
            tab.TabPages[2].Text = "Keys";
            tab.TabPages[2].BackColor = Color.White;
            tab.AddPage();
            tab.TabPages[3].Text = "Other";
            tab.TabPages[3].BackColor = Color.White;

            win.Add(tab);

            for (int j = 0; j < 4; j++)
            {
                int y = -1;
                int x = 0;
                listImagebox.Add(new ImageBox[slot]);

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
                    imgslot.Width = 32;
                    imgslot.Height = 32;
                    imgslot.Parent = tab.TabPages[j];
                    imgslot.Image = textureSlot;
                    //imgslot.SizeMode = SizeMode.Centered;
                    imgslot.MouseOver += new MouseEventHandler(imgslot_MouseOver);
                    imgslot.MouseOut += new MouseEventHandler(imgslot_MouseOut);
                    imgslot.Click += new TomShane.Neoforce.Controls.EventHandler(imgslot_Click);
                    imgslot.Tag = i;

                    listImagebox[j][i] = imgslot;

                    x++;
                }
            }

            CreateItemDetailLayout();
            
        }

        bool ismoveitem = false;
        Texture2D tempp;
        Item tempi;
        // di chuyển 1 item
        void imgslot_Click(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            if (!ismoveitem)
            {
                if (listItem[tab.SelectedIndex][(int)((ImageBox)sender).Tag] != null)
                {
                    ismoveitem = true;
                    tempp = listImagebox[tab.SelectedIndex][(int)((ImageBox)sender).Tag].Image;
                    tempi = listItem[tab.SelectedIndex][(int)((ImageBox)sender).Tag];

                    listImagebox[tab.SelectedIndex][(int)((ImageBox)sender).Tag].Image = textureSlot;
                    listItem[tab.SelectedIndex][(int)((ImageBox)sender).Tag] = null;
                }
            }
            else
            {
                ismoveitem = false;

                listImagebox[tab.SelectedIndex][(int)((ImageBox)sender).Tag].Image = tempp;
                listItem[tab.SelectedIndex][(int)((ImageBox)sender).Tag] = tempi;

                tempp = null;
                tempi = null;
            }
        }

        // khi di chuột ra
        void imgslot_MouseOut(object sender, MouseEventArgs e)
        {
            //win.Text = "inventory";
            gp.Visible = false;
        }

        // xảy ra khi di chuột đến item
        void imgslot_MouseOver(object sender, MouseEventArgs e)
        {
            int index  = (int)((ImageBox)sender).Tag;
            // kiểm tra xem item không phải null
            if (listItem[tab.SelectedIndex][index] != null)
            {
                gp.TextColor = listItem[tab.SelectedIndex][index].color;

                gp.Visible = true;
                gp.Text = listItem[tab.SelectedIndex][index].name;
                gp.Left = Mouse.GetState().X;
                gp.Top = Mouse.GetState().Y;
                gp.BringToFront();
                lbdetail.Text = listItem[tab.SelectedIndex][index].info;
            }
        }

        void CreateItemDetailLayout()
        {

        }

    }
}
