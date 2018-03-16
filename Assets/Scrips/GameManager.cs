using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject instrucciones;

    public void CargarJuego()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void MostrarInstrucciones(bool activa)
    {
        switch (activa)
        {
            case false:
                instrucciones.SetActive(true);
                break;

            case true:
                instrucciones.SetActive(false);
                break;
        }
    }





}

