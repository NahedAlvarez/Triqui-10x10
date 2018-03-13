using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    //Se crea una matriz dimensional en la cual se almacenara el tablero
    public GameObject[,] gos;
    //se definen dos variable para la cantidad de fichas del ancho y del largo que habra en el tablero 
    int width = 10;
    int height = 10;
    //un contador de fichas blancas 
    int numBlancos;
    //un booleano que controla los turnos 
    [SerializeField]
    bool turn;
    //dos variables de tipo Text para modificarlo 
    public Text Player1;
    public Text Player2;
    // numero de victorias de jugador1 y el 2 
    int winP1 = 0;
    int winP2 = 0;
    //bool que controlan los powerUps 
    bool powerUp1;
    bool powerUp2;
    //variable x,y que toma la posicion 
    int x;
    int y;

    void Start()
    {
        //se inicializa  las fichas blancas 
        numBlancos = 100;
        // se inicializa la matriz en 10x10
        gos = new GameObject[width, height];
        //se inicia  dos for anidados para crear el tablero 
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //crea la primitiva en un gameObject llmado go
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //se le da una ubicacion dependiendo de i y la j 
                go.transform.position = new Vector3(i, j, 0);
                //se le cambia el nombre a la posicion en i y j 
                go.name = i.ToString() + " " + j.ToString();
                //se  agrega a la matriz dependiendo de su i y j de los for 
                gos[i, j] = go;
            }
        }
    }


    void Update()
    {
        //se obtiene la posicion del mouse
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Se hace un cast para la posicion x del mouse sin la parte decimal 
        x = (int)(mPosition.x + 0.5f);
        //Se hace un cast para la posicion y del mouse sin la parte decimal
        y = (int)(mPosition.y + 0.5f);
        // Se llama al metodo TurnEnable que se encarga de los turnos de ambos jugadores
        TurnEnable();
        //se crea un codicional para reiniciar el tablero en caso de que se llene 
        if (numBlancos == 0)
        {
            //Se borra el tablero con este metodo 
            CleanTable();
        }
        //Se cambia el texto de las victorias en la escena 
        Player1.text = " Victorias jugador 1: " + winP1;
        Player2.text = " Victorias jugador 2: " + winP2;



    }

    void TurnEnable()
    {
        //se crea el condicional del player rojo 
        if (turn == true)
        {
            //Se crea un condicinal para que nunca se salga del array
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                //se le pregunta que si preciona el boton izquierdo del mouse 
                if (Input.GetMouseButtonDown(0))
                {
                    //se llama al objeto en la posicion del mouse en x y en y
                    GameObject go = gos[x, y];
                    //se le dice que si el objeto tiene un color blanco 
                    if (go.GetComponent<Renderer>().material.color == Color.white  )
                    {
                        //cambie el color a rojo
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        //se llama al metodo cheker que tiene como argumento un GameObject 
                        Checker(go);
                        //se llama al metodo de examinar array y se le envian los argumentos de tipo int x y que son los 
                        //que dan la posicion de la ficha en el tablero
                        ExamArray(x, y);
                        //se vuelve el turno falso para que cambie de turno
                        turn = false;
                    }
                    //Si tiene el power Up activo 
                    else if (go.GetComponent<Renderer>().material.color == Color.blue && powerUp2 == true)
                    {
                        //puede cambiar una ficha del adversario al color rojo 
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        //se llama al metodo cheker que tiene como argumento un GameObject 
                        Checker(go);
                        //se llama al metodo de examinar array y se le envian los argumentos de tipo int x y que son los 
                        //que dan la posicion de la ficha en el tablero
                        ExamArray(x, y);
                        //se vuelve el turno falso para que cambie de turno
                        turn = false;
                        //se gasta el powerUp una vez que se use 
                        powerUp2 = false;
                    }
                }
            }
        }
        else
        {
            //Se crea un condicinal para que nunca se salga del array
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                //se le pregunta que si preciona el boton izquierdo del mouse 
                if (Input.GetMouseButtonDown(0))
                {
                    //se llama al objeto en la posicion del mouse en x y en y
                    GameObject go = gos[x, y];
                    //se le dice que si el objeto tiene un color blanco 
                    if (go.GetComponent<Renderer>().material.color == Color.white)
                    {
                        //puede cambiar una ficha del adversario al color rojo 
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                        //se llama al metodo cheker que tiene como argumento un GameObject 
                        Checker(go);
                        //se llama al metodo de examinar array y se le envian los argumentos de tipo int x y que son los 
                        //que dan la posicion de la ficha en el tablero
                        ExamArray(x, y);
                        //se vuelve el turno falso para que cambie de turno
                        turn = true;
                    }
                    //Si tiene el power Up activo 
                    else if (go.GetComponent<Renderer>().material.color == Color.red && powerUp1 == true)
                    {
                        //puede cambiar una ficha del adversario al color azul 
                        go.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

                        Checker(go);
                        //se llama al metodo de examinar array y se le envian los argumentos de tipo int x y que son los 
                        //que dan la posicion de la ficha en el tablero
                        ExamArray(x, y);
                        //se vuelve el turno falso para que cambie de turno
                        turn = true;
                        //Se desactiva el powerUp 
                        powerUp1 = false;
                    }
                }
            }
        }
    }
    //este metodo se usa para limpiar el tablero 
    void CleanTable()
    {
        //se utiliza dos for anidados al igual que en el momento de crear las fichas 
        for (int _x = 0; _x < width; _x++)
        {
            for (int _y = 0; _y < height; _y++)
            {
                //go =  se pasa por todos los miembros de la matriz 
                GameObject go = gos[_x, _y];
                //Se igualan a color blanco
                go.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
        }
        //las fichas blancas vuelven a ser 0
        numBlancos = 100;
    }

    //Es el metodo que se encarga de regular las fichas blancas 
    void Checker(GameObject go)
    {
        //si una ficha es de color blanco 
        if (go.GetComponent<Renderer>().material.color == Color.white)
        {
            //los numeros blancos 
            numBlancos++;
        }
        //si es diferente a color white 
        else if (go.GetComponent<Renderer>().material.color != Color.white)
        {
            //si son de un color diferente 
            numBlancos--;
        }
    }

    //El metodo ExamArray requiere dos argumento int 
    void ExamArray(int x, int y)
    {
        //se crea un contador en vertical 
        int vertical = 4;
        //se crea un contador en horizontal 
        int horizontal = 4;
        //se crea un contador en Diagonal
        int Diagonal = 4;
        //se crea un contador para diagonal inverso 
        int DiagonalI = 4;
        //se crea una variable de color que va almacenar el color de el cuadro en el cual se dio click
        Color colorPrimero = gos[x, y].GetComponent<Renderer>().material.color;
        //el para va ir de -4 hasta que sea mayor a 4 para acceder a las 4 fichas anteriores y superiores  
        for (int i = -4; i < 4; i++)
        {
            //Se crea un condicional para limitar el acceso 
            if (x >= 0 && y + i >= 0 && x < width && y + i < height)
            {
                // se crea una variable de tipo color 
                Color goColor = gos[x, y + i].GetComponent<Renderer>().material.color;
                //se compara el primer color  con el color que se toma y+1
                if (colorPrimero == goColor)
                {
                //vertical se va a disminuir 1 
                    vertical--;
                    //vertical = a 0
                    if (vertical == 0)
                    {
                        //se identifica el turno y se la victoria 
                        if (turn == false)
                        {
                            winP1++;
                        }
                        else if (turn == true)
                        {
                            winP2++;
                        }
                        //se analiza si alguno de los jugadores tiene 2 victorias mas que las victorias 
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


