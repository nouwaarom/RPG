/// <summary>
/// Base stat.cs
/// Elbert van de Put
/// okt 15, 2012
/// 
/// This is the base class for all stats in a game
/// </summary>
public class BaseStat 
{
	public const int STARTING_EXP_COST = 100;	//publicly accesible value for all base stats to start at
	
	private int _baseValue; 					//base value of this stat
	private int _buffValue;						//amount of buff of this stat
	private int _expToLevel;					//amount of exp needed to raise this skill
	private float _levelModifier;				//modifier applied to the exp needed to raise the skill
	private string _name;
		
	public BaseStat()
	{
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_expToLevel = STARTING_EXP_COST;
		_name = "";
	}
#region setters and getters
	public int BaseValue
	{
		get{return _baseValue; }
		set{_baseValue = value; }
	}
	
	public int BuffValue
	{
		get{return _buffValue; }
		set{_buffValue = value; }
	}
	
	public int ExpToLevel
	{
		get {return _expToLevel; }
		set {_expToLevel = value; }
	}
	
	public float LevelModifier
	{
		get{return _levelModifier; }
		set{_levelModifier = value; }
	}
	
	public string Name
	{
		get{return _name; }
		set{_name = value; }
	}
#endregion
	
	private int CalculateExpToLevel()
	{
		return (int)(_expToLevel * _levelModifier);
	}
		
	public int AdjustedBaseValue
	{
		get{return _baseValue + _buffValue; }
	}
	
	public void LevelUp()
	{
		_expToLevel = CalculateExpToLevel();
		_baseValue++;
	}
}
