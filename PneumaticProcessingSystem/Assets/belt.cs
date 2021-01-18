using UnityEngine;
using System.Collections;

public class belt : MonoBehaviour
{



    public Transform roller,roller1;
    public Transform compressor;
	public float speed_compressor = 200;
	public float speed_roller = 100;
    Communication com;

  


    // Use this for initialization
    void Start()
    {
        com = GameObject.Find("Communication").GetComponent<Communication>();
    }


    // Update is called once per frame
    void Update()
    {
		if (com.compressor())
        {
			compressor.Rotate(new Vector3(0, Time.deltaTime * speed_compressor, 0));
        }

        if (com.belt_run())
        {
            if (com.belt_direction())
            {
				roller.Rotate(new Vector3(0,  Time.deltaTime * speed_roller, 0));
				roller1.Rotate(new Vector3(0, Time.deltaTime * speed_roller, 0));
            }
            else
            {
				roller.Rotate(new Vector3(0, Time.deltaTime * -speed_roller, 0));
				roller1.Rotate(new Vector3(0, Time.deltaTime * -speed_roller, 0));
            }
        }


    }

}