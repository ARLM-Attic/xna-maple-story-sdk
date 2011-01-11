using Microsoft.Xna.Framework.Graphics;

namespace Maplestory_SDK.Root_Class
{
    internal class WindowBase
    {
        // containt width , height, x, y
        int x, y, width, height;
        Texture2D skin;

        // khởi tạo
        public WindowBase(int x, int y, int width, int height, Texture2D skin)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.skin = skin;
        }
    }
}