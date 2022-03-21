using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_soria;

namespace Monogame_Soria
{
    public abstract class MultiImageSprite : Sprite
    {
        protected Dictionary<object, Texture2D> images;
        protected object selectedImage;

        public MultiImageSprite()
        {
            images = new Dictionary<object, Texture2D>();
        }
        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(
                images[selectedImage],
                Rectangle,
                Color.White);
        }
    }
}
