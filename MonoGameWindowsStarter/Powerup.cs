using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter
{
    public abstract class Powerup
    {
        public abstract void PullPowerup(float speed);
        public abstract void PushPowerup(float speed);

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

        public abstract void PickUp(Game1 game);

        public abstract void TimeOut(Game1 game);

        BoundingRectangle bounds;
        TimeSpan powerupTimer;
        public virtual Rectangle RectBounds
        {
            get { return (Rectangle)bounds; }
        }
        public virtual TimeSpan Time
        {
            get { return powerupTimer; }
            set { powerupTimer = value; }
        }

    }
}
