using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maplestory_SDK.Root_Class
{
    internal class PlayerBase
    {
        string direction = "stand";
        string Action = "Stand";
        string Facing = "left"; // left or right
        string HeadPos = "front"; // front or back

        int texture_position;

        Run Main;

        public string Skin;
        public string Face;
        public string Hair;

        Texture2D TSkin;
        Texture2D THead;
        Texture2D TFace;
        Texture2D THair;
        Texture2D TArm;

        Rectangle RSkin;
        Rectangle RHead;
        Rectangle RFace;
        Rectangle RHair;
        Rectangle RArm;
        // gadget type 1 only for face and hair
        XmlContent.Gadget1 GFace;
        XmlContent.Gadget1 GHair;
        // gadget type 2 for other
        XmlContent.Gadget2 GHead;
        XmlContent.Gadget2 GArm;

        int framecount = 8;
        int delay = 8;

        int x = 150;
        int y = 350;

        double speed = 0f;
        double maxspeed = 8f;

        int startY = 0;
        int jumpCount = 0;
        int maxjumpcount = 20;
        float jump_positionY;

        public PlayerBase(Run _Main, string _Skin, string _Face, string _Hair)
        {
            Main = _Main;
            Skin = _Skin;
            Face = _Face;
            Hair = _Hair;
            // create texture
            TSkin = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\body_" + texture_position.ToString());
            RSkin = new Rectangle(x - TSkin.Width, y, TSkin.Width, TSkin.Height);

            THead = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\Head\\head_" + HeadPos);
            TFace = Main.Content.Load<Texture2D>("Character\\Face\\" + Face + "\\Stand");
            THair = Main.Content.Load<Texture2D>("Character\\Hair\\" + Hair + "\\hairOverHead_default");
            TArm = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\arm_" + texture_position.ToString());

            RHair = new Rectangle(RHead.X - THair.Width, RHead.Y, THair.Width, THair.Height);
            RHead = new Rectangle(RSkin.X, RSkin.Y - THead.Height, THead.Width, THead.Height);
            RFace = new Rectangle(RHead.X - TFace.Width, RHead.Y, TFace.Width, TFace.Height);
            RArm = new Rectangle(RSkin.X, RSkin.Y - TArm.Height, TArm.Width, TArm.Height);

            GHead = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\Head\\head");
            GFace = Main.Content.Load<XmlContent.Gadget1>("Character\\Face\\" + Face + "\\" + Face);
            GHair = Main.Content.Load<XmlContent.Gadget1>("Character\\Hair\\" + Hair + "\\" + Hair);
            GArm = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\arminfo");
        }

        private void ReloadAllXML()
        {
            GHead = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\Head\\head");
            GFace = Main.Content.Load<XmlContent.Gadget1>("Character\\Face\\" + Face + "\\" + Face);
            GHair = Main.Content.Load<XmlContent.Gadget1>("Character\\Hair\\" + Hair + "\\" + Hair);
            GArm = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\arminfo");
        }

        public void KeyInput()
        {
            // trạng thái keyboard
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.PageDown))
            {
                ReloadAllXML();
            }
            if (direction != "up")
            {
                // khi nhấn nút di chuyển
                if (keyState.IsKeyDown(Keys.Right))
                {
                    direction = "right";
                    Facing = "right";
                    Action = "Walk";
                }
                if (keyState.IsKeyDown(Keys.Left))
                {
                    direction = "left";
                    Facing = "left";
                    Action = "Walk";
                }
                // move to other map
                // wait for code
                if (keyState.IsKeyDown(Keys.X))
                {
                    texture_position = 0;
                    direction = "up";
                    Action = "Jump";
                }
                // dont use
                // khi nhả nút di chuyển
                if (keyState.IsKeyUp(Keys.Right) && direction == "right")
                {
                    direction = "stand";
                    Facing = "right";
                    Action = "Stand";
                    speed = 0;
                }
                if (keyState.IsKeyUp(Keys.Left) && direction == "left")
                {
                    direction = "stand";
                    Facing = "left";
                    Action = "Stand";
                    speed = 0;
                }
            } // end if (direction != "up")
            else // jumping
            {
                if (keyState.IsKeyDown(Keys.Right))
                {
                    if (speed < maxspeed)
                        speed += .2f;
                    Facing = "right";
                }
                if (keyState.IsKeyDown(Keys.Left))
                {
                    if (speed > -maxspeed)
                        speed -= .2f;
                    Facing = "left";
                }
            }
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"),
                "Frame : " + texture_position.ToString() +
                "  -  Jump : " + jumpCount.ToString() +
                "  -  Action : " + Action +
                "  -  Direction : " + direction +
                "  -  Delay : " + delay +
                "  -  Speed : " + speed
                , new Vector2(15f, 5f), Color.Black);
            spriteBatch.DrawString(Main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"),
                "Location : " + x.ToString() + " : " + y.ToString()
                , new Vector2(15f, 15f), Color.Black);
            if (Facing == "left")
            {
                spriteBatch.Draw(TSkin, RSkin, Color.White);
                spriteBatch.Draw(THead, RHead, Color.White);
                spriteBatch.Draw(TFace, RFace, Color.White);
                spriteBatch.Draw(THair, RHair, Color.White);
                spriteBatch.Draw(TArm, RArm, Color.White);
            }
            else
            {
                spriteBatch.Draw(TSkin, RSkin, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(THead, RHead, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(TFace, RFace, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(THair, RHair, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(TArm, RArm, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
            }
        }

        public void UpdateTexture()
        {
            int getaction = 0;
            if (Action == "Stand") getaction = 0;
            if (Action == "Walk") getaction = 1;
            if (Action == "Jump") getaction = 2;

            TSkin = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\body_" + texture_position.ToString());
            TArm = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\arm_" + texture_position.ToString());

            if (Facing == "left")
            {
                RSkin = new Rectangle(x - TSkin.Width, y - TSkin.Height, TSkin.Width, TSkin.Height);
                RHead = new Rectangle(x + GHead.action[getaction].left[texture_position].x, RSkin.Y - THead.Height + GHead.action[getaction].left[texture_position].y, THead.Width, THead.Height);
                RHair = new Rectangle(RHead.X + GHair.left[0].x, RHead.Y + GHair.left[0].y, THair.Width, THair.Height);
                RFace = new Rectangle(RHead.X + GFace.left[0].x, RHead.Y + GFace.left[0].y, TFace.Width, TFace.Height);
                RArm = new Rectangle(x + GArm.action[getaction].left[texture_position].x, RSkin.Y - TArm.Height + GArm.action[getaction].left[texture_position].y, TArm.Width, TArm.Height);
            }
            else
            {
                RSkin = new Rectangle(x - 21, y - TSkin.Height, TSkin.Width, TSkin.Height);
                RHead = new Rectangle(RSkin.X + GHead.action[getaction].right[texture_position].x, RSkin.Y - THead.Height + GHead.action[getaction].right[texture_position].y, THead.Width, THead.Height);
                RHair = new Rectangle(RHead.X + GHair.right[0].x, RHead.Y + GHair.right[0].y, THair.Width, THair.Height);
                RFace = new Rectangle(RHead.X + GFace.right[0].x, RHead.Y + GFace.right[0].y, TFace.Width, TFace.Height);
                RArm = new Rectangle(x + GArm.action[getaction].right[texture_position].x, RSkin.Y - TArm.Height + GArm.action[getaction].right[texture_position].y, TArm.Width, TArm.Height);
            }
        }

        private void GetPlayerDelay()
        {
            if (Action == "Stand") delay = 15;
            if (Action == "Walk") delay = 5;
            if (Action == "Jump") delay = 3;
        }

        public void Animation()
        {
            GetPlayerDelay();
            //update texture : Improve smooth
            UpdateTexture();
            // update position of texture for animation
            if (framecount % delay == 0)
            {
                switch (Action)
                {
                    case "Stand":
                        if (texture_position >= 3)
                            texture_position = -1;
                        break;
                    case "Walk":
                        if (Facing == "left")
                        {
                            if (speed > -maxspeed)
                                speed -= 1f;
                        }
                        else
                        {
                            if (speed < maxspeed)
                                speed += 1f;
                        }
                        x += (int)speed;
                        if (texture_position >= 3)
                            texture_position = -1;
                        break;
                    case "Jump":
                        texture_position = -1;

                        if (jumpCount == 0)
                            startY = RSkin.Y;
                        //Sets function for determining jump y-position
                        jump_positionY = (jumpCount - 10) * (jumpCount - 10) - 100 + startY;
                        x += (int)speed;
                        y = (int)jump_positionY + TSkin.Height;
                        jumpCount++;
                        // if jump complete
                        if (jumpCount > maxjumpcount)
                        {
                            jumpCount = 0;
                            Action = "Stand";
                            direction = "stand";
                            speed = 0;
                            y = (int)jump_positionY + TSkin.Height;
                        }
                        break;
                }
                texture_position++;
            }
            framecount++;
        }
    }
}