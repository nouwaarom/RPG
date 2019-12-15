/// <summary>
/// Vital.cs
/// Elbert van de Put
/// okt 17, 2012
/// 
/// this class contains all the extra functions for a character vitals.
/// </summary>
public class Vital : ModifiedStat
{
	private int _curValue;						//this is the current value of this vital
	
	public Vital()								
	{
		_curValue = 0;
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}
	
	public int CurValue
	{
		get{
			 if(_curValue > AdjustedBaseValue)
			 _curValue = AdjustedBaseValue;
			 return _curValue; 
		   }
		set{ _curValue = value; }
	}
}

//this enumeration is a list of the vitals out character will have
public enum VitalName 
{
	Health,
	Energy
}
