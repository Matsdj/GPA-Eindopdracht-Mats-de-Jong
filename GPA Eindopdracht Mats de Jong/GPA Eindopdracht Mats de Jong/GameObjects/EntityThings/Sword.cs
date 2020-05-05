using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class Sword : RotatingSpriteGameObject
    {
        static int swingRangeMax = 120;
        float damage;
        int swingRange;
        public Sword(int scale, GameObject parent, float damage) : base("spr_Sword", scale)
        {
            this.parent = parent;
            this.damage = damage;
            origin.Y = Height;
            Reset();
        }
        public override void Reset()
        {
            base.Reset();
            if (swingRange <= 0)
            {
                swingRange = swingRangeMax;
                Degrees = -45 + swingRange / 2;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position = parent.Position;
            if (swingRange > 0)
            {
                int swingSpeed = swingRangeMax * (int)gameTime.ElapsedGameTime.TotalSeconds;
                Degrees -= swingSpeed;
                swingRange -= swingSpeed;
            } else
            {
                //visible = false;
            }
            
        }
    }
}
