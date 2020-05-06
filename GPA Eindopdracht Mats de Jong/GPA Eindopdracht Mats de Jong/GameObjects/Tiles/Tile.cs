using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    //Base Tile class
    class Tile : SpriteGameObject
    {
        public Tile(Color light, Color dark, int scale, string assetName) : base(assetName, 0, "", 0, scale)
        {
            sprite.Sprite = Custom.ColorSprite(sprite.Sprite, light, dark);
            origin = Center;
        }
    }
}
