using MFRandomizer.EncountTable;
using MFRandomizer.Extensions;

namespace MFRandomizer.EnemyRandomizer
{
    public class EncounterWriter(FileStream outputStream, StreamWriter logStream, EnemyData enemyData, RandomEncounterService randomEncounterFactory)
    {
        private readonly Dictionary<short, string> _enemiesById = enemyData.EnemiesById;

        public void Write(IEncounter encounter)
        {
            if (encounter is EnemyTableHeader enemyTableHeader)
                Write(enemyTableHeader);
            if (encounter is EnemyEncounter enemyEncounter)
                Write(enemyEncounter);
        }

        public void Write(EnemyTableHeader enemyTableHeader) => outputStream.Write(enemyTableHeader.Data);

        public void Write(EnemyEncounter enemyEncounter)
        {
            var shouldRandomizeEncounter = randomEncounterFactory.ShouldRandomizeEncounter(enemyEncounter);
            if (shouldRandomizeEncounter)
            {
                WriteRandomizedEncounter(enemyEncounter);
            }
            else
            {
                WriteNonRandomizedEncounter(enemyEncounter);
            }
        }

        private void WriteRandomizedEncounter(EnemyEncounter enemyEncounter)
        {
            if (enemyEncounter.Chunks.Any(chunk => chunk.ToShort() > 0))
                logStream.WriteLine($"Processing encounter ID {enemyEncounter.EncounterId}...");
            foreach (var chunk in enemyEncounter.Chunks)
            {
                var enemy = randomEncounterFactory.GetEnemy(chunk);
                if (enemy.EnemyId > 0)
                    logStream.WriteLine(enemy.ToString());
                outputStream.Write(enemy.Data);
            }
        }

        private void WriteNonRandomizedEncounter(EnemyEncounter enemyEncounter)
        {
            var enemiesInEncounter = enemyEncounter.Chunks.Select(chunk => chunk.ToShort())
                    .Where(_enemiesById.ContainsKey)
                    .Select(e => (e, _enemiesById[e]))
                    .ToList();
            if (enemiesInEncounter.Count > 0)
                logStream.WriteLine($"Encounter ID {enemyEncounter.EncounterId} skipped...");
            foreach (var (enemyId, enemyString) in enemiesInEncounter)
                logStream.WriteLine($"[{enemyId}]: {enemyString}");
            outputStream.Write(enemyEncounter.Data);
        }
    }
}
