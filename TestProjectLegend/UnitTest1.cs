using Xunit;
using ProjectLegend;
using ProjectLegend.CharacterClasses;
using ProjectLegend.ItemClasses.Consumables;


namespace TestProjectLegend;

public class TestPlayerHand
{
	[Fact]
	public void ATestEmptyPlayerHand()
	{
		Assert.Null(Player.Instance.Hand);
	}

	[Fact]
	public void BTestPlayerHandWithPotion()
	{
		Player.Instance.Hand = new EnergyPotion();

		var actual = Player.Instance.Hand;
		
		Assert.Equal(new EnergyPotion(), actual);
	}
}

public class PlayerStringDisplays
{
	[Fact]
	public void DisplayPlayerInventory()
	{
		
	}
}