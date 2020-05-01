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
            this.Add(new MiniMap(map, player, new Point(36, 36), 1, MiniMap.ViewLoc.TopRight));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position -= player.GlobalPosition - (GameEnvironment.Screen.ToVector2() / 2);
            
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
