using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace flappy_bird.GameObjects
{
    class Wall
    {
        private const int _MAX_HEIGHT = 700;
        private const int _MIN_HEIGHT = 0;
        private Random _rmd = new Random();
        private RectangleShape _wall;
        private float _heightWall = 0;

        public RectangleShape WallShape
        {
            get { return _wall; }
            set { _wall = value; }
        }

        public Vector2f Position
        {
            get { return _wall.Position; }
            set { _wall.Position = value; }
        }

        public float HeightWall
        {
            get { return _heightWall; }
            set { _heightWall = value; }
        }

        public int MaxHeight
        {
            get { return _MAX_HEIGHT; }
        }

        public Wall()
        {
            _heightWall = _rmd.Next(_MIN_HEIGHT, _MAX_HEIGHT);
            _wall = new RectangleShape(new Vector2f(80, _heightWall));
        }
    }
}
