using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;
using monogame_soria;

namespace Monogame_Soria
{
    public class Menu : Sprite
    {
        public enum States { principal, play, ranking, paused, end }
        public States State { get; set; }
        SpriteFont itemsFont;
        SpriteFont rankingFont;

        public Menu(Point? location = null, Point? size = null)
        {
            if (location != null)
                Location = location.Value;
            if (size != null)
                Size = size.Value;

            //Agrego el contenido del Menu
            image = Game1.Instance.Content.Load<Texture2D>("sprites/menu");
            itemsFont = Game1.Instance.Content.Load<SpriteFont>("fonts/menu");
            rankingFont = Game1.Instance.Content.Load<SpriteFont>("fonts/ranking");

        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.Instance.State == Game1.States.Ranking && Keyboard.GetState().IsKeyDown(Keys.Enter))
                Game1.Instance.State = Game1.States.Menu;

            


        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (Game1.Instance.State == Game1.States.Menu)
            {
                Game1.Instance.spriteBatch.DrawString(itemsFont, "Start", new Vector2(315, 250), Color.White);
                Game1.Instance.spriteBatch.DrawString(itemsFont, "Ranking", new Vector2(315, 300), Color.White);
                Game1.Instance.spriteBatch.DrawString(itemsFont, "Exit", new Vector2(315, 350), Color.White);
                Game1.Instance.spriteBatch.DrawString(rankingFont, "Press (SPACE) to select",
                    new Vector2(240, 500), Color.White);
                Game1.Instance.spriteBatch.DrawString(rankingFont, "Vanesa Soria 2019",
                    new Vector2(270, 550), Color.White);
            }

            if (Game1.Instance.State == Game1.States.Ranking)
            {
                int rankingPos = 190;
                Game1.Instance.spriteBatch.DrawString(rankingFont, "Press (ENTER) to go back",
                    new Vector2(240, 500), Color.White);

                Game1.Instance.spriteBatch.DrawString(rankingFont, "Player   |     Max Score", new Vector2(250, 165), Color.White);
                for (int i = 0; i < Game1.Instance.rankingScores.Count; i++)
                {
                    rankingPos += 20;
                    Game1.Instance.spriteBatch.DrawString(rankingFont, Game1.Instance.rankingScores[i], new Vector2(250, rankingPos), Color.White);
                }
            }

            if (Game1.Instance.State == Game1.States.GameOver)
            {
                Game1.Instance.spriteBatch.DrawString(itemsFont, "GAME OVER", new Vector2(300, 300), Color.White);
                Game1.Instance.spriteBatch.DrawString(rankingFont, "Press (space) Ranking",
                    new Vector2(250, 500), Color.White);
                Game1.Instance.spriteBatch.DrawString(rankingFont, "Vanesa Soria 2019",
                    new Vector2(270, 550), Color.White);
            }

            if (Game1.Instance.State == Game1.States.Finish)
            {
                Game1.Instance.spriteBatch.DrawString(itemsFont, "CONGRATULATIONS", new Vector2(240, 250), Color.White);



                Game1.Instance.spriteBatch.DrawString(rankingFont, "Press (space) Ranking",
                    new Vector2(250, 500), Color.White);
                Game1.Instance.spriteBatch.DrawString(rankingFont, "Vanesa Soria 2019",
                    new Vector2(270, 550), Color.White);
            }

        }

    }
}
