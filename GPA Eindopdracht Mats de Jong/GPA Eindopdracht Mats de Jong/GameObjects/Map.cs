using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    //This map version is laggy and not used later in this project
    class Map : GameObjectList
    {
        public int scale;
        public int tileSize;
        GameObjectList floor = new GameObjectList();
        GameObjectList walls = new GameObjectList();
        SpriteSheet floorSprite;
        SpriteSheet wallSprite;
        public Map(int scale)
        {
            this.scale = scale;
            tileSize = scale * 8; //8 is the size of the textures I'm using
            this.Add(floor);
            this.Add(walls);
            AddMissingTiles();
            floorSprite = new SpriteSheet("spr_Floor", scale);
            wallSprite = new SpriteSheet("spr_Wall", scale);
            //Colors the base sprites
            floorSprite.Sprite = Custom.ColorSprite(floorSprite.Sprite, Color.Green, Color.DarkGreen);
            wallSprite.Sprite = Custom.ColorSprite(wallSprite.Sprite, Color.Black, Color.DarkGreen);

            velocity = new Vector2(200,200);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            AddMissingTiles();
        }
        //This was to laggy
        public void AddMissingTiles()
        {
            Point screen = GameEnvironment.Screen;
            int xTiles = (int)Math.Floor((float)screen.X / tileSize) + 3;
            int yTiles = (int)Math.Floor((float)screen.Y / tileSize) + 3;
            bool[,] view = new bool[xTiles, yTiles];
            foreach (GameObject tile in floor.Children)
            {
                Vector2 pos = new Vector2(tile.GlobalPosition.X, tile.GlobalPosition.Y);
                if (pos.X < screen.X + tileSize && pos.Y < screen.Y + tileSize &&
                    pos.X >= -tileSize && pos.Y >= -tileSize)
                {
                    pos = Custom.SnapToGrid(scale,pos) / tileSize + new Vector2(1, 1);
                    view[(int)pos.X, (int)pos.Y] = true;
                }
            }
            for (int y = 0; y < yTiles; y++)
            {
                for (int x = 0; x < xTiles; x++)
                {
                    if (!view[x, y])
                    {
                        Vector2 pos1 = new Vector2(-tileSize + x * tileSize, -tileSize + y * tileSize);
                        Vector2 pos2 = Custom.SnapToGrid(scale,this.position);
                        newTile((pos1 - pos2));
                    }
                }
            }
        }
        public void newTile(Vector2 position)
        {
            SpriteGameObject floorTile = new SpriteGameObject("spr_Floor", 0, "", 0, scale);
            floorTile.Sprite = floorSprite;
            floorTile.Position = position;
            floor.Add(floorTile);

            if (GameEnvironment.Random.Next(0,6) == 0)
            {
                SpriteGameObject wall = new SpriteGameObject("spr_Wall", 0, "", 0, scale);
                wall.Position = position;
                wall.Sprite = wallSprite;
                walls.Add(wall);
            }
        }
    }
}
