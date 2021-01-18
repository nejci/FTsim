using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SensorSecondPart : MonoBehaviour
{
	
	public bool isTriggered;
	Collider other;

	// Use this for initialization
	void Start ()
	{
		isTriggered = false;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			isTriggered = true;
			this.other = other;
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player") {
			isTriggered = true;
			this.other = other;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			isTriggered = false;
			this.other = null;
		}
	}

	void Update ()
	{
		if (isTriggered && !other) {
			isTriggered = false;
		}
		//Debug.Log (isTriggered);
	}
}
