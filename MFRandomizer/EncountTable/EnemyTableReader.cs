namespace MFRandomizer.EncountTable
{
    public class EnemyTableReader
    {
        public IEnumerable<IEncounter> GetEnemyEncountersFromFile()
        {
            var inputPath = Path.Combine("Tables", "FreshEncount.TBL");

            using FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);

            const int encounterLength = 28;
            var encounter = new List<byte>();
            byte[] buffer = new byte[encounterLength];

            byte[] startBuffer = new byte[20];
            inputStream.Read(startBuffer, 0, 20);
            yield return new EnemyTableHeader(startBuffer);

            int encounterId = 0;
            while (inputStream.Read(buffer, 0, buffer.Length) > 0)
            {
                yield return new EnemyEncounter(buffer, encounterId++);
            }
        }
    }
}
