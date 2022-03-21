using System;
using Microsoft.Xna.Framework;


namespace Monogame_Soria
{
    public class EnemyFabric : FabricBase
    {
        TimeSpan previoustime;

        public override void Update(GameTime gameTime)
        {
            
            if (gameTime.TotalGameTime.Subtract(previoustime).Milliseconds > 800  && Game1.Instance.levelcurrentimage > 0)
            {
                Enemy aircraft = new Enemy(new Point(rnd.Next(
                    Game1.Instance.graphics.GraphicsDevice.Viewport.Width - 90
                    ), rnd.Next(Game1.Instance.graphics.GraphicsDevice.Viewport.Height * 0,75)), 2, rnd.Next(0,3));
                Game1.Instance.SpritesInactive.Add(aircraft);
                previoustime = gameTime.TotalGameTime;
            }
        }
    }
}
