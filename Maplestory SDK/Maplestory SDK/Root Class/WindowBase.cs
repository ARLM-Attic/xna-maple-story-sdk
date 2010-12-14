using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Maplestory_SDK.Root_Class
{
    class WindowBase
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
