using UnityEngine;
using System.Collections;

public class push2 : MonoBehaviour
{
	public float speed = 1;
	GameObject dangerSign;

	Vector3 push_vec = new Vector3(1, 0, 0);

	Communication com;

	float positionStart = 0f;
	float positionEnd = 1.1f;
	float dangerMargin = 0.1f; // do not report danger if this far away from reference
	float dangerOffset = 0.5f; // stop if this far from reference positions
	float position = 0;
	bool isOnStart = false;
	bool isOnEnd = false;
	bool dangerousPosition = false;

	// Use this for initialization
	void Start()
	{
		com = GameObject.Find("Communication").GetComponent<Communication>();
		dangerSign = GameObject.FindGameObjectWithTag ("Danger_potiskac_2");
	}

	void fwd()
	{

		if (position < (positionEnd + dangerOffset))
		{			
			position += Time.deltaTime * speed * push_vec.x;
			transform.Translate(Time.deltaTime * speed * push_vec);
		}            
	}

	void rvs()
	{
		if (position > (positionStart-dangerOffset))
		{
			position -= Time.deltaTime * speed * push_vec.x;
			transform.Translate(Time.deltaTime * speed * -push_vec);
		}
	}



	// Update is called once per frame
	void Update()
	{

		isOnStart = position <= positionStart;
		isOnEnd = position >= positionEnd;
		dangerousPosition = (position < positionStart-dangerMargin) || (position > positionEnd+dangerMargin);

		com.final_B_fwd(isOnEnd);
		com.final_B_rvs(isOnStart);

		if (dangerousPosition) {
			dangerSign.SetActive(true);
		} else {
			dangerSign.SetActive(false);
		}



		if (com.push_B_run())
		{
			if (com.push_B_dir())
			{
				rvs();
			}
			else
			{
				fwd();
			}
		}
	}
}