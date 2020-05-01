using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    //The first map version was laggy so I rethought how to generate the map.
    class MapV2 : GameObjectGrid
    {
        private int scale;
        private int tileSize;
        private Point size = new Point();
        public Color[] floorColors = { Color.Green, Color.DarkGreen };
        public Color[] wallColors = { Color.DarkGreen, new Color(0, 40, 0) };
        public MapV2(int scale, Point size) : base(size.Y, size.X)
        {
            this.scale = scale;
            tileSize = scale * 8;
            CellWidth = tileSize;
            CellHeight = tileSize;
            GenerateMap(size);
            //velocity = new Vector2(-200,-200);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach(GameObject obj in grid)
            {
                obj.Velocity = this.velocity;
            }
        }
        public void GenerateMap(Point size)
        {
            this.size = size;
            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    if (x == 0 || y == 0 || x == size.X-1 || y == size.Y-1 ||GameEnvironment.Random.Next(0, 6) == 0)
                    {
                        Wall wall = new Wall(wallColors[0], wallColors[1], scale);
                        Add(wall, x, y);
                    } else
                    {
                        Floor floor = new Floor(floorColors[0], floorColors[1], scale);
                        Add(floor, x, y);
                    }
                }
            }
            /*
            //Print in console how the map array looks
            for (int y = 0; y < size.Y; y++)
            {
                String consoleLine = "";
                for (int x = 0; x < size.X; x++)
                {
                    if (grid[x,y] is Floor)
                    {
                        consoleLine += 0 + ":";
                    }
                    else
                    {
                        consoleLine += 1 + ":";
                    }
                }
                Console.WriteLine(consoleLine);
            }
            //*/
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
        public int TileSize
        {
            get { return tileSize; }
        }
    }
}
