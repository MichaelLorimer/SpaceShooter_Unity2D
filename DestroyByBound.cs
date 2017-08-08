using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBound : MonoBehaviour
{
	void OnTriggerExit (Collider other)
	{
		Destroy (other.gameObject);
	}
}
