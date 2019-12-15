/// <summary>
/// RPG camera.cs
/// Elbert van de Put
/// nov 6, 2012
/// </summary>
using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/RPGCamera")]
public class RPGCamera : MonoBehaviour
{
	public Transform target;
	public string PlayerTagName = "Player";
	
	public float walkDistance;
	public float runDistance;
	public float height;
	
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	
	private Transform _myTransform;
	
	private float _x;
	private float _y;
	
	private bool _camButtonDown = false;
	
	void Awake()
	{
		_myTransform = transform;
	}
	
	// Use this for initialization
	void Start ()
	{
		if(target == null)
		{
			Debug.LogWarning("No target found on the camera");
		}
		
		else
		{
			CameraSetup();
		}
	}
	
	void Update()
	{
		if(Input.GetMouseButtonDown(1))			//Use the Input Manager to make this user selectable
		{
			_camButtonDown = true;
		}
		
		if(Input.GetMouseButtonUp(1))
		{
			_camButtonDown = false;
		}
	}
	
	//called every frame after all the updates are called
	void LateUpdate()
	{
		if(target != null)
		{
			if(_camButtonDown)
			{	
				//hide the mouse pointer
				Cursor.visible = false;
				
				_x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
		        _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
		 		
		 		//y = ClampAngle(y, yMinLimit, yMaxLimit);
		 		       
		        Quaternion rotation = Quaternion.Euler(_y, _x, 0f);
		        Vector3 position = rotation * new Vector3(0.0f, 0.0f + height, -walkDistance) + target.position;
		        
		        //_myTransform.rotation = rotation;
		        _myTransform.position = position;
				
				_myTransform.LookAt (target);
			}
			else
			{
				//make the mouse pointer visible again
				Cursor.visible = true;
				
				//_myTransform.position = new Vector3(target.position.x,
				//							target.position.y + height,
				//							target.position.z - walkDistance);
				//_myTransform.LookAt(target);
				
				
				// Calculate the current rotation angles
				float wantedRotationAngle = target.eulerAngles.y;
				float wantedHeight = target.position.y + height;
					
				float currentRotationAngle = _myTransform.eulerAngles.y;
				float currentHeight = _myTransform.position.y;
				
				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
				// Damp the height
				currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
				// Convert the angle into a rotation
				Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);
				
				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				_myTransform.position = target.position;
				_myTransform.position -= currentRotation * Vector3.forward * walkDistance;
			
				// Set the height of the camera
				_myTransform.position =  new Vector3(_myTransform.position.x,
													 currentHeight,
													 _myTransform.position.z);
				
				// Always look at the target
				_myTransform.LookAt (target);
				
				_x = currentRotationAngle;
				_y = currentHeight;
			}
		}
		
		else
		{
			GameObject go = GameObject.FindGameObjectWithTag(PlayerTagName);
			
			if(go == null)
			{
				return;
			}
			else
			{
				target = go.transform;
			}
		}
	}
	
	public void CameraSetup()
	{
		_myTransform.position = new Vector3(target.position.x,
											target.position.y + height,
											target.position.z - walkDistance);
		_myTransform.LookAt(target);
	}
}
