using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maplestory_SDK.Root_Class
{
    internal class Map
    {
        public const int size = 32; //The size of each tile in pixels (this will be later stored in an XML loading file)
        public static int height; //in tiles
        public static int width; //in tiles
        public string path = @"C:\"; // path to save or load map data

        // use for encode or decode
        string passPhrase = "nds9@(dsaklsan1@dsa";        // can be any string
        string saltValue = "smdsao@kdsap)_9231";        // can be any string
        string hashAlgorithm = "SHA1";             // can be "MD5"
        int passwordIterations = 1502;                  // can be any number
        string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        int keySize = 256;                // can be 192 or 128

        static int[,] tileMap;

        public Map(GraphicsDevice graphics) //Creates a new map based on the size of the screen
        {
            height = graphics.DisplayMode.Height / size;
            width = graphics.DisplayMode.Width / size;

            tileMap = new int[height, width]; //Initializes the new map

            for (int y = 0; y < height; y++)  //Defaults all values to -1 (saves processing time)
                for (int x = 0; x < width; x++)
                    tileMap[y, x] = -1;
        }

        public static int getType(int y, int x)
        {
            if (!(x < 0 || y < 0) && !(x > width || y > height))
            {
                return tileMap[y, x];
            }
            else
                return -1;
        }

        public void Set(int y, int x, int currentTile) //SAFELY sets the value of a clicked tile based on the currently selected tile in the editor
        {
            if (!(x < 0 || y < 0))
            {
                tileMap[y, x] = currentTile;
            }
        }

        public void DrawMap(SpriteBatch spriteBatch, Texture2D tileSheet)
        {
            height = tileMap.GetLength(0); //Map height (in tiles)
            width = tileMap.GetLength(1); //Map width (in tiles)

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    int cell = tileMap[y, x]; //Cell = value of tile at map position (y,x)

                    if (cell > -1)//If cell exists; otherwise, continue (saves processing time)
                    {
                        Rectangle sourceBounds = SourcePoint(cell, tileSheet); //Gets coodinates of tile on the tilesheet
                        Rectangle bounds = new Rectangle(x * size, y * size, size, size); //Sets where and what size the tile will be in the game

                        spriteBatch.Draw(tileSheet, bounds, sourceBounds, Color.White); //Draws the tile
                    }
                }
        }

        private Rectangle SourcePoint(int cell, Texture2D tileSheet) //Takes an integer value and converts it to its corresponding tile location
        {
            //Example of the data processed:
            //0  1  2
            //3  4  5
            //6  7  8
            //9  10 11
            //12 13 14
            //15 16 17
            //
            //If cell = 7, for example:
            //tileWidth = 3
            //y = 7 / 3 = 2
            //x = 7 % 3 (the remainder of 7 / 3) = 1
            //(x,y) value returned = (32,64)

            Rectangle point = new Rectangle(0, 0, size, size); //The point to be returned
            int tileWidth = tileSheet.Width / size;              //How many tiles can fit in the tileset horizontally
            int y = cell / tileWidth;                         //Returns the row of the tile
            int x = cell % tileWidth;                         //Returns how many tiles "cell" is displaced by

            point = new Rectangle(x * size, y * size, size, size); //Compacts the above data into a source rectangle

            return point; //returns value
        }

        /// <summary>
        /// Tạo file map cho map hiện tại
        /// dùng để import sau này khi cần dùng đến map
        /// [ cần làm : bổ sung tên title,layer,.. ]
        /// </summary>
        /// <param name="name">tên file save</param>
        /// <param name="mapname">tên map</param>
        /// <param name="titlename">tên titleset</param>
        public void CreateTextOutput(string filename, string mapname, string titlename, int[] charpos)
        {
            // initialize
            FileStream fs = new FileStream(path + filename + ".xml", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            // write
            sw.WriteLine("<" + mapname + ">");
            sw.WriteLine("<Title name=" + titlename + "/>");
            sw.WriteLine("<Character>" + charpos[0].ToString() + ";" + charpos[1].ToString() + "</Character>");
            sw.WriteLine("<Matrix>");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j == width - 1)
                        sw.WriteLine(tileMap[i, j].ToString() + "|");
                    else
                        sw.Write(tileMap[i, j].ToString() + ";");
                }
            }
            sw.WriteLine("</Matrix>");
            sw.Write("</" + mapname + ">");
            // dispose
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// Nhập map có sẵn vào
        /// </summary>
        /// <param name="filename">tên file load</param>
        public void ImportMap(string filename, Character mainplayer)
        {
            // initialize
            FileStream fs = new FileStream(path + filename + ".xml", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            // read all
            string content = sr.ReadToEnd();
            Regex reg = new Regex(@"<Matrix>(?<content>[\d-\D][^<]+)</Matrix>");
            Match matrix = reg.Match(content);
            reg = new Regex(@"<Character>(?<charpos>[\d-\D][^<]+)</Character>");
            Match charpos = reg.Match(content);
            // return if cant find
            if (!matrix.Success) return;
            // import map data
            content = matrix.Groups["content"].Value.Replace("\r\n", "");
            string[] YY = content.Split('|');
            string[] XX;
            for (int y = 0; y < height; y++)
            {
                XX = YY[y].Split(';');
                for (int x = 0; x < width; x++)
                {
                    tileMap[y, x] = Int32.Parse(XX[x]);
                }
            }
            // import char position
            if (!charpos.Success) return;
            content = charpos.Groups["charpos"].Value.Replace("\r\n", "");
            XX = content.Split(';');
            mainplayer.SetPos(Int32.Parse(XX[0]), Int32.Parse(XX[1]));
        }
    }
}