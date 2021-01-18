using UnityEngine;
using System.Collections;

public class belt1 : MonoBehaviour
{
    public Transform[] roller;
	public float roller_speed = 100;
    Communication com;

    // Use this for initialization
    void Start()
    {
        com = GameObject.Find("Communication").GetComponent<Communication>();
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        { 			
            if (com.belt_run(i))
            {
                if (com.belt_direction(i))
                {
					roller[i].Rotate(new Vector3( 0, 0, Time.deltaTime * roller_speed));
                }
                else
                {
					roller[i].Rotate(new Vector3( 0, 0 , -Time.deltaTime * roller_speed));
                }
            }
        }


    }

}