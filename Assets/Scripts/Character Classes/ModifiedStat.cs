/// <summary>
/// Modified stat.cs
/// Elbert van de Put
/// Okt 15, 2012
/// 
/// This is the base class for all stats that are modiefiable by attributes
/// <summary>
using System.Collections.Generic;			//Generic was added so we can use the List,.

public class ModifiedStat : BaseStat 
{
	private List<ModifyingAttribute> _mods;	//list of attributes that modify this stat
	private int _modValue;					//amount added to the basevalue of this stat
	
	public ModifiedStat()
	{
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	public void AddModifier(ModifyingAttribute mod)
	{
		_mods.Add(mod);
	}
	
	private void CalculateModValue()
	{
		_modValue = 0;
		
		if(_mods.Count > 0)
		{
			foreach(ModifyingAttribute att in _mods)
			{
				_modValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
			}
		}
	}
	
	public new int AdjustedBaseValue
	{
		get{return BaseValue + BuffValue + _modValue; }
	}
	
	public void Update()
	{
		CalculateModValue();
	}
	
	public string GetModifyingAttributesString()
	{
		string temp = "";
		
		for(int cnt = 0; cnt < _mods.Count; cnt++)
		{
			temp += _mods[cnt].attribute.Name;
			temp += "_";
			temp += _mods[cnt].ratio;
			
			if(cnt < _mods.Count - 1)
			{
				temp += "|";
			}
		}
		return temp;
	}
}
/// <summary>
/// the structure that hold an attribute and a ratio that will be added as a modifying attribute to our ModifiedStats
/// </summary>
public struct ModifyingAttribute
{
	public Attribute attribute;
	public float ratio;
	
	
	public ModifyingAttribute(Attribute att, float rat)
	{
		attribute = att;
		ratio = rat;
		
	}
}