using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace flappy_bird.GameObjects
{
    class Bird
    {
        private Texture _bird;
        private RectangleShape _birdShape;
        private Vector2f _velocity = new Vector2f(0, 600);

        public RectangleShape BirdShape
        {
            get { return _birdShape; }
            set { _birdShape = value; }
        }

        public Vector2f Position
        {
            get { return _birdShape.Position; }
            set { _birdShape.Position = value; }
        }

        public Vector2f Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }


        public Bird()
        {
            _bird = new Texture("F:/CFC/Complementaire/git-repo/flappy-bird/flappy-bird/img/flappybird.png");
            _birdShape = new RectangleShape(new Vector2f(80, 60));
            _birdShape.Texture = _bird;
        }
    }
}
