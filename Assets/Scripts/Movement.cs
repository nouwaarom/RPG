/// <summary>
/// Movement.cs
/// Elbert van de Put
/// 9 nov, 2012
/// </summary>
using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour
{
	private Transform _myTransform;					//cached transform of this gameobject
	private CharacterController _controller;		//cached CharacterController
	private Vector3 _moveDirection; 				//the direction that the character moves
	private CollisionFlags _collisionFlags;			//the collisionFlags we have from the last frame
	
	
	public float rotateSpeed = 250f;
	public float walkSpeed = 5f;
	public float runMultiplier = 2f;
	public float waterSpeed = 2f;
	public float strafeSpeed = 3f;
	public float gravity = 10f;
	public float airTime = 0f;						//how long we are in the air since we touched the ground
	public float fallTime = .5f;					//time falling befor it is noticed as a fall
	public float jumpHeight = 7f;					//the height that we move when we jump
	public float jumpTime = 1.1f;

    public bool isMobile = true;

    private Animator animator;
	
	void Awake()
	{
		_myTransform = transform;
		_controller = GetComponent<CharacterController>();
	}
	
	// Use this for initialization
	void Start ()
	{
		_moveDirection = Vector3.zero;

        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_myTransform = transform;

        if (!isMobile) {
            return;
        }

        bool isRunning = false;

		Rotate();
		
		if(_myTransform.position.y < -3)
		{
			_moveDirection = new Vector3(Input.GetAxis("Strafe"), 0, Input.GetAxis("Move Forward"));
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= waterSpeed;
			_moveDirection.y -= gravity * Time.deltaTime;
		}
		
		if(_controller.isGrounded)
		{
			airTime = 0;
            animator.SetBool("Jump", false); // We dont wish to jump if we are on the ground.
			
			_moveDirection = new Vector3(Input.GetAxis("Strafe"), 0, Input.GetAxis("Move Forward"));
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			
			if(Input.GetButton("Move Forward"))
			{
                // Only run if there is enough stamina.
				if(Input.GetAxis("Move Forward") > 0 && Input.GetButton("Run Forward") && GetComponent<PlayerHealth>().SpendStamina(3.0f * Time.deltaTime))
				{
                    isRunning = true;
					_moveDirection *= runMultiplier;
				}
			}
			
			if(Input.GetButton("Jump"))
			{
				if(airTime < jumpTime)
				{
					_moveDirection.y += jumpHeight;
					Jump();
				}
			}
			
			else if(Input.GetButton ("Strafe"))
			{
				Strafe();
			}
			
			else
			{
				Idle();
			}
		}
		
		if(Input.GetButton("Move Forward") && airTime < fallTime)
		{
			if(Input.GetButton("Run Forward") && Input.GetAxis("Move Forward") > 0 && (isRunning || GetComponent<PlayerHealth>().SpendStamina(3.0f * Time.deltaTime)))
			{
				Run();
			}
			else
			{
				if(Input.GetAxis("Move Forward") > 0)
				{
					Walk();
				}
				else
				{
					WalkRevert();
				}
			}
		}
		
		if(Input.GetButton("Strafe") && airTime < fallTime)
		{
			Strafe();
		}
		
		if((_collisionFlags & CollisionFlags.CollidedBelow) == 0)
		{
			airTime += Time.deltaTime;
			
			if(airTime > fallTime)
			{
				Fall();
			}
		}
		
		_moveDirection.y -= gravity * Time.deltaTime;
		_collisionFlags = _controller.Move(_moveDirection * Time.deltaTime);
	}
	
	private void Rotate()
	{
		if(Input.GetButton("Rotate Player"))
		{
			_myTransform.Rotate(0, Input.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed, 0);
		}
	}
	
	private void Idle()
	{
		animator.SetInteger("Speed", 0);
	}
	
	private void Run()
	{
		animator.SetInteger("Speed", 2);
	}
	
	private void Walk()
	{
		animator.SetInteger("Speed", 1);
	}
	
	private void WalkRevert()
	{
		animator.SetInteger("Speed", -1);
	}
	
	private void Strafe()
	{
		animator.SetInteger("Speed", 1);
	}
	
	private void Jump()
	{
		animator.SetBool("Jump", true);
	}
	
	private void Fall()
	{
		animator.SetBool("Jump", true);
	}

    public void Die()
    {
        isMobile = false;
        //GetComponent<Animation>().CrossFade("Dying");
    }
}
