using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Custom
{
    public static Color[] ColorSprite(Texture2D Texture, Color colorLight, Color colorDark)
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
        return pixels;
    }
    //Is only used in the laggy Map version
    public static Vector2 SnapToGrid(int scale, Vector2 pos)
    {
        int tileSize = scale * 8;
        pos.X = (int)Math.Floor((float)pos.X / tileSize) * tileSize;
        pos.Y = (int)Math.Floor((float)pos.Y / tileSize) * tileSize;
        return pos;
    }

    public static Color HSL2RGB(double h, double sl, double l)

    {
        double v;
        double r, g, b;

        r = l;   // default to gray
        g = l;
        b = l;
        v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);

        if (v > 0)
        {
            double m;
            double sv;
            int sextant;
            double fract, vsf, mid1, mid2;

            m = l + l - v;
            sv = (v - m) / v;
            h *= 6.0;
            sextant = (int)h;
            fract = h - sextant;
            vsf = v * sv * fract;
            mid1 = m + vsf;
            mid2 = v - vsf;

            switch (sextant)
            {
                case 0:
                    r = v;
                    g = mid1;
                    b = m;
                    break;
                case 1:
                    r = mid2;
                    g = v;
                    b = m;
                    break;
                case 2:
                    r = m;
                    g = v;
                    b = mid1;
                    break;
                case 3:
                    r = m;
                    g = mid2;
                    b = v;
                    break;
                case 4:
                    r = mid1;
                    g = m;
                    b = v;
                    break;
                case 5:
                    r = v;
                    g = m;
                    b = mid2;
                    break;
            }
        }
        Color rgb = new Color(0,0,0,255);
        rgb.R = Convert.ToByte(r * 255.0f);
        rgb.G = Convert.ToByte(g * 255.0f);
        rgb.B = Convert.ToByte(b * 255.0f);
        return rgb;
    }
}
