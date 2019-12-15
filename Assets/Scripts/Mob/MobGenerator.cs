/// <summary>
/// Mob generator.cs
/// Elbert van de Put
/// okt 18, 2012
/// 
/// this script makes the mobs spawn
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour 
{
	public enum State
	{
		Idle,
		Initialize,
		Setup,
		SpawnMob
	}
	public GameObject[] mobPrefabs;				//an array to hold the prefabs of mobs we want to spawn
	public GameObject[] spawnPoints;			//this array will hold a reference to all the spawnpoints in the scene
		
	public State state; 						//this is our local variable that holds our current state
	
	void Awake()
	{
		state = MobGenerator.State.Initialize;
	}
	
	// Use this for initialization
	IEnumerator Start () 
	{
		while(true)
		{
			switch(state)
			{
			case State.Initialize:
				Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMob:
				SpawnMob();
				break;
			}
			
			yield return 0;
		}
	}
	
	//initialize everything thath is neccesary to create the mobs
	private void Initialize()
	{
		Debug.Log ("Mobgen is in the initialize function");
		if(!CheckForMobPrefabs())
		{
			return;
		}
		
		if(!CheckForSpawnPoints())
		{
			return;
		}
		state = MobGenerator.State.Setup;
	}
	
	private void Setup()
	{
		Debug.Log ("Mobgen is in the setup function");
		
		state = MobGenerator.State.SpawnMob;
	}
	
	private void SpawnMob()
	{
		Debug.Log ("Mobgen is in the spawn mob function");
		
		GameObject[] gos = AvailableSpawnPoints();
		
		for(int cnt = 0; cnt < gos.Length; cnt++)
		{
			int random = Random.Range(0, mobPrefabs.Length);
			
			GameObject go = Instantiate(mobPrefabs[random],
										gos[cnt].transform.position,
										Quaternion.identity
										) as GameObject;
			go.tag = "Enemy";
			go.name = mobPrefabs[random].name;
		}
		
		state = MobGenerator.State.Idle;
	}
	
	
	
	//ckeck to see if we have at least one prefab to spawn
	private bool CheckForMobPrefabs()
	{
		if(mobPrefabs.Length > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	//check to see if we have at least one spawnpoint
	private bool CheckForSpawnPoints()
	{
		if(spawnPoints.Length > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	
	
	//generate a list of available spawnpoints that do not have any mobs childed to it
	private GameObject[] AvailableSpawnPoints()
	{
		List<GameObject> gos = new List<GameObject>();
		
		for(int cnt = 0; cnt < spawnPoints.Length; cnt++)
		{
			if(spawnPoints[cnt].transform.childCount == 0)
			{
				Debug.Log("Mobgen: Spawnpoint available");
				gos.Add(spawnPoints[cnt]);
			}
		}
		
		return gos.ToArray();
	}
}
