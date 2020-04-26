using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class BaseEntity : RotatingSpriteGameObject
    {
        public BaseEntity(String asset, float scale, Vector2 position) : base(asset, scale)
        {
            this.Sprite.Sprite = Custom.ColorSprite(this.Sprite.Sprite, Color.Chocolate, Color.SaddleBrown);
            this.position = position;
        }
    }
}
