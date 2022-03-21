using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using Microsoft.Xna.Framework.Media;

namespace Monogame_Soria
{
    public class Boss : MultiImageSprite
    {
        public enum States { active }
        public States State { get; set; }
        private Random rnd;
       

        public Boss(Point location, int health, int speed)
        {
            Location = location;
            Health = health;
            Speed = speed;
            Size = new Point(130, 110);
            images.Add(0, Game1.Instance.Content.Load<Texture2D>("sprites/boss"));
            selectedImage = 0;
        }

        public override void Update(GameTime gametime)
        {

           

            int x = Location.X;
            int y = Location.Y;
            int speed = Speed;
            rnd = new Random();


            x += speed;
            Location = new Point(x, y);

            //Rebotar en la Pantalla
            if (x >= Game1.Instance.GraphicsDevice.Viewport.Width - 111)
            {
                Speed = -3;
            }

            if (x == 0)
            {
                Speed = 3;
            }

            //Disparar
            if (rnd.Next(1000) > 980)
            {
                Game1.Instance.SpritesInactive.Add(new EnemyShoot(new Point(Location.X + 35, Location.Y + 45), new Point(10, 20), 5));
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
