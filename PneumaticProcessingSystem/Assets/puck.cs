using UnityEngine;
using System.Collections;

public class puck : MonoBehaviour {

    public Transform beltColider;
	public float speed;

    Communication com;
	Rigidbody rb;
	Vector3 direction = new Vector3(0,0,1);

    // Use this for initialization
    void Start () {
        com = GameObject.Find("Communication").GetComponent<Communication>();
		rb = GetComponent<Rigidbody> ();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (transform.GetComponent<Renderer>().bounds.Intersects(beltColider.GetComponent<Renderer>().bounds) && com.belt_run())
        {
            if (com.belt_direction())
            {
				rb.MovePosition(rb.position - direction * (Time.fixedDeltaTime * speed));
            }
            else
            {
				rb.MovePosition(rb.position + direction * (Time.fixedDeltaTime * speed));
            }
        }
    }
}
