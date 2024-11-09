using KaimiraGames;
using MFRandomizer.EncountTable;
using MFRandomizer.Extensions;
using System.Linq;

namespace MFRandomizer.EnemyRandomizer
{
    public class RandomEncounterService(EnemyData enemyData)
    {
        private static byte[] _ffSequence = [255, 255];
        private static byte[] _c101Sequence = [193, 1];
        private static byte[] _5a02Sequence = [90, 2];
        private static List<byte[]> _startSequences = [_ffSequence, _c101Sequence, _5a02Sequence];

        private readonly WeightedList<short> _enemyPool = enemyData.RandomEnemyIdPool;
        private readonly Dictionary<short, string> _enemiesById = enemyData.EnemiesById;
        private readonly ISet<short> _replacementBlacklist = enemyData.ReplacementBlacklist;

        public Enemy GetEnemy(byte[] chunk)
        {
            var encounterPart = chunk.ToShort();
            var enemyIsKnown = _enemiesById.TryGetValue(encounterPart, out var enemy);
            var enemyName = enemyIsKnown ? enemy : $"UNKNOWN({encounterPart})";
            if (!enemyIsKnown || _replacementBlacklist.Contains(encounterPart))
            {
                return new(chunk, encounterPart, enemyName);
            }

            short randomizedEnemy = _enemyPool.Next();
            return new RandomEnemy(encounterPart, enemyName, randomizedEnemy, _enemiesById[randomizedEnemy]);
        }

        public bool ShouldRandomizeEncounter(EnemyEncounter enemyEncounter)
        {
            var encounterId = enemyEncounter.EncounterId;
            var earlyFightsExceptions = new List<int>
            {
                // tutorial soldiers at mine start
                561,
                // EC_Ishiki_takai01_EX: 306, mine boss
                812,
                // event at funeral crashed
                // 5 skeletons at funeral
                572,
                // forced skelton fight at start of cathedral
                823,
            };
            // replace this with a list of encounters containing _EX enemies
            var scenarioRelevantEncountersNonBoss = new List<int>
            {
                // mid cathedral encounter, mage soldier with skeletons
                830,
            };
            if (earlyFightsExceptions.Contains(encounterId) || scenarioRelevantEncountersNonBoss.Contains(encounterId))
                return false;

            // without this safeguard, fort human boss will have infinite turns, better to just randomize normal encounters?
            var startsWithExpectedSequence = _startSequences.Any(sequence => enemyEncounter.Chunks.First().SequenceEqual(sequence));

            // special encounters, for example dungeon bosses or the 5 ghosts at the cathedral crystal
            var containsEXEnemies = enemyEncounter.Chunks
                .Any(chunk =>
                {
                    var enemyId = chunk.ToShort();
                    _enemiesById.TryGetValue(enemyId, out var enemy);
                    return enemy?.Contains("_EX") == true;
                });
            return startsWithExpectedSequence && !containsEXEnemies;
        }
    }
}
