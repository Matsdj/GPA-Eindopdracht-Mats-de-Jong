using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class Cursor : SpriteGameObject
    {
        public Cursor(int scale = 1) : base("spr_Cursor",0,"",0, scale)
        {

        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            position = inputHelper.MousePosition;
        }
    }
}
