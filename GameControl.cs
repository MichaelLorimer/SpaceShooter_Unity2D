using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour 
{
	public GameObject hazard;
	public Vector3 spawnVal;

	public int hazardCount; // current amount of hazards
	public float spawnWait; // Wait for next spawn
	public float startWait; // 
	public float waveWait; // Wait for next wave

	public GUIText scoreText; // reff to gui text component 
	public GUIText RestartText;
	public GUIText GameOverText;

	private bool GameOver;
	private bool Restart;

	private int score; // holds the current score 

	//public Vector3 pos;
	void Start()
	{
		GameOver = false;
		Restart = false;

		GameOverText.text = "";
		RestartText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves());
	}

	void Update ()
	{
		if (Restart) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);

		while (true)
		{
			for(int i= 0; i < hazardCount; i++)
			{
				Vector3 spawnPos= new Vector3 (Random.Range (-spawnVal.x, spawnVal.x), spawnVal.y, spawnVal.z);
				Quaternion spawnRot = Quaternion.identity;
				Instantiate  (hazard, spawnPos, spawnRot);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (GameOver)
			{
				RestartText.text = "Press 'R' for Restart";
				Restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue; 
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score; // set score text + score value
	}

	public void gameover()
	{
		GameOverText.text = "GAMEOVER!";
		GameOver = true;
	}
}
