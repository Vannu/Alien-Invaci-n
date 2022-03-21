using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Monogame_Soria;

namespace monogame_soria
{
    public class Smoke : MultiFrameSprite
    {
        TimeSpan lastFrame;

        public Smoke(Point location)
        {
            Location = location;
            Size = new Point(110, 80);

            for (int i = 0; i < 5; i++)
            {
                frames.Add(i, new Rectangle(i * 110, 0, 110, 80));
            }
            imageFrame = Game1.Instance.Content.Load<Texture2D>("sprites/smoke");
            currentFrame = 0;
        }
        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Subtract(lastFrame).Milliseconds > 200)
            {
                lastFrame = gameTime.TotalGameTime;
                currentFrame++;
                if (currentFrame > 4)
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