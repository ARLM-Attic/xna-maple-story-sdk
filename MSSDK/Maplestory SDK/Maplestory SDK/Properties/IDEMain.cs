using Maplestory_SDK.Root_Class;
using Maplestory_SDK.User_Class;
using Microsoft.Xna.Framework;
using TomShane.Neoforce.Controls;

namespace Maplestory_SDK.Tool
{
    internal class IDEMain
    {
        public bool ENABLE = true;

        Manager manager;
        Manager mapeditor;
        Player player;
        Map map;
        Editor GadgetEditor;
        bool isGagdetShow = false;
        public MapEditor MapEditor;
        bool isMapShow = false;

        Button btnopen = null;

        Window mainmenu = null;
        Button btngadget = null;
        Button btnmap = null;

        public IDEMain(Manager _manager, Manager _mapeditor, Player _player, Map _map)
        {
            manager = _manager;
            mapeditor = _mapeditor;
            player = _player;
            map = _map;
            GadgetEditor = new Editor(manager, player);
            MapEditor = new MapEditor(manager, mapeditor, map);
            Initialize();
        }

        /// <summary>
        /// Initialize Main Menu
        /// </summary>
        public void Initialize()
        {
            btnopen = new Button(manager);
            btnopen.Left = 750;
            btnopen.Width = 50;
            btnopen.Text = "Menu";
            btnopen.Click += new EventHandler(btnopen_Click);

            mainmenu = new Window(manager);
            mainmenu.Text = "Editor 1.0";
            mainmenu.Top = 25;
            mainmenu.Left = 695;
            mainmenu.Height = 80;
            mainmenu.Width = 105;
            mainmenu.CloseButtonVisible = false;
            mainmenu.Resizable = false;
            mainmenu.Visible = false;

            manager.Add(mainmenu);
            manager.Add(btnopen);

            InsMenu();
        }

        // Menu click
        private void btnopen_Click(object sender, EventArgs e)
        {
            if (mainmenu.Visible == false)
                mainmenu.Visible = true;
            else
                mainmenu.Visible = false;
        }

        /// <summary>
        /// Create GUI menu
        /// </summary>
        private void InsMenu()
        {
            btngadget = new Button(manager);
            btngadget.Text = "Gadget Editor";
            btngadget.Width = 90;
            btngadget.Click += new EventHandler(btngadget_Click);

            btnmap = new Button(manager);
            btnmap.Text = "Map Editor";
            btnmap.Width = 90;
            btnmap.Top = 20;
            btnmap.Click += new EventHandler(btnmap_Click);

            mainmenu.Add(btnmap);
            mainmenu.Add(btngadget);
        }

        private void btnmap_Click(object sender, EventArgs e)
        {
            if (isMapShow)
            {
                MapEditor.Hide();
                isMapShow = false;
            }
            else
            {
                MapEditor.Show();
                isMapShow = true;
            }
        }

        /// <summary>
        ///  Show / Hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btngadget_Click(object sender, EventArgs e)
        {
            if (isGagdetShow)
            {
                GadgetEditor.Hide();
                isGagdetShow = false;
            }
            else
            {
                GadgetEditor.Show();
                isGagdetShow = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            manager.Update(gameTime);
            mapeditor.Update(gameTime);
            MapEditor.UpdateCollusionDraw();
        }

        public void Draw(GameTime gameTime)
        {
            manager.Draw(gameTime);
        }

        public void EndDraw()
        {
            manager.EndDraw();
        }

        public void DrawEditor(GameTime gameTime)
        {
            mapeditor.Draw(gameTime);
        }

        public void EndDrawEditor()
        {
            mapeditor.EndDraw();
        }
    }
}