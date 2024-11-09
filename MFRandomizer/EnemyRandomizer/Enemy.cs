namespace MFRandomizer.EnemyRandomizer
{
    public class Enemy
    {
        public Enemy(byte[] data, short enemyId, string enemyName)
        {
            Data = data;
            EnemyId = enemyId;
            EnemyName = enemyName;
        }

        public byte[] Data { get; }
        public short EnemyId { get; }
        public string EnemyName { get; }

        public virtual bool IsRandomized => false;

        public virtual string ToString() => $"[{EnemyId}]: {EnemyName}";
    }
}
