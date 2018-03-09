using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public GameObject[,] gos;

    int width = 10;
    int height = 10;
    [SerializeField]
    bool turn;
    int numBlancos;

    public Text Player1;
    public Text Player2;


    int winP1 = 0;
    int winP2 = 0;

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

        Player1.text = " Victorias jugador 1: " + winP1;
        Player2.text = " Victorias jugador 2: " + winP2;





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
                        ExamArrayDiagonal(x, y);
                        ExamArrayDiagonalI(x, y);
                        turn = false;

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
                        ExamArrayDiagonal(x, y);
                        ExamArrayDiagonalI(x, y);

                        turn = true;

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
                }
            }
            else
            {
                vertical = 0;
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

                }
            }
            else
            {
                vertical = 0;
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

                }
            }
            else
            {
                horizontal = 0;
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

                }
            }
            else
            {
                horizontal = 0;
            }
        }

    }

    void ExamArrayDiagonal(int x, int y)
    {
        Color colorPrimero = gos[x, y].GetComponent<Renderer>().material.color;
        int Diagonal = 0;

        for (int i = -width; i < height; i++)
        {
            if (x+i >= 0 && y+i >= 0 && x+i < width && y+i < height)
            {

                GameObject go = gos[x + i, y + i];
                Color goColor = go.GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                    Diagonal++;
                    Ganar(turn, Diagonal);
                }

                else
                {
                    Diagonal = 0;
                }
            }
            else
            {
                Diagonal = 0;
            }
        }

    }

    void ExamArrayDiagonalI(int x, int y)
    {
        Color colorPrimero = gos[x, y].GetComponent<Renderer>().material.color;
        int Diagonal = 0;

        for (int i = -width; i < width; i++)
        {
            if (x - i >= 0 && y+i >= 0 && x - i < width && y + i < height)
            {
                Color goColor = gos[x - i, y + i].GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                    Diagonal++;
                    Ganar(turn, Diagonal);
                }
                else
                {
                    Diagonal = 0;
                }
            }

        }



    }

    void Ganar(bool turn, int count)
    {
        if (turn  == false)
        {
            if (count == 4)
            {
                CleanTable();
                winP1++;
            }

        }
        else 
        {
            if (count == 4)
            {
                CleanTable();
                winP2++;
            }
        }
    }
}

