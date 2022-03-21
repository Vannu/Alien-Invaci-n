using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace Monogame_Soria
{
    public class PlayerShoot : MultiImageSprite
    {
        int speed = 4;
        private SoundEffect impact;

        public PlayerShoot(Point location, Point size)
        {
            Location = location;
            Size = size;
            images.Add(0, Game1.Instance.Content.Load<Texture2D>("sprites/player_shoot"));
            impact = Game1.Instance.Content.Load<SoundEffect>("sounds/impact");
           // bossSong = Game1.Instance.Content.Load<SoundEffect>("sounds/boos1start");
            selectedImage = 0;
            

        }

        public override void Update(GameTime gameTime)
        {

            int x = Location.X;
            int y = Location.Y;
            y-= speed;
            Location = new Point(x, y);

            //Verificar Muerte de Enemigo
            CheckKill();
        }

        private void CheckKill()
        {
            foreach (var item in Game1.Instance.SpritesActive)
            {
                if (item is Player)
                {
                    Player player = item as Player;

                    //Chequeo si Mate un enemigo
                    foreach (var item1 in Game1.Instance.SpritesActive)
                    {
                        if (item1 is Enemy)
                        {
                            Enemy enemy = item1 as Enemy;

                            if (Rectangle.Intersects(enemy.Rectangle))
                            {
                                player.Score += 1;
                                Game1.Instance.score += 1;
                                Game1.Instance.SpritesInactive.Add(new Explotion(Location));
                                Game1.Instance.SpritesInactive.Add(enemy);
                                Game1.Instance.SpritesInactive.Add(this);
                                return;
                            }
                        }
                        if (item1 is Piedra)
                        {
                            Piedra piedra = item1 as Piedra;

                            if (Rectangle.Intersects(piedra.Rectangle))
                            {
                                player.Score += 1;
                                Game1.Instance.score += 1;
                                Game1.Instance.SpritesInactive.Add(new Explotion(Location));
                                Game1.Instance.SpritesInactive.Add(piedra);
                                Game1.Instance.SpritesInactive.Add(this);
                                return;
                            }
                        }
                    }

                    //Chequeo si Mate al Jefe

                    if (Game1.Instance.levelcurrentimage == 0)
                    {

                        
                        foreach (var item2 in Game1.Instance.BossesSpritesActive)
                        { 
                            if (item2 is Boss)
                            {
                                Boss boss = item2 as Boss;

                                if (Rectangle.Intersects(boss.Rectangle))
                                {
                                    impact.Play();
                                    boss.Health -= 1;
                                    Game1.Instance.SpritesInactive.Add(new Explotion(Location));
                                    Game1.Instance.SpritesInactive.Add(this);

                                    if (boss.Health == 0)
                                    {
                                        Game1.Instance.BossesSpritesInactive.Add(boss);

                                        //Guardar Puntaje Final
                                        StreamWriter sw = File.AppendText(Game1.Instance.mydesk + "\\monogame_soria_scores.txt");
                                        sw.WriteLine("Player1             " + player.Score);
                                        sw.Close();

                                        //Cambiar estado de juego
                                        Game1.Instance.State = Game1.States.Finish;

                                    }
                                    return;
                                }
                            }
                        }

                        
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
