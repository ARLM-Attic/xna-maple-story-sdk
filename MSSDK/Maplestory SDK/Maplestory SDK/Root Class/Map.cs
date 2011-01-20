using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Maplestory_SDK.Root_Class.MapCompoment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maplestory_SDK.Root_Class
{
    internal class Map
    {
        public Game game;
        public bool DrawCollusion = false;

        public int[] Size = new int[2]; // contain width and height

        public Layer[] MapLayer = new Layer[10];

        public int[,] visualmatrix;
        public List<int[]> collusionmap = new List<int[]>();

        Texture2D pixel;

        public Map(Game _game)
        {
            game = _game;
            pixel = game.Content.Load<Texture2D>("Temp\\pixel");
            for (int i = 0; i < MapLayer.Length; i++)
            {
                MapLayer[i] = new Layer();
            }
            Size[0] = 800;
            Size[1] = 600;
            visualmatrix = new int[Size[0], Size[1]];
            for (int x = 0; x < Size[0]; x++)
            {
                for (int y = 0; y < Size[1]; y++)
                {
                    visualmatrix[x, y] = 0;
                }
            }
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spritebatch)
        {
            // draw all layer
            for (int i = 0; i < MapLayer.Length; i++)
            {
                foreach (TextureMap temp in MapLayer[i].data)
                {
                    spritebatch.Draw(temp.texture, temp.rectangle, Color.White);
                }
            }
            //draw collusion
            if (DrawCollusion)
                foreach (int[] collusionpoin in collusionmap)
                {
                    spritebatch.Draw(pixel, new Rectangle(collusionpoin[0], collusionpoin[1], 1, 1), Color.Red);
                }
        }

        /// <summary>
        /// Load map from file
        /// </summary>
        /// <param name="name">name of file u want to load</param>
        public void LoadMap(string name)
        {
            // load layer texture
            FileStream fs = new FileStream("Content\\MapData\\" + name + ".ltm", FileMode.Open, FileAccess.Read);
            BinaryReader sw = new BinaryReader(fs);

            byte[] bytes = Convert.FromBase64String(sw.ReadString());
            string templayer = System.Text.ASCIIEncoding.ASCII.GetString(bytes);

            //@"{(?<Layer>[\w-\W][^}]*)}"
            Regex layerregex = new Regex(@"{(?<Layer>[\w-\W][^}]*)}");
            MatchCollection matchs = layerregex.Matches(templayer);
            for (int i = 0; i < 10; i++)
            {
                if (matchs[i].Value.Length > 4)
                {
                    Regex textureregex = new Regex(@"\[(?<Link>[\w-\W][^|]*)\|(?<Info>[\d-:]*)\]");
                    MatchCollection textmap = textureregex.Matches(matchs[i].Groups["Layer"].Value);
                    foreach (Match match in textmap)
                    {
                        string[] recdata = match.Groups["Info"].Value.Split(':');
                        Rectangle rec = new Rectangle(Int32.Parse(recdata[0]), Int32.Parse(recdata[1]), Int32.Parse(recdata[2]), Int32.Parse(recdata[3]));
                        Texture2D text = game.Content.Load<Texture2D>(match.Groups["Link"].Value);
                        MapLayer[i].AddTexture(text, rec, match.Groups["Link"].Value, match.Groups["Info"].Value);
                    }
                }
            }
            sw.Close();
            fs.Close();

            FileStream fs1 = new FileStream("Content\\MapData\\" + name + ".clm", FileMode.Open, FileAccess.Read);
            BinaryReader sw1 = new BinaryReader(fs1);
            bytes = Convert.FromBase64String(sw1.ReadString());
            templayer = System.Text.ASCIIEncoding.ASCII.GetString(bytes);

            layerregex = new Regex(@"\[(?<X>[\d]*):(?<Y>[\d]*)\]");
            matchs = layerregex.Matches(templayer);

            collusionmap.Clear();
            foreach (Match match in matchs)
            {
                collusionmap.Add(new int[] { Int32.Parse(match.Groups["X"].Value), Int32.Parse(match.Groups["Y"].Value) });
            }

            sw1.Close();
            fs1.Close();

            UpdateCollusionMap();
        }

        public void UpdateCollusionMap()
        {
            foreach (int[] collusionpoin in collusionmap)
            {
                visualmatrix[collusionpoin[0], collusionpoin[1]] = 1;
            }
        }

        public void UpdateCollusionVisual()
        {
            collusionmap.Clear();
            for (int x = 0; x < Size[0]; x++)
            {
                for (int y = 0; y < Size[1]; y++)
                {
                    if (visualmatrix[x, y] == 1)
                        collusionmap.Add(new int[] { x, y });
                }
            }
        }

        /// <summary>
        /// save map to file
        /// </summary>
        /// <param name="name">name of file u want to save</param>
        public void SaveMap(string name)
        {
            // create folder
            Directory.CreateDirectory("Content\\MapData\\");
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            // write map layer
            FileStream fs = new FileStream("Content\\MapData\\" + name + ".ltm", FileMode.Create);
            BinaryWriter sw = new BinaryWriter(fs);
            string savetemp = "";
            string temp = "";

            for (int i = 0; i < MapLayer.Length; i++)
            {
                foreach (TextureMap tex in MapLayer[i].data)
                {
                    temp += "[" + tex.link + "|" + tex.info + "]";
                }
                if (temp == "")
                    temp = "[]";
                savetemp += "{" + temp + "}";
                temp = "";
            }

            sw.Write(Convert.ToBase64String(encoding.GetBytes(savetemp)));
            sw.Close();
            fs.Close();

            // write map collusion
            FileStream fs1 = new FileStream("Content\\MapData\\" + name + ".clm", FileMode.Create);
            BinaryWriter sw1 = new BinaryWriter(fs1);

            temp = "";

            foreach (int[] collusionpoin in collusionmap)
            {
                temp += "[" + collusionpoin[0] + ":" + collusionpoin[1] + "]";
            }

            sw1.Write(Convert.ToBase64String(encoding.GetBytes(temp)));
            sw1.Close();
            fs1.Close();
        }
    }
}