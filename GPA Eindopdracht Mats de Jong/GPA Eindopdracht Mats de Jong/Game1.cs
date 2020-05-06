using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : GameEnvironment
    {
        protected override void LoadContent()
        {
            base.LoadContent();
            // Create a new SpriteBatch, which can be used to draw textures.
            screen = new Point(1280, 720);
            ApplyResolutionSettings();

            PlayingState playingState = new PlayingState();
            GameStateManager.AddGameState("StartState", new StartState(playingState));
            GameStateManager.SwitchTo("StartState");
            GameStateManager.AddGameState("PlayState", playingState);
        }
    }
}
