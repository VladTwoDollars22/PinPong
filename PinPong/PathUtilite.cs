namespace PinPong
{
    public static class PathUtilite
    {
        private static int repeatsBorder = 5;
        public static string CalculatePath(string pathToObj)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string currentPath = Path.Combine(currentDirectory, pathToObj);

            int repeats = 0;

            while (!File.Exists(currentPath) && repeats < repeatsBorder)
            {
                currentDirectory = Path.GetFullPath(Path.Combine(currentDirectory, ".."));
                currentPath = Path.Combine(currentDirectory, pathToObj);

                repeats++;
            }

            if (!File.Exists(currentPath))
            {
                return string.Empty; 
            }

            return currentPath;
        }
    }
}
