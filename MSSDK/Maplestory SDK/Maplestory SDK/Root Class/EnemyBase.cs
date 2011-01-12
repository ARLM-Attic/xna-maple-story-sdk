using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maplestory_SDK.Root_Class
{
    internal class EnemyBase
    {
        // BMK initialize
        public string enemysprite;

        Texture2D Sprite;
        Rectangle RSprite;

        string direction = "stand";
        string facing = "left";
        string Action = "stand";
        string type = "";

        Run Main;

        public bool canattack;
        public bool canjump;

        int standpos;
        int walkpos;
        int hitpos;
        int diepos;
        int attackpos;
        int skillpos;

        int texture_position = 0;

        // position
        int x = 150;
        int y = 235;

        int framecount = 8;
        int delay = 4;

        /// <summary>
        /// Create base of Enemy
        /// </summary>
        /// <param name="_Main">main game, use to get content</param>
        /// <param name="enemysprite">name of enemy sprite</param>
        /// <param name="listattribute">list of attribute of enemy</param>
        /// <param name="canattack">can enemy attack ?</param>
        /// <param name="canjump">can enemy jump ?</param>
        public EnemyBase(Run _Main, string enemysprite, bool canattack, bool canjump)
        {
            // put value into variable
            Main = _Main;
            this.enemysprite = enemysprite;
            this.canattack = canattack;
            this.canjump = canjump;

            Sprite = Main.Content.Load<Texture2D>("Enemy\\" + enemysprite + "\\" + Action + type + "_" + texture_position);
            RSprite = new Rectangle(x, y, Sprite.Width, Sprite.Height);
        }

        /// <summary>
        /// Draw enemy
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, RSprite, Color.White);
        }

        /// <summary>
        /// Create animation for enemy
        /// </summary>
        public void Animation()
        {
            if (framecount % delay == 0)
            {
                switch (Action)
                {
                    case "stand":
                        if (texture_position >= 6)
                            texture_position = 0;
                        UpdateTexture();
                        break;
                }
                texture_position++;
            }
            framecount++;
        }

        /// <summary>
        /// update enemy texture
        /// </summary>
        public void UpdateTexture()
        {
            Sprite = Main.Content.Load<Texture2D>("Enemy\\" + enemysprite + "\\" + Action + type + "_" + texture_position);
            RSprite.Width = Sprite.Width;
            RSprite.Height = 133;
        }
    }
}