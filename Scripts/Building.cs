using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MyRTS2018
{
    abstract class Building
    {
        protected int x;
        protected int y;
        protected int health;
        protected string faction;
        protected string symbol;
        
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }

        }

        public string Faction
        {
            get { return faction; }
            set { faction = value; }
        }

        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public Building(int posX, int posY, int health, string faction, string Symbol)
        {
            this.X = posX;
            this.Y = posY;
            this.Health = health;
            this.Faction = faction;
            this.Symbol = Symbol;
        }

        public abstract bool isAlive();
        public abstract string toString();
        public abstract void save();
        public abstract int generateResources();
        public abstract Unit spawnNewUnits(string[,] grid, string FIELD_SYMBOL, int unitsToProduce);
    }
}
