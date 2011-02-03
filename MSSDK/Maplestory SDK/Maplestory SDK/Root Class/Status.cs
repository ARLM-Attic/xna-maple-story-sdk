using Maplestory_SDK.User_Class;
using Microsoft.Xna.Framework;
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
        StatusBox if_name;
        StatusBox if_title;
        //* nhóm 1
        GroupBox if_panel1;
        StatusBox if_hptitle;
        StatusBox if_mptitle;
        StatusBox if_aptitle;
        ProgressBar if_hpbar;
        ProgressBar if_mpbar;
        ProgressBar if_apbar;
        ProgressBar if_expbar;
        //* nhóm 2
        GroupBox if_panel2;
        Panel if_subpanel;
        // -- thuộc tính chính
        StatusBox if_atk;
        StatusBox if_agi;
        StatusBox if_mag;
        StatusBox if_luk;
        // -- thuộc tính kèm theo
        StatusBox if_crit;
        StatusBox if_drop;
        // -- thuộc tính phụ
        StatusBox if_atksub;
        StatusBox if_defsub;
        StatusBox if_speedsub;
        // -- thuộc tính kháng yếu tố
        StatusBox if_fire;
        StatusBox if_air;
        StatusBox if_lighting;
        StatusBox if_light;
        StatusBox if_wind;
        StatusBox if_water;
        StatusBox if_earth;
        StatusBox if_shadow;
        // ------------------
        StatusBox if_statuspoint;

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

            // update giá trị của thông tin về nhân vật
            if_name.Value = player.name;
            if_title.Value = player.titlename;
            if_atk.Value = player.str.ToString() + " ";
            if_agi.Value = player.agi.ToString() + " ";
            if_mag.Value = player.mag.ToString() + " ";
            if_luk.Value = player.luk.ToString() + " ";
            if_crit.Value = player.crit.ToString() + " ";
            if_drop.Value = player.droprate.ToString() + " ";

            if_statuspoint.Value = player.APoint.ToString() + " ";
        }

        public void Draw(SpriteBatch spritebatch)
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

            if_name = new StatusBox(manager);
            if_name.Init();
            if_name.Left = 3;
            if_name.SizeMode = SizeMode.Stretched;
            if_name.Value = player.name;
            if_name.Top = 5;
            if_name.Width = 226;
            if_name.Height = 23;
            if_name.Parent = if_win;
            if_name.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_0");

            if_title = new StatusBox(manager);
            if_title.Init();
            if_title.SizeMode = SizeMode.Stretched;
            if_title.Value = player.titlename;
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

            if_hptitle = new StatusBox(manager);
            if_hptitle.Init();
            if_hptitle.SizeMode = SizeMode.Stretched;
            if_hptitle.Left = 10;
            if_hptitle.Top = 19;
            if_hptitle.Width = 55;
            if_hptitle.Height = 19;
            if_hptitle.Parent = if_panel1;
            if_hptitle.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_mptitle = new StatusBox(manager);
            if_mptitle.Init();
            if_mptitle.SizeMode = SizeMode.Stretched;
            if_mptitle.Left = 10;
            if_mptitle.Top = 39;
            if_mptitle.Width = 55;
            if_mptitle.Height = 19;
            if_mptitle.Parent = if_panel1;
            if_mptitle.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_aptitle = new StatusBox(manager);
            if_aptitle.Init();
            if_aptitle.SizeMode = SizeMode.Stretched;
            if_aptitle.Left = 10;
            if_aptitle.Top = 59;
            if_aptitle.Width = 55;
            if_aptitle.Height = 19;
            if_aptitle.Parent = if_panel1;
            if_aptitle.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_hpbar = new ProgressBar(manager);
            if_hpbar.Init();
            if_hpbar.Mode = ProgressBarMode.Default;
            if_hpbar.Range = player.maxhp;
            if_hpbar.Value = player.hp;
            if_hpbar.Color = Color.Red;
            if_hpbar.Left = 71;
            if_hpbar.Top = 19;
            if_hpbar.Width = 147;
            if_hpbar.Height = 19;
            if_hpbar.Parent = if_panel1;

            if_mpbar = new ProgressBar(manager);
            if_mpbar.Init();
            if_mpbar.Range = player.maxmp;
            if_mpbar.Value = player.mp;
            if_mpbar.Color = Color.Blue;
            if_mpbar.Left = 71;
            if_mpbar.Top = 39;
            if_mpbar.Width = 147;
            if_mpbar.Height = 19;
            if_mpbar.Parent = if_panel1;

            if_apbar = new ProgressBar(manager);
            if_apbar.Init();
            if_apbar.Range = player.maxchari;
            if_apbar.Value = player.chari;
            if_apbar.Color = Color.YellowGreen;
            if_apbar.Left = 71;
            if_apbar.Top = 59;
            if_apbar.Width = 147;
            if_apbar.Height = 19;
            if_apbar.Parent = if_panel1;

            if_expbar = new ProgressBar(manager);
            if_expbar.Init();
            if_expbar.Range = player.expnextlevel;
            if_expbar.Value = player.exp;
            if_expbar.Color = Color.Wheat;
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

            if_atk = new StatusBox(manager);
            if_atk.Init();
            if_atk.SizeMode = SizeMode.Stretched;
            if_atk.Value = player.str.ToString() + " ";
            if_atk.TextAlignment = Alignment.MiddleRight;
            if_atk.Label = " Str : ";
            if_atk.LabelColor = Color.Red;
            if_atk.SizeMode = SizeMode.Stretched;
            if_atk.Left = 10;
            if_atk.SizeMode = SizeMode.Stretched;
            if_atk.Top = 19;
            if_atk.Width = 85;
            if_atk.Height = 19;
            if_atk.Parent = if_panel2;
            if_atk.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_agi = new StatusBox(manager);
            if_agi.Init();
            if_agi.SizeMode = SizeMode.Stretched;
            if_agi.Value = player.agi.ToString() + " ";
            if_agi.TextAlignment = Alignment.MiddleRight;
            if_agi.Label = " Agi : ";
            if_agi.LabelColor = Color.Red;
            if_agi.Left = 10;
            if_agi.Top = 38;
            if_agi.Width = 85;
            if_agi.Height = 19;
            if_agi.Parent = if_panel2;
            if_agi.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_mag = new StatusBox(manager);
            if_mag.Init();
            if_mag.SizeMode = SizeMode.Stretched;
            if_mag.Value = player.mag.ToString() + " ";
            if_mag.TextAlignment = Alignment.MiddleRight;
            if_mag.Label = " Mag : ";
            if_mag.LabelColor = Color.Red;
            if_mag.Left = 10;
            if_mag.Top = 57;
            if_mag.Width = 85;
            if_mag.Height = 19;
            if_mag.Parent = if_panel2;
            if_mag.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_luk = new StatusBox(manager);
            if_luk.Init();
            if_luk.SizeMode = SizeMode.Stretched;
            if_luk.Value = player.luk.ToString() + " ";
            if_luk.TextAlignment = Alignment.MiddleRight;
            if_luk.Label = " Luck : ";
            if_luk.LabelColor = Color.Red;
            if_luk.Left = 10;
            if_luk.Top = 76;
            if_luk.Width = 85;
            if_luk.Height = 19;
            if_luk.Parent = if_panel2;
            if_luk.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_crit = new StatusBox(manager);
            if_crit.Init();
            if_crit.SizeMode = SizeMode.Stretched;
            if_crit.Value = player.crit.ToString() + " ";
            if_crit.TextAlignment = Alignment.MiddleRight;
            if_crit.Label = " Crit : ";
            if_crit.LabelColor = Color.Red;
            if_crit.Left = 10;
            if_crit.Top = 95;
            if_crit.Width = 85;
            if_crit.Height = 19;
            if_crit.Parent = if_panel2;
            if_crit.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_3");

            if_drop = new StatusBox(manager);
            if_drop.Init();
            if_drop.SizeMode = SizeMode.Stretched;
            if_drop.Value = player.droprate.ToString() + " ";
            if_drop.TextAlignment = Alignment.MiddleRight;
            if_drop.Label = " Drop : ";
            if_drop.LabelColor = Color.Red;
            if_drop.Left = 10;
            if_drop.Top = 114;
            if_drop.Width = 85;
            if_drop.Height = 19;
            if_drop.Parent = if_panel2;
            if_drop.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_3");

            if_subpanel = new Panel(manager);
            if_subpanel.Init();
            if_subpanel.Left = 101;
            if_subpanel.Top = 19;
            if_subpanel.Width = 117;
            if_subpanel.Height = 65;
            if_subpanel.Parent = if_panel2;

            if_atksub = new StatusBox(manager);
            if_atksub.Init();
            if_atksub.SizeMode = SizeMode.Stretched;
            if_atksub.Value = player.pysatk.ToString() + " ";
            if_atksub.TextAlignment = Alignment.MiddleRight;
            if_atksub.Label = " Pys Atk : ";
            if_atksub.LabelColor = Color.Red;
            if_atksub.Left = 3;
            if_atksub.Top = 3;
            if_atksub.Width = 111;
            if_atksub.Height = 19;
            if_atksub.Parent = if_subpanel;
            if_atksub.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_0");

            if_defsub = new StatusBox(manager);
            if_defsub.Init();
            if_defsub.SizeMode = SizeMode.Stretched;
            if_defsub.Value = player.pysdef.ToString() + " ";
            if_defsub.TextAlignment = Alignment.MiddleRight;
            if_defsub.Label = " Pys Def : ";
            if_defsub.LabelColor = Color.Red;
            if_defsub.Left = 3;
            if_defsub.Top = 22;
            if_defsub.Width = 111;
            if_defsub.Height = 19;
            if_defsub.Parent = if_subpanel;
            if_defsub.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_0");

            if_speedsub = new StatusBox(manager);
            if_speedsub.Init();
            if_speedsub.SizeMode = SizeMode.Stretched;
            if_speedsub.Value = player.pysatkspeed.ToString() + " ";
            if_speedsub.TextAlignment = Alignment.MiddleRight;
            if_speedsub.Label = " Pys Speed : ";
            if_speedsub.LabelColor = Color.Red;
            if_speedsub.Left = 3;
            if_speedsub.Top = 41;
            if_speedsub.Width = 111;
            if_speedsub.Height = 19;
            if_speedsub.Parent = if_subpanel;
            if_speedsub.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_0");

            if_fire = new StatusBox(manager);
            if_fire.Init();
            if_fire.SizeMode = SizeMode.Stretched;
            if_fire.Value = player.av_fire.ToString();
            if_fire.TextAlignment = Alignment.MiddleCenter;
            if_fire.Left = 101;
            if_fire.Top = 90;
            if_fire.Width = 29;
            if_fire.Height = 19;
            if_fire.Parent = if_panel2;
            if_fire.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_air = new StatusBox(manager);
            if_air.Init();
            if_air.Value = player.av_air.ToString();
            if_air.TextAlignment = Alignment.MiddleCenter;
            if_air.SizeMode = SizeMode.Stretched;
            if_air.Left = 130;
            if_air.Top = 90;
            if_air.Width = 29;
            if_air.Height = 19;
            if_air.Parent = if_panel2;
            if_air.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_lighting = new StatusBox(manager);
            if_lighting.Init();
            if_lighting.Value = player.av_lighting.ToString();
            if_lighting.TextAlignment = Alignment.MiddleCenter;
            if_lighting.SizeMode = SizeMode.Stretched;
            if_lighting.Left = 160;
            if_lighting.Top = 90;
            if_lighting.Width = 29;
            if_lighting.Height = 19;
            if_lighting.Parent = if_panel2;
            if_lighting.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_light = new StatusBox(manager);
            if_light.Init();
            if_light.Value = player.av_light.ToString();
            if_light.TextAlignment = Alignment.MiddleCenter;
            if_light.SizeMode = SizeMode.Stretched;
            if_light.Left = 189;
            if_light.Top = 90;
            if_light.Width = 29;
            if_light.Height = 19;
            if_light.Parent = if_panel2;
            if_light.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_wind = new StatusBox(manager);
            if_wind.Init();
            if_wind.Value = player.av_wind.ToString();
            if_wind.TextAlignment = Alignment.MiddleCenter;
            if_wind.SizeMode = SizeMode.Stretched;
            if_wind.Left = 101;
            if_wind.Top = 114;
            if_wind.Width = 29;
            if_wind.Height = 19;
            if_wind.Parent = if_panel2;
            if_wind.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_water = new StatusBox(manager);
            if_water.Init();
            if_water.Value = player.av_water.ToString();
            if_water.TextAlignment = Alignment.MiddleCenter;
            if_water.SizeMode = SizeMode.Stretched;
            if_water.Left = 130;
            if_water.Top = 114;
            if_water.Width = 29;
            if_water.Height = 19;
            if_water.Parent = if_panel2;
            if_water.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_earth = new StatusBox(manager);
            if_earth.Init();
            if_earth.Value = player.av_earth.ToString();
            if_earth.TextAlignment = Alignment.MiddleCenter;
            if_earth.SizeMode = SizeMode.Stretched;
            if_earth.Left = 160;
            if_earth.Top = 114;
            if_earth.Width = 29;
            if_earth.Height = 19;
            if_earth.Parent = if_panel2;
            if_earth.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");

            if_shadow = new StatusBox(manager);
            if_shadow.Init();
            if_shadow.Value = player.av_shadow.ToString();
            if_shadow.TextAlignment = Alignment.MiddleCenter;
            if_shadow.SizeMode = SizeMode.Stretched;
            if_shadow.Left = 189;
            if_shadow.Top = 114;
            if_shadow.Width = 29;
            if_shadow.Height = 19;
            if_shadow.Parent = if_panel2;
            if_shadow.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");
            // create status point
            if_statuspoint = new StatusBox(manager);
            if_statuspoint.Init();
            if_statuspoint.SizeMode = SizeMode.Stretched;
            if_statuspoint.Left = 152;
            if_statuspoint.Top = 292;
            if_statuspoint.Width = 75;
            if_statuspoint.Height = 18;
            if_statuspoint.Value = player.APoint.ToString() + " ";
            if_statuspoint.TextAlignment = Alignment.MiddleRight;
            if_statuspoint.Label = " AP : ";
            if_statuspoint.LabelColor = Color.Red;
            if_statuspoint.Parent = if_win;
            if_statuspoint.Image = manager.Content.Load<Texture2D>("Content\\Temp\\status_1");
        }

        private void if_win_ResizeEnd(object sender, EventArgs e)
        {
            if_win.Text = if_win.Width.ToString() + "   " + if_win.Height.ToString();
        }
    }
}