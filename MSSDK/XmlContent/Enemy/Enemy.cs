using System.Collections.Generic;

namespace XmlContent.Enemy
{
    public class Enemy
    {
        public string name;
        public int atk;
        public int def;
        public int speed;
        public int maxhp;
        public int maxmp;

        public bool canattack;
        public bool canjump;
        public bool canhit;
        public bool haveskill;

        public int attacktypecount;
        public List<DataType> attack;
        public List<DataType> data;
    }
}