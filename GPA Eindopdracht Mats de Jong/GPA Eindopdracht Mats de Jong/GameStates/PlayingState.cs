using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class PlayingState : GameObjectList
    {
        static int scale = 6;
        //Map map = new Map(scale);
        public MapV2 map;
        public BaseEntity player;
        GameObjectList world = new GameObjectList();
        public PlayingState()
        {
            map = new MapV2(scale, new Point(50, 50));
            world.Add(map);
            
            player = new BaseEntity("spr_Humanoid", scale, map.RandomFreePositionInMap(), map, true);
            player.movementSpeed *= 1.5f;
            world.Add(player);
            for(int i = 0; i < map.Columns/10; i++)
            {
                world.Add(new BaseEntity("spr_Humanoid", scale, map.RandomFreePositionInMap(), map, false));
            }
            this.Add(world);
            this.Add(new MiniMap(map, player, world, new Point(42, 42), 1, MiniMap.ViewLoc.TopRight));
            this.Add(new Cursor(2));

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            world.Position -= player.GlobalPosition - (GameEnvironment.Screen.ToVector2() / 2);

        }
    }
}
