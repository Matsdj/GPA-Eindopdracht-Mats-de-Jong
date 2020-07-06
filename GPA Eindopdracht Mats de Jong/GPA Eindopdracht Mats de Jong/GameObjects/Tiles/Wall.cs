using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class Wall : Tile
    {
        public Wall(int area, int scale) : base(scale, "spr_Wall")
        {
            sprite.Sprite = textures[area].wall;
        }
    }
}
