using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	/*	Pour enregistrer une perte de point, appeller:
			RegisterLossOfPoints(int Nombre de points perdus, string Raison);

			Pour lister toutes les pertes de points (Valeur ET Raison):
			foreach (KeyValuePair<int, PointLoss> entry in PointSummary) {
					Debug.Log(entry.Value.Points);
					Debug.Log(entry.Value.Reason);
			}
	*/

	// Structure de données permettant de garder une trace des pertes de points.
	public struct PointLoss {
			public int Points;
			public string Reason;

			public PointLoss(int pts, string reason){
					Points = pts;
					Reason = reason;
			}
	}

	// Score actuel du joueur.
	public int currentScore;

	// Résumé de toutes les fautes commises par le joueur,
	// indéxées par ordre croissant.
	public Dictionary<int,PointLoss> PointSummary;

	void Start () {

		  DontDestroyOnLoad(this);
			ResetScoreManager();
	}

	// Réinitialise le Score Manager.
	public void ResetScoreManager(){

			currentScore = 100;
			PointSummary = new Dictionary<int,PointLoss>();
	}

	// Fonction à appeller pour enregistrer une perte de points.
	// Modifie le score et enregistre la perte de points dans le dictionnaire.
	public void RegisterLossOfPoints(int pts, string reason){

			currentScore -= pts;
			if (currentScore<0) currentScore = 0;

			PointLoss record = new PointLoss(pts, reason);
			PointSummary.Add(PointSummary.Count, record);
			Debug.Log(currentScore);
	}
}
