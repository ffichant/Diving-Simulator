﻿using System.Collections;
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
	public Dictionary<int,PointLoss> PointSummaryPrep;
	public Dictionary<int,PointLoss> PointSummaryDive;

	void Start () {
		
	}

	// Réinitialise le Score Manager.
	public void ResetScoreManager(){

			currentScore = 100;
			PointSummaryPrep = new Dictionary<int,PointLoss>();
			PointSummaryDive = new Dictionary<int,PointLoss>();
	}

	// Fonction à appeller pour enregistrer une perte de points.
	// Modifie le score et enregistre la perte de points dans le dictionnaire.
	public void RegisterLossOfPointsPrep(int pts, string reason){

			currentScore -= pts;

			PointLoss record = new PointLoss(pts, reason);
			PointSummaryPrep.Add(PointSummaryPrep.Count, record);
	}

	public void RegisterLossOfPointsDive(int pts, string reason){

		currentScore -= pts;

		PointLoss record = new PointLoss(pts, reason);
		PointSummaryDive.Add(PointSummaryDive.Count, record);
	}

	public string getAllErrorPrep()
	{
		string tmp = "Préparation :\n";
		foreach(KeyValuePair<int, PointLoss> error in PointSummaryPrep)
		{
			tmp += "-" +error.Value.Points + "%" +  "\t\t" + error.Value.Reason;
		}
		tmp += "\n\n";
		return tmp;
	}

	public string getAllErrorDive()
	{
		string tmp = "Plongée :\n";
		foreach(KeyValuePair<int, PointLoss> error in PointSummaryPrep)
		{
			tmp += "-" +error.Value.Points + "%" +  "\t\t" + error.Value.Reason;
		}
		tmp += "\n\n";
		return tmp;
	}

}
>>>>>>> 999bbf216c8ef03952a395be0a4aa4fa92a284b8
