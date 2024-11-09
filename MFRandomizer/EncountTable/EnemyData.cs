using KaimiraGames;

namespace MFRandomizer.EncountTable
{
    public class EnemyData
    {
        public Dictionary<short, string> EnemiesById { get; set; } = new Dictionary<short, string>();

        public WeightedList<short> RandomEnemyIdPool { get; set; } = new WeightedList<short>();

        public ISet<short> ExcludedEnemyIds { get; set; } = new HashSet<short>();

        public ISet<short> ReplacementBlacklist { get; set; } = new HashSet<short>();
    }
}
