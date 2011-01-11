namespace Maplestory_SDK
{
#if WINDOWS || XBOX

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            using (Run game = new Run())
            {
                game.Run();
            }
        }
    }

#endif
}