using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using flappy_bird.GameObjects;

namespace flappy_bird
{
    /// <summary>
    /// Class Window
    /// </summary>
    class Window
    {
        private List<Drawable> _items = new List<Drawable>();
        private List<Wall> _listWall = new List<Wall>();
        Wall wall;
        bool val = false;
        Bird bird = new Bird();
        RectangleShape background = new RectangleShape();
        Texture bg = new Texture("F:/CFC/Complementaire/git-repo/flappy-bird/flappy-bird/background.png");

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="windowWidth"></param>
        /// <param name="windowHeight"></param>
        public Window(uint windowWidth, uint windowHeight)
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;

            background.Size = new Vector2f(1920, 1080);
            background.Texture = bg;

            RenderWindow window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Flappy Bird", Styles.Fullscreen, settings);

            
            GenerateWall(windowHeight);

            // Set framerate limit to 60
            window.SetFramerateLimit(60);

            Clock clock = new Clock();
            float deltatime = 0;
            bird.Position = new Vector2f(200, 540);
            bird.BirdShape.Origin = bird.BirdShape.Size / 2;

            while (window.IsOpen)
            {
                deltatime = clock.ElapsedTime.AsSeconds();
                clock.Restart();
                
                foreach (RectangleShape item in _items)
                {
                    item.FillColor = Color.Green;
                }

                window.Clear();

                window.Draw(background);
                Movement(windowWidth, deltatime, windowHeight);

                foreach (var item in _items)
                {
                    window.Draw(item);
                }
                
                bird.Position += bird.Velocity * deltatime;
                window.Display();
                
                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }
            }
        }
        public void GenerateWall(uint windowHeight)
        {
            float posX = 1800;
            int indexWallTop = 0;
            for (int x = 0; x < 4; x++)
            {
                for (int i = 0; i < 2; i++)
                {
                    wall = new Wall();
                    if (i == 0)
                    {
                        wall.Position = new Vector2f(posX, 0);
                    }
                    else
                    {
                        wall.HeightWall = wall.MaxHeight - _listWall[indexWallTop].HeightWall;
                        wall.Position = new Vector2f(posX, windowHeight - wall.HeightWall);
                    }

                    wall.WallShape.Size = new Vector2f(80, wall.HeightWall);
                    _listWall.Add(wall);
                    _items.Add(wall.WallShape);
                }
                posX += 600;
                indexWallTop += 2;
            }
            _items.Add(bird.BirdShape);

        }
        public void Movement(uint windowWidth, float deltatime, uint windowHeight)
        {
            _items.Clear();
            float moveX = 600;
            float spaceBetweenWall = 600;
            int count = 0;
            float height = 0;
            
            foreach(var item in _listWall)
            {
                if (item.Position.X + 80 < 0)
                {
                    if (val == false)
                    {
                        item.Position = new Vector2f(windowWidth + spaceBetweenWall - 200, item.Position.Y);
                    }
                    else
                    {
                        val = true;
                        item.Position = new Vector2f(windowWidth + spaceBetweenWall, item.Position.Y);
                    }
                    if(count == 0)
                    {
                        height = item.GenerateHeight();
                        item.HeightWall = height;
                        item.Position = new Vector2f(item.Position.X, 0);
                    }
                    else
                    {
                        item.HeightWall = item.MaxHeight - height;
                        item.Position = new Vector2f(item.Position.X, windowHeight - item.HeightWall);
                    }
                    count++;
                }
                else
                {
                    item.Position = new Vector2f(item.Position.X - moveX * deltatime, item.Position.Y);
                }
                item.WallShape.Size = new Vector2f(80, item.HeightWall);


                bird.Velocity += new Vector2f(0, 300)*deltatime;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    bird.Velocity = new Vector2f(0, -400);
                }
                bird.BirdShape.Rotation = ((float)Math.Atan2(bird.Velocity.Y, 300)) * 180 / (float)Math.PI;
                
                _items.Add(bird.BirdShape);
                _items.Add(item.WallShape);
            }
        }
    }
}
