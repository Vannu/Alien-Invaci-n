using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Soria
{
    public abstract class DirectionSprite : MultiImageSprite
    {
        //Cantidad de Puntos del Jugador
        public virtual int Direction { get; set; }
    }
}
