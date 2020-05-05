using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class Sword : Attack
    {
        protected int swingRangeMax = 180;
        protected float swingTime = 0.2f; //in seconds
        int swingRange;
        public Sword(int scale, GameObject wielder, GameObject target, float damage) : base("spr_Sword", scale, wielder, target, damage)
        {
            offsetDegrees = -45;
            Reset();
        }
        public override void Reset()
        {
            if (swingRange <= 0 && cooldown <= 0)
            {
                base.Reset();
                swingRange = swingRangeMax;
                Degrees += swingRangeMax / 2;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position = wielder.Position;
            if (visible)
            {
                int x = (int)(Math.Cos((Degrees + offsetDegrees / 2) * (Math.PI / 180)) * 32);
                int y = (int)(Math.Sin((Degrees + offsetDegrees / 2) * (Math.PI / 180)) * 32);
                position += new Vector2(x, y);
                int swingSpeed = (int)(swingRangeMax * gameTime.ElapsedGameTime.TotalSeconds / swingTime);
                Degrees -= swingSpeed;
                swingRange -= swingSpeed;
                foreach (GameObject obj in (parent as GameObjectList).Children)
                {
                    if (obj != wielder && obj is BaseEntity)
                    {
                        BaseEntity entity = obj as BaseEntity;
                        if (CollidesWith(entity) && !damagedObjects.Contains(obj))
                        {
                            entity.health -= damage;
                            damagedObjects.Add(entity);
                        }
                    }
                }
            }
            else
            {
                cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (swingRange < 0)
            {
                 visible = false;
            }
        }
    }
}
