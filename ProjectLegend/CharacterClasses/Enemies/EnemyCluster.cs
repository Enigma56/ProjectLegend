using System.Collections.Generic;
using ProjectLegend.GameUtilities;

namespace ProjectLegend.CharacterClasses.Enemies
{
    public class EnemySets
    {
        public HashSet<Enemy> AllPool { get; set; }

        public HashSet<Enemy> WeakPool { get; set; }
        public HashSet<Enemy> StrongPool { get; set; }
        public HashSet<Enemy> PowerfulPool { get; set; }
        
        public EnemySets() //To create enemy pools to pull from
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
    }

    public class EnemyCluster
    {
        public List<Enemy> Cluster;

        public int MaxEnemies { get; set; }

        public bool Defeated { get; set; }

        public EnemyCluster(int numEnemies, int threatLevel = 0)
        {
            MaxEnemies = numEnemies;
            //Fill cluster randomly from a GameUtils Function
            //Utils.FillCluster(numEnemies, threatLevel)
        }
    }
}