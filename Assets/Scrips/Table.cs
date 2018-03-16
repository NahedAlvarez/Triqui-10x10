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
                        //se llama al metodo limpiar el tablero 
                        CleanTable();
                        //se  rompe el for para que no siga buscando victorias
                        break;
                    }
                }

                else
                {
                    //si el color es diferente entonces que el contador vuelva a ser 
                    vertical = 4;
                }
            }
            else
            {
                //si se sale del array entonces que el contador vuelva a ser 
                vertical = 4;
            }
            //Se crea un condicional para limitar el acceso al array 
            if (x + i >= 0 && y >= 0 && x + i < width && y < height)
            {
                // se crea una variable de tipo GameObject que almacena el objeto qeu se toma del array 
                GameObject go = gos[x + i, y];
                //Se crea una variable de tipo color de la cual se toma el color del objeto almacenado 
                Color goColor = go.GetComponent<Renderer>().material.color;
                //se compara el color del objeto almacenado con el color del primer objeto que se tomo 
                if (colorPrimero == goColor)
                {
                    //si es igual se le resta al contador 
                    horizontal--;
                    //si el contador horizontal llega a 0
                    if (horizontal == 0)
                    {
                        //se verifica el turno para sumar la victoria
                        if (turn == false)
                        {
                            winP1++;
                        }
                        else if (turn == true)
                        {
                            winP2++;
                        }
                        //se verifica si el jugador acumulo 2 victorias sobre las derrotas del oponente para activar su powerUp

                        if (winP1 - 2 >= winP2)
                        {
                            powerUp1 = true;
                        }

                        if (winP2 - 2 >= winP1)
                        {
                            powerUp2 = true;
                        }
                        //se limpia el tablero 
                        CleanTable();
                        //Se rompe el ciclo para que no tenga en cuenta victorias de otro tipo
                        break;
                    }

                }

                else
                {
                    //si el color es diferente entonce el contador vuelve a ser 0
                    horizontal = 4;

                }
            }

            else
            {
                //si el color es diferente entonce el contador vuelve a ser 0
                horizontal = 4;
            }

            //Se crea un condicional para limitar el acceso al array  
            if (x + i >= 0 && y + i >= 0 && x + i < width && y + i < height)
            {
                //se buscan los datos de el objeto almacenado en la matriz y se almacena en un gameObject 
                GameObject go = gos[x + i, y + i];
                //se toma el coloor del objeto almacenado 
                Color goColor = go.GetComponent<Renderer>().material.color;
                //se compara el color 
                if (colorPrimero == goColor)
                {
                    //Se le resta a diagonal 
                    Diagonal--;
                    //si el condicional es igual a 0 
                    if (Diagonal == 0)
                    {
                        //se verifica en que turno se esta para darle la victoria 
                        if (turn == false)
                        {
                            winP1++;
                        }
                        else if (turn == true)
                        {
                            winP2++;
                        }
                        //Se verifica si va ganando por 2 o mas victorias para activar su powerUp
                        if (winP1 - 2 >= winP2)
                        {
                            powerUp1 = true;
                        }

                        if (winP2 - 2 >= winP1)
                        {
                            powerUp2 = true;
                        }
                        //se limpia el tablero 
                        CleanTable();
                        //Se rompe el for para no tener mas victorias de otras formas
                        break;
                    }
                }

                else
                {
                    //si es de un color diferente entonces se restablece el contador 
                    Diagonal = 4;
                }
            }

            else
            {
                //si se sale del array 
                Diagonal = 4;
            }

            //se crea un condicional para que no se salga del tamaño del array 
            if (x - i >= 0 && y + i >= 0 && x - i < width && y + i < height)
            {
                //se crea una variable de tipo Color qeu almacena el color de las piezas 
                Color goColor = gos[x - i, y + i].GetComponent<Renderer>().material.color;
                //se compara con el color que se toma primero de gameObject 
                if (colorPrimero == goColor)
                {
                    //si el color es igaul se le resta al contador 
                    DiagonalI--;
                    //si el contador es igual a 0
                    if (DiagonalI == 0)
                    {
                        //se verifica el turno para atribuirle la victoria al que este en este turno 
                        if (turn == false)
                        {
                            winP1++;
                        }
                        else if (turn == true)
                        {
                            winP2++;
                        }
                        //Se verifica la cantidad de victorias para saber si hay que dar el power UP 
                        if (winP1 - 2 >= winP2)
                        {
                            //se activa el powerUp
                            powerUp1 = true;
                        }

                        if (winP2 - 2 >= winP1)
                        {
                            //Se Activa el power Up
                            powerUp2 = true;
                        }
                        //SE limpia el tablero 
                        CleanTable();
                        //Se  sale del bucle para no generar mas victorias 
                        break;
                    }
                }

                else
                {
                    //se les devuelve el valor inicial al contador 
                    DiagonalI = 4;
                }
            }
            else
            {
                //Se le devuelve el valor inicial al contador 
                DiagonalI = 4;
            }
        }
    }
}


