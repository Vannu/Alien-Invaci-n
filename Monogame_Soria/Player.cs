using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using monogame_soria;

namespace Monogame_Soria
{
    public class Player : ScoreSprite
    {
        public enum States { active }
        public States State { get; set; }
        private SoundEffect shootSound, gameoverSound;
        TimeSpan lastshoot;
        TimeSpan lastSmoke;
        SpriteFont font;

        int speed;


        public Player(Point? location = null, Point? size = null, int? health = null)
        {
            if (location != null)
                Location = location.Value;
            if (size != null)
                Size = size.Value;
            if (health != null)
                Health = health.Value;

            //Agrego el contenido del Jugador
            images.Add(States.active, Game1.Instance.Content.Load<Texture2D>("sprites/player"));
            shootSound = Game1.Instance.Content.Load<SoundEffect>("sounds/player_shoot");
            gameoverSound = Game1.Instance.Content.Load<SoundEffect>("sounds/GameOver");
            font = Game1.Instance.Content.Load<SpriteFont>("fonts/score");
            selectedImage = State;
            speed = 5;
        }


        public override void Update(GameTime gameTime)
        {
            State = States.active;
            selectedImage = States.active;
            int x = Location.X;
            int y = Location.Y;

            //Movimiento a la derecha
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                x += speed;
            }
            //Movimiento a la Izquierda
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                x -= speed;
            }
            //Movimiento Arriba
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                y -= speed;
            }
            //Movimiento Abajo
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                y += speed;
            }
            //Disparar
            if (Keyboard.GetState().IsKeyDown(Keys.Space) &&
                gameTime.TotalGameTime.Subtract(lastshoot).Milliseconds >= 300)
            {
                Disparar();
                lastshoot = gameTime.TotalGameTime;
            }
            //Impedir que salga de la pantalla
            x = MathHelper.Clamp(x, 0, Game1.Instance.GraphicsDevice.Viewport.Width - Size.X);
            y = MathHelper.Clamp(y, 0, Game1.Instance.GraphicsDevice.Viewport.Height - Size.Y);



            //Finalizar Juego al quedarse sin Vida
            if (Health < 3)
            {

                if (gameTime.TotalGameTime.Subtract(lastSmoke).Milliseconds > 900)
                {
                    lastSmoke = gameTime.TotalGameTime;
                    Game1.Instance.SpritesInactive.Add(new Smoke(new Point(Location.X - 20, Location.Y + 40)));
                }
                speed = 2;
            }

            if (Health <= 0)
            {
                gameoverSound.Play();
                Game1.Instance.BossesSpritesInactive.Add(this);
                
                Game1.Instance.State = Game1.States.GameOver;
                //***************************************************
                if (Game1.Instance.State == Game1.States.GameOver)
                {
                    Health = 10;
                    Score = 0;
                    speed = 5;
                  
                }
               
            }
            //Actualizar Posicion
            Location = new Point(x, y);
        }

        private void Disparar()
        {
            Game1.Instance.SpritesInactive.Add(new PlayerShoot(new Point(Location.X + 5, Location.Y + 20), new Point(10, 20)));
            shootSound.Play();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            //Puntaje y Vida
            Game1.Instance.spriteBatch.DrawString(font, "Points: " + Score.ToString(), new Vector2(10, 10), Color.White);
            Game1.Instance.spriteBatch.DrawString(font, "Health: " + Health.ToString(), new Vector2(10, 50), Color.White);
        }

    }
}