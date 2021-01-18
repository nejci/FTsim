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
    public void foto_1(bool val) { write(0, 0, val); }
    public void foto_2(bool val) { write(0, 1, val); }
    public void final_A_fwd(bool val) { write(0, 2, val); }
    public void final_A_rvs(bool val) { write(0, 3, val); }

    public void foto_3(bool val) { write(0, 4, val); }
    public void foto_4(bool val) { write(0, 5, val); }
    public void final_B_rvs(bool val) { write(0, 6, val); }
    public void final_B_fwd(bool val) { write(0, 7, val); }

    public void foto_5(bool val) { write(1, 0, val); }


    //buttons
    public void key(bool val) { write(1, 2, val); }
    public void green(bool val) { write(1, 3, val); }
    public void black_l_u(bool val) { write(1, 4, val); }
    public void black_l_d(bool val) { write(1, 5, val); }
    public void black_r_u(bool val) { write(1, 6, val); }
    public void black_r_d(bool val) { write(1, 7, val); }


    //inputs
    public bool push_A_dir() { return read(0, 0); }
	public bool push_A_run() { return read(0, 1); }

    public bool belt_direction(int id) {
        if(id == 0)
			return read(0, 2);
        if (id == 1)
            return read(0, 4);
        if (id == 2)
            return read(1, 2);
        else
			return read(1, 6);
    }

    public bool belt_run(int id)
    {
		if (id == 0)
            return read(0, 3);
        if (id == 1)
            return read(0, 5);
        if (id == 2)
            return read(1, 3);
        else
            return read(1, 7);
    }

    public bool drill_A_dir() { return read(0, 6); }
    public bool drill_A_run() { return read(0, 7); }

    public bool drill_B_dir() { return read(1, 0); }
	public bool drill_B_run() { return read(1, 1); }

	public bool push_B_dir() { return read(1, 4); }
	public bool push_B_run() { return read(1, 5); }




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