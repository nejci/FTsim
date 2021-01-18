using UnityEngine;
using System.Collections;

public class press : MonoBehaviour
{
	public float speed;
    Communication com;

    // Use this for initialization
    void Start()
    {
        com = GameObject.Find("Communication").GetComponent<Communication>();
		speed = 400f;
    }

    void ccw()
    {
		transform.Rotate(new Vector3(0, 0, Time.deltaTime * -speed));

    }

    void cw()
    {
		transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
    }



    // Update is called once per frame
    void Update()
    {

        if (com.drill_A_run())
        {
            if (com.drill_A_dir())
            {
                ccw();
            }
            else
            {
                cw();
            }
        }




    }
}