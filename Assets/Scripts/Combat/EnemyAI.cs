using UnityEngine;

/**
 * Moves towards the player and tries to attack it using melee. 
 */
public class EnemyAI : MonoBehaviour 
{
	public GameObject target;
	public int moveSpeed;
	public int rotationSpeed;
	public float coolDown;

	private float attackTimer;
	private int maxDistance;
	
	private Transform myTransform;
    private Animation animation;
	
	void Awake()
	{
		myTransform = transform;
	}
	
	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player");
        animation = GetComponent<Animation>();
		
		maxDistance = Random.Range(20, 60);

		attackTimer = 0;
	}
	
	// Update is called once per frame
	void Update() 
	{
		//look at target
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);

        float distance = Vector3.Distance(target.transform.position, myTransform.position);
		
		if(attackTimer > 0)
			attackTimer -= Time.deltaTime;
		else if (distance < 2) {
			Attack();
		} else if (distance < maxDistance) {
			//move towards target
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
            animation.CrossFade("Creep");
		}
    }
    
	private void Attack()
	{
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);
		
        if (direction > 0) {
            animation.CrossFade("Shew");
            PlayerHealth eh = (PlayerHealth)target.GetComponent("PlayerHealth");
            eh.AddjustCurrentHealth(-3);

            attackTimer = coolDown;
        }
	}
}
