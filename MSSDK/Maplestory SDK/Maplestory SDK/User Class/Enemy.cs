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
    }
}