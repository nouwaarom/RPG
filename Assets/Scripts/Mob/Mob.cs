/// <summary>
/// Mob.cs
/// Elbert van de Put
/// okt 18, 2012
/// 
/// the mob class
/// </summary>
public class Mob : BaseCharacter 
{
	public int curHealth;
	public int maxHealth;
	
	
	// Use this for initialization
	void Start () 
	{
		//change this later
		GetPrimaryAttribute((int)AttributeName.Strength).BaseValue = 100;
		GetVital((int)VitalName.Health).Update();
		curHealth = 100;
		maxHealth = 100;
		Name = this.name;
		GetComponent<EnemyHealth>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void DisplayHealth(bool b)
	{
		GetComponent<EnemyHealth>().enabled = b;
		GetComponent<EnemyHealth>().DisplayName(Name);
	}
}
