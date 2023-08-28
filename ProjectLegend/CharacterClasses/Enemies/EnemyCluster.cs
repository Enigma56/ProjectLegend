using System.Collections.Generic;
using System.Linq;
using ProjectLegend.GameUtilities.FuncUtils;

namespace ProjectLegend.CharacterClasses.Enemies
{
    public static class EnemySets
    {
        public static HashSet<Enemy> AllPool { get; }
        public static HashSet<Enemy> WeakPool { get; }
        public static HashSet<Enemy> StrongPool { get; }
        public static HashSet<Enemy> PowerfulPool { get; }
        
        static EnemySets() //To create enemy pools to pull from
        {
            AllPool = new HashSet<Enemy>();
            WeakPool = new HashSet<Enemy>();
            StrongPool = new HashSet<Enemy>();
            PowerfulPool = new HashSet<Enemy>();
            
            WeakPool.Add(new BinSpider());
            WeakPool.Add(new CarthageSpider());
            StrongPool.Add(new Prowler());
            StrongPool.Add(new Flyer());
            PowerfulPool.Add(new Leviathan());
            PowerfulPool.Add(new Goliath());
            
            AllPool.UnionWith(WeakPool);
            AllPool.UnionWith(StrongPool);
            AllPool.UnionWith(PowerfulPool);
        }

        public static Enemy GetEnemy()
        {
            int enemyIndex = Generators.RandomNumber.Next(WeakPool.Count);
            if(enemyIndex == 1)
                return new BinSpider();
            else
            {
                return new CarthageSpider();
            }
        }
    }

    public class EnemyCluster
    {
        public Queue<Enemy> Cluster { get; }

        public bool Defeated { get; set; }
        
        public EnemyCluster(int maxEnemies)
        {
            Cluster = new Queue<Enemy>();
            FillCluster(maxEnemies);
        }

        private void FillCluster(int numEnemies) //Currently just grabs from weak pool
        {
            for(int i = 0; i < numEnemies; i++)
            {
                Cluster.Enqueue(EnemySets.GetEnemy()); //does not return a new object, just the reference
            }
        }

        private void Finish()
        {
            Defeated = true;
        }
    }
}