using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fantasy
{
    class Level
    {
        static int tileWidth = 30;
        static int tileHeight = 30;
        const int MAPSIZE = 24;
        int[,] map;

        // Overloaded Constructor
        public Level(string name)
        {
            // Load Level
            LoadMapData(name);
        }

        // Getters and Setters
        public int[,] GetSet
        { get { return map; } set { map = value; } }
        // Update 
        public byte Collision(Vector2 tilePos)
        {
            // Collisions
            if (map[(int)tilePos.Y, (int)tilePos.X] == 1)
                return 1;
            // Enemy
            else if (map[(int)tilePos.Y, (int)tilePos.X] == 2)
                return 2; 
            // Nothing
            return 0;

        }
        public void Draw(SpriteBatch spriteBatch, Texture2D tiles)
        {
            for (int y = 0; y < MAPSIZE; y++)
            {
                for (int x = 0; x < MAPSIZE; x++)
                {
                    spriteBatch.Draw(tiles, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(map[y, x] * 128 , 0, 128, 128), Color.White);
                }
            }
        }
        // Load Map
        public void LoadMapData(string name)
        {
            string path = "Levels/" + name + ".txt";

            // Width and height of our tile array
            int width = 0;
            int height = File.ReadLines(path).Count();

            StreamReader sReader = new StreamReader(path);
            string line = sReader.ReadLine();
            string[] tileNo = line.Split(',');

            width = tileNo.Count();

            // Creating a new instance of the tile map
            map = new int[height, width];
            sReader.Close();

            // Re-initialising sReader
            sReader = new StreamReader(path);

            for (int y = 0; y < height; y++)
            {
                line = sReader.ReadLine();
                tileNo = line.Split(',');

                for (int x = 0; x < width; x++)
                    map[y, x] = Convert.ToInt32(tileNo[x]);
            }
            sReader.Close();
        }
    }
}