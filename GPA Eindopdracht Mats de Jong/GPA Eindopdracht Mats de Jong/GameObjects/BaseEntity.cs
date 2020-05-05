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
        protected MapV2 map;
        protected GameObjectList world;
        protected PathfindingAI pathfindingAI;
        protected SpriteGameObject targetEntity;
        protected Vector2[] path;
        protected RotatingSpriteGameObject meleeAttack;
        public BaseEntity(String asset, int scale, Vector2 position, MapV2 map, GameObjectList world, bool isPlayer, SpriteGameObject targetEntity) : base(asset, scale)
        {
            this.isPlayer = isPlayer;

            this.Origin = this.Center;
            this.Sprite.Sprite = Custom.ColorSprite(this.Sprite.Sprite, Color.Chocolate, Color.SaddleBrown);

            this.position = position;
            this.map = map;
            this.pathfindingAI = new PathfindingAI();
            this.targetEntity = targetEntity;
            this.world = world;

            this.movementSpeed *= scale;
            this.meleeAttack = new Sword(scale, this, targetEntity, attack);
            world.Add(meleeAttack);
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
                if (obj is Wall && CollidesWith(obj))
                {
                    while (CollidesWith(obj) && position != obj.Position)
                    {
                        Vector2 dist = position - obj.Position;
                        position += dist / dist.Length();
                    }
                }
            }
        }
        public virtual void EntityCollision()
        {
            foreach(GameObject obj in world.Children)
            {
                if (obj is BaseEntity && obj != this)
                {
                    if ((obj.Position - position).Length() < Width*scale)
                    {
                        Vector2 dist = Position - obj.Position;
                        Position += dist / dist.Length();
                    }
                }
            }
        }
        public virtual void AI()
        {
            if (targetEntity == null)
            {
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
                Rectangle spritePart = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
                for (int i = 0; i < path.Length; i++)
                {
                    spriteBatch.Draw(Sprite.Sprite, path[i] + parent.Position + Origin, spritePart, new Color(255,255,255,100), 0.0f, new Vector2(0, 0), scale/2, SpriteEffects.None, 0);
                }
                spriteBatch.Draw(Sprite.Sprite, path[0] + parent.Position + Origin, spritePart, new Color(0, 255, 0, 100), 0.0f, new Vector2(0, 0), scale/2, SpriteEffects.None, 0);
                spriteBatch.Draw(Sprite.Sprite, path[path.Length-1] + parent.Position + Origin, spritePart, new Color(255, 0, 0, 100), 0.0f, new Vector2(0, 0), scale/2, SpriteEffects.None, 0);
            }
            base.Draw(gameTime, spriteBatch);
            
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (isPlayer)
            {
                //Movement
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
                //Attack1
                if (inputHelper.MouseLeftButtonDown())
                {
                    meleeAttack.Reset();
                }
            }
        }
    }
}
