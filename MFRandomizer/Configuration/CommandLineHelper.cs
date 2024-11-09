using Microsoft.Extensions.Configuration;

namespace MFRandomizer.Configuration
{
    public static class CommandLineHelper
    {
        public static RandomizerConfiguration ParseCommandLine(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            if (args.Contains("--help"))
            {
                Console.WriteLine("--randomizeSpecialEncounters | True / False | Toggles randomization of special encounters");
                Console.WriteLine("--randomizeRivalEncounters | True / False | Toggles randomization of rival encounters");
                Environment.Exit(0);
            }

            var randomizerConfiguration = new RandomizerConfiguration();

            bool.TryParse(config["randomizeSpecialEncounters"], out var shouldRandomizeSpecialEncounters);
            bool.TryParse(config["randomizeRivalEncounters"], out var shouldRandomizeRivalEncounters);
            randomizerConfiguration.ShouldRandomizeSpecialEncounters = args.Contains("--randomizeSpecialEncounters") || shouldRandomizeSpecialEncounters;
            randomizerConfiguration.ShouldRandomizeRivalEncounters = args.Contains("--randomizeRivalEncounters") || shouldRandomizeRivalEncounters;

            return randomizerConfiguration;
        }
    }
}
