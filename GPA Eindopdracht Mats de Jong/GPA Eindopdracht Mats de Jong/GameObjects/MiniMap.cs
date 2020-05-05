using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class MiniMap : GameObject
    {
        protected BaseEntity player;
        protected MapV2 map;
        protected GameObjectList world;
        protected Point tileCount;
        private Rectangle miniMapRect;
        protected float drawScale;
        protected ViewLoc viewLoc;
        protected int spriteWidth;

        public MiniMap(MapV2 map, BaseEntity player, GameObjectList world, Point tileCount, float drawScale, ViewLoc viewLoc)
        {
            this.map = map;
            this.player = player;
            this.world = world;
            this.tileCount = tileCount;
            this.drawScale = drawScale;
            this.viewLoc = viewLoc;

            Texture2D tileTexture = (map.Objects[0,0] as SpriteGameObject).Sprite.Sprite;
            int realTileSize = (int)(tileTexture.Width * drawScale);
            this.spriteWidth = tileTexture.Width;

            int realWidth = (tileCount.X) * realTileSize;
            int realHeight = (tileCount.Y) * realTileSize;
            miniMapRect = new Rectangle((int)DrawLoc.X, (int)DrawLoc.Y, realWidth, realHeight);
        }
        public enum ViewLoc
        {
            TopLeft,
            TopRight
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            miniMapRect.Location = new Point((int)DrawLoc.X, (int)DrawLoc.Y);
            Rectangle backgroundRect = new Rectangle(miniMapRect.X - spriteWidth, miniMapRect.Y - spriteWidth, miniMapRect.Width + spriteWidth*2, miniMapRect.Height + spriteWidth*2);
            DrawingHelper.DrawRectangle(backgroundRect, spriteBatch, Color.Gray, true);
            DrawingHelper.DrawRectangle(miniMapRect, spriteBatch, Color.Black, true);
            DrawMiniMap(spriteBatch);
        }
        private void DrawMiniMap(SpriteBatch spriteBatch)
        {
            for (int y = Math.Max(map.GridLoc(player).Y - (int)(tileCount.Y / 2), 0); y < map.Rows && y < map.GridLoc(player).Y + (int)(tileCount.Y / 2); y++)
            {
                for (int x = Math.Max(map.GridLoc(player).X - (int)(tileCount.X / 2), 0); x < map.Columns && x < map.GridLoc(player).X + (int)(tileCount.X / 2); x++)
                {
                    DrawOneTile(spriteBatch,map.Get(x,y) as SpriteGameObject);
                }
            }
            foreach(GameObject obj in world.Children)
            {
                if (obj is BaseEntity)
                {
                    DrawOneTile(spriteBatch, (obj as BaseEntity));
                }
            }
        }
        private void DrawOneTile(SpriteBatch spriteBatch, SpriteGameObject obj)
        {
            Texture2D sprite = obj.Sprite.Sprite;
            Vector2 realPos = ConvertToRealPosistion(obj);
            if (realPos.X >= miniMapRect.X && realPos.X < miniMapRect.X + miniMapRect.Width && realPos.Y >= miniMapRect.Y && realPos.Y < miniMapRect.Y + miniMapRect.Height)
            {
                Rectangle spritePart = new Rectangle(0, 0, sprite.Width, sprite.Height);
                Color col = Color.White;
                if (obj is RotatingSpriteGameObject)
                {
                    RotatingSpriteGameObject rotObj = obj as RotatingSpriteGameObject;
                    realPos += new Vector2(0.5f * spritePart.Width, 0.5f * spritePart.Height);
                    spriteBatch.Draw(sprite, realPos, null, col, rotObj.Angle, rotObj.Origin, drawScale, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(sprite, realPos, spritePart, col, 0.0f, new Vector2(0, 0), drawScale, SpriteEffects.None, 0);
                }
            }
        }
        private Vector2 ConvertToRealPosistion(SpriteGameObject obj)
        {
            float x = map.GridLoc(obj).X;
            float y = map.GridLoc(obj).Y;
            Texture2D sprite = obj.Sprite.Sprite;
            int xReal = (int)((x - (map.GridLoc(player).X - tileCount.X / 2)) * sprite.Width * drawScale + DrawLoc.X);
            int yReal = (int)((y - (map.GridLoc(player).Y - tileCount.Y / 2)) * sprite.Height * drawScale + DrawLoc.Y);
            return new Vector2(xReal, yReal);
        }
        public Vector2 DrawLoc
        {
            get 
            {
                if (viewLoc == ViewLoc.TopRight)
                {
                    return new Vector2(GameEnvironment.Screen.X - miniMapRect.Width, 0);
                } else
                {
                    return new Vector2(0,0);
                }
            }
        }
    }
}