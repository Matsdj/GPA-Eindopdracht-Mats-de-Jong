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
        static float scale = 8;
        Map map = new Map(scale);
        BaseEntity player = new BaseEntity("spr_Humanoid",scale, new Vector2(GameEnvironment.Screen.X/2,GameEnvironment.Screen.Y/2));
        public PlayingState()
        {
            this.Add(map);
            this.Add(player);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            base.Draw(gameTime, spriteBatch);
        }
    }
}
