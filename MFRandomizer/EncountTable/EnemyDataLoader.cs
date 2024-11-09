using KaimiraGames;
using MFRandomizer.Configuration;
using System.Collections.Generic;

namespace MFRandomizer.EncountTable
{
    public class EnemyDataLoader(RandomizerConfiguration configuration)
    {
        private static string _enemyTablePath = Path.Combine("Tables", "Enemy.dat");

        public EnemyData Load()
        {
            var enemiesById = GetEnemiesFromFile().ToDictionary();
            var excludedEnemies = CreateExcludedEnemySet(enemiesById);
            var replacementBlacklist = CreateReplacementBlacklist(enemiesById);

            var enemyPool = new WeightedList<short>();
            foreach (var (enemyId, enemyName) in enemiesById.Where(kv => !excludedEnemies.Contains(kv.Key)))
            {
                if (enemyName.Contains("rival", StringComparison.OrdinalIgnoreCase))
                {
                    enemyPool.Add(enemyId, 2);
                }
                else if (enemyName.Contains("boss", StringComparison.OrdinalIgnoreCase))
                {
                    enemyPool.Add(enemyId, 1);
                }
                if (enemyName.Contains("dragon", StringComparison.OrdinalIgnoreCase))
                {
                    enemyPool.Add(enemyId, 1);
                }
                else
                {
                    enemyPool.Add(enemyId, 20);
                }
            }

            return new EnemyData
            {
                RandomEnemyIdPool = enemyPool,
                EnemiesById = enemiesById,
                ReplacementBlacklist = replacementBlacklist
            };
        }

        private IEnumerable<(short EnemyId, string EnemyName)> GetEnemiesFromFile()
        {
            using (var streamReader = new StreamReader(_enemyTablePath))
            {
                var readLine = streamReader.ReadLine();
                do
                {
                    var enemyIdString = readLine.Split("=")
                        .Last().Trim(',').Trim();
                    var enemyId = short.Parse(enemyIdString);
                    var enemyName = readLine.Split("=").First().Trim();
                    yield return (enemyId, enemyName);
                    readLine = streamReader.ReadLine();
                }
                while (readLine != null);
            }
        }
        private ISet<short> CreateReplacementBlacklist(Dictionary<short, string> enemiesById)
        {
            // add EX_encounters to this list, later with the option to make it optional
            var excludedEnemies = new List<int>();
            // soul crystals and foes
            excludedEnemies.AddRange(Enumerable.Range(241, 6));
            // crashing soldiers
            excludedEnemies.AddRange(new List<int> { 251, 252, 253, 255, 259, 261, 265, 266, 268, 270, 271, 272, 273, 277 });
            // Homo Flameus
            excludedEnemies.AddRange(new List<int> { 211, 212, 213, 328, 338 });
            // test ids
            excludedEnemies.AddRange(Enumerable.Range(351, 14));
            // dragons
            excludedEnemies.AddRange(enemiesById.Where(kv => kv.Value.Contains("dragon", StringComparison.InvariantCultureIgnoreCase)).Select(kv => (int)kv.Key));
            // bosses
            excludedEnemies.AddRange(enemiesById.Where(kv => kv.Value.Contains("boss", StringComparison.InvariantCultureIgnoreCase)).Select(kv => (int)kv.Key));
            // rivals
            if (!configuration.ShouldRandomizeRivalEncounters)
                excludedEnemies.AddRange(enemiesById.Where(kv => kv.Value.Contains("rival", StringComparison.InvariantCultureIgnoreCase)).Select(kv => (int)kv.Key));
            // BUI ids
            excludedEnemies.AddRange(enemiesById.Where(kv => kv.Value.Contains("RC_BUI", StringComparison.InvariantCultureIgnoreCase)).Select(kv => (int)kv.Key));

            var excludedEnemySet = excludedEnemies.Select(x => (short)x).ToHashSet();
            return excludedEnemySet;
        }

        private static ISet<short> CreateExcludedEnemySet(Dictionary<short, string> enemiesById)
        {
            var excludedEnemies = new List<int>();
            // soul crystals and foes
            excludedEnemies.AddRange(Enumerable.Range(241, 6));
            // crashing soldiers
            // working ids: 254, 256, 257, 258, 260, 262, 263, 264, 267, 269, 274, 275, 276, 278
            excludedEnemies.AddRange(new List<int> { 251, 252, 253, 255, 259, 261, 265, 266, 268, 270, 271, 272, 273, 277 });
            // Homo Flameus
            excludedEnemies.AddRange(new List<int> { 211, 212, 213, 328, 338 });
            // test ids
            excludedEnemies.AddRange(Enumerable.Range(351, 14));
            // dragons and bosses
            var workingDragons = new List<int> { 502, 503, 536, 537, 543 };
            var workingBosses = new List<int> { 512, 513, 514, 515, 523, 528, 531, 532, 545, 548, 549, 551, 553, 560, 562, 563, 564, 565 };
            excludedEnemies.AddRange(enemiesById.Where(kv => kv.Value.Contains("boss", StringComparison.InvariantCultureIgnoreCase)).Select(kv => (int)kv.Key));
            excludedEnemies = excludedEnemies.Where(enemy => !workingDragons.Contains(enemy)).ToList();
            excludedEnemies = excludedEnemies.Where(enemy => !workingBosses.Contains(enemy)).ToList();
            // BUI ids
            excludedEnemies.AddRange(Enumerable.Range(700, 85));
            var excludedEnemySet = excludedEnemies.Select(x => (short)x).ToHashSet();
            return excludedEnemySet;
        }
    }
}
