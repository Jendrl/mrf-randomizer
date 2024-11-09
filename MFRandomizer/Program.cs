using MFRandomizer.Configuration;
using System.Text.Json;

namespace MFRandomizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var randomizerConfiguration = CommandLineHelper.ParseCommandLine(args);

            var randomizer = new EnemyRandomizer.EnemyRandomizer(new(randomizerConfiguration), new(), randomizerConfiguration);
            randomizer.RandomizeEnemies();

            Console.WriteLine($"Randomized with: {JsonSerializer.Serialize(randomizerConfiguration, new JsonSerializerOptions() { WriteIndented = true })}");
        }
    }
}
