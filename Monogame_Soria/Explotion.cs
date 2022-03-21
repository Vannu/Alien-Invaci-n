using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Monogame_Soria
{
    public class Explotion : MultiFrameSprite
    {
        static SoundEffect Sound;
        TimeSpan lastFrame;

        public Explotion(Point location)
        {
            if (Sound == null)            
                Sound = Game1.Instance.Content.Load<SoundEffect>("sounds/explosion_aircraft");

            Location = location;
            Size = new Point(60, 50);

            for (int i = 0; i < 10; i++)
            {
                frames.Add(i, new Rectangle(i * 60, 0, 60, 50));
            }             
            imageFrame = Game1.Instance.Content.Load<Texture2D>("sprites/explosion_aircraft");
            currentFrame = 0;
        }
        bool PlaySound = false;
        public override void Update(GameTime gameTime)
        {
            if (PlaySound == false)
            {
                PlaySound = true;
                Sound.Play();
            }
            if (gameTime.TotalGameTime.Subtract(lastFrame).Milliseconds > 50)
            {
                lastFrame = gameTime.TotalGameTime;
                currentFrame++;
                if (currentFrame > 9)
                {
                    Game1.Instance.SpritesInactive.Add(this);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
