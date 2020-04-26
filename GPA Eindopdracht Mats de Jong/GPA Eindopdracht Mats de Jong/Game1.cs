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
            screen = new Point(1080, 720);
            ApplyResolutionSettings();

            GameStateManager.AddGameState("PlayState", new PlayingState());
            GameStateManager.SwitchTo("PlayState");
        }
    }
}
