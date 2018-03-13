using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public GameObject[,] gos;

    int width = 10;
    int height = 10;

    int numBlancos;

    [SerializeField]
    bool turn;

    public Text Player1;
    public Text Player2;

    int winP1 = 0;
    int winP2 = 0;

    bool powerUp1;
    bool activador1;
    bool powerUp2;
    bool activador2;

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
                    if (go.GetComponent<Renderer>().material.color == Color.white  )
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        Checker(go);
                        ExamArray(x, y);
                        turn = false;
                    }

                    else if (go.GetComponent<Renderer>().material.color == Color.blue && powerUp2 == true)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        Checker(go);
                        ExamArray(x, y);
                        turn = false;
                        powerUp2 = false;
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
                        ExamArray(x, y);
                        turn = true;
                    }
                    else if(go.GetComponent<Renderer>().material.color == Color.red && powerUp1 == true)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                        Checker(go);
                        ExamArray(x, y);
                        turn = true;
                        powerUp1 = false;
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
        if (go.GetComponent<Renderer>().material.color == Color.white)
        {
            numBlancos++;
        }

        else if (go.GetComponent<Renderer>().material.color != Color.white)
        {
            numBlancos--;
        }
    }


    void ExamArray(int x, int y)
    {
        int vertical = 4;
        int horizontal = 4;
        int Diagonal = 4;
        int DiagonalI = 4;

        Color colorPrimero = gos[x, y].GetComponent<Renderer>().material.color;

        for (int i = -4; i < 4; i++)
        {
            if (x >= 0 && y + i >= 0 && x < width && y + i < height)
            {
                Color goColor = gos[x, y + i].GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                
                    vertical--;

                    if (vertical == 0)
                    {
                        if (turn == false)
                        {
                            winP1++;
                        }
                        else if (turn == true)
                        {
                            winP2++;
                        }

                        if (winP1 - 2 >= winP2)
                        {
                            powerUp1 = true;
                        }

                        if (winP2 - 2 >= winP1)
                        {
                            powerUp2 = true;
                        }

                        CleanTable();
                        break;
                    }
                }

                else
                {
                    vertical = 4;
                }
            }
            else
            {
                vertical = 4;
            }

            if (x + i >= 0 && y >= 0 && x + i < width && y < height)
            {
                GameObject go = gos[x + i, y];
                Color goColor = go.GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                    horizontal--;

                    if (horizontal == 0)
                    {
                        if (turn == false)
                        {
                            winP1++;
                        }
                        else if (turn == true)
                        {
                            winP2++;
                        }

                        if (winP1 - 2 >= winP2)
                        {
                            powerUp1 = true;
                        }

                        if (winP2 - 2 >= winP1)
                        {
                            powerUp2 = true;
                        }

                        CleanTable();
                        break;
                    }

                }

                else
                {
                    horizontal = 4;

                }
            }

            else
            {
                horizontal = 4;
            }


            if (x + i >= 0 && y + i >= 0 && x + i < width && y + i < height)
            {
                GameObject go = gos[x + i, y + i];
                Color goColor = go.GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                    Diagonal--;

                    if (Diagonal == 0)
                    {
                        if (turn == false)
                        {
                            winP1++;
                        }
                        else if (turn == true)
                        {
                            winP2++;
                        }

                        if (winP1 - 2 >= winP2)
                        {
                            powerUp1 = true;
                        }

                        if (winP2 - 2 >= winP1)
                        {
                            powerUp2 = true;
                        }

                        CleanTable();
                        break;
                    }
                }

                else
                {
                    Diagonal = 4;
                }
            }

            else
            {
                Diagonal = 4;
            }


            if (x - i >= 0 && y + i >= 0 && x - i < width && y + i < height)
            {
                Color goColor = gos[x - i, y + i].GetComponent<Renderer>().material.color;

                if (colorPrimero == goColor)
                {
                    DiagonalI--;

                    if (DiagonalI == 0)
                    {
                        if (turn == false)
                        {
                            winP1++;
                        }
                        else if (turn == true)
                        {
                            winP2++;
                        }

                        if (winP1 - 2 >= winP2)
                        {
                            powerUp1 = true;
                        }

                        if (winP2 - 2 >= winP1)
                        {
                            powerUp2 = true;
                        }

                        CleanTable();
                        break;
                    }
                }

                else
                {
                    DiagonalI = 4;
                }
            }
            else
            {
                DiagonalI = 4;
            }
        }
    }
}


