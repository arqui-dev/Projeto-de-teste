using UnityEngine;
using System.Collections;

public class NavigationManager : MonoBehaviour {

	public GameObject SEBRAE;
	public GameObject record;
	public GameObject management;
	public GameObject market;
	public GameObject team;
	public GameObject eutubo;

	public GameObject [] shadows;

	void Awake()
	{
		// REMOVE THIS AND CREATE IT ON THE LOAD PLACE
		PlayerData.Create();

		Deactivate();
	}

	/// <summary>
	/// Activate the specified button
	/// </summary>
	/// <param name="op">1 to 5 in order</param>
	public void Activate(int op) {

		Deactivate();
		shadows[op-1].SetActive(true);

		switch (op) {
		case 1:
			SEBRAE.SetActive(true);
			break;
		case 2:
			record.SetActive(true);
			break;
		case 3:
			management.SetActive(true);
			break;
		case 4:
			market.SetActive(true);
			break;
		case 5:
			team.SetActive(true);
			break;
		case 6:
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

		for(int i = 0; i < shadows.Length; i++)
		{
			shadows[i].SetActive(false);
		}
	}
}
