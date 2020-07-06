using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    //Base Tile class
    public class Tile : SpriteGameObject
    {
        public static List<areaTextures> textures;
        public static areaTextures baseTextures;
        public Tile(int scale, string assetName) : base(assetName, 0, "", 0, scale)
        {
            if (textures == null)
            {
                textures = new List<areaTextures>();
                Texture2D floor = GameEnvironment.AssetManager.GetSprite("spr_Floor");
                Texture2D wall = GameEnvironment.AssetManager.GetSprite("spr_Wall");
                baseTextures = new areaTextures(floor, wall);
                addArea();
            }

            origin = Center;
        }

        public void addArea()
        {
            textures.Add(new areaTextures());
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this is Floor)
            {
                sprite.Sprite = textures[0].floor;
            }
            if (this is Wall)
            {
                sprite.Sprite = textures[0].wall;
            }
        }
    }
    public class areaTextures
    {
        public Texture2D floor;
        public Texture2D wall;

        public areaTextures(Texture2D floor, Texture2D wall)
        {
            this.floor = floor;
            this.wall = wall;
        }
        public areaTextures()
        {
            Color mainCol = new Color(GameEnvironment.Random.Next(0, 255), GameEnvironment.Random.Next(0, 255), GameEnvironment.Random.Next(0, 255));
            createAreaTextures(mainCol);
        }
        public areaTextures(Color mainCol)
        {
            createAreaTextures(mainCol);
        }
        private void createAreaTextures(Color mainCol)
        {
            float lightness = 0.2f; //Higher is lighter
            float darkness = 0.2f; //Lower is darker
            Color light = new Color((int)(mainCol.R + (255 - mainCol.R) * lightness), (int)(mainCol.G + (255 - mainCol.G) * lightness), (int)(mainCol.B + (255 - mainCol.B) * lightness));
            Color dark = new Color((int)(mainCol.R * darkness), (int)(mainCol.G * darkness), (int)(mainCol.B * darkness));

            floor = new Texture2D(GameEnvironment.Graphics, Tile.baseTextures.floor.Width, Tile.baseTextures.floor.Height);
            wall = new Texture2D(GameEnvironment.Graphics, Tile.baseTextures.wall.Width, Tile.baseTextures.wall.Height);
            floor.SetData<Color>(Custom.ColorSprite(Tile.baseTextures.floor, light, mainCol));
            wall.SetData<Color>(Custom.ColorSprite(Tile.baseTextures.wall, mainCol, dark));
        }
    }
}
