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
        string Action = "attack";
        string type = "1";
        int attacktype = 0;

        Run Main;

        public bool canattack;
        public bool canjump;

        int texture_position = 0;

        // position
        int x = 400;
        int y = 350;

        int framecount = 8;
        int delay = 8;
        // create variable contain information of enemy
        XmlContent.Enemy.Enemy EnemyData;

        /// <summary>
        /// Create base of Enemy
        /// </summary>
        /// <param name="_Main">main game, use to get content</param>
        /// <param name="enemysprite">name of enemy sprite</param>
        /// <param name="canattack">can enemy attack ?</param>
        /// <param name="canjump">can enemy jump ?</param>
        public EnemyBase(Run _Main, string enemysprite, bool canattack, bool canjump, XmlContent.Enemy.Enemy _EnemyData)
        {
            // put value into variable
            Main = _Main;
            this.enemysprite = enemysprite;
            this.canattack = canattack;
            this.canjump = canjump;
            // load enemy data
            EnemyData = _EnemyData;
            Sprite = Main.Content.Load<Texture2D>("Enemy\\" + enemysprite + "\\" + Action + type + "_" + texture_position);
            RSprite = new Rectangle(x - Sprite.Width, y - Sprite.Height, Sprite.Width, Sprite.Height);
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
        /// Update
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Get random attack
        /// </summary>
        public void UpdateAttack()
        {
            // get random attack
            if (Action == "attack" && texture_position == 0)
            {
                System.Random rand = new System.Random();
                attacktype = rand.Next(EnemyData.attacktypecount);
                type = (attacktype + 1).ToString();
            }
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
                        if (texture_position >= EnemyData.data[0].posecount)
                            texture_position = 0;
                        UpdateTexture();
                        break;
                    case "attack":
                        if (texture_position >= EnemyData.attack[attacktype].posecount)
                            texture_position = 0;
                        UpdateAttack();
                        UpdateTexture();
                        break;
                    case "hit":
                        if (texture_position >= EnemyData.data[1].posecount)
                            texture_position = 0;
                        UpdateTexture();
                        break;
                    case "skill":
                        if (texture_position >= EnemyData.data[3].posecount)
                            texture_position = 0;
                        UpdateTexture();
                        break;
                    case "die":
                        if (texture_position >= EnemyData.data[2].posecount)
                            texture_position = 0;
                        UpdateTexture();
                        break;
                    case "walk":
                        if (texture_position >= EnemyData.data[0].posecount)
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
            RSprite.Height = Sprite.Height;
            RSprite = new Rectangle(x - Sprite.Width, y - Sprite.Height, Sprite.Width, Sprite.Height);
        }
    }
}