using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOncont : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerEx;

	public int scoreValue; // astroid value
	private GameControl gameCunt;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameCunt = gameControllerObject.GetComponent <GameControl> ();
		}
		else if (gameCunt == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "boundary")
		{
			return;
		}
		Instantiate(explosion, other.transform.position, other.transform.rotation);

		if (other.tag == "Player") 
		{
			Instantiate(playerEx, other.transform.position, other.transform.rotation);
			gameCunt.gameover ();
		}

		gameCunt.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);

	}
}
