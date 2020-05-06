using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class Bolt : Attack
    {
        protected int lifeTimeMax = 2; //in seconds, decides how long the bolt lives
        protected float speed = 128;
        protected float piercing = 1;
        GameObjectGrid grid;
        public Bolt(int scale, GameObject wielder, GameObject target, float damage, GameObjectGrid grid) : base("spr_Bolt", scale, wielder, target, damage, 6)
        {
            //Grid is needed for wall collision
            this.grid = grid;
            Reset();
            cooldown = 0;
        }
        //Reset is called when an entity attacks
        public override void Reset()
        {
            if (cooldown <= 0)
            {
                base.Reset();
                position = wielder.Position;
                Vector2 dist = target.GlobalPosition - wielder.GlobalPosition;
                if (dist.Length() > 0)
                {
                    velocity = dist / dist.Length() * speed * scale;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (visible)
            {
                //Damage other BaseEntities
                foreach (GameObject obj in (parent as GameObjectList).Children)
                {
                    if (obj != wielder && obj is BaseEntity)
                    {
                        BaseEntity entity = obj as BaseEntity;
                        if (CollidesWith(entity) && !damagedObjects.Contains(obj) && damagedObjects.Count <= piercing)
                        {
                            entity.health -= damage;
                            damagedObjects.Add(entity);
                        }
                    }
                }
                //Disapear against walls
                foreach (SpriteGameObject obj in grid.Objects)
                {
                    if (obj is Wall && CollidesWith(obj))
                    {
                        if (CollidesWith(obj) && position != obj.Position)
                        {
                            Vector2 dist = position - obj.Position;
                            position += dist / dist.Length();
                            visible = false;
                        }
                    }
                }
            }
            //Disapear when cooldown is 0 or when it has attacked to entities
            if (damagedObjects.Count > piercing || cooldown <= 0)
            {
                visible = false;
            }
            if (cooldown > 0)
            {
                cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (wielder is BaseEntity && (wielder as BaseEntity).IsPlayer) cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
