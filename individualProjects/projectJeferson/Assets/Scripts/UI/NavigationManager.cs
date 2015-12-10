using UnityEngine;
using System.Collections;

public class NavigationManager : MonoBehaviour {

	public GameObject SEBRAE;
	public GameObject record;
	public GameObject management;
	public GameObject market;
	public GameObject team;

	/// <summary>
	/// Activate the specified button
	/// </summary>
	/// <param name="op">1 to 5 in order</param>
	public void activate(int op) {
		switch (op) {
		case 1:
			deactivate();
			SEBRAE.SetActive(true);
			break;
		case 2:
			deactivate();
			record.SetActive(true);
			break;
		case 3:
			deactivate();
			management.SetActive(true);
			break;
		case 4:
			deactivate();
			market.SetActive(true);
			break;
		case 5:
			deactivate();
			team.SetActive(true);
			break;
		}
	}

	/// <summary>
	/// Deactivate all buttons
	/// </summary>
	public void deactivate() {
		SEBRAE.SetActive (false);
		record.SetActive (false);
		management.SetActive (false);
		market.SetActive (false);
		team.SetActive (false);
	}
}
