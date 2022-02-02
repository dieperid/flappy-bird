using SFML.Graphics;
using SFML.System;

namespace flappy_bird.GameObjects
{
    /// <summary>
    /// Class Bird
    /// </summary>
    class Bird
    {
        #region[Attributes]
        /// <summary>
        /// Texture for the bird
        /// </summary>
        private Texture _bird;
        /// <summary>
        /// Rectangle shape for the bird
        /// </summary>
        private RectangleShape _birdShape;
        /// <summary>
        /// Vector2f for the velocity of the bird
        /// </summary>
        private Vector2f _velocity = new Vector2f(0,0);
        #endregion

        #region[Getter, Setter]
        /// <summary>
        /// Getter, setter on _birdShape
        /// </summary>
        public RectangleShape BirdShape
        {
            get { return _birdShape; }
            set { _birdShape = value; }
        }
        /// <summary>
        /// Getter, setter on _birdShape.Position
        /// </summary>
        public Vector2f Position
        {
            get { return _birdShape.Position; }
            set { _birdShape.Position = value; }
        }
        /// <summary>
        /// Getter, setter on _velocity
        /// </summary>
        public Vector2f Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }
        #endregion

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public Bird()
        {
            // Adding the texture, the rectangle shape to the bird
            _bird = new Texture(Img.flappybird);
            _birdShape = new RectangleShape(new Vector2f(80, 60));
            _birdShape.Texture = _bird;
        }
    }
}
