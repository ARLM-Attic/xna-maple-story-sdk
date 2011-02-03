using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maplestory_SDK.Root_Class
{
    internal class PlayerBase
    {
        #region Declare

        Map map;

        Rectangle Playerrec;
        Texture2D pixel;

        int visualX;
        int visualY;
        int visualH;

        int visualUp;
        int visualDown;
        int visualLeft;
        int visualRight;

        bool stopanimation;

        int gravite = 45;
        float fallspeed = 0;

        KeyboardState keyState;
        KeyboardState oldState;
        public string direction = "stand";
        public string Action = "Stand";
        public string Facing = "left"; // left or right
        public string HeadPos = "front"; // front or back
        public string WeaponType;
        public string AttackType = "1";

        public bool DEBUGMODE = false;
        public string debugselect = "";
        public int debugx, debugy;

        public int texture_position;

        Run Main;

        public string Skin;
        public string Face;
        public string Hair;

        public Texture2D TSkin;
        public Texture2D THead;
        public Texture2D TFace;
        public Texture2D THair;
        public Texture2D TArm;

        public Rectangle RSkin;
        public Rectangle RHead;
        public Rectangle RFace;
        public Rectangle RHair;
        public Rectangle RArm;
        // gadget type 1 only for face and hair
        public XmlContent.Gadget1 GFace;
        public XmlContent.Gadget1 GHair;
        // gadget type 2 for other
        public XmlContent.Gadget2 GHead;
        public XmlContent.Gadget2 GArm;

        public int framecount = 8;
        public int delay = 1000;

        public int x = 150;
        public int y = 100;

        public double speed = 0f;
        public double maxspeed = 10.9f;

        public int startY = 0;
        public int jumpCount = 0;
        public int maxjumpcount = 10;
        public float jump_positionY;

        public int attackcount = 0;
        public int attackdelay = 24;

        public bool iscreatexml = false;
        public int attackpose = 0;
        public XmlDocument xml;

        public bool isaddcomplete = false;
        public bool iscreatexmlcomplete = false;
        public string link;

        #endregion Declare

        /// <summary>
        /// initialize Player
        /// </summary>
        /// <param name="_Main"></param>
        /// <param name="_Skin">Skin name</param>
        /// <param name="_Face">face name</param>
        /// <param name="_Hair">hair name</param>
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

            pixel = Main.Content.Load<Texture2D>("Temp\\pixel");

            loadAllXML();
        }

        /// <summary>
        ///  it's really useful
        /// </summary>
        private void loadAllXML()
        {
            GHead = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\Head\\head");
            GFace = Main.Content.Load<XmlContent.Gadget1>("Character\\Face\\" + Face + "\\" + Face);
            GHair = Main.Content.Load<XmlContent.Gadget1>("Character\\Hair\\" + Hair + "\\" + Hair);
            GArm = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\arminfo");
        }

        /// <summary>
        /// key input
        /// </summary>
        public void KeyInput()
        {
            keyState = Keyboard.GetState();
            // debug mode
            if (DEBUGMODE == true)
            {
                DebufModeMethod();
            }
            else
            {
                if (keyState.IsKeyDown(Keys.PageDown))
                {
                    loadAllXML();
                }
                if (direction != "up" && direction != "down")
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
                    if (KeypressTest(Keys.X))
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
                    if (KeypressTest(Keys.Z))
                    {
                        if (attackcount > attackdelay)
                        {
                            texture_position = 0;
                            Action = "Attack";
                            attackcount = 0;
                        }
                    }
                } // end if (direction != "up")
                else if (direction == "up")// jumping
                {
                    if (KeypressTest(Keys.Z))
                    {
                        if (attackcount > attackdelay)
                        {
                            texture_position = 0;
                            Action = "Attack";
                            attackcount = 0;
                        }
                    }
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
                else if (direction == "down")// jumping
                {
                    if (KeypressTest(Keys.Z))
                    {
                        if (attackcount > attackdelay)
                        {
                            texture_position = 0;
                            Action = "Attack";
                            attackcount = 0;
                        }
                    }
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
            oldState = keyState;
        }

        /// <summary>
        /// use for update
        /// i dont think i need it now, but maybe in furture
        /// </summary>
        public void Update(Map _map, SpriteBatch spriteBatch)
        {
            stopanimation = false;
            // update attack delay
            attackcount++;
            //update map
            map = _map;
            // update player collusion
            Playerrec = new Rectangle(x, y, RSkin.Width, RSkin.Height);
            visualX = x - (RSkin.Width / 2);
            visualY = y;
            visualH = y + RSkin.Height;
            //if (Action == "Stand") EqualizeSpeed();
            if (direction != "up")
                CheckFall();
            if (map.visualmatrix[x, y] == 1)
            {
                y -= 1;
            }
        }

        /// <summary>
        /// check left collison
        /// </summary>
        public void CheckLeft()
        {
            bool passleft = CheckPassable(x, y, 4);
            int xnext = x;
            if (speed > -maxspeed)
                speed -= 1f;
            // check pass
            for (int i = 0; i > (int)speed; i--)
            {
                if (x + i < 0) xnext = 0;
                xnext = x + i;

                // get pass info

                passleft = CheckPassable(xnext, y, 4);

                if (!passleft)
                {
                    // check
                    for (int j = 5; j > -1; j--)
                    {
                        // check pass at point
                        passleft = CheckPassable(xnext, y - j, 4);
                        if (!passleft)
                        {
                            if (speed < maxspeed)
                                speed += 1f;
                            y -= j + 1;
                            x += i - 1;
                            break;
                        }
                    }
                    break;
                }
            }
            if (passleft)
            {
                x += (int)speed;
            }
        }

        /// <summary>
        /// check right collision
        /// </summary>
        public void CheckRight()
        {
            bool passright = CheckPassable(x, y, 6);
            int xnext = x;
            if (speed < maxspeed)
                speed += 1f;

            // check pass
            for (int i = 0; i <= (int)speed; i++)
            {
                if (x + i >= map.Size[0]) xnext = map.Size[0] - 2;
                xnext = x + i;

                // get pass info

                passright = CheckPassable(xnext, y, 6);

                if (!passright)
                {
                    // check
                    for (int j = 5; j > -1; j--)
                    {
                        // check pass at point
                        passright = CheckPassable(xnext, y - j, 6);
                        if (!passright)
                        {
                            if (speed < maxspeed)
                                speed += 1f;
                            y -= j + 1;
                            x += i + 1;
                            break;
                        }
                    }
                    break;
                }
            }
            if (passright)
            {
                x += (int)speed;
            }
        }

        /// <summary>
        /// check if dont have any block below
        /// actor will fall
        /// </summary>
        public void CheckFall()
        {
            bool pass = CheckPassable(x, y, 2);
            for (int i = 0; i <= (int)fallspeed; i++)
            {
                pass = CheckPassable(x, y + i, 2);
                if (!pass)
                {
                    y += i;
                    break;
                }
            }

            if (pass)
            {
                if (fallspeed < gravite)
                    fallspeed += 1f;
                else
                    fallspeed = gravite;
                if (fallspeed > 4)
                {
                    texture_position = 0;
                    Action = "Jump";
                }
                direction = "down";
            }
            else
            {
                if (Action == "Jump") Action = "Stand";
                if (direction == "down") direction = Facing;
                fallspeed = 0;
            }
        }

        /// <summary>
        /// use for equalize player speed to 0
        /// </summary>
        private void EqualizeSpeed()
        {
            if (speed > 0)
                speed -= .1f;
            if (speed < 0)
                speed += .1f;
        }

        public bool CheckPassable(int x, int y, int direct)
        {
            if (x < 1) x = 1;
            if (x > map.Size[0] - 1) x = map.Size[0] - 3;
            if (y < 1) y = 1;
            if (y > map.Size[1] - 1) y = map.Size[1] - 3;

            visualUp = map.visualmatrix[x, y - 1];
            visualDown = map.visualmatrix[x, y + 1];
            visualLeft = map.visualmatrix[x - 1, y];
            visualRight = map.visualmatrix[x + 1, y];

            switch (direct)
            {
                case 2:
                    if (visualDown == 1) return false;
                    break;
                case 4:
                    if (visualLeft == 1) return false;
                    break;
                case 6:
                    if (visualRight == 1) return false;
                    break;
                case 8:
                    if (visualUp == 1) return false;
                    break;
            }
            return true;
        }

        /// <summary>
        /// Draw Character
        /// </summary>
        /// <param name="spriteBatch">dont care of this</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw information
            spriteBatch.DrawString(Main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"),
                "Frame : " + texture_position.ToString() +
                "  -  Jump : " + jumpCount.ToString() +
                "  -  Action : " + Action +
                "  -  Direction : " + direction +
                "  -  Delay : " + delay +
                "  -  Speed : " + speed
                , new Vector2(15f, 5f), Color.Black);

            try
            {
                spriteBatch.DrawString(Main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"),
                               "Location : " + x.ToString() + " : " + y.ToString() +
                               "  -  Fall Speed : " + fallspeed.ToString() +
                               "  -  Collusion this point : " + map.visualmatrix[x, y].ToString()
                               , new Vector2(15f, 15f), Color.Black);
            }
            catch (System.Exception ex)
            {
            }

            spriteBatch.Draw(pixel, new Rectangle(x, y, 4, 4), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(x, y, 2, 2), Color.Aqua);

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

        /// <summary>
        /// Use for update character texture
        /// </summary>
        public void UpdateTexture()
        {
            // declare action by number
            int getaction = 0;
            if (Action == "Stand") getaction = 0;
            if (Action == "Walk") getaction = 1;
            if (Action == "Jump") getaction = 2;
            if (Action == "Attack") getaction = 0;
            // load texture
            if (Action == "Attack")
            {
                TSkin = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\" + WeaponType + "\\" + AttackType + "\\body_" + texture_position.ToString());
                TArm = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\" + WeaponType + "\\" + AttackType + "\\arm_" + texture_position.ToString());
                GArm = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\" + Action + "\\" + WeaponType + "\\" + AttackType + "\\arminfo");
                GHead = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\" + Action + "\\" + WeaponType + "\\" + AttackType + "\\head");
            }
            else
            {
                TSkin = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\body_" + texture_position.ToString());
                TArm = Main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\arm_" + texture_position.ToString());
                GArm = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\arminfo");
                GHead = Main.Content.Load<XmlContent.Gadget2>("Character\\Skin\\" + Skin + "\\Head\\head");
            }
            // fix position
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

        /// <summary>
        /// Get all delay action of character
        /// right, u can change this value if u want
        /// but, i think it ok! :)
        /// dont touch that if u dont know wat u r doing.
        /// </summary>
        private void GetPlayerDelay()
        {
            if (!DEBUGMODE)
            {
                if (Action == "Stand") delay = 8;
                if (Action == "Walk") delay = 3;
                if (Action == "Jump") delay = 3;
                if (Action == "Attack" && WeaponType == "Hand") delay = 6;
                if (Action == "Attack" && WeaponType == "Sword") delay = 8;
                if (Action == "Attack" && WeaponType == "Gun") delay = 8;
                if (Action == "Attack" && WeaponType == "Bow") delay = 4;
            }
        }

        /// <summary>
        /// it's contain all animation of character
        /// </summary>
        public void Animation()
        {
            if (!DEBUGMODE)
            {
                if (!stopanimation)
                {
                    GetPlayerDelay();
                    //update texture : Improve smooth
                    UpdateTexture();
                    // update position of texture for animation
                    if (framecount % delay == 0)
                    {
                        if (direction == "up")
                        {
                            if (Action != "Attack")
                                texture_position = -1;

                            if (jumpCount == 0)
                                startY = y;
                            //Sets function for determining jump y-position
                            jump_positionY = (jumpCount - 10) * (jumpCount - 10) - 100 + startY;
                            x += (int)speed;
                            y = (int)jump_positionY;
                            jumpCount++;
                            // if jump complete
                            if (jumpCount > maxjumpcount)
                            {
                                jumpCount = 0;
                                Action = "Jump";
                                direction = "down";
                            }
                        }
                        if (direction == "down")
                        {
                            if (Action != "Attack")
                                texture_position = -1;

                            y += (int)fallspeed;
                            x += (int)speed;
                        }
                        switch (Action)
                        {
                            case "Stand":
                                if (texture_position >= 3)
                                    texture_position = -1;
                                break;
                            case "Walk":
                                if (Facing == "left")
                                    CheckLeft();
                                else
                                    CheckRight();
                                if (texture_position >= 3)
                                    texture_position = -1;
                                break;
                            case "Attack":
                                switch (WeaponType)
                                {
                                    case "Hand":
                                        {
                                            switch (AttackType)
                                            {
                                                case "1":
                                                case "2":
                                                    {
                                                        if (texture_position >= 1)
                                                        {
                                                            texture_position = -1;
                                                            if (direction == "up" || direction == "down")
                                                                Action = "Jump";
                                                            else
                                                                Action = "Stand";
                                                        }
                                                        break;
                                                    }
                                                case "3":
                                                    {
                                                        if (texture_position >= 2)
                                                        {
                                                            texture_position = -1;
                                                            if (direction == "up" || direction == "down")
                                                                Action = "Jump";
                                                            else
                                                                Action = "Stand";
                                                        }
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case "Sword":
                                        {
                                            if (texture_position >= 2)
                                            {
                                                texture_position = -1;
                                                if (direction == "up" || direction == "down")
                                                    Action = "Jump";
                                                else
                                                    Action = "Stand";
                                            }
                                            break;
                                        }
                                    case "Gun":
                                        {
                                            if (texture_position >= 4)
                                            {
                                                texture_position = -1;
                                                if (direction == "up" || direction == "down")
                                                    Action = "Jump";
                                                else
                                                    Action = "Stand";
                                            }
                                            break;
                                        }
                                    case "Bow":
                                        {
                                            if (texture_position >= 1)
                                            {
                                                texture_position = -1;
                                                if (direction == "up" || direction == "down")
                                                    Action = "Jump";
                                                else
                                                    Action = "Stand";
                                            }
                                            break;
                                        }
                                }
                                break;
                        }
                        texture_position++;
                    }
                    framecount++;
                }
            }
        }

        #region Debug Method

        /// <summary>
        /// Debug method use to update keys press
        /// </summary>
        private void DebufModeMethod()
        {
            speed = 0;

            #region Modifier Gadget

            // modifier gadget
            if (debugselect != "")
            {
                if (KeypressTest(Keys.Down)) // down
                {
                    switch (debugselect)
                    {
                        case "Head":
                            RHead.Y++;
                            debugy = RHead.Y - RSkin.Y + THead.Height;
                            DebugHeadUpdate();
                            break;
                        case "Face":
                            RFace.Y++;
                            debugy = RFace.Y - RHead.Y;
                            break;
                        case "Hair":
                            RHair.Y++;
                            debugy = RHair.Y - RHead.Y;
                            break;
                        case "Arm":
                            RArm.Y++;
                            debugy = RArm.Y - RSkin.Y + TArm.Height;
                            break;
                    }
                }
                if (KeypressTest(Keys.Up)) // up
                {
                    switch (debugselect)
                    {
                        case "Head":
                            RHead.Y--;
                            debugy = RHead.Y - RSkin.Y + THead.Height;
                            DebugHeadUpdate();
                            break;
                        case "Face":
                            RFace.Y--;
                            debugy = RFace.Y - RHead.Y;
                            break;
                        case "Hair":
                            RHair.Y--;
                            debugy = RHair.Y - RHead.Y;
                            break;
                        case "Arm":
                            RArm.Y--;
                            debugy = RArm.Y - RSkin.Y + TArm.Height;
                            break;
                    }
                }
                if (KeypressTest(Keys.Left)) // left
                {
                    switch (debugselect)
                    {
                        case "Head":
                            RHead.X--;
                            if (Facing == "left") debugx = RHead.X - x;
                            if (Facing == "right") debugx = RHead.X - RSkin.X;
                            DebugHeadUpdate();
                            break;
                        case "Face":
                            RFace.X--;
                            debugx = RFace.X - RHead.X;
                            break;
                        case "Hair":
                            RHair.X--;
                            debugx = RHair.X - RHead.X;
                            break;
                        case "Arm":
                            RArm.X--;
                            debugx = RArm.X - x;
                            break;
                    }
                }
                if (KeypressTest(Keys.Right)) // right
                {
                    switch (debugselect)
                    {
                        case "Head":
                            RHead.X++;
                            if (Facing == "left") debugx = RHead.X - x;
                            if (Facing == "right") debugx = RHead.X - RSkin.X;
                            DebugHeadUpdate();
                            break;
                        case "Face":
                            RFace.X++;
                            debugx = RFace.X - RHead.X;
                            break;
                        case "Hair":
                            RHair.X++;
                            debugx = RHair.X - RHead.X;
                            break;
                        case "Arm":
                            RArm.X++;
                            debugx = RArm.X - x;
                            break;
                    }
                }
            }

            #endregion Modifier Gadget

            oldState = keyState;
        }

        /// <summary>
        /// save data as XML file
        /// </summary>
        public void SaveXML()
        {
            if (Action == "Attack")
            {
                if (AttackType == "")
                    link = "C:\\MSSDK_Temp\\" + Action + "_" + WeaponType + "_";
                else
                    link = "C:\\MSSDK_Temp\\" + Action + "_" + WeaponType + "_" + AttackType + "_";
            }
            else
                link = "C:\\MSSDK_Temp\\" + Action + "_";

            Directory.CreateDirectory("C:\\MSSDK_Temp\\");
            xml.Save(link + debugselect + ".xml");
            link = link + debugselect + ".xml";

            iscreatexml = false;
            isaddcomplete = false;
            iscreatexmlcomplete = false;
        }

        /// <summary>
        /// Add value to node
        /// </summary>
        public void AddNodeValue()
        {
            XmlNode leftnode;
            XmlNode rightnode;
            if (debugselect == "Head" || debugselect == "Arm")
            {
                leftnode = xml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0];
                rightnode = xml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[1];
            }
            else
            {
                leftnode = xml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0];
                rightnode = xml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[1];
            }
            if (Facing == "left")
            {
                leftnode.ChildNodes[texture_position].ChildNodes[0].InnerText = texture_position.ToString();
                leftnode.ChildNodes[texture_position].ChildNodes[1].InnerText = debugx.ToString();
                leftnode.ChildNodes[texture_position].ChildNodes[2].InnerText = debugy.ToString();
            }
            else
            {
                rightnode.ChildNodes[texture_position].ChildNodes[0].InnerText = texture_position.ToString();
                rightnode.ChildNodes[texture_position].ChildNodes[1].InnerText = debugx.ToString();
                rightnode.ChildNodes[texture_position].ChildNodes[2].InnerText = debugy.ToString();
            }
            isaddcomplete = true;
        }

        /// <summary>
        /// Create a template xml file
        /// </summary>
        public void CreateXML()
        {
            iscreatexml = true;
            // create new xml
            xml = new XmlDocument();
            XmlDocument temp = new XmlDocument();
            switch (debugselect)
            {
                case "Head":
                case "Arm":
                    {
                        GetAttackPose();
                        xml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><XnaContent><Asset Type=\"XmlContent.Gadget2\"><action><Item><left><Item><pos>0</pos><x>0</x><y>0</y></Item></left><right><Item><pos>0</pos><x>0</x><y>0</y></Item></right></Item></action></Asset></XnaContent>");
                        // get face node
                        XmlNode leftnode = xml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0];
                        XmlNode rightnode = xml.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[1];
                        // create empty xml for add value
                        for (int i = 0; i < attackpose; i++)
                        {
                            leftnode.AppendChild(leftnode.ChildNodes[0].CloneNode(true));
                        }
                        for (int i = 0; i < attackpose; i++)
                        {
                            rightnode.AppendChild(rightnode.ChildNodes[0].CloneNode(true));
                        }
                        break;
                    }
                case "Face":
                case "Hair":
                    {
                        xml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><XnaContent><Asset Type=\"XmlContent.Gadget1\"><Item><left><Item><pos>0</pos><x>0</x><y>0</y></Item></left><right><Item><pos>0</pos><x>0</x><y>0</y></Item></right></Item></Asset></XnaContent>");
                        // get face node
                        break;
                    }
                default:
                    {
                        iscreatexml = false;
                        return;
                    }
            }
        }

        /// <summary>
        /// get number pose of attack
        /// </summary>
        private void GetAttackPose()
        {
            if (Action == "Attack" && WeaponType == "Hand" && AttackType == "1") attackpose = 1;
            if (Action == "Attack" && WeaponType == "Hand" && AttackType == "2") attackpose = 1;
            if (Action == "Attack" && WeaponType == "Hand" && AttackType == "3") attackpose = 2;
            if (Action == "Attack" && WeaponType == "Sword" && AttackType == "1") attackpose = 2;
            if (Action == "Attack" && WeaponType == "Sword" && AttackType == "2") attackpose = 2;
            if (Action == "Attack" && WeaponType == "Sword" && AttackType == "3") attackpose = 2;
            if (Action == "Attack" && WeaponType == "Gun") attackpose = 4;
            if (Action == "Attack" && WeaponType == "Bow") attackpose = 1;

            if (Action == "Stand") attackpose = 3;
            if (Action == "Walk") attackpose = 3;
            if (Action == "Jump") attackpose = 0;
        }

        /// <summary>
        /// use to update head when in debug mode
        /// </summary>
        private void DebugHeadUpdate()
        {
            if (Facing == "left")
            {
                RHead = new Rectangle(x + debugx, RSkin.Y - THead.Height + debugy, THead.Width, THead.Height);
                RHair = new Rectangle(RHead.X + GHair.left[0].x, RHead.Y + GHair.left[0].y, THair.Width, THair.Height);
                RFace = new Rectangle(RHead.X + GFace.left[0].x, RHead.Y + GFace.left[0].y, TFace.Width, TFace.Height);
            }
            else
            {
                RHead = new Rectangle(RSkin.X + debugx, RSkin.Y - THead.Height + debugy, THead.Width, THead.Height);
                RHair = new Rectangle(RHead.X + GHair.right[0].x, RHead.Y + GHair.right[0].y, THair.Width, THair.Height);
                RFace = new Rectangle(RHead.X + GFace.right[0].x, RHead.Y + GFace.right[0].y, TFace.Width, TFace.Height);
            }
        }

        #endregion Debug Method

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
    }
}