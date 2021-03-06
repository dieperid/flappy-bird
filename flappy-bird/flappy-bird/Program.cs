namespace flappy_bird
{
    /// <summary>
    /// Class program
    /// </summary>
    class Program
    {
        #region[Attributes]
        /// <summary>
        /// Size of the window for the width
        /// </summary>
        private const uint _SIZE_WINDOW_WIDTH = 1920;
        /// <summary>
        /// Size of the window for the height
        /// </summary>
        private const uint _SIZE_WINDOW_HEIGHT = 1080;
        #endregion

        /// <summary>
        /// Main of the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            _ = new Window(_SIZE_WINDOW_WIDTH, _SIZE_WINDOW_HEIGHT);
        }
    }
}
