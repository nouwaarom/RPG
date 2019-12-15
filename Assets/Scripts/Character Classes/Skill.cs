/// <summary>
/// Skill.cs
/// Elbert van de Put
/// okt 17, 2012
/// 
/// this class contains all the methods that are needed for a skill
/// </summary>
public class Skill : ModifiedStat 
{
	private bool _known;				//boolean to toggle if a character knows a skill
	
	public Skill()
	{
		_known = false;
		ExpToLevel = 25;	
		LevelModifier = 1.2f;
	}
	
	public bool Known
	{
		get{ return _known; }
		set{ _known = value; }
	}
}

//this enumeration is a list of the skills the character has
public enum SkillName
{
	Melee_Offense,
	Melee_Defense,
	Shielding,
	Archery,
	Throwing,
	Magic,
	Hunting
}
	

