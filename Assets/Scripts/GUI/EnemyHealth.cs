using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
	public int maxHealth = 100;
	private int curHealth = 50;
	public float healthBarLength = 300f;
	
	public Texture texture;
	
	private float _currentHealthBarLength;
	private string _name = "";
	
	// Use this for initialization
	void Start () 
	{
        AddjustCurrentHealth(0);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width - healthBarLength - 14, 2, healthBarLength + 14, 89), "");
		
		GUI.DrawTexture(new Rect(Screen.width - _currentHealthBarLength - 10, 10, _currentHealthBarLength, 30), texture);
		GUI.Label(new Rect(Screen.width - (_currentHealthBarLength / 2 + 200)/ 2, 15, 100, 20), curHealth + " HP" + "/" + maxHealth + " HP");
		
		GUI.Label(new Rect(Screen.width - (healthBarLength / 2 + 200)/ 2, 40, 100, 20), _name);
	}
	
	public void DisplayName(string name)
	{
		_name = name;
	}
	
	public void AddjustCurrentHealth(int adj)
	{
		curHealth += adj;

        if (curHealth <= 0) {
            Destroy(this.gameObject, 0.5f);
        }
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		
		if(maxHealth < 1)
			maxHealth = 1;
		
		_currentHealthBarLength = healthBarLength * (curHealth / (float)maxHealth);
	}
}
