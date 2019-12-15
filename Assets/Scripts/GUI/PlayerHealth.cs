using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
	public int maxHealth = 100;
	private float curHealth = 100;
	
	public int maxStamina = 100;
	private float curStamina = 100;
	
	public float healthBarLength = 300f;
	
	public Texture healthTexture;
	public Texture manaTexture;
	
	private float _currentHealthBarLength;
	private float _currentStaminaBarLength;

    private float lastDamageTime = 0.0f;
    private float lastStaminaTime = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
        AddjustCurrentHealth(0);
		_currentStaminaBarLength = healthBarLength;
	}
	
	// Update is called once per frame
	void Update () 
	{
        // Start regening health after 5 seconds.
        if (Time.time > lastDamageTime + 5.0f) {
            AddjustCurrentHealth(3f * Time.deltaTime);
        }

        // Start regening after 3 seconds.
        if (Time.time > lastStaminaTime + 2.0f) {
            UpdateStamina(10f * Time.deltaTime);
        }
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(2, 2, healthBarLength + 14, 89), "");
		
		GUI.DrawTexture(new Rect(10, 10, _currentHealthBarLength, 30), healthTexture);
		GUI.Label(new Rect(_currentHealthBarLength / 2 - 50, 15, 100, 20), curHealth.ToString("#.#") + " HP" + "/" + maxHealth + " HP");
		
		GUI.DrawTexture(new Rect(10, 50, _currentStaminaBarLength, 30), manaTexture);
		GUI.Label(new Rect(_currentStaminaBarLength / 2 - 50, 55, 100, 20), curStamina.ToString("#.#") + "/" + maxStamina);
	}
	
	public void AddjustCurrentHealth(float adj)
	{
		curHealth += adj;
        if (adj < 0.0f)
        {
            lastDamageTime = Time.time;
        }
		
		if(curHealth < 0) {
            GetComponent<Movement>().Die();
        } 
		
		if(curHealth > maxHealth)
			curHealth = maxHealth;

        if (maxHealth < 1) {
            maxHealth = 1;
        }
		
		_currentHealthBarLength = healthBarLength * (curHealth / (float)maxHealth);
	}

	public bool SpendStamina(float amount)
	{
        lastStaminaTime = Time.time;
        if (curStamina < amount) {
            return false;
        }

        UpdateStamina(-amount);

        return true;
	}

    private void UpdateStamina(float amount)
    {
		curStamina += amount;
        if (curStamina > maxStamina) {
            curStamina = maxStamina;
        }
		
		_currentStaminaBarLength = healthBarLength * (curStamina / (float)maxStamina);
    }
}
