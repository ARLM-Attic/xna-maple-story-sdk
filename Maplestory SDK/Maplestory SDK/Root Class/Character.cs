using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Maplestory_SDK.Root_Class
{

    class Character
    {

        string direction = "stand";
        string facing = "left";
        

        int frameCount = 8; // frame count
        int delay; // number frame when texture next positon

        float speed = 0f;
        const float maxspeed = 10f;

        int startY = 0;
        int jumpCount = 0;

        int x = 150;
        int y = 337;

        // biến chứa ID
        public string Skin;
        public string Face;
        public string Hair;
        // khởi tạo texture chứa hình ảnh nhân vật
        Texture2D Pbody;
        Rectangle Bodybounds;
        Texture2D Parm;
        Rectangle Armbounds;
        Texture2D Phead;
        Rectangle Headbounds;
        Texture2D Pface;
        Rectangle Facebounds;
        Texture2D PHair;
        Rectangle Hairbounds;
        // chứa tổng số ảnh mà cần cho di chuyển
        int standmax;
        int walkmax;
        // vị trí ảnh
        int texture_position = 0;
        // biến main dùng để gọi content
        // main variable use for call content
        Run main;
        // hành động của người chơi
        // action of player
        string Action = "Stand";

        /// <summary>
        /// hàm khởi tạo 
        /// </summary>
        /// <param name="main">gọi đến main, mặc định là this</param>
        /// <param name="skinname">tên thư mục chứa da của nhân vật</param>
        /// <param name="facename">tên thư mục chứa mặt của nhân vật</param>
        /// <param name="hairname">tên thư mục chứa Tóc của nhân vật</param>
        /// <param name="standmax">số ảnh khi đứng</param>
        /// <param name="walkmax">số ảnh khi đi</param>
        public Character( Run main,string skinname,string facename,string hairname, int standmax, int walkmax)
        {
            // load texture
            Pbody = main.Content.Load<Texture2D>("Character\\Skin\\" + skinname + "\\" + Action + "\\body_" + texture_position.ToString());
            Parm = main.Content.Load<Texture2D>("Character\\Skin\\" + skinname + "\\" + Action + "\\arm_" + texture_position.ToString());
            Phead = main.Content.Load<Texture2D>("Character\\Skin\\" + skinname + "\\Head\\head_front");
            Pface = main.Content.Load<Texture2D>("Character\\Face\\" + facename + "\\default");
            PHair = main.Content.Load<Texture2D>("Character\\Hair\\" + hairname + "\\hairOverHead_default");
            // load variable
            this.main = main;
            this.Skin = skinname;
            this.Face = facename;
            this.Hair = hairname;
            // load bounds
            Bodybounds = new Rectangle(x, y, Pbody.Width, Pbody.Height);
            Armbounds = new Rectangle(x + 16, y + 2, Parm.Width, Parm.Height);
            Headbounds = new Rectangle(x - 5, y - 33, Phead.Width, Phead.Height);
            Facebounds = new Rectangle(x - 1, y - 17, Pface.Width, Pface.Height);
            Hairbounds = new Rectangle(x - 13, y - 41, Pface.Width, Pface.Height);
            // load max image
            this.standmax = standmax;
            this.walkmax = walkmax;
        }

        /// <summary>
        /// update dữ liệu
        /// </summary>
        public void UpdateTexture()
        {
            Pbody = main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\body_" + texture_position.ToString());
            Parm = main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\arm_" + texture_position.ToString());
            Phead = main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\Head\\head_front");
            Pface = main.Content.Load<Texture2D>("Character\\Face\\" + Face + "\\default");
            PHair = main.Content.Load<Texture2D>("Character\\Hair\\" + Hair + "\\hairOverHead_default");
            // đặt lại kích cỡ
            Bodybounds.Width = Pbody.Width;
            Bodybounds.Height = Pbody.Height;
            Armbounds.Width = Parm.Width;
            Armbounds.Height = Parm.Height;
            Headbounds.Width = Phead.Width;
            Headbounds.Height = Phead.Height;
            Facebounds.Width = Pface.Width;
            Facebounds.Height = Pface.Height;
            Hairbounds.Width = PHair.Width;
            Hairbounds.Height = PHair.Height;
        }
        /// <summary>
        /// hiển thị nhân vật lên màn hình
        /// </summary>
        /// <param name="spriteBatch">spriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // debug
            spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Frame : " + texture_position.ToString(), new Vector2(15f, 15f), Color.Black);
            spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Body X : " + Bodybounds.X.ToString() + " - Body Y : " + Bodybounds.Y.ToString(), new Vector2(15f, 25f), Color.Black);
            spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Arm X : " + Armbounds.X.ToString() + " - Arm Y : " + Armbounds.Y.ToString(), new Vector2(15f, 35f), Color.Black);
            spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Head X : " + Headbounds.X.ToString() + " - Head Y : " + Headbounds.Y.ToString(), new Vector2(15f, 45f), Color.Black);
            spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Select 1, 2 to choose skin", new Vector2(15f, 400f), Color.Black);
            spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Select 3, 4, 5, 6 to choose eye", new Vector2(15f, 415f), Color.Black);
            spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Select 7, 8, 9, 0 to choose hair", new Vector2(15f, 430f), Color.Black);
            if (facing == "right")
            {
                spriteBatch.Draw(Pbody, Bodybounds, null, Color.White, 0f,new Vector2(0,0),SpriteEffects.FlipHorizontally,0f);
                // must draw armor or anything.. in here
                spriteBatch.Draw(Parm, Armbounds,null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(Phead, Headbounds,null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(Pface, Facebounds, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(PHair, Hairbounds, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
                // draw hair
            }
            else // left
            {
                spriteBatch.Draw(Pbody, Bodybounds, Color.White);
                // must draw armor or anything.. in here
                spriteBatch.Draw(Parm, Armbounds, Color.White);
                spriteBatch.Draw(Phead, Headbounds, Color.White);
                spriteBatch.Draw(Pface, Facebounds, Color.White);
                spriteBatch.Draw(PHair, Hairbounds, Color.White);
            }
        }

        public void SetPos(int x, int y)
        {
            Bodybounds.X = x;
            Bodybounds.Y = y;
        }

        /// <summary>
        /// update khi nhập từ bàn phím
        /// </summary>
        public void KeyInput()
        {
            // trạng thái keyboard
            KeyboardState keyState = Keyboard.GetState();
            if (direction != "up")
            {

                // khi nhấn nút di chuyển
                if (keyState.IsKeyDown(Keys.Right))
                {

                    direction = "right";
                    facing = "right";
                    Action = "Walk";

                }
                if (keyState.IsKeyDown(Keys.Left))
                {
                    
                    direction = "left";
                    facing = "left";
                    Action = "Walk";
                    
                }
                // move to other map 
                // wait for code
                if (keyState.IsKeyDown(Keys.Up))
                {
                    direction = "stand";
                    Action = "Stand";
                }
                // dont use
                // khi nhả nút di chuyển
                if (keyState.IsKeyUp(Keys.Right) && direction == "right")
                {
                    direction = "stand";
                    facing = "right";
                    Action = "Stand";
                    speed = 0;
                }
                if (keyState.IsKeyUp(Keys.Left) && direction == "left")
                {
                    direction = "stand";
                    facing = "left";
                    Action = "Stand";
                    speed = 0;
                }
            } // end if (direction != "up")
        }

        /// <summary>
        ///  dựa theo file XML trong Character.wz
        ///  fix as xml file in character.wz
        ///  
        ///  tôi sẽ chuyển nó sang file xml sau
        ///  i'll change it to xml file soon
        /// </summary>
        /// <param name="TexturePos"></param>
        public void FixPosition(int TexturePos)
        {
            if (Action == "Stand")
            {
                if (facing == "left")
                {
                    if (texture_position == 0)
                    {
                        Armbounds.X = Bodybounds.X + 16;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Headbounds.X = Bodybounds.X - 4;
                        Headbounds.Y = Bodybounds.Y - 33;
                        Facebounds.X = Bodybounds.X - 1;
                        Facebounds.Y = Bodybounds.Y - 17;
                        Hairbounds.X = Bodybounds.X - 9;
                        Hairbounds.Y = Bodybounds.Y - 41;
                        Bodybounds.X += 1;
                    }
                    if (texture_position == 1)
                    {
                        Armbounds.X = Bodybounds.X + 16;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Headbounds.X = Bodybounds.X - 6;
                        Headbounds.Y = Bodybounds.Y - 33;
                        Facebounds.X = Bodybounds.X - 3;
                        Facebounds.Y = Bodybounds.Y - 17;
                        Hairbounds.X = Bodybounds.X - 11;
                        Hairbounds.Y = Bodybounds.Y - 41;
                        Bodybounds.X -= 1;
                    }
                    if (texture_position == 2)
                    {
                        Armbounds.X = Bodybounds.X + 16;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Headbounds.X = Bodybounds.X - 6;
                        Headbounds.Y = Bodybounds.Y - 33;
                        Facebounds.X = Bodybounds.X - 3;
                        Facebounds.Y = Bodybounds.Y - 17;
                        Hairbounds.X = Bodybounds.X - 11;
                        Hairbounds.Y = Bodybounds.Y - 41;
                        Bodybounds.X -= 1;
                    }
                    if (texture_position == 3)
                    {
                        Armbounds.X = Bodybounds.X + 16;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Headbounds.X = Bodybounds.X - 4;
                        Headbounds.Y = Bodybounds.Y - 33;
                        Facebounds.X = Bodybounds.X - 1;
                        Facebounds.Y = Bodybounds.Y - 17;
                        Hairbounds.X = Bodybounds.X - 9;
                        Hairbounds.Y = Bodybounds.Y - 41;
                        Bodybounds.X += 1;
                    }
                }
                else if (facing == "right")
                {
                    if (texture_position == 0)
                    {
                        Armbounds.X = Bodybounds.X - 4;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Headbounds.X = Bodybounds.X - 10;
                        Headbounds.Y = Bodybounds.Y - 33;
                        Facebounds.X = Bodybounds.X - 0;
                        Facebounds.Y = Bodybounds.Y - 17;
                        Hairbounds.X = Bodybounds.X - 13;
                        Hairbounds.Y = Bodybounds.Y - 41;
                    }
                    if (texture_position == 1)
                    {
                        Armbounds.X = Bodybounds.X - 5;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Headbounds.X = Bodybounds.X - 10;
                        Headbounds.Y = Bodybounds.Y - 33;
                        Facebounds.X = Bodybounds.X - 0;
                        Facebounds.Y = Bodybounds.Y - 17;
                        Hairbounds.X = Bodybounds.X - 13;
                        Hairbounds.Y = Bodybounds.Y - 41;
                    }
                    if (texture_position == 2)
                    {
                        Armbounds.X = Bodybounds.X - 4;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Headbounds.X = Bodybounds.X - 10;
                        Headbounds.Y = Bodybounds.Y - 33;
                        Facebounds.X = Bodybounds.X - 0;
                        Facebounds.Y = Bodybounds.Y - 17;
                        Hairbounds.X = Bodybounds.X - 13;
                        Hairbounds.Y = Bodybounds.Y - 41;
                    }
                    if (texture_position == 3)
                    {
                        Armbounds.X = Bodybounds.X - 4;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Headbounds.X = Bodybounds.X - 10;
                        Headbounds.Y = Bodybounds.Y - 33;
                        Facebounds.X = Bodybounds.X - 0;
                        Facebounds.Y = Bodybounds.Y - 17;
                        Hairbounds.X = Bodybounds.X - 13;
                        Hairbounds.Y = Bodybounds.Y - 41;
                    }
                }
            } // end if (Action == "Stand")
            else if (Action == "Walk")
            {
                if (direction == "left")
                {
                    if (texture_position == 0)
                    {
                        Headbounds.X = Bodybounds.X - 6;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X + 19;
                        Armbounds.Y = Bodybounds.Y + 3;
                        Facebounds.X = Bodybounds.X - 3;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 10;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                    if (texture_position == 1)
                    {
                        Headbounds.X = Bodybounds.X - 5;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X + 9;
                        Armbounds.Y = Bodybounds.Y + 4;
                        Facebounds.X = Bodybounds.X - 2;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 10;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                    if (texture_position == 2)
                    {
                        Headbounds.X = Bodybounds.X - 5;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X + 19;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Facebounds.X = Bodybounds.X - 2;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 10;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                    if (texture_position == 3)
                    {
                        Headbounds.X = Bodybounds.X - 5;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X + 21;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Facebounds.X = Bodybounds.X - 2;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 10;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                    if (texture_position == 4)
                    {
                        Headbounds.X = Bodybounds.X - 6;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X + 17;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Facebounds.X = Bodybounds.X - 3;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 10;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                }
                else if (direction == "right")
                {
                    if (texture_position == 0)
                    {
                        Headbounds.X = Bodybounds.X - 8;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X - 4;
                        Armbounds.Y = Bodybounds.Y + 3;
                        Facebounds.X = Bodybounds.X + 2;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 10;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                    if (texture_position == 1)
                    {
                        Headbounds.X = Bodybounds.X - 7;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X + 3;
                        Armbounds.Y = Bodybounds.Y + 4;
                        Facebounds.X = Bodybounds.X + 3;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 9;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                    if (texture_position == 2)
                    {
                        Headbounds.X = Bodybounds.X - 7;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X - 6;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Facebounds.X = Bodybounds.X + 3;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 9;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                    if (texture_position == 3)
                    {
                        Headbounds.X = Bodybounds.X - 6;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X - 6;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Facebounds.X = Bodybounds.X + 4;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 8;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                    if (texture_position == 4)
                    {
                        Headbounds.X = Bodybounds.X - 8;
                        Headbounds.Y = Bodybounds.Y - 32;
                        Armbounds.X = Bodybounds.X - 6;
                        Armbounds.Y = Bodybounds.Y + 2;
                        Facebounds.X = Bodybounds.X + 2;
                        Facebounds.Y = Bodybounds.Y - 16;
                        Hairbounds.X = Bodybounds.X - 10;
                        Hairbounds.Y = Bodybounds.Y - 40;
                    }
                }
            } // end if (Action == "Walk")
                
        }

        public void Move()
        {
            if (Action == "Stand")
            {
                delay = 8;
            }
            else if (Action == "Walk")
            {
                delay = 4;
            }

            // chuyển động
            if (frameCount % delay == 0)
            {
                switch (direction)
                {
                    case "stand":

                        // di chuyển ảnh
                        //Bodybounds.X += (int)speed;
                        // nếu vị trí ảnh lớn hơn số lượng ảnh
                        if (texture_position >= standmax)
                            texture_position = 0; // đặt vị trí về 0

                        FixPosition(texture_position);
                        // update lại hình ảnh
                        UpdateTexture();
                        break;
                    case "left":
                        if (speed > -maxspeed)
                            speed -= 2;

                        // di chuyển ảnh
                        Bodybounds.X += (int)speed;

                        if (texture_position >= walkmax)
                            texture_position = 0; // đặt vị trí về 0

                        FixPosition(texture_position);
                        UpdateTexture();
                        break;
                    case "right":
                        if (speed < maxspeed)
                            speed += 2;

                        // di chuyển ảnh
                        Bodybounds.X += (int)speed;

                        if (texture_position >= walkmax)
                            texture_position = 0; // đặt vị trí về 0

                        FixPosition(texture_position);
                        UpdateTexture();
                        break;
                }

                texture_position++; // cộng vị trí ảnh thêm 1
            } // end if
            frameCount++; // xác định độ trễ khi chạy ảnh
        }

        /*
        public void KeyInput()
        {
            KeyboardState keyState = Keyboard.GetState();
            // move or stand
            if (direction != "up")
            {
                if (keyState.IsKeyDown(Keys.Right))
                {
                    direction = "right";
                    facing = "right";
                }
                if (keyState.IsKeyDown(Keys.Left))
                {
                    direction = "left";
                    facing = "left";
                }
                if (keyState.IsKeyDown(Keys.Up))
                {
                    direction = "up";
                }
                if (keyState.IsKeyUp(Keys.Right) && direction == "right")
                {
                    direction = "stand";
                    facing = "right";
                }
                if (keyState.IsKeyUp(Keys.Left) && direction == "left")
                {
                    direction = "stand";
                    facing = "left";
                }
            }
            else // jumping
            {
                if (keyState.IsKeyDown(Keys.Right))
                {
                    if (speed < maxspeed)
                        speed += .2f;
                    facing = "right";
                }
                if (keyState.IsKeyDown(Keys.Left))
                {
                    if (speed > -maxspeed)
                        speed -= .2f;
                    facing = "left";
                }
            }
        }

        bool debug = false;
        public void Move()
        {
            if (direction != "up" && !CheckCollision(bounds) && debug)
            {
                //fall
                startY = bounds.Y + 99;
                direction = "up";
                jumpCount = 11;
            }

            if (frameCount % delay == 0)
            {
                switch (direction)
                {
                    case "stand":
                        Equalize(2);
                        bounds.X += (int)speed;
                        if (frameCount / delay >= 4)
                            frameCount = 0;
                        srcBounds = new Rectangle(frameCount / delay * 40, 0, 40, 48);
                        break;
                    case "left":
                        if (speed > -maxspeed)
                            speed -= 1;
                        bounds.X += (int)speed;
                        if (frameCount / delay >= 8)
                            frameCount = 0;
                        srcBounds = new Rectangle(frameCount / delay * 40, 48, 40, 48);
                        break;
                    case "right":
                        if (speed < maxspeed)
                            speed += 1;
                        bounds.X += (int)speed;
                        if (frameCount / delay >= 8)
                            frameCount = 0;
                        srcBounds = new Rectangle(frameCount / delay * 40, 48, 40, 48);
                        break;
                    case "up":
                        debug = true;
                        if (speed > -4 && speed < 4)
                            srcBounds.Y = 96;
                        else
                            srcBounds.Y = 48;
                        if (srcBounds.Y == 0 || srcBounds.Y == 96)
                        {
                            if (jumpCount < 2)
                            {
                                if (frameCount / delay >= 9)
                                    frameCount = 0;
                            }
                            else if (jumpCount > 2 && jumpCount <= 10)
                            {
                                if (frameCount / delay > 3)
                                    frameCount = 2 * delay;
                            }
                            else if (jumpCount > 10 && jumpCount <= 18)
                            {
                                if (frameCount / delay > 5)
                                    frameCount = 4 * delay;
                            }
                            else if (jumpCount > 18 && CheckCollision(new Rectangle(bounds.X, bounds.Y + Map.size, bounds.Width, bounds.Height)))
                            {
                                if (frameCount / delay >= 9)
                                    frameCount = 0;
                            }
                            else if (jumpCount > 18) //if he's falling a long time, don't loop the animation where he hits the ground
                                frameCount = 4 * delay;

                            srcBounds = new Rectangle(frameCount / delay * 40, 96, 40, 48);
                        }
                        else if (srcBounds.Y == 48)
                        {
                            if (frameCount / delay >= 8)
                                frameCount = 0;
                            if (jumpCount <= 10)
                                srcBounds = new Rectangle((frameCount / delay) / 2 * 40, 48, 40, 48);
                            else
                                srcBounds = new Rectangle(frameCount / delay * 40, 48, 40, 48);
                        }
                        if (jumpCount == 0)
                            startY = bounds.Y;

                        //Sets function for determining jump y-position
                        float jump_positionY = (jumpCount - 10) * (jumpCount - 10) - 100 + startY;
                        //Sets terminal velocity
                        float max_velocityY = 2 * (20 - 10); //derivative of jump_positionY where 20 is max jumpCount value
                        float max_positionY = startY + max_velocityY * (jumpCount - 20);

                        if (jumpCount >= 20)
                            jump_positionY = max_positionY;

                        bounds = new Rectangle(bounds.X + (int)speed,
                            (int)jump_positionY, 40, 48);
                        jumpCount++;
                        break;
                }
            }

            //Check collision
            if (jumpCount >= 10 && CheckCollision(bounds))
            {
                //Stop
                direction = "stand";
                jumpCount = 0;
            }

            frameCount++;
        }

        public int[] GetPos()
        {
            return new int[] { bounds.X, bounds.Y };
        }
        public void SetPos(int x, int y)
        {
            bounds.X = x;
            bounds.Y = y;
        }


        //Checks collision
        private bool CheckCollision(Rectangle bounds)
        {
            int cutoff = bounds.Y + Map.size;

            //Create a rectangle for the current block, if it is not -1
            Rectangle tempBounds = new Rectangle(bounds.X, cutoff, bounds.Width, cutoff - bounds.Y);
            tempBounds.Y += tempBounds.Height - Map.size;

            int x = (tempBounds.X + bounds.Width / 2) / Map.size;
            int y = tempBounds.Y / Map.size + 1;
            int type = Map.getType(y, x);

            if (type > -1)
            {
                //Test to see if the character collides
                Rectangle tileBounds = new Rectangle(x * Map.size, y * Map.size, Map.size, Map.size);
                if (tempBounds.Intersects(tileBounds))
                {
                    this.bounds = new Rectangle(bounds.X, tileBounds.Y - bounds.Height, bounds.Width, bounds.Height);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Equalize(int i)
        {
            for (int k = 0; k < i; k++)
            {
                if (speed < 0f)
                    speed += 1;
                else if (speed > 0f)
                    speed -= 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (facing == "right")
                spriteBatch.Draw(texture, bounds, srcBounds, Color.White);
            else
                spriteBatch.Draw(texture, bounds, srcBounds, Color.White,
                    0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
        }

        */
    }
}
