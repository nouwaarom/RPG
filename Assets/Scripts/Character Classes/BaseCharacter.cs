using UnityEngine;
using System.Collections;
using System;

public class BaseCharacter : MonoBehaviour 
{
	private string _name;
	private int _level;
	private uint _freeExp;
	
	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;
	
	public void Awake()
	{
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;
		
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}
	
	#region name level and exp setters and getters
	public string Name
	{
		get{ return _name; }
		set{ _name = value; }
	}
	
	public int Level
	{
		get{ return _level; }
		set{ _level = value; }
	}
	
	public uint FreeExp
	{
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}
	#endregion
	
	public void AddExp(uint exp)
	{
		_freeExp += exp;
		CalculateLevel();
	}
	
	public void CalculateLevel()
	{
		
	}
	
	private void SetupPrimaryAttributes()
	{
		for(int cnt = 0; cnt < _primaryAttribute.Length; cnt++)
		{
			_primaryAttribute[cnt] = new Attribute();
			_primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
		}
	}
	
	private void SetupVitals()
	{
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt] = new Vital();
		
		SetupVitalModifiers();
	}
	
	private void SetupSkills()
	{
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt] = new Skill();
		
		SetupSkillModifiers();
	}
	#region attribute vital and skill setters and getters
	public Attribute GetPrimaryAttribute(int index)
	{
		return _primaryAttribute[index];
	}
	
	public Vital GetVital(int index)
	{
		return _vital[index];
	}
	
	public Skill GetSkill(int index)
	{
		return _skill[index];
	}
	#endregion
	
	private void SetupVitalModifiers()
	{
		//health
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), 10f));
		//energy
		GetVital((int)VitalName.Energy).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), 10f));
	}
	
	private void SetupSkillModifiers()
	{
		//melee offense
		GetSkill((int)SkillName.Melee_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), .4f));
		GetSkill((int)SkillName.Melee_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .33f));
		//melee defense
		GetSkill((int)SkillName.Melee_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .4f));
		//shielding
		GetSkill((int)SkillName.Shielding).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .2f));
		//archery
		GetSkill((int)SkillName.Archery).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), .4f));
		GetSkill((int)SkillName.Archery).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .2f));
		//throwing
		GetSkill((int)SkillName.Throwing).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .4f));
		GetSkill((int)SkillName.Throwing).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .2f));
		//magic
		GetSkill((int)SkillName.Magic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .4f));
		//hunting
		GetSkill((int)SkillName.Hunting).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .2f));
		GetSkill((int)SkillName.Throwing).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .2f));
		GetSkill((int)SkillName.Throwing).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), .2f));
	}
	
	public void StatUpdate()
	{
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt].Update();
		
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt].Update();
	}
}
