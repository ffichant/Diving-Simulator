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

    // Structure de donnees permettant de garder une trace des pertes de points.
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

	// Resume de toutes les fautes commises par le joueur,
	// indexees par ordre croissant.
	public Dictionary<int,PointLoss> PointSummaryPrep;
	public Dictionary<int,PointLoss> PointSummaryDive;

	void Start () {
        DontDestroyOnLoad(this);
        ResetScoreManager();
	}

	// Reinitialise le Score Manager.
	public void ResetScoreManager(){

			currentScore = 100;
			PointSummaryPrep = new Dictionary<int,PointLoss>();
			PointSummaryDive = new Dictionary<int,PointLoss>();
	}

	// Fonction a appeller pour enregistrer une perte de points.
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
		string tmp = "Preparation :\n";
		foreach(KeyValuePair<int, PointLoss> error in PointSummaryPrep)
		{
			tmp += "-" +error.Value.Points + "%" +  "\t\t" + error.Value.Reason;
		}
		tmp += "\n\n";
		return tmp;
	}

	public string getAllErrorDive()
	{
		string tmp = "Plongee :\n";
		foreach(KeyValuePair<int, PointLoss> error in PointSummaryDive)
		{
			tmp += "-" +error.Value.Points + "%" +  "\t\t" + error.Value.Reason;
		}
		tmp += "\n\n";
		return tmp;
	}

}
