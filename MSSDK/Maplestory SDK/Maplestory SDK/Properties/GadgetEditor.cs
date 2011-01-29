using Maplestory_SDK.User_Class;
using Microsoft.Xna.Framework;
using TomShane.Neoforce.Controls;

namespace Maplestory_SDK.Tool
{
    internal class Editor
    {
        #region Declare

        Manager manager;
        Player playerinfo;

        bool Visible = true;
        Label lbDEBUGMODE;
        Label lbWaring;
        Panel panel2;
        Label lbselectgadget;
        ListBox listgadget;

        Panel panel3;
        Label lbselectaction;
        ListBox listaction;

        Panel panel4;
        Label lntexturepostion;
        Label lnface;

        public SideBar bar;
        public SideBar inforbar;

        #endregion Declare

        /// <summary>
        /// Create Editor GUI
        /// </summary>
        /// <param name="Game"></param>
        /// <param name="grap"></param>
        /// <param name="Skin">skin name</param>
        /// <param name="playerinfo"></param>
        public Editor(Manager _manager, Player playerinfo)
        {
            this.playerinfo = playerinfo;
            manager = _manager;

            Create();
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            manager.Update(gameTime);
        }

        /// <summary>
        /// draw
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                manager.BeginDraw(gameTime);
                manager.EndDraw();
            }
        }

        /// <summary>
        /// Create GUI
        /// </summary>
        public void Create()
        {
            // information bar
            inforbar = new SideBar(manager);
            inforbar.Init();
            inforbar.Height = 30;
            inforbar.Width = 800;

            lbDEBUGMODE = new Label(manager);
            lbDEBUGMODE.Init();
            lbDEBUGMODE.Text = "Gadget Editor : " + playerinfo.player.DEBUGMODE.ToString();
            lbDEBUGMODE.Top = 5;
            lbDEBUGMODE.Left = 5;
            lbDEBUGMODE.TextColor = Color.White;
            lbDEBUGMODE.Width = 150;

            lbWaring = new Label(manager);
            lbWaring.Init();
            lbWaring.Text = "";
            lbWaring.Top = 5;
            lbWaring.Left = 170;
            lbWaring.TextColor = Color.Blue;
            lbWaring.Width = 620;

            inforbar.Add(lbWaring);
            inforbar.Add(lbDEBUGMODE);

            // bottom bar
            bar = new SideBar(manager);
            bar.Init();
            bar.Top = 380;
            bar.Height = 100;
            bar.Width = 800;

            // debug setting panel
            Panel panel1 = new Panel(manager);
            panel1.Init();
            panel1.Top = 10;
            panel1.Left = 10;
            panel1.Width = 75;
            panel1.Height = 80;
            panel1.BevelBorder = BevelBorder.All;
            panel1.BevelStyle = BevelStyle.Bumped;
            panel1.BevelColor = Color.White;

            Button btnDebug = new Button(manager);
            btnDebug.Init();
            btnDebug.Text = "Gadget";
            btnDebug.Top = 5; // this is in pixels, top-left is the origin
            btnDebug.Left = 5;
            btnDebug.Width = 60;
            btnDebug.Height = 20;
            btnDebug.Click += new EventHandler(btnDebug_Click);

            panel1.Add(btnDebug);
            bar.Add(panel1);

            panel2 = new Panel(manager);
            panel2.Init();
            panel2.Top = 10;
            panel2.Left = 100;
            panel2.Width = 100;
            panel2.Height = 80;
            panel2.BevelBorder = BevelBorder.All;
            panel2.BevelStyle = BevelStyle.Bumped;
            panel2.BevelColor = Color.White;
            panel2.Visible = false;

            lbselectgadget = new Label(manager);
            lbselectgadget.Init();
            lbselectgadget.Text = "Select Gadget";
            lbselectgadget.Top = 5;
            lbselectgadget.Left = 5;
            lbselectgadget.TextColor = Color.White;
            lbselectgadget.Width = 100;

            listgadget = new ListBox(manager);
            listgadget.Init();
            listgadget.Top = 25;
            listgadget.Width = 100;
            listgadget.Height = 55;
            listgadget.ItemIndexChanged += new EventHandler(listgadget_ItemIndexChanged);

            panel2.Add(listgadget);
            panel2.Add(lbselectgadget);
            bar.Add(panel2);

            panel3 = new Panel(manager);
            panel3.Init();
            panel3.Top = 10;
            panel3.Left = 210;
            panel3.Width = 100;
            panel3.Height = 80;
            panel3.BevelBorder = BevelBorder.All;
            panel3.BevelStyle = BevelStyle.Bumped;
            panel3.BevelColor = Color.White;
            panel3.Visible = false;

            lbselectaction = new Label(manager);
            lbselectaction.Init();
            lbselectaction.Text = "Select Action";
            lbselectaction.Top = 5;
            lbselectaction.Left = 5;
            lbselectaction.TextColor = Color.White;
            lbselectaction.Width = 100;

            listaction = new ListBox(manager);
            listaction.Init();
            listaction.Top = 25;
            listaction.Width = 100;
            listaction.Height = 55;
            listaction.ItemIndexChanged += new EventHandler(listaction_ItemIndexChanged);

            panel3.Add(listaction);
            panel3.Add(lbselectaction);
            bar.Add(panel3);

            panel4 = new Panel(manager);
            panel4.Init();
            panel4.Top = 10;
            panel4.Left = 320;
            panel4.Width = 255;
            panel4.Height = 80;
            panel4.BevelBorder = BevelBorder.All;
            panel4.BevelStyle = BevelStyle.Bumped;
            panel4.BevelColor = Color.White;
            panel4.Visible = false;

            Button btnper = new Button(manager);
            btnper.Init();
            btnper.Text = "<";
            btnper.Top = 5; // this is in pixels, top-left is the origin
            btnper.Left = 5;
            btnper.Width = 20;
            btnper.Height = 20;
            btnper.Click += new EventHandler(btnper_Click);

            Button btnnext = new Button(manager);
            btnnext.Init();
            btnnext.Text = ">";
            btnnext.Top = 5; // this is in pixels, top-left is the origin
            btnnext.Left = 30;
            btnnext.Width = 20;
            btnnext.Height = 20;
            btnnext.Click += new EventHandler(btnnext_Click);

            Button btnper1 = new Button(manager);
            btnper1.Init();
            btnper1.Text = "<";
            btnper1.Top = 55; // this is in pixels, top-left is the origin
            btnper1.Left = 160;
            btnper1.Width = 20;
            btnper1.Height = 20;
            btnper1.Click += new EventHandler(btnper1_Click);

            Button btnnext1 = new Button(manager);
            btnnext1.Init();
            btnnext1.Text = ">";
            btnnext1.Top = 55; // this is in pixels, top-left is the origin
            btnnext1.Left = 225;
            btnnext1.Width = 20;
            btnnext1.Height = 20;
            btnnext1.Click += new EventHandler(btnnext1_Click);

            Label lnface1 = new Label(manager);
            lnface1.Init();
            lnface1.Text = "Set Face";
            lnface1.Top = 30;
            lnface1.Left = 170;
            lnface1.TextColor = Color.White;
            lnface1.Width = 220;

            lnface = new Label(manager);
            lnface.Init();
            lnface.Text = playerinfo.player.Facing;
            lnface.Top = 55;
            lnface.Left = 185;
            lnface.TextColor = Color.White;
            lnface.Width = 220;

            lntexturepostion = new Label(manager);
            lntexturepostion.Init();
            lntexturepostion.Text = "Texture position is : " + playerinfo.player.texture_position.ToString();
            lntexturepostion.Top = 5;
            lntexturepostion.Left = 55;
            lntexturepostion.TextColor = Color.White;
            lntexturepostion.Width = 220;

            Button btnSave = new Button(manager);
            btnSave.Init();
            btnSave.Text = "Save this position";
            btnSave.Top = 30; // this is in pixels, top-left is the origin
            btnSave.Left = 5;
            btnSave.Width = 150;
            btnSave.Height = 20;
            btnSave.Click += new EventHandler(btnSave_Click);

            Button btnExport = new Button(manager);
            btnExport.Init();
            btnExport.Text = "Export XML";
            btnExport.Top = 55; // this is in pixels, top-left is the origin
            btnExport.Left = 5;
            btnExport.Width = 150;
            btnExport.Height = 20;
            btnExport.Click += new EventHandler(btnExport_Click);

            panel4.Add(btnper1);
            panel4.Add(btnnext1);
            panel4.Add(lnface);
            panel4.Add(lnface1);
            panel4.Add(lntexturepostion);
            panel4.Add(btnper);
            panel4.Add(btnnext);
            panel4.Add(btnSave);
            panel4.Add(btnExport);
            bar.Add(panel4);

            manager.Add(bar);
            manager.Add(inforbar);

            bar.Visible = false;
            inforbar.Visible = false;
        }

        public void Show()
        {
            bar.Visible = true;
            inforbar.Visible = true;
        }

        public void Hide()
        {
            bar.Visible = false;
            inforbar.Visible = false;
        }

        // export
        private void btnExport_Click(object sender, EventArgs e)
        {
            playerinfo.player.SaveXML();
            playerinfo.player.iscreatexml = false;
            lbWaring.Text = "Export XML file is success! link : " + playerinfo.player.link;
        }

        // save
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (playerinfo.player.iscreatexml == true)
            {
                playerinfo.player.AddNodeValue();
                lbWaring.Text = "Add X : " + playerinfo.player.debugx + " and Y : " + playerinfo.player.debugy + " when Facing : " + playerinfo.player.Facing + " into XML node";
            }
        }

        // face +
        private void btnnext1_Click(object sender, EventArgs e)
        {
            GetXY();
            playerinfo.player.Facing = "right";
            playerinfo.player.UpdateTexture();
            lnface.Text = playerinfo.player.Facing;
        }

        // face -
        private void btnper1_Click(object sender, EventArgs e)
        {
            GetXY();
            playerinfo.player.Facing = "left";
            playerinfo.player.UpdateTexture();
            lnface.Text = playerinfo.player.Facing;
        }

        // texture positon +
        private void btnnext_Click(object sender, EventArgs e)
        {
            GetXY();
            switch (playerinfo.player.Action)
            {
                case "Stand":
                    if (playerinfo.player.texture_position < 3)
                        playerinfo.player.texture_position++;
                    break;
                case "Walk":
                    if (playerinfo.player.texture_position < 3)
                        playerinfo.player.texture_position++;
                    break;
                case "Jump":
                    if (playerinfo.player.texture_position < 0)
                        playerinfo.player.texture_position++;
                    break;
                case "Attack":
                    switch (playerinfo.player.WeaponType)
                    {
                        case "Hand":
                            switch (playerinfo.player.AttackType)
                            {
                                case "1":
                                    if (playerinfo.player.texture_position < 1)
                                        playerinfo.player.texture_position++;
                                    break;
                                case "2":
                                    if (playerinfo.player.texture_position < 1)
                                        playerinfo.player.texture_position++;
                                    break;
                                case "3":
                                    if (playerinfo.player.texture_position < 2)
                                        playerinfo.player.texture_position++;
                                    break;
                            }
                            break;
                        case "Sword":
                            switch (playerinfo.player.AttackType)
                            {
                                case "1":
                                case "2":
                                case "3":
                                    if (playerinfo.player.texture_position < 2)
                                    {
                                        playerinfo.player.texture_position++;
                                    }
                                    else
                                    {
                                        playerinfo.player.texture_position = 0;
                                    }
                                    break;
                            }
                            break;
                        case "Gun":
                            if (playerinfo.player.texture_position < 4)
                                playerinfo.player.texture_position++;
                            break;
                        case "Bow":
                            if (playerinfo.player.texture_position < 1)
                                playerinfo.player.texture_position++;
                            break;
                    }
                    break;
            }
            playerinfo.player.UpdateTexture();
            lntexturepostion.Text = "Texture position is : " + playerinfo.player.texture_position.ToString();
        }

        // texture postion -
        private void btnper_Click(object sender, EventArgs e)
        {
            GetXY();
            if (playerinfo.player.texture_position > 0)
                playerinfo.player.texture_position--;
            playerinfo.player.UpdateTexture();
            lntexturepostion.Text = "Texture position is : " + playerinfo.player.texture_position.ToString();
        }

        private void listaction_ItemIndexChanged(object sender, EventArgs e)
        {
            playerinfo.player.texture_position = 0;
            panel4.Show();
            playerinfo.player.Action = listaction.Items[listaction.ItemIndex].ToString();
            playerinfo.player.UpdateTexture();
            playerinfo.player.CreateXML();
            lbselectaction.Text = "Select " + listaction.Items[listaction.ItemIndex].ToString();
            listaction.Focused = false;
        }

        private void listgadget_ItemIndexChanged(object sender, EventArgs e)
        {
            panel3.Show();
            playerinfo.player.debugselect = listgadget.Items[listgadget.ItemIndex].ToString();
            GetXY();
            lbselectgadget.Text = "Select " + listgadget.Items[listgadget.ItemIndex].ToString();
            listgadget.Focused = false;
        }

        /// <summary>
        /// get gadget x,y
        /// </summary>
        private void GetXY()
        {
            if (playerinfo.player.debugselect == "Head")
            {
                if (playerinfo.player.Facing == "left") playerinfo.player.debugx = playerinfo.player.RHead.X - playerinfo.player.x;
                if (playerinfo.player.Facing == "right") playerinfo.player.debugx = playerinfo.player.RHead.X - playerinfo.player.RSkin.X;
                playerinfo.player.debugy = playerinfo.player.RHead.Y - playerinfo.player.RSkin.Y + playerinfo.player.THead.Height;
            }
            if (playerinfo.player.debugselect == "Face")
            {
                playerinfo.player.debugx = playerinfo.player.RFace.X - playerinfo.player.RHead.X;
                playerinfo.player.debugy = playerinfo.player.RFace.Y - playerinfo.player.RHead.Y;
            }
            if (playerinfo.player.debugselect == "Hair")
            {
                playerinfo.player.debugx = playerinfo.player.RHair.X - playerinfo.player.RHead.X;
                playerinfo.player.debugy = playerinfo.player.RHair.Y - playerinfo.player.RHead.Y;
            }
            if (playerinfo.player.debugselect == "Arm")
            {
                playerinfo.player.debugx = playerinfo.player.RArm.X - playerinfo.player.x;
                playerinfo.player.debugy = playerinfo.player.RArm.Y - playerinfo.player.RSkin.Y + playerinfo.player.TArm.Height;
            }
        }

        /// <summary>
        /// add gadget and action into list
        /// </summary>
        private void AddGadget()
        {
            listgadget.Items.Add("Head");
            listgadget.Items.Add("Arm");
            listgadget.Items.Add("Face");
            listgadget.Items.Add("Hair");

            listaction.Items.Add("Walk");
            listaction.Items.Add("Stand");
            listaction.Items.Add("Jump");
            listaction.Items.Add("Attack");
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            if (playerinfo.player.DEBUGMODE)
            {
                playerinfo.player.DEBUGMODE = false;
                lbDEBUGMODE.TextColor = Color.White;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                lbWaring.Text = "";
            }
            else
            {
                playerinfo.player.DEBUGMODE = true;
                lbDEBUGMODE.TextColor = Color.Red;
                AddGadget();
                panel2.Visible = true;

                playerinfo.player.texture_position = 0;
                playerinfo.player.debugselect = "";
            }
            lbDEBUGMODE.Text = "Gadget Editor : " + playerinfo.player.DEBUGMODE.ToString();
        }
    }
}