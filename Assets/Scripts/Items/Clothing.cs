/// <summary>
/// Clothing.cs
/// Elbert van de Put
/// nov 3, 2012
/// </summary>
using UnityEngine;

public class Clothing : BuffItem
{
	private ArmorSlot _slot;		//the place to store the slot the armor is in
	
	public Clothing()
	{
		_slot = ArmorSlot.Head;
	}
	
	public Clothing(ArmorSlot slot)
	{
		_slot = slot;
	}
	
	public ArmorSlot Slot
	{
		get{return _slot; }
		set{_slot = value; }
	}
}

public enum ArmorSlot
{
	Head,
	UpperBody,
	Hands,
	Pants,
	Feet,
	Back
}
