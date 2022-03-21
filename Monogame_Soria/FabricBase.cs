using Microsoft.Xna.Framework;
using monogame_soria;
using System;

namespace Monogame_Soria
{
    public abstract class FabricBase : Updatable
    {
        protected static Random rnd;

        public FabricBase()
        {
            if (rnd == null)
                rnd = new Random();
        }

        public abstract void Update(GameTime gameTime);
    }
}
