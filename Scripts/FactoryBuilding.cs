using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyRTS2018
{

    class FactoryBuilding : Building
    {

        public String UnitToProduce = "MeleeUnit";
        private int[] spownPoints = new int[2];
        public int GameTicksPerProduction = 100;
        public FactoryBuilding(int posX, int posY, int health, string faction, string Symbol)
            : base (posX, posY, health, faction, Symbol)
        {
        }

        public override bool isAlive()
        {
            if (this.health <= 0)
                return false;
            else
                return true;
        }



        public override string toString()
        {
            string output = "Factory Building" + Environment.NewLine
                 + "x : " + X + Environment.NewLine
                 + "y : " + Y + Environment.NewLine
                 + "Health : " + Health + Environment.NewLine
                 + "Faction/Team : " + Faction + Environment.NewLine
                 + "Symbol : " + Symbol + Environment.NewLine;
            return output;
        }

        public override Unit spawnNewUnits(string[,] grid, string FIELD_SYMBOL, int unitsToProduce)
        {
            int x_1;
            int y_1;
            do
            {
                // finds free space on map
                Random rnd = new Random();
                do
                {
                    x_1 = rnd.Next(0, 20);
                    y_1 = rnd.Next(0, 20);
                }
                while (grid[x_1, y_1] != FIELD_SYMBOL);                
                if (rnd.Next(0, 2) == 0)
                {
                    // meleeununit
                    bool attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    int randomAttackRange = rnd.Next(1, 20);
                    string team = rnd.Next(0, 2) == 1 ? "RED" : "GREEN";
                    MeleeUnit mU = new MeleeUnit(x_1, y_1, 100, -1, attackOption, 1, team, "M", "Melee Unit");
                    unitsToProduce--;
                    return mU;

                }
                else
                {
                    // rangeununit
                    bool attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    int randomAttackRange = rnd.Next(1, 20);
                    string team = rnd.Next(0, 2) == 1 ? "RED" : "GREEN";
                    RangedUnit rU = new RangedUnit(x_1, y_1, 100, -1, attackOption, randomAttackRange, team, "R", "Ranged Unit");
                    unitsToProduce--;
                    return rU;
                }
               
            }
            while (unitsToProduce > 0);
        }
        public override int generateResources()
        {
            return 0;
        }
        public override void save()
        {
            FileStream outFile = null;
            StreamWriter writer = null;
            try
            {
                outFile = new FileStream(@"Files\FactoryBuilding.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(outFile);
                writer.WriteLine(x);
                writer.WriteLine(y);
                writer.WriteLine(health);
                writer.WriteLine(faction);
                writer.WriteLine(symbol);
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
