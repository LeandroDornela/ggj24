using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMMainMenu : MonoBehaviour
{
    [SerializeField, Scene] private string _gameScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartNewGame()
    {
        SceneManager.LoadScene(_gameScene);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
