using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class Map : GameObjectList
    {
        public float scale;
        public float tileSize;
        GameObjectList floor = new GameObjectList();
        GameObjectList walls = new GameObjectList();
        public Map(float scale)
        {
            this.scale = scale;
            tileSize = scale * 8; //8 is the size of the textures I'm using
            this.Add(floor);
            this.Add(walls);
            AddMissingTiles();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void AddMissingTiles()
        {
            Point screen = GameEnvironment.Screen;
            int xTiles = (int)Math.Floor((float)screen.X / tileSize) + 3;
            int yTiles = (int)Math.Floor((float)screen.Y / tileSize) + 3;
            bool[,] view = new bool[xTiles, yTiles];
            foreach (GameObject tile in floor.Children)
            {
                float x = tile.GlobalPosition.X;
                float y = tile.GlobalPosition.Y;
                if (x < screen.X + tileSize && y < screen.Y + tileSize &&
                    x > -tileSize && y > -tileSize)
                {
                    x = (float)Math.Floor((float)x / tileSize) + 1;
                    y = (float)Math.Floor((float)y / tileSize) + 1;
                    view[(int)x, (int)y] = true;
                }
            }
            for (int y = 0; y < yTiles; y++)
            {
                String line = "";
                for (int x = 0; x < xTiles; x++)
                {
                    line += ";" + view[x, y];
                    if (!view[x, y])
                    {
                        Vector2 pos1 = new Vector2(-64 + x * 64, -64 + y * 64);
                        Vector2 pos2;
                        pos2.X = (int)Math.Floor((float)this.position.X / tileSize) * 64;
                        pos2.Y = (int)Math.Floor((float)this.position.Y / tileSize) * 64;
                        newTile((pos1 - pos2));
                    }
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");

            foreach (GameObject tile in floor.Children)
            {
                float x = tile.GlobalPosition.X;
                float y = tile.GlobalPosition.Y;
                if (x < screen.X + tileSize && y < screen.Y + tileSize &&
                    x > -tileSize && y > -tileSize)
                {
                    x = (float)Math.Floor((float)x / tileSize) + 1;
                    y = (float)Math.Floor((float)y / tileSize) + 1;
                    view[(int)x, (int)y] = true;
                }
            }
            for (int y = 0; y < yTiles; y++)
            {
                String line = "";
                for (int x = 0; x < xTiles; x++)
                {
                    line += ";" + view[x, y];
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");
        }
        public void newTile(Vector2 position)
        {
            SpriteGameObject floorTile = new SpriteGameObject("spr_Floor", 0, "", 0, scale);
            floorTile.Sprite.Sprite = Custom.ColorSprite(floorTile.Sprite.Sprite, Color.Green, Color.DarkGreen);
            floorTile.Position = position;
            floor.Add(floorTile);
            if (GameEnvironment.Random.Next(0,6) == 0)
            {
                SpriteGameObject wall = new SpriteGameObject("spr_Wall", 0, "", 0, scale);
                wall.Position = position;
                wall.Sprite.Sprite = Custom.ColorSprite(wall.Sprite.Sprite, Color.Black, Color.DarkGreen);
                walls.Add(wall);
            }
            Console.WriteLine("tile added at:"+ floorTile.Position);
        }
    }
}
