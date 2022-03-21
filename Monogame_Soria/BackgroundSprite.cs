using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using monogame_soria;

namespace Monogame_Soria
{
    public abstract class BackgroundSprite : Updatable
    {
        protected List<Texture2D> images;
        public int currentImage;
        protected int width;
        protected int height;
        protected int x;
        protected int y;
        protected int speed;

        public Rectangle Rectangle1
        {
            get { return new Rectangle(x, y, width, height); }
        }

        public Rectangle Rectangle2
        {
            get { return new Rectangle(x, y - height, width, height); }
        }

        //public int CurrentImage
        //{
        //    get { return currentImage; }
        //    set { currentImage = value; }
        //}

        public BackgroundSprite()
        {
            images = new List<Texture2D>();

        }

        public abstract void Update(GameTime gametime);

        public virtual void Draw(GameTime gameTime)
        {

            if (currentImage > 0)
            {
                Game1.Instance.spriteBatch.Draw(images[currentImage], Rectangle1, Color.White);
                Game1.Instance.spriteBatch.Draw(images[currentImage - 1], Rectangle2, Color.White);
            }
            else
            {
                Game1.Instance.spriteBatch.Draw(images[currentImage], new Vector2(0, 0), Color.White);
            }
        }
    }
}

