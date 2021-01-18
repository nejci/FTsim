using UnityEngine;
using myS7ProSimLib;
using System;
using System.Runtime.InteropServices;

public class Communication : MonoBehaviour
{
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
    public void foto_insert(bool val) { write(0, 0, val); }
    public void switch_press(bool val) { write(0, 1, val); }
    public void tipka(bool val) { write(0, 2, val); }
    public void table_ref(bool val) { write(0,3, val); }
    public void switch_belt(bool val) { write(0,4, val); }
    public void foto_finish(bool val) { write(0,5, val); }


    //buttons
    public void key(bool val) { write(1, 2, val); }
    public void green(bool val) { write(1, 3, val); }
    public void black_l_u(bool val) { write(1, 4, val); }
    public void black_l_d(bool val) { write(1, 5, val); }
    public void black_r_u(bool val) { write(1, 6, val); }
    public void black_r_d(bool val) { write(1, 7, val); }


    //inputs
	public bool compressor() { return read(0, 0); }
	public bool insert_fwd() { return read(0, 1); }
	public bool insert_rvs() { return read(0, 2); }
    public bool press() { return read(0, 3); }
    public bool table_direction() { return read(0, 4); }
	public bool table_run() { return read(0, 5); }
	public bool push_on_belt_fwd() { return read(0, 7); }
    public bool push_on_belt_rvs() { return read(0, 6); }
    public bool belt_direction() { return read(1, 0); }
	public bool belt_run() { return read(1, 1); }

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
        Debug.Log("handling");
    }

    void Start()
    {
        Application.runInBackground = true;

		print("Connecting to S7ProSim ...\n");

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