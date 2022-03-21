using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Soria;

namespace monogame_soria
{
    public abstract class Sprite:Updatable
    {
        protected Texture2D image;

        #region Propiedades

        //Ubicacion del Sprite 
        public Point Location { get; set; }
        //Tamaño del Sprite
        public Point Size { get; set; }
        //Velocidad del Sprite    
        public virtual int Speed { get; set; }
        //Vida del Sprite
        public virtual int Health { get; set; }

        public Rectangle Rectangle
        {
            get { return new Rectangle(Location, Size); }
        }

        #endregion
        public abstract void Update(GameTime gametime);

        public virtual void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(image, Rectangle, Color.White);
        }

    }
}
