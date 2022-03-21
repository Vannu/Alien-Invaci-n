using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame_Soria
{
    public class StoneFactory : FabricBase
    {
        TimeSpan previoustime;

        public override void Update(GameTime gameTime)
        {

            if (gameTime.TotalGameTime.Subtract(previoustime).Milliseconds > 800 && Game1.Instance.levelcurrentimage > 0)
            {
                Piedra piedras = new Piedra(new Point(rnd.Next(
                    Game1.Instance.graphics.GraphicsDevice.Viewport.Width - 90
                    ), rnd.Next(Game1.Instance.graphics.GraphicsDevice.Viewport.Height * 0, 75)), 2, rnd.Next(0, 3));
                Game1.Instance.SpritesInactive.Add(piedras);
                previoustime = gameTime.TotalGameTime;
            }
        }
    }
}