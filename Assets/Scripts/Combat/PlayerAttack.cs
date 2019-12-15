using UnityEngine;

public class PlayerAttack : MonoBehaviour 
{
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	
	// Use this for initialization
	void Start () 
	{
		attackTimer = 0;
		coolDown = 1.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (attackTimer > 0) {
            attackTimer -= Time.deltaTime;

            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && GetComponent<PlayerHealth>().SpendStamina(21.0f)) { 
        
            Attack();
            attackTimer = coolDown;
        }
	
	}
	
	private void Attack()
	{
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);
		
        GetComponentInChildren<Animator>().SetTrigger("Attack");

		if(distance < 3) {
			if(direction > 0) {
				EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
				eh.AddjustCurrentHealth(-10);
			}
		}
	}
}
