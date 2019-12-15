/// <summary>
/// Armor.cs
/// Elbert van de Put
/// nov 4, 2012
/// </summary>
using UnityEngine;

public class Armor : Clothing
{
	private int _armorStrength;		//the strength of the armor
	
	public Armor()
	{
		_armorStrength = 0;
	}
	
	public Armor(int al)
	{
		_armorStrength = al;
	}
	
	public int ArmorStrength
	{
		get{return _armorStrength; }
		set{_armorStrength = value; }
	}
}
