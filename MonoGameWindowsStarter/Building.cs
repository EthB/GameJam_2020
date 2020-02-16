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
    public class Building
    {
        Game1 game;
        ContentManager content;
        GraphicsDevice graphicsDevice;
        Texture2D texture;
        public List<Tile> tileSet = new List<Tile>();
        public List<Window> windowSet = new List<Window>();
        List<string> tileNames = new List<string> { "sprite_0", "sprite_1", "sprite_2", "sprite_3", "sprite_4", "sprite_5", "sprite_6" };
        Random random = new Random();
        int tileSize, windowCount;
        List<Tuple<int,int>> windowLocations = new List<Tuple<int, int>>();
    public Building(Game1 game, int tileSize, ContentManager content, GraphicsDevice graphicsDevice)
        {
            int randomWindowCount;
            this.game = game;
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.tileSize = tileSize;
            tileSet.Add(new Tile(game, 0));
           
            for (int i = 1; i < tileSize; i++)
            {
                Tuple<int, int> location;
                RedoWindowPicker(windowLocations);
                randomWindowCount = random.Next(1, 4);
                tileSet.Add(new Tile(game, tileSet[i - 1].bounds.Y - 1080));
                if (i < tileSize - 2)
                {

                    switch (randomWindowCount)
                    {
                        case 1:
                            location = windowLocations[random.Next(1, windowLocations.Count)];
                            windowSet.Add(new Window(game, content, random, location.Item1, (int)(location.Item2 - (tileSet[i - 1].bounds.Y - 1080))));
                            break;
                        case 2:
                            for (int j = 0; j < 2; j++)
                            {
                                int random_location = random.Next(1, windowLocations.Count);
                                location = windowLocations[random_location];
                                windowSet.Add(new Window(game, content, random, location.Item1, (int)(location.Item2 - (tileSet[i - 1].bounds.Y - 1080))));
                                windowLocations.RemoveAt(random_location);
                            }
                            break;
                        case 3:
                            for (int j = 0; j < 3; j++)
                            {
                                int random_location = random.Next(1, windowLocations.Count);
                                location = windowLocations[random_location];
                                windowSet.Add(new Window(game, content, random, location.Item1, (int)(location.Item2 - (tileSet[i - 1].bounds.Y - 1080))));
                                windowLocations.RemoveAt(random_location);
                            }
                            break;
                        case 4:
                            for (int j = 0; j < 4; j++)
                            {
                                int random_location = random.Next(1, windowLocations.Count);
                                location = windowLocations[random_location];
                                windowSet.Add(new Window(game, content, random, location.Item1, (int)(location.Item2 - (tileSet[i - 1].bounds.Y - 1080))));
                                windowLocations.RemoveAt(random_location);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            LoadContent();

        }
        public void RedoWindowPicker(List<Tuple<int,int>> windowLocations)
        {
            windowLocations.Clear();
            windowLocations.Add(new Tuple<int, int>(400, 800));
            windowLocations.Add(new Tuple<int, int>(400, 200));
            windowLocations.Add(new Tuple<int, int>(1300, 800));
            windowLocations.Add(new Tuple<int, int>(1300, 200));
        }
        public int FindTile()
        {
            for (int i = 0; i < tileSet.Count; i++)
            {
                if(tileSet[i].bounds.Y > 0 && tileSet[i].bounds.Y < 1080)
                {
                    return i;
                }
            }
            return 0;
        }
        public void LoadContent()
        {
            tileSet[0].LoadContent(content, "sprite_8");
            for (int i = 1; i < tileSet.Count - 1; i++)
            {
                tileSet[i].LoadContent(content, tileNames[random.Next(1, 7)]);
            }
            tileSet[tileSize - 1].LoadContent(content, "sprite_7");
        }
        public void Update(GameTime gameTime)
        {
            foreach(Window window in windowSet)
            {
                window.Update(gameTime);
            }
        }
        public void PushTile(float speed)
        {
            foreach(Tile tile in tileSet)
            {
                tile.bounds.Y += speed;
            }
        }
        public void PullTile(float speed)
        {
            foreach(Tile tile in tileSet)
            {
                tile.bounds.Y -= speed;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        { 
            foreach(Tile tile in tileSet)
            {
                if(!(tile.bounds.Y < -1080))
                {
                    tile.Draw(spriteBatch);
                }
            }
            foreach (Window window in windowSet)
            {
                window.Draw(spriteBatch);
            }
        }
    }
}