namespace MFRandomizer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var randomizer = new EnemyRandomizer.EnemyRandomizer(new(), new());
            randomizer.RandomizeEnemies();
        }
    }
}
