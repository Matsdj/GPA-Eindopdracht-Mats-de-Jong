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
        public BaseEntity(String asset, float scale) : base(asset, scale)
        {
            this.Sprite.Sprite = ColorSprite(this.Sprite.Sprite, Color.Chocolate, Color.SaddleBrown);
        }
        public static Texture2D ColorSprite(Texture2D Texture, Color colorLight, Color colorDark)
        {
            //Get the Colors           
            Color[] pixels = new Color[Texture.Width * Texture.Height];
            Texture.GetData<Color>(pixels);
            //Color the white pixels
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].R == 255 && pixels[i].G == 255 && pixels[i].B == 255)
                {
                    pixels[i] = colorLight;
                }
                if (pixels[i].R == 200 && pixels[i].G == 200 && pixels[i].B == 200)
                {
                    pixels[i] = colorDark;
                }
            }
            //Return            
            Texture.SetData<Color>(pixels);
            return Texture;
        }
    }
}
