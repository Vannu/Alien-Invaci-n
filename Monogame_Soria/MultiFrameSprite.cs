using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_soria;
using System.Collections.Generic;

namespace Monogame_Soria
{
    public abstract class MultiFrameSprite : Sprite
    {
        protected Texture2D imageFrame;
        protected Dictionary<object, Rectangle> frames;
        protected int currentFrame;

        public MultiFrameSprite()
        {
            frames = new Dictionary<object, Rectangle>();
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(imageFrame, Rectangle,
                                    frames[currentFrame],
                                    Color.White);
        }
    }
}
