using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 //Declaracion del Singleton
 private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) Debug.Log("No hay GameManager");
            return instance;
        }

    }
    //Fin del singleton

    //Declaramos cualquier valor general en public
    public int playerHeatlh;
    public float maxHealth = 100;
    public int playerPoints;
    public int winPoints;
    public GameObject winPortal;

    private void Awake()
    {
        if (instance == null)
        {
            //Si no hay Gamemanager lo referenciamos y hacemos que perdure entre escenas
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Si ya hay  GameManager, el duplicado se destruye
            Destroy(gameObject);
        }
    }
        private void Update()
    {
       if (playerHeatlh < 0) playerHeatlh = 0;
       if (playerPoints >= winPoints)
        {
            winPortal.SetActive(true);
        }

    }
       
    
    //Sistema de puntos
    public void PointsUp(int gain)
    {
        playerPoints += gain;
    }

}

