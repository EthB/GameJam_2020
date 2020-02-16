using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    class LollipopPowerup : Powerup
    {

            Game1 game;
            Texture2D texture;
            BoundingRectangle bounds;
            ContentManager content;
            const int FRAME_WIDTH = 29, FRAME_HEIGHT = 52, ANIMATION_FRAME_RATE = 124;
            TimeSpan timer;
            int frame, state;
            bool pickedUp;
            TimeSpan powerupTimer;
        public override Rectangle RectBounds
        {
            get { return (Rectangle)bounds; }
        }

        public override TimeSpan Time
        {
            get { return powerupTimer; }
            set { powerupTimer = value; }
        }

        public LollipopPowerup(Game1 game, ContentManager content, int xLocation, int yLocation)
            {
                this.game = game;
                this.content = content;
                LoadContent(xLocation, yLocation);
            pickedUp = false;
            }
            public override void PushPowerup(float speed)
            {
                bounds.Y += speed;
            }
            public override void PullPowerup(float speed)
            {
                bounds.Y -= speed;
            }
            public void LoadContent(int xLocation, int yLocation)
            {
                texture = content.Load<Texture2D>("Lollipop");
                bounds.Width = 90;
                bounds.Height = 90;
                bounds.X = xLocation;
                bounds.Y = -yLocation;
            }

            public override void Update(GameTime gameTime)
            {
            if (pickedUp)
            {
                powerupTimer += gameTime.ElapsedGameTime;
            }
                timer += gameTime.ElapsedGameTime;
                while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
                {
                    if (state < 6)
                    {
                        state++;
                    }
                    else if (state == 6)
                    {
                        state = 0;
                    }
                    frame++;
                    timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
                }
                frame %= 1;
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
            if (pickedUp == false)
            {
                var source = new Rectangle(
                    frame * FRAME_WIDTH,
                    state % 7 * FRAME_HEIGHT,
                FRAME_WIDTH,
                FRAME_HEIGHT);
                spriteBatch.Draw(texture, bounds, source, Color.White);
            }
            else { }
        }

        public override void PickUp(Game1 game)
        {
            Random random = new Random();
            bounds.Y = 2000;
            if (random.Next(1,100) != 69)
            {
                pickedUp = true;
                game.speed += 5;
                powerupTimer = new TimeSpan(0);
                game.player.speed += 1;
            }
            else
            {
                //CHOKE AND DIE
                game.hits = 0;
            }
        }

        public override void TimeOut(Game1 game)
        {
            game.speed -= 5;
            pickedUp = false;
            game.player.speed -= 1;
        }

    }
}
