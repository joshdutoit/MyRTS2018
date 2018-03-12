using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyRTS2018
{
    class ResourceBuilding : Building
    {
        private string resourceType;
        private int resourcesPerGameTick;
        private int resourcesRemaining;      
        public int Max_Resources = 100;

        public ResourceBuilding(int posX, int posY, int health, string faction, string symbol, string resourceType, int resourcesPerGameTick, int resourcesRemaining)
            : base (posX, posY, health, faction, symbol)
        {
            this.resourceType = resourceType;
            this.resourcesPerGameTick = resourcesPerGameTick;
            this.resourcesRemaining = resourcesRemaining;
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
            string output = "Resource Building" + Environment.NewLine 
                 + "x : " + X + Environment.NewLine
                 + "y : " + Y + Environment.NewLine
                 + "Health : " + Health + Environment.NewLine
                 + "Faction/Team : " + Faction + Environment.NewLine
                 + "Symbol : " + Symbol + Environment.NewLine
                 + "Resource Type : " + resourceType + Environment.NewLine
                 + "Resource PergameTick : " + resourcesPerGameTick + Environment.NewLine
                 + "Resource Remaining : " + resourcesRemaining + Environment.NewLine;
            return output;
        }


        public override int generateResources()
        {
            if (resourcesRemaining > 0)
            {
                resourcesRemaining -= resourcesPerGameTick;
                return resourcesPerGameTick;
            }
            else return 0;
        }
        public override Unit spawnNewUnits(string[,] grid, string FIELD_SYMBOL, int unitsToProduce)
        {
            return null;
        }
        public override void save()
        {
            FileStream outFile = null;
            StreamWriter writer = null;
            try
            {
                outFile = new FileStream(@"Files\ResourceBuilding.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(outFile);
                writer.WriteLine(x);
                writer.WriteLine(y);
                writer.WriteLine(health);
                writer.WriteLine(faction);
                writer.WriteLine(symbol);
                writer.WriteLine(resourceType);
                writer.WriteLine(resourcesPerGameTick);
                writer.WriteLine(resourcesRemaining);
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
