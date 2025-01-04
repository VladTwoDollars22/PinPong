
namespace PinPong
{
    public static class PathUtilite
    {
        private static int repeats = 3;
        public static string CalculatePath(string pathToOBj)
        {
            string curentPath = Directory.GetCurrentDirectory() + pathToOBj;

            while (!File.Exists(curentPath) && repeats <= 4)
            {
                Console.WriteLine(repeats);
                curentPath = Path.GetFullPath(Path.Combine(curentPath, ".."));
            }

            if (!File.Exists(curentPath))
            {
                Console.WriteLine("Об'єкт не знайдено");
            }

            return curentPath;
        }
    }
}
