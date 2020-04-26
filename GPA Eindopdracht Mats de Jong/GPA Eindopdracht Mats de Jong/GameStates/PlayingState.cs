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
        static float scale = 6;
        //SpriteGameObject background = new SpriteGameObject("Asset");
        BaseEntity player = new BaseEntity("spr_Humanoid",scale);
        public PlayingState()
        {
            this.Add(player);
            Console.WriteLine(player.scale);
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
