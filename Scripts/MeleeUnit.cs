using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyRTS2018
{
    class MeleeUnit : Unit

    {
        private const int DAMAGE = 2;

        public MeleeUnit(int x, int y, int health, int speed, bool attack, int attackRange, string faction, string symbol, string unitname)
            : base (x, y, health, speed, attack, attackRange, faction, symbol, unitname)
        {
        }

        public override void move(int x, int y)
        {
            if (x >= 0 && x < 20)
                X = x;
            if (y >= 0 && y < 20)
                Y = y;
        }

        public override void combat(Unit enemy)
        {
            if (isWithInAttackRange(enemy))
            {
                enemy.Health -= DAMAGE;
            }
        }

        public override bool isWithInAttackRange(Unit enemy)
        {
            if ((Math.Abs(X - enemy.X) <= attackRange) || (Math.Abs(X - enemy.X) <= attackRange))
                return true; 
            else 
                return false;
        }

        public override Unit nearestUnit(List<Unit> list)
        {
            Unit closest = null;
            int attackRangeX, attackRangeY;
            int shortestRange = 1000;

            foreach (Unit u in list)
            {
                attackRangeX = Math.Abs(this.X - u.X);
                attackRangeY = Math.Abs(this.Y - u.Y);

                if (attackRangeX < shortestRange)
                {
                    shortestRange = attackRangeX;
                    closest = u;
                }
            }
            return closest;
        }

        public override bool isAlive()
        {
            if (this.Health <= 0)
                return false;
            else
                return true;
        }

        public override string toString()
        {
            string output = "x : " + X + Environment.NewLine
                 + "y : " + Y + Environment.NewLine
                 + "Health : " + Health + Environment.NewLine
                 + "Speed : " + Speed + Environment.NewLine
                 + "Attack : " + (Attack ? "Yes" : "No") + Environment.NewLine
                 + "Attack Range : " + AttackRange + Environment.NewLine
                 + "Faction/Team : " + Faction + Environment.NewLine
                 + "Symbol : " + Symbol + Environment.NewLine
                 + "Unit Name : " + unitName + Environment.NewLine; // assignment 2 q1.1
            return output;
        }

        public override void save()
        {
            FileStream outFile = null;
            StreamWriter writer = null;
            try
            {
                outFile = new FileStream(@"Files\MeleeUnit.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(outFile);
                writer.WriteLine(x);
                writer.WriteLine(y);
                writer.WriteLine(health);
                writer.WriteLine(speed);
                writer.WriteLine(attack);
                writer.WriteLine(attackRange);
                writer.WriteLine(faction);
                writer.WriteLine(symbol);
                writer.WriteLine(unitName);
                writer.Close();
                outFile.Close();
            }
            catch (IOException fe)
            {
                Console.WriteLine("IOException: " + fe.Message);
            }
            finally
            {
                if (outFile != null)
                    outFile.Close();
                if (writer != null)
                    writer.Close();
            }
        }
    }
}

