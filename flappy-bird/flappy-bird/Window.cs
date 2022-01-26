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

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="windowSize">Window size</param>
        public Window(uint windowWidth, uint windowHeight)
        {
            RenderWindow window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Flappy Bird", Styles.Fullscreen);

            GenerateWall(windowWidth, windowHeight);

            // Set framerate limit to 60
            window.SetFramerateLimit(60);

            Clock clock = new Clock();
            float deltatime = 0;

            while (window.IsOpen)
            {
                deltatime = clock.ElapsedTime.AsSeconds();
                clock.Restart();
                foreach (RectangleShape item in _items)
                {
                    item.FillColor = Color.Green;
                }

                window.Clear();

                foreach (var item in _items)
                {
                    window.Draw(item);
                }

                window.Display();

                Movement(windowWidth, deltatime);
                
                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }
            }
        }
        public void GenerateWall(uint windowWidth, uint windowHeight)
        {
            float posX = 200;
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
            
        }
        public void Movement(uint windowWidth, float deltatime)
        {
            _items.Clear();
            float moveX = 400;
            float spaceBetweenWall = 600;
            foreach(var item in _listWall)
            {
                if (item.Position.X > 0)
                {
                    item.Position = new Vector2f(item.Position.X - moveX * deltatime, item.Position.Y);
                }
                else
                {
                    item.Position = new Vector2f(windowWidth + spaceBetweenWall, item.Position.Y);
                }
                item.WallShape.Size = new Vector2f(80, item.HeightWall);
                _items.Add(item.WallShape);
            }
        }
    }
}
