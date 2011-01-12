using Maplestory_SDK.Root_Class;
using Microsoft.Xna.Framework.Graphics;

namespace Maplestory_SDK.User_Class
{
    internal class Enemy
    {
        public string name;

        public bool CanAttack = false; // enemy can attack
        public bool CanJump = false; // enemy can jump
        public bool HurtifHit = true; // player will damage if hit
        // attribute
        public int atk;
        public int def;
        public int speed;

        public int hp;
        public int maxhp;
        public int mp;
        public int maxmp;

        EnemyBase enemy;

        Run Main;

        /// <summary>
        /// Create new enemy
        /// </summary>
        /// <param name="_Main">main to get content</param>
        /// <param name="sprite">name of sprite of enemy</param>
        /// <param name="canattack">can enemy attack ?</param>
        /// <param name="canjump">can enemy jump ? </param>
        /// <param name="hurtifhit">can hurt if hit ?</param>
        /// <param name="listattribute"> list of attribute</param>
        public Enemy(Run _Main, string sprite, bool canattack, bool canjump, bool hurtifhit, int[] listattribute, string name)
        {
            Main = _Main;

            enemy = new EnemyBase(Main, sprite, canattack, canjump);

            CanAttack = canattack;
            CanJump = canjump;
            HurtifHit = hurtifhit;

            this.name = name;
        }

        /// <summary>
        /// Update
        /// </summary>
        public void Update()
        {
            // update enemy animation
            enemy.Animation();
        }

        /// <summary>
        ///
        /// </summary>
        public void Draw(SpriteBatch spritebatch)
        {
            enemy.Draw(spritebatch);
        }
    }
}