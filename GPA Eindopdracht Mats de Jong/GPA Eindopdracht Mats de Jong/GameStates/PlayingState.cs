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
        SpriteGameObject background;
        public MapV2 map;
        public BaseEntity player;
        GameObjectList world = new GameObjectList();
        public HUD hud;
        public PlayingState()
        {
            background = new SpriteGameObject("spr_Background",0,"",0,80);
            this.Add(background);
            Cursor cursor = new Cursor(2);

            map = new MapV2(scale, new Point(50, 50));
            world.Add(map);
            
            player = new BaseEntity("spr_Humanoid", scale, map.RandomFreePositionInMap(), map, world, true, cursor);
            //Buffs player because else it is to hard
            player.movementSpeed *= 3;
            player.maxHealth *= 4;
            player.health *= 4;

            world.Add(player);
            //Adds enemies
            for(int i = 0; i < map.Columns/5; i++)
            {
                world.Add(new BaseEntity("spr_Humanoid", scale, map.RandomFreePositionInMap(), map, world, false, player));
            }
            this.Add(world);
            this.Add(new MiniMap(map, player, world, new Point(42, 42), 1, MiniMap.ViewLoc.TopRight));

            hud = new HUD(player);
            this.Add(hud);
            this.Add(cursor);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            world.Position -= player.GlobalPosition - (GameEnvironment.Screen.ToVector2() / 2);
            //Checks if the game is over
            bool enemiesExist = false;
            foreach(GameObject obj in world.Children)
            {
                if(obj is BaseEntity && (obj as BaseEntity).Visible && !(obj as BaseEntity).IsPlayer)
                {
                    enemiesExist = true;
                }
            }
            if (player.health <= 0 || !enemiesExist) { GameEnvironment.GameStateManager.SwitchTo("StartState"); }
        }

        private float col;
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.IsKeyDown(Keys.Space))
            {
                col += 0.01f;
                if (col > 1) col = 0;
                Color mainColor = Custom.HSL2RGB(col, 0.5, 0.5);
                if (inputHelper.IsKeyDown(Keys.LeftShift))
                {
                    Tile.textures[0].floor = (new areaTextures()).floor;
                    Tile.textures[0].wall = (new areaTextures()).wall;
                }
                else
                {
                    Tile.textures[0] = new areaTextures(mainColor);
                }
            }
        }
    }
}
