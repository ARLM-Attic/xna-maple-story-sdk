using System.Collections.Generic;
using Maplestory_SDK.Root_Class;
using Maplestory_SDK.Root_Class.MapCompoment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TomShane.Neoforce.Controls;

namespace Maplestory_SDK.Tool
{
    internal class MapEditor
    {
        #region Fields

        Manager manager;
        Manager mapeditor;
        // main
        Window mainmenu;
        // control in main
        Button btntitles;
        Button btncolusion;
        Button btnevent;
        // window
        Panel wintitles;
        Window wincolusion;
        Window winevent;
        // titles window control
        ListBox ListTextureMap;
        ListBox ListTextureHave;
        ImageBox ImgDisplayTexture;
        Button btnSave;
        ComboBox cbListType;
        ComboBox cbListMap;
        ComboBox cbListObj;
        ComboBox cbLayer;
        // collusion window control
        Label lbtext;
        SpinBox numberupdown;
        Button btnconfim;
        Button btnSavecollusion;
        // event window control

        // variable
        Map map;
        // contain list texture u have
        List<Texture2D> TListTextureHave = new List<Texture2D>();
        // contain list layer
        List<ImageBox>[] ListTextureLayer = new List<ImageBox>[10];

        //int visualX;
        //int visualY;
        int[,] visualmatrix;
        List<int[]> collusionmap = new List<int[]>();
        Texture2D pixel;

        #endregion Fields

        public MapEditor(Manager _manager, Manager _mapeditor, Map _map)
        {
            manager = _manager;
            mapeditor = _mapeditor;
            map = _map;
            visualmatrix = map.visualmatrix;
            collusionmap = map.collusionmap;
            Initialize();
        }

        public void Initialize()
        {
            mainmenu = new Window(manager);
            mainmenu.Text = "Map Editor";
            mainmenu.Top = 105;
            mainmenu.Left = 695;
            mainmenu.Width = 105;
            mainmenu.Height = 120;
            mainmenu.Resizable = false;
            mainmenu.CloseButtonVisible = false;
            mainmenu.Visible = false;

            manager.Add(mainmenu);

            btntitles = new Button(manager);
            btntitles.Text = "Titles Editor";
            btntitles.Width = 90;
            btntitles.Click += new EventHandler(btntitles_Click);

            btncolusion = new Button(manager);
            btncolusion.Text = "Collusion";
            btncolusion.Top = 20;
            btncolusion.Width = 90;
            btncolusion.Click += new EventHandler(btncolusion_Click);

            btnevent = new Button(manager);
            btnevent.Text = "Event";
            btnevent.Top = 40;
            btnevent.Width = 90;

            Button btnSaveAll = new Button(manager);
            btnSaveAll.Text = "Save All";
            btnSaveAll.Top = 60;
            btnSaveAll.Width = 90;
            btnSaveAll.Click += new EventHandler(btnSaveAll_Click);

            mainmenu.Add(btntitles);
            mainmenu.Add(btncolusion);
            mainmenu.Add(btnevent);
            mainmenu.Add(btnSaveAll);

            InsTitles();
            InsCollusion();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            map.SaveMap("testmap");
        }

        private void btncolusion_Click(object sender, EventArgs e)
        {
            if (wincolusion.Visible == false)
            {
                wincolusion.Visible = true;
                InitCollusion();
            }
            else
            {
                wincolusion.Visible = false;
                // update
            }
        }

        private void btntitles_Click(object sender, EventArgs e)
        {
            if (wintitles.Visible == false)
            {
                wintitles.Visible = true;
                InitLayer();
            }
        }

        #region Titles Editor

        #region Method Titles

        public void Show()
        {
            mainmenu.Visible = true;
        }

        public void Hide()
        {
            mainmenu.Visible = false;
        }

        private void GetTextureHave()
        {
            XmlContent.Map.Map mapinfo = manager.Game.Content.Load<XmlContent.Map.Map>("Map\\Data\\" + cbListMap.Items[cbListMap.ItemIndex]);
            if ((string)cbListType.Items[cbListType.ItemIndex] == "Back")
            {
                foreach (string text in mapinfo.backs)
                {
                    Texture2D temp = manager.Game.Content.Load<Texture2D>("Map\\" + cbListType.Items[cbListType.ItemIndex] + "\\" + mapinfo.name + "\\" + text);
                    temp.Tag = "Map\\" + cbListType.Items[cbListType.ItemIndex] + "\\" + mapinfo.name + "\\" + text;
                    TListTextureHave.Add(temp);
                    ListTextureHave.Items.Add(text);
                }
                ListTextureHave.ItemIndex = 0;
            }
            else if ((string)cbListType.Items[cbListType.ItemIndex] == "Titles")
            {
                foreach (string text in mapinfo.titles)
                {
                    Texture2D temp = manager.Game.Content.Load<Texture2D>("Map\\" + cbListType.Items[cbListType.ItemIndex] + "\\" + mapinfo.name + "\\" + text);
                    temp.Tag = "Map\\" + cbListType.Items[cbListType.ItemIndex] + "\\" + mapinfo.name + "\\" + text;
                    TListTextureHave.Add(temp);
                    ListTextureHave.Items.Add(text);
                }
                ListTextureHave.ItemIndex = 0;
            }
            else
            {
                foreach (string text in mapinfo.objs[cbListObj.ItemIndex])
                {
                    Texture2D temp = manager.Game.Content.Load<Texture2D>("Map\\" + cbListType.Items[cbListType.ItemIndex] + "\\" + mapinfo.name + "\\" + cbListObj.Items[cbListObj.ItemIndex] + "\\" + text);
                    temp.Tag = "Map\\" + cbListType.Items[cbListType.ItemIndex] + "\\" + mapinfo.name + "\\" + cbListObj.Items[cbListObj.ItemIndex] + "\\" + text;
                    TListTextureHave.Add(temp);
                    ListTextureHave.Items.Add(text);
                }
                ListTextureHave.ItemIndex = 0;
            }
            //ListTextureHave.ItemIndex = 0;
        }

        private void InitCollusion()
        {
            // initialize matrix
            visualmatrix = map.visualmatrix;
            collusionmap = map.collusionmap;
        }

        private void InitLayer()
        {
            for (int i = 0; i < ListTextureLayer.Length; i++)
            {
                ListTextureLayer[i].Clear();
            }
            // add texture layer from map and clear map
            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                foreach (TextureMap text in map.MapLayer[i].data)
                {
                    ImageBox texmap = new ImageBox(manager);
                    texmap.Init();
                    texmap.Top = text.rectangle.Y;
                    texmap.Left = text.rectangle.X;
                    texmap.Width = text.rectangle.Width;
                    texmap.Height = text.rectangle.Height;
                    texmap.Image = text.texture;
                    texmap.Movable = true;
                    texmap.SizeMode = SizeMode.Stretched;
                    texmap.Resizable = true;
                    texmap.MouseDown += new MouseEventHandler(texmap_MouseDown);

                    mapeditor.Add(texmap);
                    texmap.Tag = new List<object> { j, "Texture " + j.ToString(), text.link };
                    ListTextureLayer[i].Add(texmap);
                    j++;
                }
                map.MapLayer[i].data.Clear();
                j = 0;
            }

            ListTextureMap.Items.Clear();
            foreach (ImageBox img in ListTextureLayer[cbLayer.ItemIndex])
            {
                List<object> temp = (List<object>)img.Tag;

                ListTextureMap.Items.Add((string)temp[1]);
                img.Tag = new List<object> { ListTextureMap.Items.Count - 1, temp[1], temp[2] };
            }
            // check for
            for (int i = 0; i < ListTextureLayer.Length; i++)
            {
                if (i != cbLayer.ItemIndex)
                {
                    foreach (ImageBox img in ListTextureLayer[i])
                    {
                        img.Enabled = false;
                        img.Color = Color.PaleVioletRed;
                    }
                }
                else
                {
                    foreach (ImageBox img in ListTextureLayer[i])
                    {
                        img.Enabled = true;
                        img.Color = Color.White;
                    }
                }
            }
        }

        private void GetData()
        {
            // initialize list texture layer
            for (int i = 0; i < ListTextureLayer.Length; i++)
            {
                ListTextureLayer[i] = new List<ImageBox>();
            }

            XmlContent.Map.MapList maplist = manager.Game.Content.Load<XmlContent.Map.MapList>("Map\\MapList");

            foreach (string text in maplist.name)
            {
                cbListMap.Items.Add(text);
            }

            cbListMap.ItemIndex = 0;

            cbListType.Items.Add("Titles");
            cbListType.Items.Add("Objects");
            cbListType.Items.Add("Back");

            cbListType.ItemIndex = 2;

            cbListObj.Items.Add("artificiality");
            cbListObj.Items.Add("foot");
            cbListObj.Items.Add("nature");

            cbListObj.ItemIndex = 0;

            for (int i = 1; i <= ListTextureLayer.Length; i++)
            {
                cbLayer.Items.Add("Layer " + i.ToString());
            }

            cbLayer.ItemIndex = 0;
        }

        #endregion Method Titles

        public void InsTitles()
        {
            wintitles = new Panel(manager);
            wintitles.Init();
            wintitles.Text = "Titles Editor";
            wintitles.Visible = false;
            wintitles.Resizable = false;
            wintitles.Movable = true;
            wintitles.Top = 3;
            wintitles.Left = 2;
            wintitles.Width = 350;
            wintitles.Height = 387;
            wintitles.Alpha = 200;
            manager.Add(wintitles);

            ListTextureMap = new ListBox(manager);
            ListTextureMap.Init();
            ListTextureMap.Top = 240;
            ListTextureMap.Left = 171;
            ListTextureMap.Width = 173;
            ListTextureMap.Height = 108;
            ListTextureMap.ItemIndexChanged += new EventHandler(ListTextureMap_ItemIndexChanged);

            ListTextureHave = new ListBox(manager);
            ListTextureHave.Init();
            ListTextureHave.Top = 32;
            ListTextureHave.Left = 5;
            ListTextureHave.Width = 160;
            ListTextureHave.Height = 316;
            ListTextureHave.ItemIndexChanged += new EventHandler(ListTextureHave_ItemIndexChanged);
            ListTextureHave.MousePress += new MouseEventHandler(ListTextureHave_MousePress);

            ImgDisplayTexture = new ImageBox(manager);
            ImgDisplayTexture.Init();
            ImgDisplayTexture.Top = 32;
            ImgDisplayTexture.Width = 173;
            ImgDisplayTexture.Height = 173;
            ImgDisplayTexture.Left = 171;
            ImgDisplayTexture.SizeMode = SizeMode.Stretched;

            btnSave = new Button(manager);
            btnSave.Init();
            btnSave.Text = "Save";
            btnSave.Top = 357;
            btnSave.Left = 269;
            btnSave.Width = 75;
            btnSave.Height = 23;
            btnSave.Click += new EventHandler(btnSave_Click);

            cbListType = new ComboBox(manager);
            cbListType.Init();
            cbListType.Top = 8;
            cbListType.Left = 5;
            cbListType.Width = 60;
            cbListType.Height = 21;
            cbListType.ItemIndexChanged += new EventHandler(cbListType_ItemIndexChanged);

            cbListMap = new ComboBox(manager);
            cbListMap.Init();
            cbListMap.Top = 8;
            cbListMap.Left = 171;
            cbListMap.Width = 173;
            cbListMap.Height = 21;

            cbListObj = new ComboBox(manager);
            cbListObj.Init();
            cbListObj.Top = 8;
            cbListObj.Left = 71;
            cbListObj.Width = 94;
            cbListObj.Height = 21;
            cbListObj.ItemIndexChanged += new EventHandler(cbListObj_ItemIndexChanged);

            cbLayer = new ComboBox(manager);
            cbLayer.Init();
            cbLayer.Top = 211;
            cbLayer.Left = 171;
            cbLayer.Width = 173;
            cbLayer.Height = 21;
            cbLayer.ItemIndexChanged += new EventHandler(cbLayer_ItemIndexChanged);

            wintitles.Add(ListTextureMap);
            wintitles.Add(ListTextureHave);
            wintitles.Add(ImgDisplayTexture);
            wintitles.Add(btnSave);
            wintitles.Add(cbListType);
            wintitles.Add(cbListMap);
            wintitles.Add(cbListObj);
            wintitles.Add(cbLayer);
            GetData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            map.MapLayer = new Layer[10];
            // add all temp texture in all layer to map layer
            for (int i = 0; i < ListTextureLayer.Length; i++)
            {
                map.MapLayer[i] = new Layer();
                foreach (ImageBox img in ListTextureLayer[i])
                {
                    map.MapLayer[i].AddTexture(img.Image, img.ControlRect, (string)((List<object>)img.Tag)[2], img.ControlRect.X + ":" + img.ControlRect.Y + ":" + img.ControlRect.Width + ":" + img.ControlRect.Height);
                }
            }
            // remove all texture control in screen
            for (int i = 0; i < ListTextureLayer.Length; i++)
            {
                foreach (ImageBox img in ListTextureLayer[i])
                {
                    img.Dispose();
                }
            }

            ListTextureMap.Items.Clear();
            wintitles.Visible = false;
        }

        private void cbLayer_ItemIndexChanged(object sender, EventArgs e)
        {
            ListTextureMap.Items.Clear();
            foreach (ImageBox img in ListTextureLayer[cbLayer.ItemIndex])
            {
                List<object> temp = (List<object>)img.Tag;

                ListTextureMap.Items.Add((string)temp[1]);
                img.Tag = new List<object> { ListTextureMap.Items.Count - 1, temp[1], temp[2] };
            }
            // check for
            for (int i = 0; i < ListTextureLayer.Length; i++)
            {
                if (i != cbLayer.ItemIndex)
                {
                    foreach (ImageBox img in ListTextureLayer[i])
                    {
                        img.Enabled = false;
                        img.Color = Color.PaleVioletRed;
                    }
                }
                else
                {
                    foreach (ImageBox img in ListTextureLayer[i])
                    {
                        img.Enabled = true;
                        img.Color = Color.White;
                    }
                }
            }
        }

        private void cbListObj_ItemIndexChanged(object sender, EventArgs e)
        {
            ListTextureHave.Items.Clear();
            TListTextureHave.Clear();
            GetTextureHave();
        }

        private void cbListType_ItemIndexChanged(object sender, EventArgs e)
        {
            ListTextureHave.Items.Clear();
            TListTextureHave.Clear();
            GetTextureHave();
        }

        private void ListTextureHave_MousePress(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                Texture2D temp = TListTextureHave[ListTextureHave.ItemIndex];

                // create new image box
                ImageBox texmap = new ImageBox(manager);
                texmap.Init();
                texmap.Top = 0;
                texmap.Left = 0;
                texmap.Width = temp.Width;
                texmap.Height = temp.Height;
                texmap.Image = temp;
                texmap.Movable = true;
                texmap.SizeMode = SizeMode.Stretched;
                texmap.Resizable = true;
                texmap.MouseDown += new MouseEventHandler(texmap_MouseDown);
                mapeditor.Add(texmap);
                ListTextureMap.Items.Add(ListTextureHave.Items[ListTextureHave.ItemIndex] + " // " + cbListType.Items[cbListType.ItemIndex]);
                texmap.Tag = new List<object> { ListTextureMap.Items.Count - 1, ListTextureHave.Items[ListTextureHave.ItemIndex] + " // " + cbListType.Items[cbListType.ItemIndex], (string)temp.Tag };
                ListTextureLayer[cbLayer.ItemIndex].Add(texmap);
            }
        }

        private void ListTextureMap_ItemIndexChanged(object sender, EventArgs e)
        {
            ImgDisplayTexture.Image = ListTextureLayer[cbLayer.ItemIndex][ListTextureMap.ItemIndex].Image;
        }

        private void ListTextureHave_ItemIndexChanged(object sender, EventArgs e)
        {
            ImgDisplayTexture.Image = TListTextureHave[ListTextureHave.ItemIndex];
        }

        #region Texture in Screen

        private void texmap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButton.Middle)
            {
                if (ListTextureLayer[cbLayer.ItemIndex].Contains((ImageBox)sender))
                {
                    List<object> temp = (List<object>)((ImageBox)sender).Tag;
                    int index = (int)temp[0];
                    ListTextureLayer[cbLayer.ItemIndex].RemoveAt(index);
                    ((ImageBox)sender).Dispose();

                    ListTextureMap.Items.Clear();
                    foreach (ImageBox img in ListTextureLayer[cbLayer.ItemIndex])
                    {
                        ListTextureMap.Items.Add((string)temp[1]);
                        img.Tag = new List<object> { ListTextureMap.Items.Count - 1, ((List<object>)img.Tag)[1], ((List<object>)img.Tag)[2] };
                    }
                }
            }
            if (e.Button == MouseButton.Right)
            {
                ((ImageBox)sender).SendToBack();
            }
        }

        #endregion Texture in Screen

        #endregion Titles Editor

        #region Collusion

        public void UpdateCollusionDraw()
        {
        }

        public void CollusionDraw(SpriteBatch spritebatch)
        {
            if (wincolusion.Visible)
            {
                MouseState mouseState = Mouse.GetState();
                int Xnow = (int)(mouseState.X);
                int Ynow = (int)(mouseState.Y);
                // add
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    for (int xx = 0; xx < (int)numberupdown.Value * 2 + 1; xx++)
                    {
                        for (int yy = 0; yy < (int)numberupdown.Value * 2 + 1; yy++)
                        {
                            int x = Xnow - (int)numberupdown.Value;
                            if ((x + xx) >= map.Size[0]) x = map.Size[0] - 1;
                            else if ((x + xx) < 0) x = 0;
                            else x = x + xx;

                            int y = Ynow - (int)numberupdown.Value;
                            if ((y + yy) >= map.Size[1]) y = map.Size[1] - 1;
                            else if ((y + yy) < 0) y = 0;
                            else y = y + yy;

                            visualmatrix[x, y] = 1;
                        }
                    }
                    map.visualmatrix = visualmatrix;
                    map.UpdateCollusionVisual();
                    collusionmap = map.collusionmap;
                }
                // remove
                if (mouseState.MiddleButton == ButtonState.Pressed)
                {
                    for (int xx = 0; xx < (int)numberupdown.Value * 2 + 1; xx++)
                    {
                        for (int yy = 0; yy < (int)numberupdown.Value * 2 + 1; yy++)
                        {
                            int x = Xnow - (int)numberupdown.Value;
                            if ((x + xx) >= map.Size[0]) x = map.Size[0] - 1;
                            else if ((x + xx) < 0) x = 0;
                            else x = x + xx;

                            int y = Ynow - (int)numberupdown.Value;
                            if ((y + yy) >= map.Size[1]) y = map.Size[1] - 1;
                            else if ((y + yy) < 0) y = 0;
                            else y = y + yy;

                            visualmatrix[x, y] = 0;
                        }
                    }
                    map.visualmatrix = visualmatrix;
                    map.UpdateCollusionVisual();
                    collusionmap = map.collusionmap;
                }
            }
            // draw
            foreach (int[] collusionpoin in collusionmap)
            {
                spritebatch.Draw(pixel, new Rectangle(collusionpoin[0], collusionpoin[1], 1, 1), Color.Red);
            }
        }

        private void CollusionGetData()
        {
            pixel = manager.Game.Content.Load<Texture2D>("Temp\\pixel");
        }

        private void InsCollusion()
        {
            wincolusion = new Window(manager);
            wincolusion.Init();
            wincolusion.Text = "Collusion";
            wincolusion.Top = 0;
            wincolusion.Left = 0;
            wincolusion.Width = 367;
            wincolusion.Height = 40;
            wincolusion.CloseButtonVisible = false;
            wincolusion.Resizable = false;
            wincolusion.Movable = false;
            wincolusion.Visible = false;
            wincolusion.Alpha = 120;

            lbtext = new Label(manager);
            lbtext.Init();
            lbtext.Text = "Size";
            lbtext.Top = 14;
            lbtext.Left = 12;
            lbtext.Width = 35;
            lbtext.Height = 13;
            lbtext.Parent = wincolusion;

            numberupdown = new SpinBox(manager, SpinBoxMode.Range);
            numberupdown.Init();
            numberupdown.Text = "1";
            numberupdown.Value = 1;
            numberupdown.Maximum = 20;
            numberupdown.Minimum = 1;
            numberupdown.Step = 1;
            numberupdown.Top = 12;
            numberupdown.Left = 62;
            numberupdown.Width = 117;
            numberupdown.Height = 20;
            numberupdown.Parent = wincolusion;
            numberupdown.Enabled = true;

            manager.Add(wincolusion);
            CollusionGetData();
        }

        #endregion Collusion
    }
}