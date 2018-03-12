using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyRTS2018
{
    public class GameEngine : MonoBehaviour
    {

        // Use this for initialization
        public int height;
        public int gameTime = 0;
        public int width;
        public int refresh_number = 5;
        private Map map = new Map();

        void Start()
        {          
            int xStart = 20;
            int yStart = 20;
            var tileSize = new Vector2(2, 2);

            //grass tiles
            for (int k = 0; k < height; k++)
            {
                var yPos = +k * tileSize.y - yStart;
                for (int i = 0; i < width; i++)
                {
                    var xPos = i * tileSize.x - xStart;
                    Instantiate(Resources.Load("grass"), new Vector3(xPos, yPos, 0), Quaternion.identity);

                }
            }
            //new game
            map.populate();

        }
            // Update is called once per frame
            void Update()
        {
            gameTime++;

        //    Debug.Log(refresh_number);
           // Debug.Log(gameTime);
            if (gameTime % refresh_number == 0)
            {
                playGame();
                redraw();
              //  Debug.Log("redraw");
            }
        }

        void playGame()
        {
            int newX, newY;
            Unit closest;
            map.checkHealth();
            // Units attacking eachother
            for (int j = 0; j < map.UnitsOnMap.Count; j++)
            {
                // if unit does not attack
                if (!map.UnitsOnMap[j].Attack)
                {
                    closest = map.UnitsOnMap[j].nearestUnit(map.UnitsOnMap);
                    if (map.UnitsOnMap[j].X < closest.X)
                        newX = map.UnitsOnMap[j].X + 1;

                    else if (map.UnitsOnMap[j].X > closest.X)
                        newX = map.UnitsOnMap[j].X - 1;
                    else
                        newX = map.UnitsOnMap[j].X;

                    if (map.UnitsOnMap[j].Y < closest.Y)
                        newY = map.UnitsOnMap[j].Y + 1;
                    else if (map.UnitsOnMap[j].Y > closest.Y)
                        newY = map.UnitsOnMap[j].Y - 1;
                    else
                        newY = map.UnitsOnMap[j].Y;
                    map.update(map.UnitsOnMap[j], newX, newY);
                }

                // if unit does attack
                if (map.UnitsOnMap[j].Attack)
                {
                    for (int i = 0; i < map.UnitsOnMap.Count; i++)
                    {
                        if (map.UnitsOnMap[j].Faction != map.UnitsOnMap[i].Faction)
                            map.UnitsOnMap[j].combat(map.UnitsOnMap[i]);
                    }
                }
                if (!map.UnitsOnMap[j].Attack)
                {
                    for (int i = 0; i < map.UnitsOnMap.Count; i++)
                    {
                        if ((map.UnitsOnMap[j].Faction != map.UnitsOnMap[i].Faction) && (map.UnitsOnMap[j].Attack))
                            map.UnitsOnMap[j].Attack = true;
                    }
                }
                if (map.UnitsOnMap[j].Health < 25)
                {
                    newX = map.UnitsOnMap[j].X + 1;
                    newY = map.UnitsOnMap[j].Y - 1;
                    map.update(map.UnitsOnMap[j], newX, newY);
                }
            }
        }

        void redraw()
        {
            int xStart = 20;
            int yStart = 20;
            var tileSize = new Vector2(2, 2);
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Unit");
            Debug.Log("=====");
            Debug.Log(allObjects.Length);
            Debug.Log(map.BuildingsOnMap.Count);
            Debug.Log(map.UnitsOnMap.Count);
            // Deleting all Units
            foreach (GameObject child in allObjects)
            {
                Destroy(child.gameObject);
            }

            // Buildings
            for (int j = 0; j < map.BuildingsOnMap.Count; j++)
            {
                var xPos = map.BuildingsOnMap[j].X * tileSize.x - xStart;
                var yPos = map.BuildingsOnMap[j].Y * tileSize.y - yStart;
                // resource buildings
                if (map.BuildingsOnMap[j].GetType() == typeof(ResourceBuilding))
                {
                    // red building
                    if (map.BuildingsOnMap[j].Faction == "RED")
                    {
                        map.resources_red += map.BuildingsOnMap[j].generateResources();
                        Instantiate(Resources.Load("ResourceR"), new Vector3(xPos, yPos, -3), Quaternion.identity);

                    }
                    //green building
                    else
                    {
                        map.resources_green += map.BuildingsOnMap[j].generateResources();
                        Instantiate(Resources.Load("ResourceB"), new Vector3(xPos, yPos, -3), Quaternion.identity);
                    }
                }
                // factory building
                Unit tmpUnit;
                if (map.BuildingsOnMap[j].GetType() == typeof(FactoryBuilding))
                {
                    // red factory
                    if (map.BuildingsOnMap[j].Faction == "RED")
                    {
                      //  Debug.Log(map.BuildingsOnMap[j].X);
                        Instantiate(Resources.Load("FactoryR"), new Vector3(xPos, yPos, -4), Quaternion.identity);
                        string image_name = "hp" + (Mathf.RoundToInt(map.BuildingsOnMap[j].Health / 100)).ToString();
                     //   Debug.Log(image_name);             
                      //  Instantiate(Resources.Load(image_name), new Vector3(xPos, yPos, -5), Quaternion.identity);
                        if (map.resources_red > 10)
                        {
                            tmpUnit = map.BuildingsOnMap[j].spawnNewUnits(map.Grid, map.FIELD_SYMBOL, 1);
                            map.resources_red -= 10;
                            this.map.unitsOnMap.Add(tmpUnit);
                            this.map.grid[tmpUnit.x, tmpUnit.y] = tmpUnit.Symbol;
                        }
                    }
                    // green factory
                    else if (map.BuildingsOnMap[j].Faction == "GREEN")
                    {
                        Instantiate(Resources.Load("FactoryB"), new Vector3(xPos, yPos, -4), Quaternion.identity);
                        if (map.resources_green > 10)
                        {
                            tmpUnit = map.BuildingsOnMap[j].spawnNewUnits(map.Grid, map.FIELD_SYMBOL, 1);
                            map.resources_green -= 10;
                            this.map.unitsOnMap.Add(tmpUnit);
                            this.map.grid[tmpUnit.x, tmpUnit.y] = tmpUnit.Symbol;
                        }
                    }
                }
            }

            // Placing Units
            foreach (Unit temp in map.UnitsOnMap)
            {
                if (temp != null)
                {
                    var unitType = temp.GetType().ToString();
                    var xPos = temp.X * tileSize.x - xStart;
                    var yPos = temp.Y * tileSize.y - yStart;
                    if (unitType.Contains("Melee Unit"))
                    {
                        //melee unit
                        if (temp.Faction.Equals("RED"))
                            Instantiate(Resources.Load("MeleeB"), new Vector3(xPos, yPos, -2), Quaternion.identity);
                        else
                            Instantiate(Resources.Load("MeleeR"), new Vector3(xPos, yPos, -2), Quaternion.identity);
                    }
                    else //ranged unit                            
                    {
                        if (temp.Faction.Equals("RED"))
                            Instantiate(Resources.Load("RangedR"), new Vector3(xPos, yPos, -2), Quaternion.identity);
                        else
                            Instantiate(Resources.Load("RangedB"), new Vector3(xPos, yPos, -2), Quaternion.identity);
                    }
                }
            }
        }
    }
}