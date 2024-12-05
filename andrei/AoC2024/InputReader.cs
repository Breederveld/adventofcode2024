using System.Reflection;

namespace AoC2024
{
    internal class InputReader
    {
        public static string GetInput(string fileName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var numbers = new List<int>();
            using (var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.Inputs.{fileName}.txt"))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
