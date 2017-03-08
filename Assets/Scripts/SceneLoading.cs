using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SceneLoading : MonoBehaviour {

    public int loadedSceneID = 0;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);//Has to remain between scenes
    }

    // Update is called once per frame
    void Update()
    {
        //Gérer les inputs du joueur
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Sélection de la scène 1");
            unloadCurrentScene();
            loadedSceneID = 1;
            SceneManager.LoadScene("OnBoat", LoadSceneMode.Additive);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Sélection de la scène 2");
            unloadCurrentScene();
            loadedSceneID = 2;
            SceneManager.LoadScene("Underwater", LoadSceneMode.Additive);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Sélection de la scène 3");
            unloadCurrentScene();
            loadedSceneID = 3;
            SceneManager.LoadScene("Score", LoadSceneMode.Additive);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            //quit game
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif      
        }
    }

    void unloadCurrentScene()
    {
        if (loadedSceneID == 1)
        {
            SceneManager.UnloadSceneAsync("OnBoat");
            loadedSceneID = 0;
        }
        else if (loadedSceneID == 2)
        {
            SceneManager.UnloadSceneAsync("Underwater");
            loadedSceneID = 0;
        }
        else if (loadedSceneID == 3)
        {
            SceneManager.UnloadSceneAsync("Score");
            loadedSceneID = 0;
        }
    }
}