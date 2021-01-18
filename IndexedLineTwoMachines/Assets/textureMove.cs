using UnityEngine;
using System.Collections;

public class textureMove : MonoBehaviour {

	public int beltIndex;
	public float speed = 1; 
	public bool inverseDimension;
	public bool inverseDirection;

	private float offset = 0;
	private Vector2 offsetOrig;
	private Renderer _myrenderer;

	Communication com;
	// Use this for initialization
	void Start () {
		com = GameObject.Find("Communication").GetComponent<Communication>();
		_myrenderer = GetComponent<Renderer>();
		offsetOrig = _myrenderer.material.GetTextureOffset ("_MainTex");

	}
	
	// Update is called once per frame
	void Update () {
		float x;
		float y;
		int dir = 1;

		offset += (Time.deltaTime * speed);
		if (!com.belt_direction (beltIndex)) {
			dir = -1;
		}
		if (inverseDirection) {
			dir = dir * (-1);
		}

		if (!inverseDimension) {
			x = dir*offset + offsetOrig.x;
			y = 0;
		} else {
			x = 0;
			y = dir*offset + offsetOrig.y;
		}

		Vector2 offsetVec = new Vector2 (x, y);

		if (com.belt_run(beltIndex))
		{
			_myrenderer.material.SetTextureOffset ("_MainTex", offsetVec);
		}
	}
}
