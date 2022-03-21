using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;

namespace Monogame_Soria
{
    public abstract class ScoreSprite : MultiImageSprite
    {
        //Cantidad de Puntos del Jugador
        public virtual int Score { get; set; }
    }
}
