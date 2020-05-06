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
        protected bool isPlayer;
        public float movementSpeed = 16,
            maxHealth = 10,
            health = 0,
            attack = 5;
        protected MapV2 map;
        protected GameObjectList world;
        protected PathfindingAI pathfindingAI;
        protected SpriteGameObject targetEntity;
        protected Vector2[] path;
        protected Attack primaryAttack;
        protected Attack secondaryAttack;
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
            this.health = maxHealth;
            this.primaryAttack = new Sword(scale, this, targetEntity, attack);
            this.secondaryAttack = new Bolt(scale, this, targetEntity, attack, map);
            world.Add(primaryAttack);
            world.Add(secondaryAttack);
        }
        public override void Update(GameTime gameTime)
        {
            if (velocity != new Vector2(0, 0)) AngularDirection = velocity;
            if (!isPlayer && visible) { AI(); }
            WallCollision();
            EntityCollision();
            base.Update(gameTime);
            if (visible && health <= 0)
            {
                visible = false;
                velocity = new Vector2(0, 0);
                (parent.Parent as PlayingState).hud.score += 10;
            }
        }
        public virtual void WallCollision()
        {
            //Pushes you out of wall
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
            //Pushes you out of other Entities
            if (visible) foreach (GameObject obj in world.Children)
                {
                    if (obj.Visible && obj != this && obj is BaseEntity)
                    {
                        if ((obj.Position - position).Length() < Width * scale)
                        {
                            Vector2 dist = Position - obj.Position;
                            Position += dist / dist.Length();
                        }
                    }
                }
        }
        public virtual void AI()
        {
            //Follows path that is generated
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
            //Auto attacks
            if ((targetEntity.Position - position).Length() < map.CellWidth * 2) primaryAttack.Reset();
            if ((targetEntity.Position - position).Length() < map.CellWidth * 10) secondaryAttack.Reset();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible)
            {
                base.Draw(gameTime, spriteBatch);

                //Draw Health above entity
                Rectangle BackBar = new Rectangle((int)GlobalPosition.X - Width * scale / 2, (int)GlobalPosition.Y - Height * scale,
                    (int)Width * scale, (int)Height * scale / 4);
                DrawingHelper.DrawRectangle(BackBar, spriteBatch, Color.Black, true);
                Rectangle healthBar = new Rectangle((int)GlobalPosition.X - Width * scale / 2, (int)GlobalPosition.Y - Height * scale,
                    (int)(Width * scale * (health / maxHealth)), (int)Height * scale / 4);
                DrawingHelper.DrawRectangle(healthBar, spriteBatch, Color.Lime, true);
            }
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (isPlayer && visible)
            {
                //Movement
                if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.IsKeyDown(Keys.D)) { velocity.X = 1; }
                else
                    if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.IsKeyDown(Keys.A)) { velocity.X = -1; }
                else
                    velocity.X = 0;


                if (inputHelper.IsKeyDown(Keys.Down) || inputHelper.IsKeyDown(Keys.S)) { velocity.Y = 1; }
                else
                    if (inputHelper.IsKeyDown(Keys.Up) || inputHelper.IsKeyDown(Keys.W)) { velocity.Y = -1; }
                else
                    velocity.Y = 0;
                if (velocity != new Vector2(0, 0))
                {
                    velocity = velocity / velocity.Length() * movementSpeed;
                }
                //Attack1
                if (inputHelper.MouseLeftButtonDown())
                {
                    primaryAttack.Reset();
                }
                //Attack2
                if (inputHelper.MouseRightButtonDown())
                {
                    secondaryAttack.Reset();
                }
            }
        }
        public bool IsPlayer
        {
            get { return isPlayer; }
        }
        public Attack PrimaryAttack
        {
            get { return primaryAttack; }
        }
        public Attack SecondaryAttack
        {
            get { return secondaryAttack; }
        }
    }
}
