using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maplestory_SDK.Root_Class.MapCompoment
{
    public class TextureMap
    {
        public string link;
        public string info;
        public Texture2D texture;
        public Rectangle rectangle;

        public TextureMap(Texture2D text, Rectangle rec, string _link, string _info)
        {
            texture = text;
            rectangle = rec;
            link = _link;
            info = _info;
        }

        public void SetPosition(int x, int y)
        {
            rectangle.X = x;
            rectangle.Y = y;
        }
    }
}