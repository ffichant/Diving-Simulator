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
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            //quit game
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif      
        }
    }

	public void loadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
	}

	public void quitGame()
	{
		Application.Quit ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif      
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