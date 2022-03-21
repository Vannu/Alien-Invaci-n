using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogame_soria
{
    class Sounds
    {
        public static void Play (Song gameplayMusic)
        {
            try
            {
                MediaPlayer.Play(gameplayMusic);
                MediaPlayer.IsRepeating = true;
            }
            catch{ }            
        }
        public static void Stop ()
        {
            MediaPlayer.Stop();
        }
    }
}
