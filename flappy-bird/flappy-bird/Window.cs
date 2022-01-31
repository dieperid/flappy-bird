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
        private string _pathFont = "F:/CFC/Complementaire/git-repo/flappy-bird/flappy-bird/fonts/C&C-Red-Alert.ttf";
        private List<Drawable> _items = new List<Drawable>();
        private List<Wall> _listWall = new List<Wall>();
        private Wall _wall;
        private Bird _bird = new Bird();
        private RectangleShape _background;
        Text gameOver = new Text();
        Text score = new Text();

        private Texture _bgTexture = new Texture("F:/CFC/Complementaire/git-repo/flappy-bird/flappy-bird/img/background.png");
        private float _deltatime = 0;
        private int _score = 0;
        

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="windowWidth"></param>
        /// <param name="windowHeight"></param>
        public Window(uint windowWidth, uint windowHeight)
        {
            score.Font = new Font(_pathFont);
            gameOver.Font = new Font(_pathFont);
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;

            gameOver.Position = new Vector2f(100, 200);
            gameOver.CharacterSize = 100;

            score.Position = new Vector2f(100, 100);
            score.CharacterSize = 100;


            _background = new RectangleShape();
            _background.Size = new Vector2f(windowWidth, windowHeight);
            _background.Texture = _bgTexture;

            RenderWindow window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Flappy Bird", Styles.Fullscreen, settings);

            GenerateWall();

            // Set framerate limit to 60
            window.SetFramerateLimit(60);

            Clock clock = new Clock();
            _bird.Position = new Vector2f(200, 540);
            _bird.BirdShape.Origin = _bird.BirdShape.Size / 2;
            

            while (window.IsOpen)
            {
                _deltatime = clock.ElapsedTime.AsSeconds();
                clock.Restart();

                window.Clear();

                if (!CheckCollision(windowHeight) == true)
                {
                    Movement();
                    _bird.Position += _bird.Velocity * _deltatime;
                }

                window.Draw(_background);

                foreach (var item in _items)
                {
                     window.Draw(item);
                }
                
                score.DisplayedString = $"SCORE : {_score}";
                gameOver.DisplayedString = "YOU LOSE FDP";

                if (CheckCollision(windowHeight) == true)
                {
                    window.Draw(gameOver);
                }

                window.Draw(score);
                window.Display();
                
                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }
            }
        }
        public void GenerateWall()
        {
            float posStartX = 1920;
            int indexWallTop = 0;
            for (int x = 0; x < 4; x++)
            {
                for (int i = 0; i < 2; i++)
                {
                    _wall = new Wall();
                    if (i == 0)
                    {
                        _wall.Position = new Vector2f(posStartX, -_wall.HeightWall + _wall.YPosWall);
                        _wall.WallShape.Texture = _wall.WallTop;
                    }
                    else
                    {
                        _wall.YPosWall = _listWall[indexWallTop].Position.Y + _wall.HeightWall + _wall.SpaceTopBottom;
                        _wall.Position = new Vector2f(posStartX,_wall.YPosWall); 
                        _wall.WallShape.Texture = _wall.WallBottom;
                    }

                    _wall.WallShape.Size = new Vector2f(80, _wall.HeightWall);
                    _listWall.Add(_wall);
                    _items.Add(_wall.WallShape);
                }
                posStartX += 600;
                indexWallTop += 2;
            }
            _items.Add(_bird.BirdShape);
        }
        public void Movement()
        {
            _items.Clear();
            float spaceBetweenWall = 600;
            int count = 0;
            float height = 0;
            
            foreach(var item in _listWall)
            {
                if (item.Position.X + 400 <= 0)
                {
                    if (count % 2 == 0)
                    {
                        height = item.GenerateHeight();
                        item.YPosWall = height;
                        item.Position = new Vector2f(item.Position.X, -_wall.HeightWall + item.YPosWall);
                    }
                    else
                    {
                        item.YPosWall = height + _wall.SpaceTopBottom;
                        item.Position = new Vector2f(item.Position.X, item.YPosWall);
                    }
                    count++;

                    item.Position = new Vector2f(1920, item.Position.Y);
                }
                else
                {
                    item.Position = new Vector2f(item.Position.X - spaceBetweenWall* _deltatime, item.Position.Y);
                }
                item.WallShape.Size = new Vector2f(80, item.HeightWall);


                _bird.Velocity += new Vector2f(0, 300)*_deltatime;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    _bird.Velocity = new Vector2f(0, -400);
                }
                _bird.BirdShape.Rotation = ((float)Math.Atan2(_bird.Velocity.Y, 300)) * 180 / (float)Math.PI;
                
                
                _items.Add(item.WallShape);
            }
            _items.Add(_bird.BirdShape);
        }

        public bool CheckCollision(float windowHeight)
        {
            int count = 0;

            foreach (var item in _listWall)
            {
                if (count % 2 == 0)
                {
                    if (_bird.Position.X >= item.Position.X && _bird.Position.X <= item.Position.X + _wall.WallWidth)
                    {
                        if (_bird.Position.Y >= 0 && _bird.Position.Y <= 0 + item.YPosWall)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (_bird.Position.X >= item.Position.X && _bird.Position.X <= item.Position.X + _wall.WallWidth)
                    {
                        if (_bird.Position.Y >= item.Position.Y && _bird.Position.Y <= windowHeight)
                        {
                            return true;
                        }
                    }
                }
                count++;
            }
            _score += 100;
            return false;
        }
    }
}
