using MFRandomizer.Configuration;
using MFRandomizer.EncountTable;

namespace MFRandomizer.EnemyRandomizer
{
    public class EnemyRandomizer(EnemyDataLoader enemyDataLoader, EnemyTableReader enemyTableReader, RandomizerConfiguration configuration)
    {
        public void RandomizeEnemies()
        {
            var outputPath = Path.Combine("..", "MFEssentials", "CPK", "CPK.Random", "COMMON", "battle", "table", "Encount.TBL");
            Directory.CreateDirectory(outputPath);

            var enemyData = enemyDataLoader.Load();
            var randomEncounterFactory = new RandomEncounterService(enemyData, configuration);

            using var logStream = new StreamWriter("log.txt");
            using FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

            var encounterWriter = new EncounterWriter(outputStream, logStream, enemyData, randomEncounterFactory);

            foreach (var encounter in enemyTableReader.GetEnemyEncountersFromFile())
            {
                encounterWriter.Write(encounter);
            }
        }
    }
}
