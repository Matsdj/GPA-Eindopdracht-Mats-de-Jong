using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class Floor : SpriteGameObject
    {
        public Floor(Color light, Color dark, int scale) : base("spr_Floor", 0, "", 0, scale)
        {
            sprite.Sprite = Custom.ColorSprite(sprite.Sprite,light,dark);
            origin = Center;
        }
    }
}
