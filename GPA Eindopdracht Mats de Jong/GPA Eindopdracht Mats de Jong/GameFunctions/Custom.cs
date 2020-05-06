using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Custom
{
    public static Texture2D ColorSprite(Texture2D Texture, Color colorLight, Color colorDark)
    {
        //Get the Colors           
        Color[] pixels = new Color[Texture.Width * Texture.Height];
        Texture.GetData<Color>(pixels);
        //Color the white and lightgray pixels
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
    //Is only used in the laggy Map version
    public static Vector2 SnapToGrid(int scale, Vector2 pos)
    {
        int tileSize = scale * 8;
        pos.X = (int)Math.Floor((float)pos.X / tileSize) * tileSize;
        pos.Y = (int)Math.Floor((float)pos.Y / tileSize) * tileSize;
        return pos;
    }
}
