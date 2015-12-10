using UnityEngine;
using System.Collections;

public class NavigationManager : MonoBehaviour {

	public GameObject SEBRAE;
	public GameObject record;
	public GameObject management;
	public GameObject market;
	public GameObject team;
	public GameObject eutubo;

	void Awake()
	{
		Deactivate();
	}

	/// <summary>
	/// Activate the specified button
	/// </summary>
	/// <param name="op">1 to 5 in order</param>
	public void Activate(int op) {
		switch (op) {
		case 1:
			Deactivate();
			SEBRAE.SetActive(true);
			break;
		case 2:
			Deactivate();
			record.SetActive(true);
			break;
		case 3:
			Deactivate();
			management.SetActive(true);
			break;
		case 4:
			Deactivate();
			market.SetActive(true);
			break;
		case 5:
			Deactivate();
			team.SetActive(true);
			break;
		case 6:
			Deactivate();
			eutubo.SetActive(true);
			break;
		}
	}

	/// <summary>
	/// Deactivate all buttons
	/// </summary>
	public void Deactivate() {
		SEBRAE.SetActive (false);
		record.SetActive (false);
		management.SetActive (false);
		market.SetActive (false);
		team.SetActive (false);
		eutubo.SetActive(false);
	}
}
