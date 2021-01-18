﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hand : MonoBehaviour {

    public Transform[] cylinders;
	public Transform hand_switch_ref;
	public Transform hand_danger_open;
    public Transform handLeft;
    public Transform handRight;

    public Transform dropDown_ref;
    public Transform dropDown_imp;

	float speed;
	float signals;
	float pulseTrigger = 0;

    //0.5 path: 0.5/15 per pulse
    Vector3 close_vec;

    Communication com;
	Collider other;

    public Transform grabbed = null;

    bool referenceSwitch, isPuckGrabbed, danger_close, danger_open;
	GameObject dangerSign;


	public void puckGrabbed(){
		isPuckGrabbed = true;
		//Debug.Log("Puck is Grabbed.");
	}
	
	public void puckFree(){
		isPuckGrabbed = false;
        //Debug.Log("Puck is not grabbed anymore.");
	}

	void OnTriggerEnter(Collider other){
		if(other.transform == hand_switch_ref)
		{
			referenceSwitch = true;
			//Debug.Log("hand-Reached the switch");
		}
		if (other.transform == hand_danger_open)
		{
			danger_open = true;
			//Debug.Log("Danger - fully opened");
		}	
	}
	void OnTriggerExit(Collider other)
	{
		if(other.transform == hand_switch_ref)
		{
			referenceSwitch = false;
		}
		if (other.transform == hand_danger_open)
		{
			danger_open = false;
		}
	}

    void OnCollisionEnter(Collision collision)
    {
		
		if (collision.transform == handRight)
        {
			danger_close = true;
            //Debug.Log("danger - hands together");
        }
		if (collision.gameObject.tag == "Player"){
			this.other = collision.collider;
		}
    }

    void OnCollisionExit(Collision collision)
    {		
		if (collision.transform == handRight)
        {
			danger_close = false;
        }
		if (collision.gameObject.tag == "Player"){
			this.other = null;
		}

    }

    // Use this for initialization
    void Start()
    {
		danger_close = false;
		danger_open = false;
		referenceSwitch = false;
		isPuckGrabbed = false;

        com = GameObject.Find("Communication").GetComponent<Communication>();
		dangerSign = GameObject.FindGameObjectWithTag ("Danger_stisk");
		if (!com.paramsRead) {
			com.ReadParams ();
		}
		speed = com.hand_speed;
		signals = com.hand_signals;

		close_vec = new Vector3(speed, 0, 0);
    }

    void close(float dt)
    {
		if (!(danger_close || isPuckGrabbed ))
        {
            handRight.Translate(dt * close_vec);
            handLeft.Translate(dt * close_vec);
            //Debug.Log("hand closing");

            pulseTrigger -= dt * close_vec.x;
            if (pulseTrigger < 0)
            {
                pulseTrigger = 0.5f/signals + pulseTrigger;
                if (dropDown_imp.GetComponent<Dropdown>().value == 0)
                    com.hand_imp(true);
                //Debug.Log("triggering close");
            }
        }
    }

    void open(float dt)
    {
		if (!danger_open)
        {
            grabbed = null;
            handRight.Translate(dt * -close_vec);
            handLeft.Translate(dt * -close_vec);
            //Debug.Log("hand opening");

            pulseTrigger += dt * close_vec.x;
            if (pulseTrigger > 0.5f / signals)
            {
                pulseTrigger = pulseTrigger - 0.5f / signals;
                if (dropDown_imp.GetComponent<Dropdown>().value == 0)
                    com.hand_imp(true);
                //Debug.Log("triggering open");
            }
        }

        
    }



    // Update is called once per frame
    void Update()
    {
		if (!other) {
			isPuckGrabbed = false;
		}

		if (danger_open || danger_close) {
			dangerSign.SetActive (true);
		}
		else{
			dangerSign.SetActive (false);	
		}

        if (dropDown_ref.GetComponent<Dropdown>().value == 0)
			com.hand_ref(referenceSwitch);
        else
            com.hand_ref(dropDown_ref.GetComponent<Dropdown>().value == 2);


		if (dropDown_imp.GetComponent<Dropdown>().value == 0)
			com.hand_imp(false);
		else
			com.hand_imp(dropDown_imp.GetComponent<Dropdown>().value == 2);
		
		if (com.hand_close())
        {
            close(Time.deltaTime);
        }
		if (com.hand_open())
        {
            open(Time.deltaTime);
        }
        
    }

}
