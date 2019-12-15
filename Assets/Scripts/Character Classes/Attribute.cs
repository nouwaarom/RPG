/// <summary>
/// Attribute.cs
/// Elbert van de Put
/// Okt 15, 2012
/// 
/// This is the class for all of the character attributes in game
/// </summary>
public class Attribute : BaseStat 
{
	new public const int STARTING_EXP_COST = 50;
	
	public Attribute()
	{
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;
	}
}

public enum AttributeName
{
	Speed,
	Concentration,
	Willpower,
	Strength,
	Agility
}
