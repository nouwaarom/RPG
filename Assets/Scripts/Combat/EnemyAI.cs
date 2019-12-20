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

    public AudioClip biteAudio;
    public AudioClip roarAudio;

	private float attackTimer;
	private int maxDistance;
	
	private Transform myTransform;
    private Animation animationComponent;

    private AudioSource audioSource;
    private Rigidbody myBody;
	
	void Awake()
	{
		myTransform = transform;
	}
	
	// Use this for initialization
	void Start () 
	{
        myBody = GetComponent<Rigidbody>();
		target = GameObject.FindGameObjectWithTag("Player");
        animationComponent = GetComponent<Animation>();
        audioSource = gameObject.AddComponent<AudioSource>();
		
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
            // Correct for current velocity perpendicular to target velocity.
            Vector3 correctedForward = myTransform.forward - (myBody.velocity.normalized * (Vector3.Dot(myBody.velocity.normalized, myTransform.forward)));
            myBody.AddForce(correctedForward * moveSpeed);
            
            animationComponent.CrossFade("Creep");
		}
    }
    
	private void Attack()
	{
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);

        audioSource.PlayOneShot(biteAudio);
		
        if (direction > 0) {
            animationComponent.CrossFade("Shew");
            PlayerHealth eh = (PlayerHealth)target.GetComponent("PlayerHealth");
            eh.AddjustCurrentHealth(-3);

            attackTimer = coolDown;
        }
	}
}
