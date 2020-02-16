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
using Microsoft.Xna.Framework.Media;

namespace MonoGameWindowsStarter
{
    public class Window
    {
        Game1 game;
        Texture2D texture;
        public BoundingRectangle bounds;
        const int FRAME_WIDTH = 780, FRAME_HEIGHT = 750, ANIMATION_FRAME_RATE = 200;
        TimeSpan timer;
        int frame, state, windowState, willBreak;
        bool isBroken;
        double windowTimer;
        Random random;
        bool hasChecked = false, setState = false;
        SoundEffect glassShatter;

        public Window(Game1 game, ContentManager content,Random random, int xLocation, int yLocation)
        { 
            this.game = game;
            this.random = random;
            
           
            LoadContent(content, xLocation, -yLocation);
        }
        public void LoadContent(ContentManager content, int xLocation, int yLocation)
        {
            texture = content.Load<Texture2D>("Window");
            bounds.Width = 200;
            bounds.Height = 200;
            bounds.X = xLocation;
            bounds.Y = yLocation;
            glassShatter = content.Load<SoundEffect>("glassBreak");
        }

        public void PushWindow()
        {
            bounds.Y += game.speed;
        }
        public void PullWindow()
        {
            bounds.Y -= game.speed;
        }
        public void Update(GameTime gameTime)
        {

            if(bounds.Y >= 25 && bounds.Y <= 1080 && !hasChecked)
            {
                hasChecked = true;
                if(random.Next(1, 5) == 1)
                {
                    willBreak = 1;
                }
            }


            if (willBreak == 1)
            {
                isBroken = true;
            }

            if (isBroken)
            {
                windowState = 2;
                //glassShatter.Play();
            }
            else
            {
                windowState = 0;
            }

            if (windowState == 0)
            {
                timer += gameTime.ElapsedGameTime;
            }
            timer += gameTime.ElapsedGameTime;
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }
            frame %= 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(
                  0,
                  windowState % 3 * FRAME_HEIGHT,
                  FRAME_WIDTH,
                  FRAME_HEIGHT);
            spriteBatch.Draw(texture, bounds, source, Color.White);
        }
    }
}
