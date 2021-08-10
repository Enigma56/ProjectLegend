using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using ProjectLegend.GameUtilities;

namespace ProjectLegend.ItemClasses.GearClasses.GearTypes.CommonGear
{

    public sealed class CHTatteredHelm : Head
    {
        public CHTatteredHelm()
        {
            Name = "Tattered Helm";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CHLeatherCap : Head
    {
        public CHLeatherCap()
        {
            Name = "Leather Cap";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CHBaldCap : Head
    {
        public CHBaldCap()
        {
            Name = "Bald Cap";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CHDentedIronHelm : Head
    {
        public CHDentedIronHelm()
        {
            Name = "Dented Iron Helm";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CHShatteredObsidianCrown : Head //Restorable
    {
        public CHShatteredObsidianCrown()
        {
            Name = "Shattered Obsidian Helm";
            Rarity = new Common();
            
            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CHVineCirclet : Head
    {
        public CHVineCirclet()
        {
            Name = "Vine Circlet";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
    public sealed class CHStrawHat : Head
    {
        public CHStrawHat()
        {
            Name = "Dusty Straw Hat";
            Rarity = new Common();

            GearStats = RollCommonStats();
            DropRate = .5;
        }
    }
    
}