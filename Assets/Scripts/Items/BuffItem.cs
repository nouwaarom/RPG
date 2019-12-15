/// <summary>
/// Buff item.cs
/// Elbert van de Put
/// nov 3, 2012
/// 
/// </summary>
using UnityEngine;
using System;
using System.Collections;

public class BuffItem : Item
{
	private int[] buffMods;
	private BaseStat[] stat;
	
	private Hashtable buffs;
	/// <summary>
	/// Strength, 50
	/// Melee Offence, 100
	/// </summary>
	
	public BuffItem()
	{
		buffs = new Hashtable();
	}
	
	public BuffItem(Hashtable ht)
	{
		buffs = ht;
	}
	
	public void AddBuff(BaseStat stat, int mod)
	{
		try
		{
			buffs.Add(stat.Name, mod);
		}
		catch(Exception ex)
		{
			Debug.LogWarning(ex.ToString());
		}
	}
	
	public void RemoveBuff(BaseStat stat)
	{
		buffs.Remove(stat.Name);
	}
	
	public int BuffCount()
	{
		return buffs.Count;
	}
	
	public Hashtable GetBuffs()
	{
		return buffs;
	}
}
