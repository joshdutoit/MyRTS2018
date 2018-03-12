using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyRTS2018
{
    class Map
    {
        private const int MAX_RANDOM_UNITS = 50;
        private const int MAX_RANDOM_BUILDINGS = 50;
        public string FIELD_SYMBOL = " .";
        public string[,] grid = new string[20, 20];
        public List<Unit> unitsOnMap = new List<Unit>();
        private List<Building> buildingsOnMap = new List<Building>();
        private int numberOfBuildingsOnMap = 0;
        public int resources_red = 0;
        public int resources_green = 0;
        public int numberOfUnitsOnMap = 0;
        public string displayText;

        public string[,] Grid
        {
            get
            {
                return grid;
            }
        }

        public List<Unit> UnitsOnMap
        {
            get
            {
                return unitsOnMap;
            }
        }
        public List<Building> BuildingsOnMap
        {
            get
            {
                return buildingsOnMap;
            }
        }

        public void populate()
        {
            Random rnd = new Random();
            int numberRandomUnits = rnd.Next(0, MAX_RANDOM_UNITS) + 1;
            int numberRandomBuildings = rnd.Next(0, MAX_RANDOM_BUILDINGS) + 1;
            int x, y, randomAttackRange;
            bool attackOption;
            string team;
            // spawning field symbols in map
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    grid[i, j] = FIELD_SYMBOL;
                }
            }
            // generating units and placing on grid
            for (int k = 1; k <= numberRandomUnits; k++)
            {
                do
                {
                    x = rnd.Next(0, 20);
                    y = rnd.Next(0, 20);
                } while (grid[x, y] != FIELD_SYMBOL);

                if (rnd.Next(1, 3) == 1)
                {
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    team = rnd.Next(0, 2) == 1 ? "RED" : "GREEN";
                    Unit tmpUnit = new MeleeUnit(x, y, 100, -1, attackOption, 1, team, "M", "Melee Unit");
                    unitsOnMap.Add(tmpUnit);
                    grid[x, y] = tmpUnit.Symbol;
                    numberOfUnitsOnMap++;
                }

                else
                {
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    randomAttackRange = rnd.Next(1, 20);
                    team = rnd.Next(0, 2) == 1 ? "RED" : "GREEN";
                    Unit tmpUnit = new RangedUnit(x, y, 100, -1, attackOption, randomAttackRange, team, "R", "Ranged Unit");
                    unitsOnMap.Add(tmpUnit);
                    grid[x, y] = unitsOnMap[numberOfUnitsOnMap].Symbol;
                    numberOfUnitsOnMap++;


                }
            }
            Building tmp;
            // generating buildings and placing on grid
            //red resource building
            do
            {
                x = rnd.Next(0, 20);
                y = rnd.Next(0, 20);
            } while (grid[x, y] != FIELD_SYMBOL);
            tmp = new ResourceBuilding(x, y, 1000, "RED", "O", "Gold", 1, rnd.Next(10, 100));
            buildingsOnMap.Add(tmp);
            grid[x, y] = tmp.Symbol;
            numberOfBuildingsOnMap++;
            //green resource building
            do
            {
                x = rnd.Next(0, 20);
                y = rnd.Next(0, 20);
            } while (grid[x, y] != FIELD_SYMBOL);
            tmp = new ResourceBuilding(x, y, 1000, "GREEN", "O", "Gold", 1, rnd.Next(10, 100));
            buildingsOnMap.Add(tmp);
            grid[x, y] = tmp.Symbol;
            numberOfBuildingsOnMap++;
            // red facotrybuilding
            do
            {
                x = rnd.Next(0, 20);
                y = rnd.Next(0, 20);
            } while (grid[x, y] != FIELD_SYMBOL);
            tmp = new FactoryBuilding(x, y, 1000, "RED", "F");
            buildingsOnMap.Add(tmp);
            grid[x, y] = tmp.Symbol;
            numberOfBuildingsOnMap++;
            //green factorybuilding
            do
            {
                x = rnd.Next(0, 20);
                y = rnd.Next(0, 20);
            } while (grid[x, y] != FIELD_SYMBOL);
            tmp = new FactoryBuilding(x, y, 1000, "GREEN", "F");
            buildingsOnMap.Add(tmp);
            grid[x, y] = tmp.Symbol;
            numberOfBuildingsOnMap++;
        }

        private void moveOnMap(Unit u, int newX, int newY)
        {
            grid[u.X, u.Y] = FIELD_SYMBOL;
            grid[newX, newY] = u.Symbol;
        }

        public void update(Unit u, int newX, int newY)
        {
            if ((newX >= 0 && newX < 20) && (newY >= 0 && newY < 20))
            {
                moveOnMap(u, newX, newY);
                u.move(newX, newY);
            }
        }

        public void checkHealth()
        {
            for (int i = 0; i < numberOfUnitsOnMap; i++)
            {
                if (!unitsOnMap[i].isAlive())
                {
                    grid[unitsOnMap[i].X, unitsOnMap[i].Y] = FIELD_SYMBOL;
                    unitsOnMap.RemoveAt(i);
                    numberOfUnitsOnMap--;
                }
            }
        }

        /*    public void save()
              {
                  Console.WriteLine("save method started");
                  FileStream outFile = null;
                  StreamWriter writer = null;
                  try
                  {
                      outFile = new FileStream(@"Files\Map.txt", FileMode.Append, FileAccess.Write);
                      writer = new StreamWriter(outFile);
                      // convert map class to json
                      // save json to file
                      writer.WriteLine(map_json);
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
        */
        public void load()
        {
            string map_json = System.IO.File.ReadAllText(@"Files\Map.txt");
            displayText = map_json;
            //Map tempMap = JsonConvert.DeserializeObject<Map>(map_json);
           // causes crash
        }
    }
}










      
    

