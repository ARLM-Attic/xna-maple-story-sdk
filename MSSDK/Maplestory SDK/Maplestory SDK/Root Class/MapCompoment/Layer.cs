using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maplestory_SDK.Root_Class.MapCompoment
{
    public class Layer
    {
        public List<TextureMap> data;

        public Layer()
        {
            data = new List<TextureMap>();
        }

        /// <summary>
        /// add new texture into a row what was selected
        /// </summary>
        /// <param name="import">texture what u want to add into row</param>
        /// <param name="row">index of row in list</param>
        public void AddTexture(Texture2D import, Rectangle rec, string link, string info)
        {
            data.Add(new TextureMap(import, rec, link, info));
        }

        /// <summary>
        /// remove texture
        /// </summary>
        /// <param name="row">index of row in list</param>
        /// <param name="index">index of texture in row</param>
        public void RemoveTexture(int index)
        {
            if (data.Count > 0)
                data.RemoveAt(index);
        }
    }
}