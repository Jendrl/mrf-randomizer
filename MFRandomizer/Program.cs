using MFRandomizer.Configuration;
using Microsoft.Extensions.Configuration;

namespace MFRandomizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            var randomizerConfiguration = new RandomizerConfiguration();
            bool.TryParse(config["randomizeSpecialEncounters"], out var shouldRandomizeSpecialEncounters);
            bool.TryParse(config["randomizeRivalEncounters"], out var shouldRandomizeRivalEncounters);
            randomizerConfiguration.ShouldRandomizeSpecialEncounters = args.Contains("--randomizeSpecialEncounters") || shouldRandomizeSpecialEncounters;
            randomizerConfiguration.ShouldRandomizeRivalEncounters = args.Contains("--randomizeRivalEncounters") || shouldRandomizeRivalEncounters;
            Console.WriteLine(string.Join(", ", args));
            Console.WriteLine(randomizerConfiguration.ShouldRandomizeSpecialEncounters);

            var randomizer = new EnemyRandomizer.EnemyRandomizer(new(randomizerConfiguration), new(), randomizerConfiguration);
            randomizer.RandomizeEnemies();
        }
    }
}
