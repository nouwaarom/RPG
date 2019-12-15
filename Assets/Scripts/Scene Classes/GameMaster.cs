using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour 
{
	public GameObject playerCharacter;
	public GameObject gameSettings;
	//public Camera mainCamera;
	
	private GameObject _pc;
	private PlayerCharacter _pcScript;
	
	private Vector3 _playerSpawnPointPos;					// place of the spawnpoint

	// Use this for initialization
	void Start () 
	{
		_playerSpawnPointPos = new Vector3(-30, 1, 0); 		//set place of spawnpoint
		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);
		
		if(go == null)
		{
			Debug.LogWarning("Cant find player spawn point");
			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
			go.transform.position = _playerSpawnPointPos;
			Debug.Log("Created a new spawn point");
		}
		
		Debug.Log("The player has spawned");
		_pc = Instantiate(playerCharacter, go.transform.position, Quaternion.identity) as GameObject;
		_pc.name = "Player Character";
		_pcScript = _pc.GetComponent<PlayerCharacter>();
		
		//camera is done in 3rd person camera script
		//mainCamera.transform.position = new Vector3(_pc.transform.position.x,
		//											_pc.transform.position.y + 6f,
		//											_pc.transform.position.z - 14f);
		//mainCamera.transform.Rotate(15, 0, 0);
		
		LoadCharacter();
	}
	
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.LoadLevel("Main Menu");
		}
	}
	
	public void LoadCharacter()
	{
		GameObject gs = GameObject.Find("*Game Settings");
		
		if(gs == null)
		{
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "*Game Settings";
		}
		
		GameSettings gsScript = GameObject.Find("*Game Settings").GetComponent<GameSettings>();
		
		gsScript.LoadCharacterData();
	}
}
