using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class StartState : GameObjectList
    {
        PlayingState playingState;
        TextGameObject info;
        public StartState(PlayingState playingState)
        {
            //Needs playingstate to get score
            this.playingState = playingState;

            this.Add(new SpriteGameObject("spr_Background", 0, "", 0, 80));

            info = new TextGameObject("GameFont", 0, "");
            info.Text = "Press any key to play";
            info.Position = new Vector2(GameEnvironment.Screen.X / 2 - info.Size.X / 2, GameEnvironment.Screen.Y / 2);
            this.Add(info);
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            int score = playingState.hud.score;
            if (score > 0)
            {
                info.Text = "Your score was: " + score;
            }
            else
            {
                if (inputHelper.AnyKeyPressed)
                {
                    GameEnvironment.GameStateManager.SwitchTo("PlayState");
                }
            }
        }
    }
}
