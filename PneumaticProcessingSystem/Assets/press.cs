using UnityEngine;
using System.Collections;

public class press : MonoBehaviour
{

    Vector3 push_vec = new Vector3(0, -1, 0);

    Communication com;

    float position = 0;


    // Use this for initialization
    void Start()
    {
        com = GameObject.Find("Communication").GetComponent<Communication>();
    }

    void fwd()
    {        
        if (position < 0.4f)
        {
            position -= Time.deltaTime * push_vec.y;
            transform.Translate(Time.deltaTime * push_vec);
        }

    }

    void rvs()
    {
        if (position > 0)
        {
            position += Time.deltaTime * push_vec.y;
            transform.Translate(Time.deltaTime * -push_vec);
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (com.press())
        {
            fwd();
        }
        else
        {
            rvs();
        }



    }
}