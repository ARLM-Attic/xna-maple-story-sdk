////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: ItemBox.cs                                  //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 1/2/2011                                   //
//                                                            //
//       Author: NamKazt                                   //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by NamKazt                              //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

using Microsoft.Xna.Framework;

////////////////////////////////////////////////////////////////////////////
using Microsoft.Xna.Framework.Graphics;

////////////////////////////////////////////////////////////////////////////

#endregion //// Using /////////////

namespace TomShane.Neoforce.Controls
{
    public class ItemBox : Control
    {
        #region //// Fields ////////////

        ////////////////////////////////////////////////////////////////////////////
        private Texture2D backimage = null;
        private Texture2D image = null;
        private SizeMode sizeMode = SizeMode.Normal;
        private Rectangle sourceRect = Rectangle.Empty;
        private int numberitem = 0;
        ////////////////////////////////////////////////////////////////////////////

        #endregion //// Fields ////////////

        #region //// Properties ////////

        ////////////////////////////////////////////////////////////////////////////
        public int NumberItem
        {
            get { return numberitem; }
            set
            {
                numberitem = value;
                Invalidate();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public Texture2D Image
        {
            get { return image; }
            set
            {
                image = value;
                if (image != null)
                    sourceRect = new Rectangle(0, 0, image.Width, image.Height);
                Invalidate();
                if (!Suspended) OnImageChanged(new EventArgs());
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public Texture2D BackImage
        {
            get { return backimage; }
            set
            {
                backimage = value;
                Invalidate();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set
            {
                if (value != null && image != null)
                {
                    int l = value.Left;
                    int t = value.Top;
                    int w = value.Width;
                    int h = value.Height;

                    if (l < 0) l = 0;
                    if (t < 0) t = 0;
                    if (w > image.Width) w = image.Width;
                    if (h > image.Height) h = image.Height;
                    if (l + w > image.Width) w = (image.Width - l);
                    if (t + h > image.Height) h = (image.Height - t);

                    sourceRect = new Rectangle(l, t, w, h);
                }
                else if (image != null)
                {
                    sourceRect = new Rectangle(0, 0, image.Width, image.Height);
                }
                else
                {
                    sourceRect = Rectangle.Empty;
                }
                Invalidate();
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public SizeMode SizeMode
        {
            get { return sizeMode; }
            set
            {
                if (value == SizeMode.Auto && image != null)
                {
                    Width = image.Width;
                    Height = image.Height;
                }
                sizeMode = value;
                Invalidate();
                if (!Suspended) OnSizeModeChanged(new EventArgs());
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        #endregion //// Properties ////////

        #region //// Events ////////////

        ////////////////////////////////////////////////////////////////////////////
        public event EventHandler ImageChanged;
        public event EventHandler SizeModeChanged;
        ////////////////////////////////////////////////////////////////////////////

        #endregion //// Events ////////////

        #region //// Construstors //////

        ////////////////////////////////////////////////////////////////////////////
        public ItemBox(Manager manager)
            : base(manager)
        {
        }

        ////////////////////////////////////////////////////////////////////////////

        #endregion //// Construstors //////

        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        public override void Init()
        {
            base.Init();
            CanFocus = false;
            Color = Color.White;
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            string textnum = "";
            if (numberitem < 10)
                textnum = "0" + numberitem.ToString();
            else
                textnum = numberitem.ToString();

            SkinLayer s = new SkinLayer(Skin.Layers[0]);
            renderer.Draw(backimage, rect.X, rect.Y, new Rectangle(sourceRect.X, sourceRect.Y, backimage.Width, backimage.Height), Color);

            if (image != null)
            {
                if (sizeMode == SizeMode.Normal)
                {
                    renderer.Draw(image, rect.X, rect.Y, sourceRect, Color);
                }
                else if (sizeMode == SizeMode.Auto)
                {
                    renderer.Draw(image, rect.X, rect.Y, sourceRect, Color);
                }
                else if (sizeMode == SizeMode.Stretched)
                {
                    renderer.Draw(image, rect, sourceRect, Color);
                }
                else if (sizeMode == SizeMode.Centered)
                {
                    int x = (rect.Width / 2) - (image.Width / 2);
                    int y = (rect.Height / 2) - (image.Height / 2);

                    renderer.Draw(image, x, y, sourceRect, Color);
                }
                renderer.Draw(renderer.Manager.Content.Load<Texture2D>("Content\\Temp\\slot_num"), new Rectangle(rect.X + 20, rect.Y + 22, 12, 10), Color.White);
                //renderer.DrawString(this, s, numberitem.ToString(), new Rectangle(rect.X + 22, rect.Y + 22, 10, 10));
                renderer.DrawString(renderer.Manager.Content.Load<SpriteFont>("Content\\Fonts\\Arial"), textnum, rect.X + 22, rect.Y + 22, Color.Red);
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected virtual void OnImageChanged(EventArgs e)
        {
            if (ImageChanged != null) ImageChanged.Invoke(this, e);
        }

        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected virtual void OnSizeModeChanged(EventArgs e)
        {
            if (SizeModeChanged != null) SizeModeChanged.Invoke(this, e);
        }

        ////////////////////////////////////////////////////////////////////////////

        #endregion //// Methods ///////////
    }
}