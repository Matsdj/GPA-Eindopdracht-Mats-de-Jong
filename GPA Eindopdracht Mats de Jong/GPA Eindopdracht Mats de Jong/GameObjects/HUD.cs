using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class HUD : TextGameObject
    {
        public int score;
        protected BaseEntity player;
        public HUD(BaseEntity player) : base("GameFont")
        {
            text = "test";
            this.player = player;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            text = "score: " + score + "\n" +
                "Health: " + player.health + "\n" +
                "primaryCooldown: " + (int)player.PrimaryAttack.Cooldown + "\n" +
                "secondaryCooldown: " + (int)player.SecondaryAttack.Cooldown;
        }
    }
}
