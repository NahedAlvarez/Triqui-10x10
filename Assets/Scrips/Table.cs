using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject[,] gos;

    int width = 10;
    int height = 10;

    void Start()
    {
        gos = new GameObject[10, 10];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j  < height; j++)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = new Vector3(i, j, 0);
                go.name = i.ToString() + " " + j.ToString();
                gos[i, j] = go;
             
                
            }
        }	
	}


    void Update()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)(mPosition.x + 0.5f);
        int y = (int)(mPosition.y + 0.5f);

        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject go = gos[x, y];
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
           

        }

        for (int _x = 0; _x < width; _x++)
        {
            for(int _y = 0; _y < height; _y++)
            {

                if ( x >= 0 && y >= 0 && x < width && y < height)
                {
                    GameObject go = gos[x, y];
                    if (go.GetComponent<Renderer>().material.color != Color.red)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    }
                }
               


                
               
                
                    
            }
        }



        

    }
}
