using UnityEngine;
using System.Collections;

public class push : MonoBehaviour
{
	public float speed = 1;
	public bool reversed = false;
    Vector3 push_vec = new Vector3(1, 0, 0);
	Rigidbody rb;
	float s;
    Communication com;


    // Use this for initialization
    void Start()
    {
        com = GameObject.Find("Communication").GetComponent<Communication>();
		rb = this.GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
		s = speed;
		if (reversed) {
			s = -speed;
		}

		if (com.insert_fwd())
        {
			rb.AddForce(push_vec * s);

		}
		if (com.insert_rvs ()) {
			rb.AddForce(push_vec * (-s));        
		}
    }
}