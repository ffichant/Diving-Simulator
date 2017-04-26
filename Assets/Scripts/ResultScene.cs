using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour {

	public GameObject textBoxError;
	public GameObject textBoxScore;
	public SceneLoading sceneLoading;
	public ScoreManager scoreManager;

	void Start ()
	{
		sceneLoading = GameObject.FindWithTag ("SceneManager").GetComponent<SceneLoading> ();
		scoreManager = GameObject.FindWithTag ("Score").GetComponent<ScoreManager> ();
		afficherScore ();
	}

	public void afficherScore()
	{
		string score = "";
		string scoreTotal = "Score Total : \t\t\t\t\t\t\t";
		string tmp = scoreManager.getAllErrorPrep();    
		score += tmp;
		tmp = scoreManager.getAllErrorDive();
		score += tmp;
		textBoxError.GetComponent<Text> ().text = score;
		textBoxScore.GetComponent<Text> ().text = scoreTotal + scoreManager.currentScore + "%";
	}

	public void loadScene(string sceneName)
	{
		sceneLoading.loadScene (sceneName);
	}
}
