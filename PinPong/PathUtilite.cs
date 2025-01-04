
namespace PinPong
{
    public static class PathUtilite
    {
        private static int repeatsBorder = 4;
        public static string CalculatePath(string pathToOBj)
        {
            string pathDirectory = Directory.GetCurrentDirectory();
            string curentPath = Path.Combine(pathDirectory,pathToOBj);

            int repeats = 0;

            while (!File.Exists(curentPath) && repeats <= repeatsBorder)
            {
                Console.WriteLine(repeats);
                pathToOBj = Path.GetFullPath(Path.Combine(pathToOBj, ".."));
                curentPath = Path.Combine(pathDirectory, pathToOBj);

                repeats++;
            }

            if (!File.Exists(curentPath))
            {
                Console.WriteLine("Об'єкт не знайдено");
            }

            return curentPath;
        }
    }
}
