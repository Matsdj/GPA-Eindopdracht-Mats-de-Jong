using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GPA_SlimeCrash
{
    class GridObject : SpriteGameObject
    {
        public int resistance;

        public GridObject(string assetName, float scale = 1, int resistance = 1, int layer = 0, string id = "", int sheetIndex = 0) : base(assetName, layer, id, sheetIndex, scale)
        {
            this.resistance = resistance;
        }
    }
}
