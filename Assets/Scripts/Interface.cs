﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
	private int score;
	private int numEnemies;
	private int numLives;
	private bool lost;
	private bool won;

	public int startLives = 3;
	public Shooter shooter;
	public Text ScoreText, EnemiesText, MagazineText, LivesText, wonText, lostText;

	void Start () {
		numEnemies = 0;
		score = 0;
		numLives = 3;
		lost = false;
		won = false;
	}

	void Update () {
		//set the text to a '|' for each bullet you can shoot
		int magLeft = shooter.getMagazineLeft ();
		if (magLeft > 0) {
			MagazineText.text = new string ('|', magLeft);
		} else {
			MagazineText.text = "Press R to reload";
		}

		ScoreText.text = score.ToString ();
		EnemiesText.text = numEnemies.ToString ();
		LivesText.text = new string ('❤', numLives);

		if (won) {
			wonText.enabled = true;
		} else if (lost) {
			lostText.enabled = true;
		}
	}

	public void OnEnemyKill(int scoreAmount, bool playerKill) {
		score += scoreAmount;
		numEnemies -= 1;

		if (numEnemies == 0 && playerKill) {
			won = true;
			stopGame ();
		}
	}

	public void OnEnemySpawn() {
		if (!won && !lost) {
			numEnemies += 1;
		}
	}

	public void OnPlayerDeath() {
		if (!won && !lost) {
			numLives -= 1;
			if (numLives == 0) {
				lost = true;
				stopGame ();
			}
		}
	}

	private void stopGame() {
		MonoBehaviour[] components = shooter.GetComponentsInParent<MonoBehaviour>();
		foreach (Component c in components) {
			Destroy (c);
		}
	}
}
