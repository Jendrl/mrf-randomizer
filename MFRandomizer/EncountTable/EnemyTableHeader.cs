namespace MFRandomizer.EncountTable
{
    public class EnemyTableHeader : IEncounter
    {
        public EnemyTableHeader(byte[] buffer)
        {
            Data = buffer.ToArray();
        }

        public byte[] Data { get; }
    }
}
