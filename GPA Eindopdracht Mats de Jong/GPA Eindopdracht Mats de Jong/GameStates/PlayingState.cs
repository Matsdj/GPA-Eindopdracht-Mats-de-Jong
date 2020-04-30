using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class PlayingState : GameObjectList
    {
        static int scale = 8;
        //Map map = new Map(scale);
        public MapV2 map = new MapV2(scale, new Point(100, 100));
        BaseEntity player = new BaseEntity("spr_Humanoid",scale, new Vector2(0,0),true);
        int lineCount = 0;
        public PlayingState()
        {
            this.Add(map);
            int x = GameEnvironment.Random.Next(1, map.Columns);
            int y = GameEnvironment.Random.Next(1, map.Rows);
            player.Position = new Vector2(x, y)*map.TileSize;
            this.Add(player);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position -= player.GlobalPosition - (GameEnvironment.Screen.ToVector2() / 2);
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            base.Draw(gameTime, spriteBatch);
        }
        private static void DrawMap(SpriteBatch spriteBatch, MapV2 map)
        {
            for (int y = 0; y < map.Rows; y++)
            {
                for (int x = 0; x < map.Columns; x++)
                {
                    Texture2D sprite = (map.Get(x, y) as SpriteGameObject).Sprite.Sprite;
                    
                }
            }
        }
    }
}
