using System;

namespace ProjectLegend.ItemClasses.GearClasses.GearTypes
{

    public abstract class Head : Gear //Omnifensive
    {
        protected Head()
        {
            Slot = 0;
        }
    }

    public abstract class Chest : Gear //Defensive
    {
        protected Chest()
        {
            Slot = 1;
        }
    }

    public abstract class Legs : Gear //Omnifensive
    {

        protected Legs()
        {
            Slot = 2;
        }
    }

    public abstract class Weapon : Gear //Offensive
    {

        protected Weapon()
        {
            Slot = 3;
        }
    }
}