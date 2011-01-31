using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TomShane.Neoforce.Controls;

namespace Maplestory_SDK.Root_Class
{
    internal class Inventory
    {
        // game
        Game game;
        // manager
        Manager manager;
        // maximum slot available
        public int slot = 36;
        // texture of slot
        Texture2D textureSlot1;
        Texture2D textureSlot2;
        Texture2D textureSlot3;
        // contain list item of character
        public List<Item[]> listItem = new List<Item[]>();

        public List<ItemBox[]> listItembox = new List<ItemBox[]>();

        // interface declare
        Window win;
        TabControl tab;
        // item detail layout
        ItemDetailsBox gp;
        Label lbdetail;

        KeyboardState keyState;
        KeyboardState oldState;

        /// <summary>
        /// Constructor
        /// </summary>
        public Inventory(Game _game, Manager _manager)
        {
            game = _game;
            textureSlot1 = game.Content.Load<Texture2D>("Temp\\slot_0");
            textureSlot2 = game.Content.Load<Texture2D>("Temp\\slot_1");
            textureSlot3 = game.Content.Load<Texture2D>("Temp\\slot_2");
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
        public bool AddItem(Item item, int number)
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
                    listItembox[item.type][i].Image = item.icon;
                    listItembox[item.type][i].NumberItem = number;
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

        // hiển thị item
        public void Draw(SpriteBatch spriteBatch)
        {
            // kiểm tra nếu đang di chuyển
            // thì vẽ item đó theo vị trí của con trỏ
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
        private void CreateInterface()
        {
            // khởi tạo window inventory
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
            // khởi tạo bảng thông tin chi tiết item
            gp = new ItemDetailsBox(manager);
            gp.Init();
            gp.Alpha = 200;
            //gp.IconVisible = false;
            gp.CloseButtonVisible = false;
            gp.Text = "Thông tin";
            gp.Width = 150;
            gp.Height = 260;
            gp.Top = 0;
            gp.Left = 0;
            gp.CanFocus = true;
            gp.Visible = false;

            manager.Add(gp);
            // khởi tạo hiển thị chi tiết item
            lbdetail = new Label(manager);
            lbdetail.Init();
            lbdetail.Parent = gp;
            lbdetail.Top = 0;
            lbdetail.Left = 0;
            lbdetail.Width = 100;
            lbdetail.Height = 70;
            lbdetail.Alignment = Alignment.TopLeft;
            lbdetail.Text = "info";
            // khởi tạo tab phân loại item
            tab = new TabControl(manager);
            tab.Init();
            tab.Top = 0;
            tab.Left = 0;
            tab.Width = 211;
            tab.Height = 254 - 32;
            tab.AddPage();
            tab.TabPages[0].Text = "Equips";
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

            // thêm item box vào các tab
            for (int j = 0; j < 4; j++)
            {
                int y = -1;
                int x = 0;
                listItembox.Add(new ItemBox[slot]);

                for (int i = 0; i < slot; i++)
                {
                    if (x % 6 == 0)
                    {
                        y++;
                        x = 0;
                    }
                    // khởi tạo item box
                    ItemBox imgslot = new ItemBox(manager);
                    imgslot.Init();
                    imgslot.Left = x * 32 + 6;
                    imgslot.Top = y * 32 + 1;
                    imgslot.Width = 32;
                    imgslot.Height = 32;
                    imgslot.Parent = tab.TabPages[j];
                    imgslot.BackImage = textureSlot1;
                    //imgslot.SizeMode = SizeMode.Centered;
                    imgslot.MouseOver += new MouseEventHandler(imgslot_MouseOver);
                    imgslot.MouseOut += new MouseEventHandler(imgslot_MouseOut);
                    imgslot.Click += new TomShane.Neoforce.Controls.EventHandler(imgslot_Click);
                    imgslot.Tag = i;

                    listItembox[j][i] = imgslot;

                    x++;
                }
            }
            // cái này có lẽ bỏ :))
            CreateItemDetailLayout();
        }

        // các biến dùng cho việc di chuyển item
        bool ismoveitem = false;
        Texture2D tempp;
        Item tempi;
        ItemBox tempitembox;
        int num;
        int type;
        int position;

        // di chuyển 1 item
        private void imgslot_Click(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            // TODO : sửa lại sự kiện khi di chuyển 1 item đến vị trí 1 item khác, thay thế vị trí và item :)
            // kiểm tra nếu ko phải đang di chuyển item
            if (!ismoveitem)
            { // nếu item được chọn khác null
                if (listItem[tab.SelectedIndex][(int)((ItemBox)sender).Tag] != null)
                {
                    // gán giá trị đang di chuyển là true
                    ismoveitem = true;
                    // gán thuộc tính item vào các biến temp
                    tempitembox = listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag];
                    num = listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].NumberItem;
                    tempp = listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].Image;
                    tempi = listItem[tab.SelectedIndex][(int)((ItemBox)sender).Tag];
                    type = tab.SelectedIndex;
                    position = (int)((ItemBox)sender).Tag;
                    // xoá item ở vị trí đang chọn
                    listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].NumberItem = 0;
                    listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].Image = null;
                    listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].BackImage = textureSlot2;
                    listItem[tab.SelectedIndex][(int)((ItemBox)sender).Tag] = null;
                }
            }
            else
            {
                // nếu tab của item đang di chuyển trùng với tab hiện tại
                if (type == tab.SelectedIndex)
                {
                    // gán giá trị đang di chuyển là false
                    ismoveitem = false;
                    // nếu vị trí di chuyển đến đã có 1 item khác
                    if (((ItemBox)sender).Image != null)
                    {
                        // hoán đổi vị trí
                        listItembox[type][position].Image = listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].Image;
                        listItem[type][position] = listItem[tab.SelectedIndex][(int)((ItemBox)sender).Tag];
                        listItembox[type][position].NumberItem = listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].NumberItem;
                    }
                    // thay đổi thuộc tính item đã chọn là item đã được di chuyển
                    tempitembox.BackImage = textureSlot1;
                    listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].Image = tempp;
                    listItem[tab.SelectedIndex][(int)((ItemBox)sender).Tag] = tempi;
                    listItembox[tab.SelectedIndex][(int)((ItemBox)sender).Tag].NumberItem = num;
                    // xoá temp
                    num = 0;
                    tempp = null;
                    tempi = null;
                }
                else // tab của item đang di chuyển khác với tab hiện tại
                {
                    // gán giá trị đang di chuyển là false
                    ismoveitem = false;
                    // trả lại về vị trí cũ
                    tempitembox.BackImage = textureSlot1;
                    listItembox[type][position].Image = tempp;
                    listItem[type][position] = tempi;
                    listItembox[type][position].NumberItem = num;
                    // xoá temp
                    num = 0;
                    tempp = null;
                    tempi = null;
                }
            }
        }

        // khi di chuột ra
        private void imgslot_MouseOut(object sender, MouseEventArgs e)
        {
            //win.Text = "inventory";
            gp.Visible = false;
            if (((ItemBox)sender).BackImage != textureSlot2)
                ((ItemBox)sender).BackImage = textureSlot1;
        }

        // xảy ra khi di chuột đến item
        private void imgslot_MouseOver(object sender, MouseEventArgs e)
        {
            int index = (int)((ItemBox)sender).Tag;
            ((ItemBox)sender).BackImage = textureSlot3;
            // kiểm tra xem item không phải null
            if (listItem[tab.SelectedIndex][index] != null)
            {
                // hiển thị chi tiết item
                gp.TextColor = listItem[tab.SelectedIndex][index].color;
                gp.Icon = listItem[tab.SelectedIndex][index].drop;
                gp.Visible = true;
                gp.Text = listItem[tab.SelectedIndex][index].name;
                gp.Left = (win.Left + 229 > game.GraphicsDevice.PresentationParameters.BackBufferWidth - 150) ? win.Left - 155 : win.Left + 229;
                gp.Top = win.Top;
                // đặt lên layer trên cùng
                gp.BringToFront();
                // gán giá trị thông tin cho laber
                lbdetail.Text = listItem[tab.SelectedIndex][index].info;
            }
        }

        private void CreateItemDetailLayout()
        {
        }
    }
}