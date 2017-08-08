using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class ShipMove : MonoBehaviour 
{
	public float moveSpeed; // Maximum move speed of the ship
	public float tilt; // Maximum tilt of the ship
	public Boundary boundary;

	//public float xMin, xMax, zMin, zMax;
	public GameObject shot; // Game object referencing the shot (lazor bolt) 
	public Transform shotSpawn = null; // transform to hold the rotation data of the shot
	public float fireRate; // rate of firing shots

	private float nextFire;// time til next fire 

	// audio 
	private AudioSource audioSource;

	void Start ()
	{
		//rb.GetComponent<Rigidbody>(); // cache the component (broken)
		// fix? rb=GetComponent
		audioSource = GetComponent<AudioSource>();
	}

	void Update() // every frame update
	{
		// 
		if(Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
	}

	// FixedUpdate is called once per frame just before each phisics step
	void FixedUpdate () 
	{
		float MoveHori = Input.GetAxis ("Horizontal");
		float moveVert = Input.GetAxis ("Vertical");


		// Move Ship, Get Axis returns 0-1 value 
		GetComponent<Rigidbody>().velocity = new Vector3 (MoveHori, 0.0f, moveVert)*moveSpeed;

		GetComponent<Rigidbody>().position = new Vector3 // defining the boundary
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);

	}
}
