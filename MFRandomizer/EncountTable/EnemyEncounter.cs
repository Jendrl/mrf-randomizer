namespace MFRandomizer.EncountTable
{
    public class EnemyEncounter : IEncounter
    {
        public EnemyEncounter(byte[] enemyEncounterBuffer, int encounterId)
        {
            Data = enemyEncounterBuffer.ToArray();
            Chunks = enemyEncounterBuffer.Chunk(2).ToList();
            EncounterId = encounterId;
        }

        public byte[] Data { get; }

        public List<byte[]> Chunks { get; }

        public int EncounterId { get; }
    }
}
