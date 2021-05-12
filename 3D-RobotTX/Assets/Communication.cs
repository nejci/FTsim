using UnityEngine;
using myS7ProSimLib;
using System;
using System.Runtime.InteropServices;
using System.IO;

public class Communication : MonoBehaviour
{
    
	public string file_params = "config.txt";
	public float table_speed = 20f;
	public int table_signals = 150;
	public float horizontal_speed = 0.25f;
	public int horizontal_signals = 103;
	public float vertical_speed = 0.42f;
	public int vertical_signals = 78;
	public float hand_speed = 0.3f;
	public int hand_signals = 17;
	public float PLC_cycle = 0.050f;
	public int isShowFPS = 1;
	public bool paramsRead;

	public void ReadParams()
	{
		string line; 
		if (File.Exists (file_params)) {
			//Read the text from directly from the test.txt file
			StreamReader reader = new StreamReader (file_params); 

			while ((line = reader.ReadLine ()) != null) {  
				string[] lineparts = line.Split (':');
				if (lineparts [0] == "table_speed") {					
					table_speed = System.Convert.ToSingle (lineparts [1]);
				} 
				else if (lineparts [0] == "table_num_impulses") {
					table_signals = System.Convert.ToInt32 (lineparts [1]);
				}
				else if (lineparts [0] == "lift_speed") {
					vertical_speed = System.Convert.ToSingle (lineparts [1]);
				}
				else if (lineparts [0] == "lift_num_impulses") {
					vertical_signals = System.Convert.ToInt32 (lineparts [1]);
				}
				else if (lineparts [0] == "extend_speed") {
					horizontal_speed = System.Convert.ToSingle (lineparts [1]);
				}
				else if (lineparts [0] == "extend_num_impulses") {
					horizontal_signals = System.Convert.ToInt32 (lineparts [1]);
				}
				else if (lineparts [0] == "hand_speed") {
					hand_speed = System.Convert.ToSingle (lineparts [1]);
				}
				else if (lineparts [0] == "hand_num_impulses") {
					hand_signals = System.Convert.ToInt32 (lineparts [1]);
				}
				else if (lineparts [0] == "PLC_cycle") {
					PLC_cycle = System.Convert.ToSingle (lineparts [1]);
				}
				else if (lineparts[0] == "showFPS") {
					isShowFPS = System.Convert.ToInt32(lineparts[1]);
				}
			}
			reader.Close ();
			paramsRead = true;
		} else {
			Debug.Log ("File with params not found!");
		}
	}

	public bool read(int byteIndex, int bitIndex)
    {
        bool output = false;
        object refOutput = output;
		if (ps != null) {
			ps.ReadOutputPoint (byteIndex, bitIndex, PointDataTypeConstants.S7_Bit, ref refOutput);
		}
		return (bool)refOutput;
    }

    public void write(int byteIndex, int bitIndex, bool val)
    {
        object refInput = val;
		if (ps != null) {
			ps.WriteInputPoint (byteIndex, bitIndex, ref refInput);
		}
    }

    //outputs
	public void table_ref(bool val) { write(0,5, val); }
	public void table_imp_a(bool val) { write(0, 3, val); }
	public void table_imp_b(bool val) { write(0, 4, val); }
	public void extend_ref(bool val) { write(1,0, val); }
	public void extend_imp(bool val) { write(1,1, val); }
	public void lift_ref(bool val) { write(0,2, val); }
	public void lift_imp_a(bool val) { write(0,0, val); }
	public void lift_imp_b(bool val) { write(0,1, val); }
	public void hand_ref(bool val) { write(0,6, val); }
	public void hand_imp(bool val) { write(0,7, val); }

    //buttons
    public void key(bool val) { write(1, 2, val); }
    public void green(bool val) { write(1, 3, val); }
    public void black_l_u(bool val) { write(1, 4, val); }
    public void black_l_d(bool val) { write(1, 5, val); }
    public void black_r_u(bool val) { write(1, 6, val); }
    public void black_r_d(bool val) { write(1, 7, val); }


    //inputs
	public bool table_CW() { return read(0, 7); }
	public bool table_CCW() { return read(0, 6); }
	public bool extend_forward() { return read(1, 3); }
	public bool extend_reverse() { return read(1, 2); }
	public bool lift_up() { return read(0, 4); }
	public bool lift_down() { return read(0, 5); }
	public bool hand_open() { return read(1, 1); }
	public bool hand_close() { return read(1, 0); }

    //lights
    public bool red_left() { return read(1, 4); }
    public bool yellow_left() { return read(1, 5); }
    public bool red_right() { return read(1, 6); }
    public bool yellow_right() { return read(1, 7); }





    /*
    * Used tlbimp.exe to generate library DLL
    * place new generated DLL on assets/plugins
    * also note that the DLL is treated as managed code
    */
    public S7ProSimClass ps;
	private bool connectionEstablished = false;


    public static void ps_ConnectionError(string controlEngine, int error)
    {
		Debug.Log("Connection to PLCSIM threw errors.");
    }

    void Start()
    {
		paramsRead = false;
        Application.runInBackground = true;
		ReadParams ();

		print("Connecting to S7-PLCSIM ...\n");

		try
		{
			ps = new S7ProSimClass();
			ps.Connect();
			ps.SetScanMode(ScanModeConstants.ContinuousScan);
			connectionEstablished = true;
		}
		catch (COMException e)
		{
			print(e.ToString());
			connectionEstablished = false;
		}

        // Here we pass the ref as an obj, since WriteInputPoint method
        // can take bit, word, dwords, as addresses ref obj can take the
        // for of bool, int, float, etc.

    }
	void Update(){
		if (!connectionEstablished) {
			if (GameObject.FindWithTag ("Dialog_error_PLCSIM") == null) {
				Dialog.MessageBox("Dialog_error_PLCSIM", "Error", "The connection with the S7-PLCSIM simulator cannot be established.", "Exit", () => { Application.Quit(); });
			}
		}
	}


}