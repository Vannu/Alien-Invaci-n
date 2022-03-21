using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Soria
{
    public class Enemy : DirectionSprite
    {
        private Random rnd;
       

        public Enemy(Point location, int speed, int direction)
        {
            Location = location;
            Speed = speed;
            Direction = direction;
            images.Add(0, Game1.Instance.Content.Load<Texture2D>("sprites/enemy"));
           // image=Game1.Instance.Content.Load<Texture2D>("sprites/Piedras");
            //imageFrame =images.Add(0, Game1.Instance.Content.Load<Texture2D>("sprites/Piedras"));
            selectedImage = 0;
            Size = new Point(90, 72);
        }

        public override void Update(GameTime gametime)
        {
            int x = Location.X;
            int y = Location.Y;
            int speed = Speed;
            int dir = Direction;


            rnd = new Random();

            //Movimiento del enemigo segun direccion
            if (dir == 0)
            {
                x -= speed;
            }
            if (dir == 1)
            {
                x += speed;
            }
            if (dir == 2)
            {
              //  images.Add(0, Game1.Instance.Content.Load<Texture2D>("sprites/Piedras"));
                y += speed;
            }
            //Destruyo a los enemigos que salen de la pantalla
            if (x > Game1.Instance.graphics.GraphicsDevice.Viewport.Width - (Size.X / 2) || x < -(Size.X / 2))
            {
                Game1.Instance.SpritesInactive.Add(this);
                return;
            }
            if (y > Game1.Instance.graphics.GraphicsDevice.Viewport.Height - (Size.Y / 2) || y < -(Size.Y / 2))
            {
                Game1.Instance.SpritesInactive.Add(this);
                return;
            }

            //Actualizar posicion
            Location = new Point(x, y);

            //Disparar
            if (rnd.Next(1000) > 980)
            {
                Game1.Instance.SpritesInactive.Add(new EnemyShoot(new Point(Location.X + 35, Location.Y + 45), new Point(10,20),5));
            }

            //Verificar Colisiones
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            foreach (var item in Game1.Instance.SpritesActive)
            {
                if (item is Player)
                {
                    Player player = item as Player;
                    if (Rectangle.Intersects(player.Rectangle))
                    {
                        player.Health -= 1;
                        Game1.Instance.SpritesInactive.Add(new Explotion(Location));
                        Game1.Instance.SpritesInactive.Add(this);
                        return;
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
