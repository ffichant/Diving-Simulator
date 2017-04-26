using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScene : MonoBehaviour {

	public GameObject textBoxError;
	public GameObject textBoxScore;
	public ScoreManager scoreManager;

	public Button BackToMenu;

	void Start (){
			scoreManager = GameObject.FindWithTag ("Score").GetComponent<ScoreManager> ();
			BackToMenu = BackToMenu.GetComponent<Button>();
			BackToMenu.onClick.AddListener(BackOnClick);
			afficherScore ();
	}

	public void afficherScore(){
			string score = "";
			string scoreTotal = "Score Total : ";
			score += scoreManager.getAllErrorPrep() + scoreManager.getAllErrorDive();
			textBoxError.GetComponent<Text> ().text = score;
			textBoxScore.GetComponent<Text> ().text = scoreTotal + scoreManager.currentScore + "%";
	}

	private void BackOnClick(){
			SceneManager.LoadScene("Menu");
	}
}
