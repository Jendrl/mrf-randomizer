using MFRandomizer.EncountTable;

namespace MFRandomizer.EnemyRandomizer
{
    public class EnemyRandomizer(EnemyDataLoader enemyDataLoader, EnemyTableReader enemyTableReader)
    {
        public void RandomizeEnemies()
        {
            var outputPath = "Encount.TBL";

            var enemyData = enemyDataLoader.Load();
            var randomEncounterFactory = new RandomEncounterService(enemyData);

            using var logStream = new StreamWriter("log.txt");
            using FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

            var encounterWriter = new EncounterWriter(outputStream, logStream, enemyData, randomEncounterFactory);

            foreach (var encounter in enemyTableReader.GetEnemyEncountersFromFile())
            {
                encounterWriter.Write(encounter);
            }

            Console.WriteLine("Done randomizing!");
        }
    }
}
