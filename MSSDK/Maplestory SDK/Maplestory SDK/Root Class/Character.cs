//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

//namespace Maplestory_SDK.Root_Class
//{
//    [System.Runtime.InteropServices.GuidAttribute("E144AE01-BEC5-4E5C-9384-D7D38BFBC291")]
//    internal class Character
//    {
//        string direction = "stand";
//        string facing = "left";
//        // debug variable
//        public bool DEBUG = true;
//        public bool INFO = true;

//        int frameCount = 8; // frame count
//        int delay; // number frame when texture next position
//        // speed
//        float speed = 0f;
//        const float maxspeed = 10f;
//        // jump attribute
//        int startY = 0;
//        int jumpCount = 0;
//        const int maxjumpcount = 20;
//        // position
//        int x = 150;
//        int y = 337;
//        // attack count
//        int attackcount;
//        // attack delay
//        public int AttackTimeCount = 25;
//        public int MaxAttackTimeCount = 25;
//        // biến chứa ID
//        public string Skin;
//        public string Face;
//        public string Hair;
//        // equipment name
//        public string AttackType;
//        public string WeaponType;
//        public string Weapon;
//        public string Shield;
//        public string Armor;
//        public string Pant;
//        public string Acc;
//        public string Shoe;
//        public string Glove;
//        public string Cape;
//        public string Hat;
//        // khởi tạo texture chứa hình ảnh nhân vật
//        Texture2D Pbody;
//        public Rectangle Bodybounds;
//        Texture2D Parm;
//        public Rectangle Armbounds;
//        Texture2D Phead;
//        public Rectangle Headbounds;
//        Texture2D Pface;
//        public Rectangle Facebounds;
//        Texture2D PHair;
//        public Rectangle Hairbounds;
//        // equipment
//        Texture2D PWeapon;
//        public Rectangle Weaponbounds;
//        Texture2D PShield;
//        public Rectangle Shieldbounds;
//        Texture2D PArmor;
//        public Rectangle Armorbounds;
//        Texture2D PPant;
//        public Rectangle Pantbounds;
//        Texture2D PAcc;
//        public Rectangle Accbounds;
//        Texture2D PShoe;
//        public Rectangle Shoebounds;
//        Texture2D PGlove;
//        public Rectangle Glovebounds;
//        Texture2D PCape;
//        public Rectangle Capebounds;
//        Texture2D PHat;
//        public Rectangle Hatbounds;
//        // chứa tổng số ảnh mà cần cho di chuyển
//        public int standmax = 4;
//        public int walkmax = 4;
//        // vị trí ảnh
//        public int texture_position = 0;
//        // biến main dùng để gọi content
//        // main variable use for call content
//        Run main;
//        // hành động của người chơi
//        // action of player
//        string Action = "Stand";
//        // create gadget
//        Gadget Ghead;
//        Gadget Garm;
//        Gadget Gface;
//        Gadget Ghair;
//        // equipment gadget
//        Gadget Garmor;
//        Gadget Gshield;
//        Gadget Gweapon;
//        Gadget Gacc;
//        Gadget Gpant;
//        Gadget Gshoe;
//        Gadget Gglove;
//        Gadget Gcape;
//        Gadget Ghat;

//        /// <summary>
//        /// hàm khởi tạo
//        /// </summary>
//        /// <param name="main">gọi đến main, mặc định là this</param>
//        /// <param name="skinname">tên thư mục chứa da của nhân vật</param>
//        /// <param name="facename">tên thư mục chứa mặt của nhân vật</param>
//        /// <param name="hairname">tên thư mục chứa Tóc của nhân vật</param>
//        /// <param name="standmax">số ảnh khi đứng</param>
//        /// <param name="walkmax">số ảnh khi đi</param>
//        public Character(Run main, string skinname, string facename, string hairname)
//        {
//            // load texture
//            Pbody = main.Content.Load<Texture2D>("Character\\Skin\\" + skinname + "\\" + Action + "\\body_" + texture_position.ToString());
//            Parm = main.Content.Load<Texture2D>("Character\\Skin\\" + skinname + "\\" + Action + "\\arm_" + texture_position.ToString());
//            Phead = main.Content.Load<Texture2D>("Character\\Skin\\" + skinname + "\\Head\\head_front");
//            Pface = main.Content.Load<Texture2D>("Character\\Face\\" + facename + "\\default");
//            PHair = main.Content.Load<Texture2D>("Character\\Hair\\" + hairname + "\\hairOverHead_default");
//            // load variable
//            this.main = main;
//            this.Skin = skinname;
//            this.Face = facename;
//            this.Hair = hairname;
//            // load gadget
//            Ghead = main.Content.Load<Gadget>("Character\\Skin\\" + skinname + "\\Head\\head");
//            Garm = main.Content.Load<Gadget>("Character\\Skin\\" + skinname + "\\arm");
//            Gface = main.Content.Load<Gadget>("Character\\Face\\" + facename + "\\" + facename);
//            Ghair = main.Content.Load<Gadget>("Character\\Hair\\" + hairname + "\\" + hairname);
//            // load body and head
//            Bodybounds = new Rectangle(x, y, Pbody.Width, Pbody.Height);
//            Armbounds = new Rectangle(x + 16, y + 2, Parm.Width, Parm.Height);
//            Headbounds = new Rectangle(x - 5, y - 33, Phead.Width, Phead.Height);
//            // gadget
//            Facebounds = new Rectangle(x + Gface.action[0].face[0].position[0].x, y + Gface.action[0].face[0].position[0].y, Pface.Width, Pface.Height);
//            Hairbounds = new Rectangle(x - 13, y - 41, Pface.Width, Pface.Height);
//        }

//        /// <summary>
//        /// update dữ liệu
//        /// </summary>
//        public void UpdateTexture() // BMK update texture
//        {
//            // check if action = attack
//            if (Action == "Attack") // maybe only attack action have diffirent
//            {
//                Pbody = main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\" + WeaponType + "\\" + AttackType + "\\body_" + texture_position.ToString());
//                Parm = main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\" + WeaponType + "\\" + AttackType + "\\arm_" + texture_position.ToString());

//                Garm = main.Content.Load<Gadget>("Character\\Skin\\" + Skin + "\\Attack\\" + WeaponType + "\\" + AttackType + "\\data_arm_" + AttackType);
//                Ghead = main.Content.Load<Gadget>("Character\\Skin\\" + Skin + "\\Attack\\" + WeaponType + "\\" + AttackType + "\\data_head_" + AttackType);
//                Gface = main.Content.Load<Gadget>("Character\\Skin\\" + Skin + "\\Attack\\" + WeaponType + "\\" + AttackType + "\\data_face_" + AttackType);
//                Ghair = main.Content.Load<Gadget>("Character\\Skin\\" + Skin + "\\Attack\\" + WeaponType + "\\" + AttackType + "\\data_hair_" + AttackType);
//            }
//            else // reset
//            { // for move or stand or jumb ...
//                Pbody = main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\body_" + texture_position.ToString());
//                Parm = main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\" + Action + "\\arm_" + texture_position.ToString());

//                Garm = main.Content.Load<Gadget>("Character\\Skin\\" + Skin + "\\arm");
//                Ghead = main.Content.Load<Gadget>("Character\\Skin\\" + Skin + "\\Head\\head");
//                Gface = main.Content.Load<Gadget>("Character\\Face\\" + Face + "\\" + Face);
//                Ghair = main.Content.Load<Gadget>("Character\\Hair\\" + Hair + "\\" + Hair);
//            }
//            // update texture
//            Phead = main.Content.Load<Texture2D>("Character\\Skin\\" + Skin + "\\Head\\head_front");
//            Pface = main.Content.Load<Texture2D>("Character\\Face\\" + Face + "\\default");
//            PHair = main.Content.Load<Texture2D>("Character\\Hair\\" + Hair + "\\hairOverHead_default");
//            // update gadget

//            // đặt lại kích cỡ
//            Bodybounds.Width = Pbody.Width;
//            Bodybounds.Height = Pbody.Height;
//            Armbounds.Width = Parm.Width;
//            Armbounds.Height = Parm.Height;
//            Headbounds.Width = Phead.Width;
//            Headbounds.Height = Phead.Height;
//            Facebounds.Width = Pface.Width;
//            Facebounds.Height = Pface.Height;
//            Hairbounds.Width = PHair.Width;
//            Hairbounds.Height = PHair.Height;

//            // check equipment
//            if (Weapon != "")
//            {
//            }
//            if (Shield != "")
//            {
//            }
//            if (Armor != "")
//            {
//            }
//            if (Pant != "")
//            {
//            }
//            if (Shoe != "")
//            {
//            }
//            if (Glove != "")
//            {
//            }
//            if (Cape != "")
//            {
//            }
//            if (Acc != "")
//            {
//            }
//            if (Shoe != "")
//            {
//            }
//        }

//        /// <summary>
//        /// hiển thị nhân vật lên màn hình
//        /// </summary>
//        /// <param name="spriteBatch">spriteBatch</param>
//        public void Draw(SpriteBatch spriteBatch)
//        {
//            // debug
//            if (INFO == true)
//            {
//                spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Frame : " + texture_position.ToString() + "   Jump Count : " + jumpCount, new Vector2(15f, 5f), Color.Black);
//                spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Body X : " + Bodybounds.X.ToString() + " - Body Y : " + Bodybounds.Y.ToString(), new Vector2(15f, 25f), Color.Black);
//                spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Arm  X : " + Armbounds.X.ToString() + " - Arm  Y : " + Armbounds.Y.ToString(), new Vector2(15f, 35f), Color.Black);
//                spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Head X : " + Headbounds.X.ToString() + " - Head Y : " + Headbounds.Y.ToString(), new Vector2(15f, 45f), Color.Black);
//                spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Face X : " + Facebounds.X.ToString() + " - Face Y : " + Facebounds.Y.ToString(), new Vector2(15f, 65f), Color.Black);
//                spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Hair X : " + Hairbounds.X.ToString() + " - Hair Y : " + Hairbounds.Y.ToString(), new Vector2(15f, 75f), Color.Black);

//                spriteBatch.DrawString(main.Content.Load<SpriteFont>("Fonts\\Segoe UI Mono"), "Action : " + Action + "  -  Weapon Type : " + WeaponType + "  -  Attack Type : " + AttackType + "  -  Attack Count : " + AttackTimeCount, new Vector2(15f, 95f), Color.Black);
//            }
//            if (facing == "right")
//            {
//                spriteBatch.Draw(Pbody, Bodybounds, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
//                // must draw armor or anything.. in here
//                spriteBatch.Draw(Parm, Armbounds, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
//                spriteBatch.Draw(Phead, Headbounds, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
//                spriteBatch.Draw(Pface, Facebounds, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
//                spriteBatch.Draw(PHair, Hairbounds, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0f);
//                // draw hair
//                // check equipment get position
//                if (Weapon != "")
//                {
//                }
//                if (Shield != "")
//                {
//                }
//                if (Armor != "")
//                {
//                }
//                if (Pant != "")
//                {
//                }
//                if (Shoe != "")
//                {
//                }
//                if (Glove != "")
//                {
//                }
//                if (Cape != "")
//                {
//                }
//                if (Acc != "")
//                {
//                }
//                if (Shoe != "")
//                {
//                }
//            }
//            else // left
//            {
//                spriteBatch.Draw(Pbody, Bodybounds, Color.White);
//                // must draw armor or anything.. in here
//                spriteBatch.Draw(Parm, Armbounds, Color.White);
//                spriteBatch.Draw(Phead, Headbounds, Color.White);
//                spriteBatch.Draw(Pface, Facebounds, Color.White);
//                spriteBatch.Draw(PHair, Hairbounds, Color.White);
//                // check equipment get position
//                if (Weapon != "")
//                {
//                }
//                if (Shield != "")
//                {
//                }
//                if (Armor != "")
//                {
//                }
//                if (Pant != "")
//                {
//                }
//                if (Shoe != "")
//                {
//                }
//                if (Glove != "")
//                {
//                }
//                if (Cape != "")
//                {
//                }
//                if (Acc != "")
//                {
//                }
//                if (Shoe != "")
//                {
//                }
//            }
//        }

//        public void SetPos(int x, int y)
//        {
//            Bodybounds.X = x;
//            Bodybounds.Y = y;
//        }

//        public int get_X()
//        {
//            return Bodybounds.X;
//        }

//        public int get_Y()
//        {
//            return Bodybounds.Y;
//        }

//        /// <summary>
//        /// update khi nhập từ bàn phím
//        /// </summary>
//        public void KeyInput()
//        {
//            // trạng thái keyboard
//            KeyboardState keyState = Keyboard.GetState();
//            if (direction != "up")
//            {
//                // khi nhấn nút di chuyển
//                if (keyState.IsKeyDown(Keys.Right))
//                {
//                    direction = "right";
//                    facing = "right";
//                    Action = "Walk";
//                }
//                if (keyState.IsKeyDown(Keys.Left))
//                {
//                    direction = "left";
//                    facing = "left";
//                    Action = "Walk";
//                }
//                // move to other map
//                // wait for code
//                if (keyState.IsKeyDown(Keys.X))
//                {
//                    direction = "up";
//                    Action = "Jump";
//                }
//                // dont use
//                // khi nhả nút di chuyển
//                if (keyState.IsKeyUp(Keys.Right) && direction == "right")
//                {
//                    direction = "stand";
//                    facing = "right";
//                    Action = "Stand";
//                    speed = 0;
//                }
//                if (keyState.IsKeyUp(Keys.Left) && direction == "left")
//                {
//                    direction = "stand";
//                    facing = "left";
//                    Action = "Stand";
//                    speed = 0;
//                }

//                // Attack
//                if (keyState.IsKeyDown(Keys.Z))
//                {
//                    if (AttackTimeCount >= MaxAttackTimeCount) // check if have fully rest time, character will attack =)) lol
//                    {
//                        System.Random rand = new System.Random();
//                        AttackType = (rand.Next(3) + 1).ToString();
//                        AttackTimeCount = 0;
//                        texture_position = 0;
//                        Action = "Attack";
//                        direction = "Nothing";
//                    }
//                }
//            } // end if (direction != "up")
//            else // jumping
//            {
//                // Attack
//                if (keyState.IsKeyDown(Keys.Z))
//                {
//                    if (AttackTimeCount >= MaxAttackTimeCount)
//                    {
//                        System.Random rand = new System.Random();
//                        AttackType = (rand.Next(3) + 1).ToString();
//                        AttackTimeCount = 0;
//                        texture_position = 0;
//                        Action = "Attack";
//                    }
//                }
//                if (keyState.IsKeyDown(Keys.Right))
//                {
//                    if (speed < maxspeed)
//                        speed += .2f;
//                    facing = "right";
//                }
//                if (keyState.IsKeyDown(Keys.Left))
//                {
//                    if (speed > -maxspeed)
//                        speed -= .2f;
//                    facing = "left";
//                }
//            }
//        }

//        /// <summary>
//        ///  dựa theo file XML trong Character.wz
//        ///  fix as xml file in character.wz
//        /// </summary>
//        /// <param name="TexturePos">frame position</param>
//        public void FixPosition(int TexturePos)
//        {
//            int f = 0;
//            int ab = 0;
//            // get action
//            if (Action == "Stand") ab = 0;
//            if (Action == "Walk") ab = 1;
//            if (Action == "Jump") ab = 2;
//            if (Action == "Attack") ab = 0;
//            if (Action == "Rope") ab = 0;
//            if (Action == "Ladder") ab = 0;
//            // get face
//            if (facing == "left") f = 0;
//            if (facing == "right") f = 1;
//            // set body position
//            Bodybounds.X = Bodybounds.X;
//            Bodybounds.Y = Bodybounds.Y;
//            Armbounds.X = Bodybounds.X + Garm.action[ab].face[f].position[texture_position].x;
//            Armbounds.Y = Bodybounds.Y + Garm.action[ab].face[f].position[texture_position].y;
//            Headbounds.X = Bodybounds.X + Ghead.action[ab].face[f].position[texture_position].x;
//            Headbounds.Y = Bodybounds.Y + Ghead.action[ab].face[f].position[texture_position].y;
//            // gadget position
//            Facebounds.X = Bodybounds.X + Gface.action[ab].face[f].position[texture_position].x;
//            Facebounds.Y = Bodybounds.Y + Gface.action[ab].face[f].position[texture_position].y;
//            Hairbounds.X = Bodybounds.X + Ghair.action[ab].face[f].position[texture_position].x;
//            Hairbounds.Y = Bodybounds.Y + Ghair.action[ab].face[f].position[texture_position].y;

//            // check equipment get position
//            if (Weapon != "")
//            {
//            }
//            if (Shield != "")
//            {
//            }
//            if (Armor != "")
//            {
//            }
//            if (Pant != "")
//            {
//            }
//            if (Shoe != "")
//            {
//            }
//            if (Glove != "")
//            {
//            }
//            if (Cape != "")
//            {
//            }
//            if (Acc != "")
//            {
//            }
//            if (Shoe != "")
//            {
//            }
//        }

//        /// <summary>
//        /// When player move
//        /// </summary>
//        public void Move()
//        {
//            if (Action == "Stand")
//            {
//                delay = 8;
//                if (DEBUG == true) delay = 80;
//            }
//            else if (Action == "Walk")
//            {
//                delay = 4;
//                if (DEBUG == true) delay = 80;
//            }
//            else if (Action == "Jump")
//            {
//                delay = 3;
//                if (DEBUG == true) delay = 80;
//            }
//            else if (Action == "Attack")
//            {
//                delay = 3;
//                if (WeaponType == "Hand") delay = 6;
//                if (direction != "notthing") delay = 3; // check if jumping set delay = jump's delay
//                if (DEBUG == true) delay = 80;
//            }

//            // chuyển động
//            if (frameCount % delay == 0)
//            {
//                // check if actor do something like attack, or anything lol
//                switch (Action) // BMK check action
//                {
//                    case "Attack":
//                        switch (WeaponType) // check weapon type
//                        {
//                            case "Hand":
//                                switch (AttackType) // check attack type
//                                {
//                                    case "1":
//                                        //if attack animation is complete
//                                        if (texture_position >= 2)
//                                        {
//                                            if (direction == "up") // check if jumping
//                                            {
//                                                texture_position = 0;
//                                                Action = "Jump"; // return jump action
//                                            }
//                                            else
//                                            {
//                                                Action = "Stand";
//                                                direction = "stand";
//                                            }
//                                        }
//                                        break;
//                                    case "2":
//                                        //if attack animation is complete
//                                        if (texture_position >= 2)
//                                        {
//                                            if (direction == "up") // check if jumping
//                                            {
//                                                texture_position = 0;
//                                                Action = "Jump"; // return jump action
//                                            }
//                                            else
//                                            {
//                                                Action = "Stand";
//                                                direction = "stand";
//                                            }
//                                        }
//                                        break;
//                                    case "3":
//                                        //if attack animation is complete
//                                        if (texture_position >= 3)
//                                        {
//                                            if (direction == "up") // check if jumping
//                                            {
//                                                texture_position = 0;
//                                                Action = "Jump"; // return jump action
//                                            }
//                                            else
//                                            {
//                                                Action = "Stand";
//                                                direction = "stand";
//                                            }
//                                        }
//                                        break;
//                                }
//                                break;
//                        }
//                        // first update for config infomation of charater
//                        UpdateTexture();
//                        // fix gadget position
//                        FixPosition(texture_position);
//                        // reupdate infomation
//                        UpdateTexture();

//                        break;
//                }
//                // move,jump
//                switch (direction)
//                {
//                    case "stand":
//                        speed = 0;
//                        // nếu vị trí ảnh lớn hơn số lượng ảnh
//                        if (texture_position >= standmax)
//                            texture_position = 0; // đặt vị trí về 0
//                        FixPosition(texture_position);

//                        // fix body for stand left
//                        if (facing == "left")
//                        {
//                            switch (texture_position)
//                            {
//                                case 0: Bodybounds.X += 1;
//                                    break;
//                                case 1: Bodybounds.X -= 1;
//                                    break;
//                                case 2: Bodybounds.X -= 1;
//                                    break;
//                                case 3: Bodybounds.X += 1;
//                                    break;
//                            }
//                        }

//                        // update lại hình ảnh
//                        UpdateTexture();
//                        break;
//                    case "left":
//                        if (speed > -maxspeed)
//                            speed -= 2;

//                        // di chuyển ảnh
//                        Bodybounds.X += (int)speed;

//                        if (texture_position >= walkmax)
//                            texture_position = 0; // đặt vị trí về 0
//                        UpdateTexture();
//                        FixPosition(texture_position);

//                        UpdateTexture();
//                        break;
//                    case "right":
//                        if (speed < maxspeed)
//                            speed += 2;

//                        // di chuyển ảnh
//                        Bodybounds.X += (int)speed;

//                        if (texture_position >= walkmax)
//                            texture_position = 0; // đặt vị trí về 0
//                        UpdateTexture();
//                        FixPosition(texture_position);
//                        UpdateTexture();
//                        break;
//                    case "up":
//                        // check if action is "attack"
//                        if (Action == "Attack")
//                        {
//                            UpdateTexture();
//                        }
//                        else
//                        {
//                            ////////////////////////////////////////////
//                            // alws must set texture position = 0 if use update in jump action
//                            if (texture_position > 0) // by default, jump have only 1 frame lolz
//                                texture_position = 0; // because of that, set position is 0
//                            UpdateTexture();
//                        }
//                        //////////////////////////////////////////////////////////////////////////////
//                        // set start Y if jump start
//                        if (jumpCount == 0)
//                            startY = Bodybounds.Y;

//                        //Sets function for determining jump y-position
//                        float jump_positionY = (jumpCount - 10) * (jumpCount - 10) - 100 + startY;
//                        //Sets terminal velocity
//                        float max_velocityY = 2 * (maxjumpcount - 10); //derivative of jump_positionY where 20 is max jumpCount value
//                        float max_positionY = startY + max_velocityY * (jumpCount - maxjumpcount);
//                        // if jump complete
//                        if (jumpCount >= maxjumpcount)
//                        {
//                            // set action is stand
//                            jump_positionY = max_positionY;
//                            jumpCount = 0;
//                            Action = "Stand";
//                            direction = "stand";
//                            Bodybounds.Y = startY;
//                        }
//                        // set body bounds
//                        Bodybounds = new Rectangle(Bodybounds.X + (int)speed,
//                            (int)jump_positionY, 40, 48);
//                        jumpCount++;

//                        FixPosition(texture_position);
//                        UpdateTexture();
//                        break;
//                }
//                texture_position++; // cộng vị trí ảnh thêm 1
//            } // end if
//            frameCount++; // xác định độ trễ khi chạy ảnh
//        }

//        public void CheckAttacktimecount()
//        {
//            if (AttackTimeCount <= MaxAttackTimeCount)
//            {
//                AttackTimeCount++;
//            }
//        }

//        /*
//        //Checks collision
//        private bool CheckCollision(Rectangle bounds)
//        {
//            int cutoff = bounds.Y + Map.size;

//            //Create a rectangle for the current block, if it is not -1
//            Rectangle tempBounds = new Rectangle(bounds.X, cutoff, bounds.Width, cutoff - bounds.Y);
//            tempBounds.Y += tempBounds.Height - Map.size;

//            int x = (tempBounds.X + bounds.Width / 2) / Map.size;
//            int y = tempBounds.Y / Map.size + 1;
//            int type = Map.getType(y, x);

//            if (type > -1)
//            {
//                //Test to see if the character collides
//                Rectangle tileBounds = new Rectangle(x * Map.size, y * Map.size, Map.size, Map.size);
//                if (tempBounds.Intersects(tileBounds))
//                {
//                    this.bounds = new Rectangle(bounds.X, tileBounds.Y - bounds.Height, bounds.Width, bounds.Height);
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            else
//            {
//                return false;
//            }
//        }
//        */
//    }
//}