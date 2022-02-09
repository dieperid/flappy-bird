using flappy_bird.GameObjects;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace flappy_bird
{
    /// <summary>
    /// Class Window
    /// </summary>
    class Window
    {
        #region[Attributes]
        /// <summary>
        /// Texture for the background
        /// </summary>
        private Texture _bgTexture = new Texture(Img.background);
        /// <summary>
        /// List of Drawable items
        /// </summary>
        private List<Drawable> _items = new List<Drawable>();
        /// <summary>
        /// List of wall
        /// </summary>
        private List<Wall> _listWall = new List<Wall>();
        /// <summary>
        /// Instantiate a wall
        /// </summary>
        private Wall _wall;
        /// <summary>
        /// Instantiate the bird
        /// </summary>
        private Bird _bird = new Bird();
        /// <summary>
        /// RectangleShape for the background of the game
        /// </summary>
        private RectangleShape _background = new RectangleShape();
        /// <summary>
        /// Deltatime for the project
        /// </summary>
        private float _deltatime = 0;
        /// <summary>
        /// Score of the player
        /// </summary>
        private int _score = 0;
        /// <summary>
        /// Instantiate a new text for the game over text
        /// </summary>
        private Text _gameOverText = new Text();
        /// <summary>
        /// Instantiate a new text for the score text
        /// </summary>
        private Text _scoreText = new Text();
        /// <summary>
        /// Instantiate a new text for the restart text
        /// </summary>
        private Text _restartText = new Text();
        /// <summary>
        /// New window
        /// </summary>
        private RenderWindow _window;
        /// <summary>
        /// Bool for the jump of the bird
        /// </summary>
        private bool _isJumping = false;
        #endregion

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="windowWidth">Width of the window</param>
        /// <param name="windowHeight">Height of the window</param>
        public Window(uint windowWidth, uint windowHeight)
        {
            #region[Window code]
            ContextSettings settings = new ContextSettings { AntialiasingLevel = 8 };   // Create settings for the Antialiasing

            // Initialize the font for the text
            _scoreText.Font = new Font(Img.C_C_Red_Alert);
            _gameOverText.Font = new Font(Img.C_C_Red_Alert);
            _restartText.Font = new Font(Img.C_C_Red_Alert);

            // Initialize the position for the score text, game over text and the restart text
            _scoreText.Position = new Vector2f(50, 50);
            _scoreText.CharacterSize = 50;
            _gameOverText.Position = new Vector2f(50, 100);
            _gameOverText.CharacterSize = 50;
            _restartText.Position = new Vector2f(50, 150);
            _restartText.CharacterSize = 50;

            // Initialize the size and the texture for the background
            _background.Size = new Vector2f(windowWidth, windowHeight);
            _background.Texture = _bgTexture;

            // Create a new window for the game
            _window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Flappy Bird", Styles.Fullscreen, settings);

            // Call the method to generate the wall
            GenerateWall();

            // Set framerate limit to 60
            _window.SetFramerateLimit(60);

            // Create a new clock for the timer
            Clock clock = new Clock();

            // Instantiate de position of the bird and create a new origin
            _bird.Position = new Vector2f(200, windowHeight / 2);
            _bird.BirdShape.Origin = _bird.BirdShape.Size / 2;
            
            // While window is open the game run
            while (_window.IsOpen)
            {
                // Instantiate a new string for the score and game over text
                _scoreText.DisplayedString = $"SCORE : {_score}";
                _gameOverText.DisplayedString = $"YOU LOSE, YOUR SCORE : {_score}";
                _restartText.DisplayedString = "Press <Enter> to restart the game ...";

                // Deltatime take the clock time
                _deltatime = clock.ElapsedTime.AsSeconds();

                // Restart the clock at each turn
                clock.Restart();

                // Clear the window
                _window.Clear();

                // Call the method CheckCollision to check if the bird as touched a wall -> if he did not hit
                if (!CheckCollision(windowHeight))
                {
                    Movement();
                    _bird.Position += _bird.Velocity * _deltatime;
                }

                // Call the method to draw all items of the game
                DrawItem();

                // Call the method CheckCollision to check if the bird as touched a wall -> if he touched
                if (CheckCollision(windowHeight))
                {
                    // Draw the game over text
                    _gameOverText.FillColor = Color.Red;
                    _window.Draw(_gameOverText);
                    _window.Draw(_restartText);

                    // If enter is pressed
                    if(Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                    {
                        // Close the window and start a new one
                        _window.Close();
                        Window window = new Window(windowWidth, windowHeight);
                    }
                }

                // Display all items to the window
                _window.Display();
                
                // If touch escape is pressed
                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    _window.Close();
                }
            }
            #endregion
        }

        /// <summary>
        /// Method GenerateWall to generate all the wall
        /// </summary>
        public void GenerateWall()
        {
            #region[GenerateWall code]
            // Position X for the start
            float posStartX = 1920;
            // Index for the top wall
            int indexWallTop = 0;

            for (int x = 0; x < 4; x++)
            {
                for (int i = 0; i < 2; i++)
                {
                    // Instantiate a new wall
                    _wall = new Wall();

                    // If i equal 0 the wall to create is the top wall
                    if (i == 0)
                    {
                        // Instantiate a new position and a new texture
                        _wall.Position = new Vector2f(posStartX, -_wall.HeightWall + _wall.HeightWallToShow);
                        _wall.WallShape.Texture = _wall.WallTop;
                    }
                    // It's the bottom wall
                    else
                    {
                        // Instantiate a new position and a new texture
                        _wall.HeightWallToShow = _listWall[indexWallTop].Position.Y + _wall.HeightWall + _wall.SpaceTopBottom;
                        _wall.Position = new Vector2f(posStartX,_wall.HeightWallToShow); 
                        _wall.WallShape.Texture = _wall.WallBottom;
                    }
                    // Instantiate a new size for the wall
                    _wall.WallShape.Size = new Vector2f(80, _wall.HeightWall);

                    // Adding the wall to the list of the wall and to the drawable list
                    _listWall.Add(_wall);
                    _items.Add(_wall.WallShape);
                }
                // Adding 600 to the position X for the next wall
                posStartX += 600;
                // Adding 2 at the index for the top wall
                indexWallTop += 2;
            }
            // Add the bird to the drawable list
            _items.Add(_bird.BirdShape);
            #endregion
        }

        /// <summary>
        /// Method Movement for the movement of the wall
        /// </summary>
        public void Movement()
        {
            #region[Movement code]
            float spaceBetweenWall = 600;   // Space between to wall
            int count = 0;                  // Counter for the top wall
            float height = 0;               // height of the wall

            // Clear the drawable items list
            _items.Clear();

            // Foreach loop for the wall list
            foreach (var item in _listWall)
            {
                // If the position x of the item is at -400
                if (item.Position.X + 400 <= 0)
                {
                    // If count % 2 equal 0 it's the top wall
                    if (count % 2 == 0)
                    {
                        // Call the GenerateHeight method to instantiate a new height
                        height = item.GenerateHeight();
                        // Change the height to show and initialize a new position
                        item.HeightWallToShow = height;
                        item.Position = new Vector2f(item.Position.X, -_wall.HeightWall + item.HeightWallToShow);
                    }
                    else
                    {
                        // Change the height to show and initialize a new position
                        item.HeightWallToShow = height + _wall.SpaceTopBottom;
                        item.Position = new Vector2f(item.Position.X, item.HeightWallToShow);
                    }
                    // Increments the counter
                    count++;

                    // Initialize the position of the wall
                    item.Position = new Vector2f(1920, item.Position.Y);
                }
                else
                {
                    // Initialize the position of the wall 
                    item.Position = new Vector2f(item.Position.X - spaceBetweenWall* _deltatime, item.Position.Y);
                }
                // Initialize a new size for the wall
                item.WallShape.Size = new Vector2f(80, item.HeightWall);

                // Moove the bird
                _bird.Velocity += new Vector2f(0, 300)*_deltatime;

                // If space is pressed and the _isJumping bool is false
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !_isJumping)
                {
                    // Set the value of _isJumping to true
                    _isJumping = true;

                    // The bird go top
                    _bird.Velocity = new Vector2f(0, -500);
                }
                // If space isn't pressed
                else if(!Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    // Reset the value of _isJumping to false
                    _isJumping = false;
                }

                // Rotation for the bird
                _bird.BirdShape.Rotation = ((float)Math.Atan2(_bird.Velocity.Y, 300)) * 180 / (float)Math.PI;
                
                // Add the wall shpae of the item to the drawable list
                _items.Add(item.WallShape);
            }
            // Add the bird shape to the drawable list
            _items.Add(_bird.BirdShape);
            #endregion
        }

        /// <summary>
        /// Method CheckCollision to check the collision with the bird and the walls
        /// </summary>
        /// <param name="windowHeight">Height of the windows</param>
        /// <returns>Flase or true</returns>
        public bool CheckCollision(float windowHeight)
        {
            #region[CheckCollision code]
            int count = 0;      // Counter for the top wall

            // Foreach loop for the wall list
            foreach (var item in _listWall)
            {
                // If count % 2 equal 0 it's the top wall
                if (count % 2 == 0)
                {
                    // Check if the position X of the bird is the same as the position x of the wall
                    if (_bird.Position.X >= item.Position.X && _bird.Position.X <= item.Position.X + _wall.WallWidth)
                    {
                        // Check the same for the position Y
                        if (_bird.Position.Y >= 0 && _bird.Position.Y <= 0 + item.HeightWallToShow)
                        {
                            // Return true if the 2 conditions are valids
                            return true;
                        }
                    }
                }
                // If it's the bottom wall
                else
                {
                    // Check if the position X of the bird is the same as the position x of the wall
                    if (_bird.Position.X >= item.Position.X && _bird.Position.X <= item.Position.X + _wall.WallWidth)
                    {
                        // Check the same for the position Y
                        if (_bird.Position.Y >= item.Position.Y && _bird.Position.Y <= windowHeight)
                        {
                            // Return true if the 2 conditions are valids
                            return true;
                        }
                    }
                }
                // Increments the counter
                count++;
            }
            // If the bird is outside of the window
            if (_bird.Position.Y > windowHeight || _bird.Position.Y < 0)
            {
                return true;
            }
            // Increments the score
            _score += 100;

            // Return false because the bird hasn't touched a wall
            return false;
            #endregion
        }
        /// <summary>
        /// Method DrawItem to draw all the item
        /// </summary>
        public void DrawItem()
        {
            #region[DrawItem code]
            // Draw the background
            _window.Draw(_background);
            // Use a foreach loop to draw each items of the drawable list
            foreach (var item in _items)
            {
                _window.Draw(item);
            }
            // Draw the score Text
            _window.Draw(_scoreText);
            #endregion
        }
    }
}
