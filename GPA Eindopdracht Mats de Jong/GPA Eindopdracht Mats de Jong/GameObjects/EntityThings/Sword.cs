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
        static int swingRangeMax = 180,
            cooldownMax = 1; //in seconds
        static float swingTime = 0.2f; //in seconds
        float damage;
        int swingRange;
        float cooldown;
        GameObject wielder;
        GameObject target;
        List<GameObject> damagedObjects = new List<GameObject>();
        public Sword(int scale, GameObject wielder, GameObject target, float damage) : base("spr_Sword", scale)
        {
            this.wielder = wielder;
            this.damage = damage;
            this.target = target;
            origin = Center;
            offsetDegrees = -45;
            Reset();
        }
        public override void Reset()
        {
            if (swingRange <= 0 && cooldown <= 0)
            {
                swingRange = swingRangeMax;
                AngularDirection = target.GlobalPosition - wielder.GlobalPosition;
                Degrees += swingRangeMax / 2;
                damagedObjects.Clear();
                cooldown = cooldownMax;
                base.Reset();
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (visible)
            {
                position = wielder.Position;
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
