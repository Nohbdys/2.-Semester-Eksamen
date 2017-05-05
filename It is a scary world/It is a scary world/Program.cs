using System;

namespace It_is_a_scary_world
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = GameWorld.Instance)
                game.Run();
        }
    }
#endif
}
