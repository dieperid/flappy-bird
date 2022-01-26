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
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="windowSize">Window size</param>
        public Window(uint windowSize)
        {
            RenderWindow window = new RenderWindow(new VideoMode(windowSize, windowSize), "Flappy Bird");
            float posX = 20;
            int indexWallTop = 0;
            // Set framerate limit to 60
            window.SetFramerateLimit(60);
            Wall wall = new Wall();
            List<Drawable> items = new List<Drawable>();
            List<Wall> listWall = new List<Wall>();

            while (window.IsOpen)
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
                        wall.HeightWall = wall.MaxHeight - listWall[indexWallTop].HeightWall;
                        wall.Position = new Vector2f(posX, windowSize - wall.HeightWall);
                    }

                    wall.WallShape.Size = new Vector2f(20, wall.HeightWall);
                    listWall.Add(wall);
                    items.Add(wall.WallShape);
                }

                foreach (RectangleShape item in items)
                {
                    item.FillColor = Color.Red;
                }

                window.Clear();

                foreach (var item in items)
                {
                    window.Draw(item);
                }

                window.Display();
                posX += 50;
                indexWallTop += 2;
            }
        }
    }
}
