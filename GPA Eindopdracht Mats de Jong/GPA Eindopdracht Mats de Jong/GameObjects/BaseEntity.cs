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
    class BaseEntity : RotatingSpriteGameObject
    {
        bool isPlayer;
        public float movementSpeed = 32,
            health = 10, 
            attack = 5;
        MapV2 map;
        PathfindingAI pathfindingAI;
        BaseEntity targetEntity;
        Vector2[] path;
        public BaseEntity(String asset, int scale, Vector2 position, MapV2 map, bool isPlayer) : base(asset, scale)
        {
            this.isPlayer = isPlayer;
            this.Sprite.Sprite = Custom.ColorSprite(this.Sprite.Sprite, Color.Chocolate, Color.SaddleBrown);
            this.position = position;
            this.map = map;
            this.origin = Center;
            this.movementSpeed *= scale;
            this.pathfindingAI = new PathfindingAI();
        }
        public override void Update(GameTime gameTime)
        {
            if (velocity != new Vector2(0, 0)) AngularDirection = velocity;
            if (!isPlayer) { AI(); }
            WallCollision();
            EntityCollision();
            base.Update(gameTime);
        }
        public virtual void WallCollision()
        {
            foreach (SpriteGameObject obj in map.Objects)
            {
                if (obj is Wall && this.CollidesWith(obj))
                {
                    while (this.CollidesWith(obj) && position != obj.Position)
                    {
                        Vector2 dist = position - obj.Position;
                        position += dist / dist.Length();
                    }
                }
            }
        }
        public virtual void EntityCollision()
        {
            foreach(GameObject obj in (parent as GameObjectList).Children)
            {
                if (obj is BaseEntity && obj != this)
                {
                    if ((obj.Position - position).Length() < Width*scale)
                    {
                        Vector2 dist = position - obj.Position;
                        position += dist / dist.Length();
                    }
                }
            }
        }
        public virtual void AI()
        {
            if (targetEntity == null)
            {
                GameObjectList world = parent as GameObjectList;
                foreach (GameObject obj in world.Children)
                {
                    if (obj is BaseEntity)
                    {
                        BaseEntity entity = obj as BaseEntity;
                        if (entity.isPlayer) targetEntity = entity;
                    }
                }
            }
            if (targetEntity != null)
            {
                path = pathfindingAI.findPath(map, this, targetEntity);
                if (path.Length > 1 && path[1] != new Vector2(0, 0))
                {
                    Vector2 distToNext = path[1] - position;
                    if (distToNext.Length() != 0) velocity = distToNext / distToNext.Length() * movementSpeed;
                }
                else
                {
                    velocity = new Vector2(0, 0);
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (path != null && path.Length != 0)
            {
                Rectangle spritePart = new Rectangle(0, 0, sprite.Width, sprite.Height);
                for (int i = 0; i < path.Length; i++)
                {
                    spriteBatch.Draw(Sprite.Sprite, path[i] + parent.Position + origin, spritePart, new Color(255,255,255,100), 0.0f, new Vector2(0, 0), scale/2, SpriteEffects.None, 0);
                }
                spriteBatch.Draw(Sprite.Sprite, path[0] + parent.Position + origin, spritePart, new Color(0, 255, 0, 100), 0.0f, new Vector2(0, 0), scale/2, SpriteEffects.None, 0);
                spriteBatch.Draw(Sprite.Sprite, path[path.Length-1] + parent.Position + origin, spritePart, new Color(255, 0, 0, 100), 0.0f, new Vector2(0, 0), scale/2, SpriteEffects.None, 0);
            }
            base.Draw(gameTime, spriteBatch);
            
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (isPlayer)
            {
                if (inputHelper.IsKeyDown(Keys.Right)) { velocity.X = 1; }
                else
                    if (inputHelper.IsKeyDown(Keys.Left)) { velocity.X = -1; }
                else
                    velocity.X = 0;


                if (inputHelper.IsKeyDown(Keys.Down)) { velocity.Y = 1; }
                else
                    if (inputHelper.IsKeyDown(Keys.Up)) { velocity.Y = -1; }
                else
                    velocity.Y = 0;
                if (velocity != new Vector2(0, 0))
                {
                    velocity = velocity / velocity.Length() * movementSpeed;
                }
            }
        }
    }
}
