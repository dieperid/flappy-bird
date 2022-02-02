using SFML.Graphics;
using SFML.System;
using System;

namespace flappy_bird.GameObjects
{
    /// <summary>
    /// Class Wall
    /// </summary>
    class Wall
    {
        #region[Attributes]
        /// <summary>
        /// Constant for the space between the top and the bottom wall
        /// </summary>
        private const float _SPACE_TOP_BOTTOM = 300;
        /// <summary>
        /// Constant for the height of the wall
        /// </summary>
        private const float _HEIGHT_WALL = 880;
        /// <summary>
        /// Constant for the width of the wall
        /// </summary>
        private const float _WIDTH_WALL = 80;
        /// <summary>
        /// Constant for the max height of a wall
        /// </summary>
        private const int _MAX_HEIGHT = 700;
        /// <summary>
        /// Constant for the min height of a wall
        /// </summary>
        private const int _MIN_HEIGHT = 200;
        /// <summary>
        /// Create a random
        /// </summary>
        private Random _rmd = new Random();
        /// <summary>
        /// RectangleShape for the wall
        /// </summary>
        private RectangleShape _wall;
        /// <summary>
        /// Texture for the bottom wall
        /// </summary>
        private Texture _wallBottom;
        /// <summary>
        /// Texture for the top wall
        /// </summary>
        private Texture _wallTop;
        /// <summary>
        /// The height of the wall to show on the screen
        /// </summary>
        private float _heightWallToShow = 0;
        #endregion

        #region[Getter, Setter]
        /// <summary>
        /// Getter, setter on _wall
        /// </summary>
        public RectangleShape WallShape
        {
            get { return _wall; }
            set { _wall = value; }
        }
        /// <summary>
        /// Getter, setter on _wall.Position
        /// </summary>
        public Vector2f Position
        {
            get { return _wall.Position; }
            set { _wall.Position = value; }
        }
        /// <summary>
        /// Getter on _HEIGHT_WALL
        /// </summary>
        public float HeightWall
        {
            get { return _HEIGHT_WALL; }
        }
        /// <summary>
        /// Getter on _wallTop
        /// </summary>
        public Texture WallTop
        {
            get { return _wallTop; }
        }
        /// <summary>
        /// Getter on _wallBottom
        /// </summary>
        public Texture WallBottom
        {
            get { return _wallBottom; }
        }
        /// <summary>
        /// Getter, setter on _heightWallToShow
        /// </summary>
        public float HeightWallToShow
        {
            get { return _heightWallToShow; }
            set { _heightWallToShow = value; }
        }
        /// <summary>
        /// Getter on _SPACE_TOP_BOTTOM
        /// </summary>
        public float SpaceTopBottom
        {
            get { return _SPACE_TOP_BOTTOM; }
        }
        /// <summary>
        /// Getter on _WIDTH_WALL
        /// </summary>
        public float WallWidth
        {
            get { return _WIDTH_WALL; }
        }
        #endregion

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public Wall()
        {
            // Initialize a new texture for the top and bottom wall, initialize the height to show and the shape of the wall
            _wallTop = new Texture(Img.tuyauTop);
            _wallBottom = new Texture(Img.tuyauDown);
            _heightWallToShow = _rmd.Next(_MIN_HEIGHT, _MAX_HEIGHT);
            _wall = new RectangleShape(new Vector2f(_WIDTH_WALL, _HEIGHT_WALL));
        }

        /// <summary>
        /// Method GenerateHeight to generate the height of the wall to show on the screen
        /// </summary>
        /// <returns>The height to show on the screen</returns>
        public float GenerateHeight()
        {
            // Random between the min height and the max height
            _heightWallToShow = _rmd.Next(_MIN_HEIGHT, _MAX_HEIGHT);
            // Return this value
            return _heightWallToShow;
        }
    }
}
