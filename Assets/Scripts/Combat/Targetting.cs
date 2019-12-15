/// <summary>
/// Targetting.cs
/// Elbert van de Put
/// okt 15, 2012
/// 
/// Script for player to be able to target mobs.
/// </summary>
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Targetting : MonoBehaviour 
{
	public List<GameObject> targets;
	public GameObject selectedTarget;
	
	private Transform myTransform;
    private PlayerAttack playerAttack;
		
	// Use this for initialization
	void Start () 
	{
        playerAttack = GetComponentInParent<PlayerAttack>();
		targets = new List<GameObject>();
		selectedTarget = null;
		myTransform = transform;
		
		AddAllEnemies();
	}
	
	public void AddAllEnemies()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject enemy in go)
            targets.Add(enemy);
	}
	
	private void SortTargetsByDistance()
	{
        targets = targets.Where(x => x != null).ToList();
		targets.Sort(delegate(GameObject t1, GameObject t2) { 
			return Vector3.Distance(t1.transform.position, myTransform.position).CompareTo(Vector3.Distance(t2.transform.position, myTransform.position));
			});
	}	
	
	private void TargetEnemy()
	{
		if(targets.Count == 0)
		{
			AddAllEnemies();
		}
		
		if(targets.Count > 0)
		{
			if(selectedTarget == null)
			{
				SortTargetsByDistance();
				selectedTarget = targets[0];
			}
			
			else
			{
				int index = targets.IndexOf(selectedTarget);
				
				if(index < targets.Count - 1)
				{
					index++;
				}
				
				else
				{
					index = 0;
				}
				DeselectTarget();
				selectedTarget = targets[index];	
			}
			SelectTarget();
		}
	}
	
	private void SelectTarget()
	{
        playerAttack.target = selectedTarget;

		selectedTarget.GetComponent<Mob>().DisplayHealth(true);
		//change outlinecolor to red
		Transform child = selectedTarget.transform.GetChild(0);
		child.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red);
	}
	
	//all changes must be before we set our target to null otherwise you will get a null reference exception
	private void DeselectTarget()
	{
		//change the outlinecolor to black again
		Transform child = selectedTarget.transform.GetChild(0);
		child.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
		selectedTarget.GetComponent<Mob>().DisplayHealth(false);
		
		selectedTarget = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//target an enemy when tab is pressed
		if(Input.GetKeyUp(KeyCode.Tab))
		{
			TargetEnemy();
		}
	}
}
