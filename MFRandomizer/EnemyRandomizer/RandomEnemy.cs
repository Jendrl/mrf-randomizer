namespace MFRandomizer.EnemyRandomizer
{
    public class RandomEnemy : Enemy
    {
        public RandomEnemy(short originalEnemyId, string originalName, short randomizedId, string randomizedName)
            : base(BitConverter.GetBytes(randomizedId), originalEnemyId, originalName)
        {
            RandomizedId = randomizedId;
            RandomizedName = randomizedName;
        }

        public override bool IsRandomized => true;

        public short RandomizedId { get; }
        public string RandomizedName { get; }

        public override string ToString() => $"[{EnemyId} -> {RandomizedId}]: {EnemyName} -> {RandomizedName}";
    }
}
