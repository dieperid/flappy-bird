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
        private const float _SPACE_TOP_BOTTOM = 300;
        private const float _HEIGHT_WALL = 880;
        private const float _WIDTH_WALL = 80;
        private const int _MAX_HEIGHT = 700;
        private const int _MIN_HEIGHT = 200;
        private Random _rmd = new Random();
        private RectangleShape _wall;
        private Texture _wallBottom;
        private Texture _wallTop;
        private float _yPosWall = 0;

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
            get { return _HEIGHT_WALL; }
        }

        public Texture WallTop
        {
            get { return _wallTop; }
        }
        public Texture WallBottom
        {
            get { return _wallBottom; }
        }

        public float YPosWall
        {
            get { return _yPosWall; }
            set { _yPosWall = value; }
        }

        public float SpaceTopBottom
        {
            get { return _SPACE_TOP_BOTTOM; }
        }

        public int MaxHeight
        {
            get { return _MAX_HEIGHT; }
        }

        public float WallWidth
        {
            get { return _WIDTH_WALL; }
        }

        public Wall()
        {
            _wallTop = new Texture("F:/CFC/Complementaire/git-repo/flappy-bird/flappy-bird/img/tuyauDown.png");
            _wallBottom = new Texture("F:/CFC/Complementaire/git-repo/flappy-bird/flappy-bird/img/tuyauTop.png");
            _yPosWall = _rmd.Next(_MIN_HEIGHT, _MAX_HEIGHT);
            _wall = new RectangleShape(new Vector2f(_WIDTH_WALL, _HEIGHT_WALL));
        }

        public float GenerateHeight()
        {
            _yPosWall = _rmd.Next(_MIN_HEIGHT, _MAX_HEIGHT);

            return _yPosWall;
        }
    }
}
