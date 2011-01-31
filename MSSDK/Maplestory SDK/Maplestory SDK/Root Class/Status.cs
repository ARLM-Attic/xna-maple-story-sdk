using Maplestory_SDK.User_Class;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TomShane.Neoforce.Controls;

//using Microsoft.Xna.Framework.Input;
//using System.Collections.Generic;

namespace Maplestory_SDK.Root_Class
{
    internal class Status
    {
        // Declare variable
        Player player;
        Manager manager;
        // Interface ---------------
        Window if_win;
        ImageBox if_name;
        ImageBox if_title;
        //* nhóm 1
        GroupBox if_panel1;
        ImageBox if_hptitle;
        ImageBox if_mptitle;
        ImageBox if_aptitle;
        ProgressBar if_hpbar;
        ProgressBar if_mpbar;
        ProgressBar if_apbar;
        ProgressBar if_expbar;
        //* nhóm 2
        GroupBox if_panel2;
        Panel if_subpanel;
        // -- thuộc tính chính
        ImageBox if_atk;
        ImageBox if_agi;
        ImageBox if_mag;
        ImageBox if_luk;
        // -- thuộc tính kèm theo
        ImageBox if_crit;
        ImageBox if_drop;
        // -- thuộc tính phụ
        ImageBox if_atksub;
        ImageBox if_defsub;
        ImageBox if_speedsub;
        // -- thuộc tính kháng yếu tố
        ImageBox if_fire;
        ImageBox if_air;
        ImageBox if_lighting;
        ImageBox if_light;
        ImageBox if_wind;
        ImageBox if_water;
        ImageBox if_earth;
        ImageBox if_shadow;
        // ------------------
        ImageBox if_statuspoint;

        // ------------------
        KeyboardState keyState;
        KeyboardState oldState;

        /// <summary>
        /// Contrucstor
        /// </summary>
        /// <param name="_player">use to get player information</param>
        /// <param name="_manager">use to draw</param>
        public Status(Player _player, Manager _manager)
        {
            player = _player;
            manager = _manager;
            // create user interface
            CreateInterface();
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

        public void Update()
        {
            // gán keystate bằng trạng thái của bàn phím
            keyState = Keyboard.GetState();
            // nếu nút I được nhấn
            if (KeypressTest(Keys.C))
            {
                if (if_win.Visible) // nếu hồm đồ đang hiển thị thì ẩn đi
                    if_win.Visible = false;
                else // còn không thì hiển thị
                    if_win.Visible = true;
            }
            oldState = keyState;
        }

        public void Draw()
        {
        }

        /// <summary>
        /// create use interface
        /// </summary>
        private void CreateInterface()
        {
            // Create base
            if_win = new Window(manager);
            if_win.Init();
            if_win.Text = "Status";
            if_win.IconVisible = false;
            if_win.Center();
            if_win.Width = 246;
            if_win.Height = 350;
            if_win.Visible = false;
            if_win.Resizable = false;
            if_win.ResizeEnd += new EventHandler(if_win_ResizeEnd);

            if_name = new ImageBox(manager);
            if_name.Init();
            if_name.Left = 3;
            if_name.Top = 5;
            if_name.Width = 226;
            if_name.Height = 23;
            if_name.Parent = if_win;
            if_name.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_0");

            if_title = new ImageBox(manager);
            if_title.Init();
            if_title.Left = 3;
            if_title.Top = 28;
            if_title.Width = 226;
            if_title.Height = 23;
            if_title.Parent = if_win;
            if_title.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            manager.Add(if_win);
            // Create first group
            if_panel1 = new GroupBox(manager);
            if_panel1.Init();
            if_panel1.Text = "";
            if_panel1.Left = 3;
            if_panel1.Top = 52;
            if_panel1.Width = 226;
            if_panel1.Height = 97;
            if_panel1.Parent = if_win;

            if_hptitle = new ImageBox(manager);
            if_hptitle.Init();
            if_hptitle.Left = 10;
            if_hptitle.Top = 19;
            if_hptitle.Width = 55;
            if_hptitle.Height = 19;
            if_hptitle.Parent = if_panel1;
            if_hptitle.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_mptitle = new ImageBox(manager);
            if_mptitle.Init();
            if_mptitle.Left = 10;
            if_mptitle.Top = 39;
            if_mptitle.Width = 55;
            if_mptitle.Height = 19;
            if_mptitle.Parent = if_panel1;
            if_mptitle.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_aptitle = new ImageBox(manager);
            if_aptitle.Init();
            if_aptitle.Left = 10;
            if_aptitle.Top = 59;
            if_aptitle.Width = 55;
            if_aptitle.Height = 19;
            if_aptitle.Parent = if_panel1;
            if_aptitle.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_hpbar = new ProgressBar(manager);
            if_hpbar.Init();
            if_hpbar.Left = 71;
            if_hpbar.Top = 19;
            if_hpbar.Width = 147;
            if_hpbar.Height = 19;
            if_hpbar.Parent = if_panel1;

            if_mpbar = new ProgressBar(manager);
            if_mpbar.Init();
            if_mpbar.Left = 71;
            if_mpbar.Top = 39;
            if_mpbar.Width = 147;
            if_mpbar.Height = 19;
            if_mpbar.Parent = if_panel1;

            if_apbar = new ProgressBar(manager);
            if_apbar.Init();
            if_apbar.Left = 71;
            if_apbar.Top = 59;
            if_apbar.Width = 147;
            if_apbar.Height = 19;
            if_apbar.Parent = if_panel1;

            if_expbar = new ProgressBar(manager);
            if_expbar.Init();
            if_expbar.Left = 10;
            if_expbar.Top = 79;
            if_expbar.Width = 208;
            if_expbar.Height = 12;
            if_expbar.Parent = if_panel1;

            // create second group
            if_panel2 = new GroupBox(manager);
            if_panel2.Init();
            if_panel2.Left = 3;
            if_panel2.Top = 149;
            if_panel2.Width = 226;
            if_panel2.Height = 141;
            if_panel2.Parent = if_win;

            if_atk = new ImageBox(manager);
            if_atk.Init();
            if_atk.Left = 10;
            if_atk.Top = 19;
            if_atk.Width = 85;
            if_atk.Height = 19;
            if_atk.Parent = if_panel2;
            if_atk.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_agi = new ImageBox(manager);
            if_agi.Init();
            if_agi.Left = 10;
            if_agi.Top = 38;
            if_agi.Width = 85;
            if_agi.Height = 19;
            if_agi.Parent = if_panel2;
            if_agi.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_mag = new ImageBox(manager);
            if_mag.Init();
            if_mag.Left = 10;
            if_mag.Top = 57;
            if_mag.Width = 85;
            if_mag.Height = 19;
            if_mag.Parent = if_panel2;
            if_mag.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_luk = new ImageBox(manager);
            if_luk.Init();
            if_luk.Left = 10;
            if_luk.Top = 76;
            if_luk.Width = 85;
            if_luk.Height = 19;
            if_luk.Parent = if_panel2;
            if_luk.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_crit = new ImageBox(manager);
            if_crit.Init();
            if_crit.Left = 10;
            if_crit.Top = 95;
            if_crit.Width = 85;
            if_crit.Height = 19;
            if_crit.Parent = if_panel2;
            if_crit.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_drop = new ImageBox(manager);
            if_drop.Init();
            if_drop.Left = 10;
            if_drop.Top = 114;
            if_drop.Width = 85;
            if_drop.Height = 19;
            if_drop.Parent = if_panel2;
            if_drop.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_subpanel = new Panel(manager);
            if_subpanel.Init();
            if_subpanel.Left = 101;
            if_subpanel.Top = 19;
            if_subpanel.Width = 117;
            if_subpanel.Height = 65;
            if_subpanel.Parent = if_panel2;

            if_atksub = new ImageBox(manager);
            if_atksub.Init();
            if_atksub.Left = 3;
            if_atksub.Top = 3;
            if_atksub.Width = 111;
            if_atksub.Height = 19;
            if_atksub.Parent = if_subpanel;
            if_atksub.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_defsub = new ImageBox(manager);
            if_defsub.Init();
            if_defsub.Left = 3;
            if_defsub.Top = 22;
            if_defsub.Width = 111;
            if_defsub.Height = 19;
            if_defsub.Parent = if_subpanel;
            if_defsub.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_speedsub = new ImageBox(manager);
            if_speedsub.Init();
            if_speedsub.Left = 3;
            if_speedsub.Top = 41;
            if_speedsub.Width = 111;
            if_speedsub.Height = 19;
            if_speedsub.Parent = if_subpanel;
            if_speedsub.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_fire = new ImageBox(manager);
            if_fire.Init();
            if_fire.Left = 101;
            if_fire.Top = 90;
            if_fire.Width = 29;
            if_fire.Height = 19;
            if_fire.Parent = if_panel2;
            if_fire.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_air = new ImageBox(manager);
            if_air.Init();
            if_air.Left = 130;
            if_air.Top = 90;
            if_air.Width = 29;
            if_air.Height = 19;
            if_air.Parent = if_panel2;
            if_air.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_lighting = new ImageBox(manager);
            if_lighting.Init();
            if_lighting.Left = 160;
            if_lighting.Top = 90;
            if_lighting.Width = 29;
            if_lighting.Height = 19;
            if_lighting.Parent = if_panel2;
            if_lighting.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_light = new ImageBox(manager);
            if_light.Init();
            if_light.Left = 189;
            if_light.Top = 90;
            if_light.Width = 29;
            if_light.Height = 19;
            if_light.Parent = if_panel2;
            if_light.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_wind = new ImageBox(manager);
            if_wind.Init();
            if_wind.Left = 101;
            if_wind.Top = 114;
            if_wind.Width = 29;
            if_wind.Height = 19;
            if_wind.Parent = if_panel2;
            if_wind.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_water = new ImageBox(manager);
            if_water.Init();
            if_water.Left = 130;
            if_water.Top = 114;
            if_water.Width = 29;
            if_water.Height = 19;
            if_water.Parent = if_panel2;
            if_water.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_earth = new ImageBox(manager);
            if_earth.Init();
            if_earth.Left = 160;
            if_earth.Top = 114;
            if_earth.Width = 29;
            if_earth.Height = 19;
            if_earth.Parent = if_panel2;
            if_earth.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_shadow = new ImageBox(manager);
            if_shadow.Init();
            if_shadow.Left = 189;
            if_shadow.Top = 114;
            if_shadow.Width = 29;
            if_shadow.Height = 19;
            if_shadow.Parent = if_panel2;
            if_shadow.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");
            // create status point
            if_statuspoint = new ImageBox(manager);
            if_statuspoint.Init();
            if_statuspoint.Left = 152;
            if_statuspoint.Top = 292;
            if_statuspoint.Width = 75;
            if_statuspoint.Height = 18;
            if_statuspoint.Parent = if_win;
            if_statuspoint.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");
        }

        private void if_win_ResizeEnd(object sender, EventArgs e)
        {
            if_win.Text = if_win.Width.ToString() + "   " + if_win.Height.ToString();
        }
    }
}