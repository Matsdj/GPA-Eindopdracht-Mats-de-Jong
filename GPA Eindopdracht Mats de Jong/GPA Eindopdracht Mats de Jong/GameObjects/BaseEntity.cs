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
        float movementSpeed = 160;
        public BaseEntity(String asset, int scale, Vector2 position, bool isPlayer) : base(asset, scale)
        {
            this.isPlayer = isPlayer;
            this.Sprite.Sprite = Custom.ColorSprite(this.Sprite.Sprite, Color.Chocolate, Color.SaddleBrown);
            this.position = position;
            origin = Center;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (velocity != new Vector2(0, 0)) AngularDirection = velocity;
            WallCollision(gameTime);
        }
        public void WallCollision(GameTime gameTime)
        {
            GameObjectGrid map = (parent as PlayingState).map;
            foreach (SpriteGameObject obj in map.Objects)
            {
                if (obj is Wall && this.CollidesWith(obj))
                {
                    while (this.CollidesWith(obj))
                    {
                        Vector2 dist = position - obj.Position;
                        position += dist / dist.Length();
                    }
                    //Console.WriteLine("[COLLISION] Wall:" + obj.Position + "Bbox" + obj.BoundingBox + " Player:" + player.Position + "Bbox" + player.BoundingBox + "[" + lineCount + "]"); lineCount++;
                }
            }
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.IsKeyDown(Keys.Right)) { velocity.X = movementSpeed; }
            else
                if (inputHelper.IsKeyDown(Keys.Left)) { velocity.X = -movementSpeed; }
            else
                velocity.X = 0;


            if (inputHelper.IsKeyDown(Keys.Down)) { velocity.Y = movementSpeed; }
            else
                if (inputHelper.IsKeyDown(Keys.Up)) { velocity.Y = -movementSpeed; }
            else
                velocity.Y = 0;
        }
    }
}
