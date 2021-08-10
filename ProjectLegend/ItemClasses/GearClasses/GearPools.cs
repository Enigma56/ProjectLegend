using System.Collections.Generic;
using ProjectLegend.ItemClasses.GearClasses.GearTypes;
using ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonGear;

namespace ProjectLegend.ItemClasses.GearClasses
{
    public class GearPool //TODO: Disambiguate the naming schemes to clarify inheritance and calling in code
    {
        public List<Gear> LootPool { get; protected set; }
    }
    public static class GearLootPools
    {
        public static CommonGear CommonGear { get; }

        static GearLootPools()
        {
            CommonGear = new CommonGear();
        }
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
            LootPool = new List<Gear>();
            
            LootPool.AddRange(CommonHead);
            LootPool.AddRange(CommonChest);
            LootPool.AddRange(CommonLegs);
            LootPool.AddRange(CommonWeapon);
        }
    }
}