using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using monogame_soria;

namespace Monogame_Soria
{
    public class MenuSelect : Sprite
    {
        KeyboardState previousKey;

        public MenuSelect(Point? location = null, Point? size = null)
        {
            if (location != null)
                Location = location.Value;
            if (size != null)
                Size = size.Value;

            image = Game1.Instance.Content.Load<Texture2D>("sprites/select");
        }
        public override void Update(GameTime gameTime)
        {
            int x = 250;
            int y = Location.Y;
            KeyboardState keyboardState = Keyboard.GetState();

            //Movimiento del Selector
            if (keyboardState.IsKeyDown(Keys.Up) && !previousKey.IsKeyDown(Keys.Up))
               y -= 50;
            if (keyboardState.IsKeyDown(Keys.Down) && !previousKey.IsKeyDown(Keys.Down))
                y += 50;
            //Impedir que salga de las opciones del Menu
            y = MathHelper.Clamp(y, 250, 350);
            

            //Acciones del Selector
            if(y == 250)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    Game1.Instance.State = Game1.States.Play;

            }
            if (y == 300)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    Game1.Instance.State = Game1.States.Ranking;
            }
            if (y == 350)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    Game1.Instance.Exit();
            }

            previousKey = keyboardState;


            //Actualizo Posicion del Selector
            Location = new Point(x, y);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Game1.Instance.State == Game1.States.Menu)
                base.Draw(gameTime);
        }
    }
}
