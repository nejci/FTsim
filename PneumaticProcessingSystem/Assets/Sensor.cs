using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum sensorName{
	foto_insert,switch_press,switch_belt,foto_finish
}

public class Sensor : MonoBehaviour
{
	// same as function name in communications
	public sensorName sensorID;

	// switches are NO, photocells are NC
	public bool isNormallyClosed = false;

	// To which dropdown menu is this sensor related
	public Transform dropDown;

	// Photocells have two ports that must collide with workpiece to fire change
	public GameObject secondPart;


	Communication com;
	bool isTriggered;
	bool writeValue;
	Collider other;
	SensorSecondPart secondSensor;
	bool secondSensorTriggered;

	// Use this for initialization
	void Start ()
	{
		com = GameObject.Find ("Communication").GetComponent<Communication> ();
		if (secondPart) {
			secondSensor = secondPart.GetComponent<SensorSecondPart> ();
		}
		isTriggered = false;
		secondSensorTriggered = true;
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
		if (secondSensor) {
			secondSensorTriggered = secondSensor.isTriggered;
		}

		if (!other || !secondSensorTriggered) {
			isTriggered = false;
		}			

		//Debug.Log (sensorID.ToString() + ": " + isTriggered.ToString());
		//Debug.Log (sensorID.ToString() + ": " + secondSensorTriggered.ToString());

		if (dropDown.GetComponent<Dropdown> ().value == 0) {
			writeValue = (!isTriggered & isNormallyClosed) | (isTriggered & !isNormallyClosed);

		} else {
			writeValue = (dropDown.GetComponent<Dropdown> ().value == 2);
		}

		switch (sensorID) {
		case sensorName.foto_insert:
			com.foto_insert (writeValue);
			break;
		case sensorName.switch_press:
			com.switch_press (writeValue);
			break;
		case sensorName.switch_belt:
			com.switch_belt (writeValue);
			break;
		case sensorName.foto_finish:
			com.foto_finish (writeValue);
			break;
		}



	}
}
