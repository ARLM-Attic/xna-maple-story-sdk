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

namespace XmlContent
{
    public class EGadget
    {
        public string name; // name of item
        public string catalog; // suck as weapon,armor,shoe,pant,...
        public string type; // suck as weapon : gun or one hand sword,...
        // action
        public List<Action> action;
    }
}
