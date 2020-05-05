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
        static int swingRangeMax = 180;
        static float swingTime = 0.2f; //in seconds
        float damage;
        int swingRange;
        GameObject wielder;
        GameObject target;
        public Sword(int scale, GameObject wielder, GameObject target, float damage) : base("spr_Sword", scale)
        {
            this.wielder = wielder;
            this.damage = damage;
            this.target = target;
            origin.Y = Height;

            Reset();
        }
        public override void Reset()
        {
            base.Reset();
            if (swingRange <= 0)
            {
                swingRange = swingRangeMax;
                AngularDirection = target.GlobalPosition - wielder.GlobalPosition;
                offsetDegrees = -45;
                Degrees += swingRangeMax / 2;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position = wielder.Position;
            if (visible)
            {
                int swingSpeed = (int)(swingRangeMax * gameTime.ElapsedGameTime.TotalSeconds / swingTime);
                Degrees -= swingSpeed;
                swingRange -= swingSpeed;
            } 
            if (swingRange < 0)
            {
                 visible = false;
            }
            
        }
    }
}
