using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Table : MonoBehaviour {

    public Transform dropDown2;

	Rigidbody tableRB;
    Vector3 rotateCW = new Vector3(0, 18, 0);
    Communication com;
    float rotation = 0;
	Quaternion deltaRotation;

    // Use this for initialization
    void Start()
    {
        com = GameObject.Find("Communication").GetComponent<Communication>();
		tableRB = GetComponent<Rigidbody> ();
    }

    void rotate_CW()
    {
		rotation += Time.fixedDeltaTime * rotateCW.y;
        deltaRotation = Quaternion.Euler (rotateCW* Time.fixedDeltaTime);
		tableRB.MoveRotation (tableRB.rotation * deltaRotation);
    }

    void rotate_CCW()
    {
		rotation -= Time.fixedDeltaTime * rotateCW.y;
        deltaRotation = Quaternion.Euler (-rotateCW* Time.fixedDeltaTime);
		tableRB.MoveRotation (tableRB.rotation * deltaRotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (com.table_run())
        {
          
            if (com.table_direction())
            {
                rotate_CCW();
            }
            else
            {
                rotate_CW();
            }
        }

        if (dropDown2.GetComponent<Dropdown>().value == 0)
            com.table_ref(Mathf.Abs(rotation) % 90 < 4);
        else
            com.table_ref(dropDown2.GetComponent<Dropdown>().value == 2);


        


    }

}
