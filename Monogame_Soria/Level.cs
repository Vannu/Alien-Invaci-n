using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Monogame_Soria
{
    public class Level : BackgroundSprite
    {
        private SoundEffect  bossSong;
        
        public int CurrentImage
        {
            get { return currentImage; }
            set { currentImage = value; }
        }

        public Level(int width, int height, int speed, int x, int y)
        {
            bossSong = Game1.Instance.Content.Load<SoundEffect>("sounds/boos1start");
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
            this.speed = speed;

            images.Add(Game1.Instance.Content.Load<Texture2D>("sprites/level/lev01s0"));
            images.Add(Game1.Instance.Content.Load<Texture2D>("sprites/level/lev01s1"));
            images.Add(Game1.Instance.Content.Load<Texture2D>("sprites/level/lev01s2"));
            images.Add(Game1.Instance.Content.Load<Texture2D>("sprites/level/lev01s3"));
            images.Add(Game1.Instance.Content.Load<Texture2D>("sprites/level/lev01s4"));
            images.Add(Game1.Instance.Content.Load<Texture2D>("sprites/level/lev01s5"));
            images.Add(Game1.Instance.Content.Load<Texture2D>("sprites/level/lev01s6"));

            currentImage = images.Count - 1;
        }

        public override void Update(GameTime gameTime)
        {

            if (currentImage <= 0) return;
            y += speed;
            if (y >= height)
            {
                y = 0;
                currentImage--;
            }

            if(currentImage == 0)
            {
               // MediaPlayer.Stop();
                bossSong.Play();
              // MediaPlayer.Play(bossSong);
                foreach (var item in Game1.Instance.SpritesActive)
                {
                    if (item is Enemy)
                    {
                        Enemy enemy = item as Enemy;
                        Game1.Instance.SpritesInactive.Add(enemy);
                    }
                }
                
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
