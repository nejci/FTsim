using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{

	public Transform key;
    public Transform button1;
    public Transform button2;
    public Transform button3;
    public Transform button4;
    public Transform green;
    public Transform red_left;
    public Transform red_right;
    public Transform yellow_right;
    public Transform yellow_left;

    Communication com;

    // Use this for initialization
    void Start()
    {
        com = GameObject.Find("Communication").GetComponent<Communication>();
    }



    // Update is called once per frame
    void Update()
    {
        //update lights
        red_left.GetComponent<Button>().interactable = com.red_left();
        red_right.GetComponent<Button>().interactable = com.red_right();
        yellow_left.GetComponent<Button>().interactable = com.yellow_left();
        yellow_right.GetComponent<Button>().interactable = com.yellow_right();

        com.black_l_u(button1.GetComponent<Toggle>().isOn);
        com.black_r_u(button2.GetComponent<Toggle>().isOn);
        com.black_r_d(button3.GetComponent<Toggle>().isOn);
        com.black_l_d(button4.GetComponent<Toggle>().isOn);
        com.green(green.GetComponent<Toggle>().isOn);
		com.key (key.GetComponent<Toggle> ().isOn);	 

    }

}
