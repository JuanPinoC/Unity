using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour 
{
	public GameObject gameManager;            //GameManager prefab to instantiate.
	private Transform PlayerTransform;

	void Awake ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (GameManager.instance == null)
			//Instantiate gameManager prefab
			Instantiate(gameManager);
			GameObject[] gos;
    		gos = GameObject.FindGameObjectsWithTag("Player");
			GameObject player = gos[0];
			PlayerTransform = player.transform;
	}

	void FixedUpdate ()
	{
		//Camera Movement
		transform.position = new Vector3( 
								PlayerTransform.position.x, 
								PlayerTransform.position.y, 
								transform.position.z
							);
	}

}