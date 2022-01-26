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
        /// Size of the window
        /// </summary>
        private const uint _SIZE_WINDOW = 800;
        #endregion

        #region[Get, Set]
        /// <summary>
        /// Getter on _SIZE_WINDOW
        /// </summary>
        public uint WindowSize
        {
            get { return _SIZE_WINDOW; }
        }
        #endregion

        /// <summary>
        /// Main of the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Window window = new Window(_SIZE_WINDOW);
        }
    }
}
