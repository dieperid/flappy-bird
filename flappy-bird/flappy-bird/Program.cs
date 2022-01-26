using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace flappy_bird
{
    /// <summary>
    /// Class program
    /// </summary>
    class Program
    {
        #region[Attributs]
        /// <summary>
        /// Size of the window for the width
        /// </summary>
        private const uint _SIZE_WINDOW_WIDTH = 1920;
        /// <summary>
        /// Size of the window for the height
        /// </summary>
        private const uint _SIZE_WINDOW_HEIGHT = 1080;
        #endregion

        #region[Get, Set]
        /// <summary>
        /// Getter on _SIZE_WINDOW_WIDTH
        /// </summary>
        public uint WindowWidth
        {
            get { return _SIZE_WINDOW_WIDTH; }
        }
        /// <summary>
        /// Getter on _SIZE_WINDOW_HEIGHT
        /// </summary>
        public uint WindowHeight
        {
            get { return _SIZE_WINDOW_HEIGHT; }
        }
        #endregion

        /// <summary>
        /// Main of the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Window window = new Window(_SIZE_WINDOW_WIDTH, _SIZE_WINDOW_HEIGHT);
        }
    }
}
