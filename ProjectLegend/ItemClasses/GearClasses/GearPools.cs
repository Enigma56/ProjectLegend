using System.Collections.Generic;
using ProjectLegend.ItemClasses.GearClasses.GearTypes;
using ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonGear;

namespace ProjectLegend.ItemClasses.GearClasses
{
    public class GearPool
    {
        public HashSet<Gear> LootPool { get; protected set; } //Cannot represent item categories
    }

    public class CommonGear : GearPool
    {
        public static List<Head> CommonHead { get; }
        public static List<Chest> CommonChest { get; }
        public static List<Legs> CommonLegs { get; }
        public static List<Weapon> CommonWeapon { get; }

        static CommonGear()
        {
            CommonHead = new List<Head>()
            {
                new CHBaldCap(),
                new CHDentedIronHelm(),
                new CHLeatherCap(),
                new CHShatteredObsidianCrown(), //R
                new CHStrawHat(),
                new CHTatteredHelm(),
                new CHVineCirclet()
            };
            CommonChest = new List<Chest>()
            {
                new CAChainMail(),
                new CADirtArmour(),
                new CAMIPlate(), //R
                new CAVineCloak(),
                new CAWoodArmour()
            };
            CommonLegs = new List<Legs>()
            {
                new CLCombatBoots(),
                new CLDesertSandals(),
                new CLFootGuards(),
                new CLTatteredBoots(),
                new CLVoidSandals() // R

            };
            CommonWeapon = new List<Weapon>()
            {
                new CWIronSword(),
                new CWObsidianHilt(), //R
                new CWVineSword(),
                new CWWoodStick(),
                new CWWoodSword()
            };
        }

        public CommonGear()
        {
            List<Gear> lootList = new List<Gear>();
            lootList.AddRange(CommonHead);
            lootList.AddRange(CommonChest);
            lootList.AddRange(CommonLegs);
            lootList.AddRange(CommonWeapon);

            LootPool = new HashSet<Gear>(lootList); 
        }
    }
}