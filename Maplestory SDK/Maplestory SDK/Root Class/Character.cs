﻿using System;
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
        Rectangle bounds = new Rectangle(57, 337, 40, 48);
        Rectangle srcBounds = new Rectangle(0, 0, 40, 48); //Determines which frame is showing

        string direction = "stand";
        string facing = "right";

        int frameCount = 0; //Counts which frame is currently in sourceBounds
        const int delay = 4;

        float speed = 0f;
        const float maxspeed = 12f;

        int startY = 0;
        int jumpCount = 0;

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
    }
}