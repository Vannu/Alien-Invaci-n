using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;

namespace Monogame_Soria
{
    public class EnemyShoot : MultiImageSprite
    {
        private SoundEffect impact;

        public EnemyShoot(Point location, Point size, int speed )
        {
            Location = location;
            Size = size;
            Speed = speed;
            images.Add(0, Game1.Instance.Content.Load<Texture2D>("sprites/enemy_shoot"));
            impact = Game1.Instance.Content.Load<SoundEffect>("sounds/impact");
            selectedImage = 0;
        }

        public override void Update(GameTime gameTime)
        {
            int x = Location.X;
            int y = Location.Y;
            int speed = Speed;
            y += speed;
            Location = new Point(x, y);

            //Verificar Disparo a Jugador
            CheckPlayerShoot();
        }

        private void CheckPlayerShoot()
        {
            foreach (var item in Game1.Instance.SpritesActive)
            {

                if (item is Player)
                {
                    Player player = item as Player;

                    if (Rectangle.Intersects(player.Rectangle))
                    {
                        impact.Play();
                        player.Health -=1;
                        Game1.Instance.SpritesInactive.Add(this);
                        return;
                    }
                }
            }
        }
    }
}
