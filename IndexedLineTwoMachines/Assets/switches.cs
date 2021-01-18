using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class switches : MonoBehaviour {

	public Transform prefab;

    public Transform foto1a, foto1b, foto2a, foto2b, foto3a, foto3b, foto4a, foto4b, foto5a, foto5b;

    public Transform dropDown0;
    public Transform dropDown1;
    public Transform dropDown2;
    public Transform dropDown3;
    public Transform dropDown4;


    Communication com;
	MoveCamera cameraClass;

    public void newPart()
    {
        prefab.tag = "Player";
        Instantiate(prefab , new Vector3(-2.05f, 8, -0.3f), Quaternion.identity);
        prefab.tag = "Untagged";
    }


    public void removePart()
    {
		GameObject [] ply = GameObject.FindGameObjectsWithTag("Player");
		if (ply != null && ply.Length > 0) {
			GameObject.Destroy(ply[0]);
		}
    }

    public void reset()
    {
		Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }



    // Use this for initialization
    void Start()
    {
        com = GameObject.Find("Communication").GetComponent<Communication>();
    }

    // Update is called once per frame
    void Update () {

        bool f1=false, f2=false, f3=false, f4=false, f5=false;

        GameObject[] pucks = GameObject.FindGameObjectsWithTag("Player");

        
        for (int i=0; i < pucks.Length; i++)
        {
			if (pucks[i].GetComponent<Renderer>().bounds.Intersects(foto1a.GetComponent<Renderer>().bounds) && pucks[i].GetComponent<Renderer>().bounds.Intersects(foto1b.GetComponent<Renderer>().bounds)){
                f1 = true;
            }
			if (pucks[i].GetComponent<Renderer>().bounds.Intersects(foto2a.GetComponent<Renderer>().bounds) && pucks[i].GetComponent<Renderer>().bounds.Intersects(foto2b.GetComponent<Renderer>().bounds)){
                f2 = true;
            }
			if (pucks[i].GetComponent<Renderer>().bounds.Intersects(foto3a.GetComponent<Renderer>().bounds) && pucks[i].GetComponent<Renderer>().bounds.Intersects(foto3b.GetComponent<Renderer>().bounds)){
                f3 = true;
            }
			if (pucks[i].GetComponent<Renderer>().bounds.Intersects(foto4a.GetComponent<Renderer>().bounds) && pucks[i].GetComponent<Renderer>().bounds.Intersects(foto4b.GetComponent<Renderer>().bounds)){
                f4 = true;
            }
			if (pucks[i].GetComponent<Renderer>().bounds.Intersects(foto5a.GetComponent<Renderer>().bounds) && pucks[i].GetComponent<Renderer>().bounds.Intersects(foto5b.GetComponent<Renderer>().bounds))
            {
                f5 = true;
            }
        }

        if (dropDown0.GetComponent<Dropdown>().value == 0)
            com.foto_1(!f1);
        else
            com.foto_1(dropDown0.GetComponent<Dropdown>().value == 2);

        if (dropDown1.GetComponent<Dropdown>().value == 0)
            com.foto_2(!f2);
        else
            com.foto_2(dropDown1.GetComponent<Dropdown>().value == 2);

        if (dropDown2.GetComponent<Dropdown>().value == 0)
            com.foto_3(!f3);
        else
            com.foto_3(dropDown2.GetComponent<Dropdown>().value == 2);

        if (dropDown3.GetComponent<Dropdown>().value == 0)
            com.foto_4(!f4);
        else
            com.foto_4(dropDown3.GetComponent<Dropdown>().value == 2);

        if (dropDown4.GetComponent<Dropdown>().value == 0)
            com.foto_5(!f5);
        else
            com.foto_5(dropDown4.GetComponent<Dropdown>().value == 2);

    }
}
