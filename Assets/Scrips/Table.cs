using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject[,] gos;

    int width = 10;
    int height = 10;
    [SerializeField]
    bool turn;
    int numBlancos;

    int winP1;
    int winP2;

    float timerReStar = 1;



    int x;
    int y;

    void Start()
    {
        numBlancos = 100;

        gos = new GameObject[10, 10];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
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
        x = (int)(mPosition.x + 0.5f);
        y = (int)(mPosition.y + 0.5f);

        TurnEnable();

        if (numBlancos == 0)
        {


            CleanTable();


        }



    }

    void TurnEnable()
    {
        if (turn == true)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject go = gos[x, y];
                    if (go.GetComponent<Renderer>().material.color == Color.white)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        Checker(go);
                        ExaminarArrayHoriz(x, y);
                        ExaminarArrayVert(x, y);
                        //turn = false;

                    }

                }
            }
        }
        else
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject go = gos[x, y];
                    if (go.GetComponent<Renderer>().material.color == Color.white)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                        Checker(go);
                        ExaminarArrayHoriz(x, y);
                        ExaminarArrayVert(x, y);
                       /// turn = true;

                    }
                }
            }
        }

    }

    void CleanTable()
    {
        for (int _x = 0; _x < width; _x++)
        {
            for (int _y = 0; _y < height; _y++)
            {

                GameObject go = gos[_x, _y];
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.white);

            }
        }
        numBlancos = 100;
    }


    void Checker(GameObject go)
    {
        if ((go.GetComponent<Renderer>().material.color == Color.white))
        {
            numBlancos++;
        }

        else
        if (go.GetComponent<Renderer>().material.color != Color.white)
        {
            numBlancos--;
        }

    }


    void ExaminarArrayVert(int x, int y)
    {
        int tempoX = x;
        int y1 = 0;
        int tempoY = y;
        
        int vertical = 0;

        GameObject go = gos[x, y];
        Color colorPrimero = go.GetComponent<Renderer>().material.color;


        for (tempoY = y; tempoY > y - 4; tempoY--)
        {
            if (tempoX >= 0 && tempoY >= 0 && tempoX < width && tempoY < height)
            {
                go = gos[tempoX, tempoY];
                Color goColor = go.GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                    vertical++;
                    Ganar(turn, vertical);
                    y1 = tempoY;
                }

                else
                {
                    vertical = 0;
                    continue;
                }
            }
            else
            {
                continue;
            }
        }

        for (tempoY = y1; tempoY < y + 4; tempoY++)
        {
            if (tempoX >= 0 && tempoY >= 0 && tempoX < width && tempoY < height)
            {
                go = gos[tempoX, tempoY];

                Color goColor = go.GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                    vertical++;
                    Ganar(turn, vertical);
                }

                else
                {
                    vertical = 0;
                    continue;
                }
            }
            else
            {
                continue;
            }
        }


    }

    void ExaminarArrayHoriz(int x, int y)
    {
        int tempoX = x;
        int x1 = 0;
        int tempoY = y;
       
        int horizontal = 0;
       

        GameObject go = gos[x, y];
        Color colorPrimero = go.GetComponent<Renderer>().material.color;
       
        for (tempoX = x; tempoX > x - 4; tempoX--)
        {
            if (tempoX >= 0 && tempoY >= 0 && tempoX < width && tempoY < height)
            {
                go = gos[tempoX, tempoY];
                Color goColor = go.GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                     x1 = tempoX;
                    horizontal++;
                    Ganar(turn, horizontal);
                }

                else
                {
                    horizontal = 0;
                    continue;
                }
            }
            else
            {
                continue;
            }
        }

        for (tempoX = x1; tempoX < x + 4; tempoX++)
        {
            if (tempoX >= 0 && tempoY >= 0 && tempoX < width && tempoY < height)
            {
                go = gos[tempoX, tempoY];

                Color goColor = go.GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                    horizontal++;
                    Ganar(turn, horizontal);
                  
                }

                else
                {
                    horizontal = 0;
                    continue;
                }
            }
            else
            {
                continue;
            }
        }
       


    }





    void Ganar(bool turn, int count)
    {
        if (count == 4)
        {
            CleanTable();
        }



    }

}

