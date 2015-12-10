using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script manage the production mechanic: the production on the scenery, new productions, what happens when a production is over.
/// </summary>
public class ProductionManager : MonoBehaviour
{
	//###########################################################
	// Public attributes

	/// <summary>
	/// Transform that contains child transforms positioned on the possible random places to instantiate the attribute objects.
	/// </summary>
	public Transform randomPlacesToInstantiateAttributesContainer;

	/// <summary>
	/// Transform that contains child transforms positioned on the possible random places to instantiate the production objects.
	/// </summary>
	public Transform randomPlacesToInstantiateProductionContainer;

	/// <summary> A production object to be instantiated. </summary>
	public GameObject objectProduction;

	public GameObject statusScreen;

	/// <summary>
	/// Attribute objects, in the order: Content, Quality and Innovation.
	/// </summary>
	public GameObject [] objectAttribute;

	/// <summary> Array with the folder objects. </summary>
	public FolderObject [] folders;

	/// <summary> Button start recording. </summary>
	public Button btnRecord;

	/// <summary> Timer bar to show the player the remain. </summary>
	public Image objTimerBar;

	/// <summary> Time the player has to finish it's recording. </summary>
	public float totalRecordingTime = 60;

	// COMMENT
	public Text txtVideoScore;

	public Text [] skillStatus;


	//###########################################################
	// Private attributes

	/// <summary> Random places to instantiate attribute objects. </summary>
	Transform [] randomPlacesToInstantiateAttributes;

	/// <summary> Random places to instantiate production objects. </summary>
	Transform [] randomPlacesToInstantiateProductions;

	/// <summary> Time limit to record. </summary>
	float finishRecordingTime = 0;

	/// <summary> The game running state. </summary>
	bool recording = false;

	/// <summary> Time is over. </summary>
	bool finished = false;

	//###########################################################
	// Public methods

	// COMMENT
	public void Discard()
	{
		Debug.Log ("The video was discarded.");
	}

	public void Release()
	{
		Debug.Log ("The video was released.");
	}

	/// <summary>
	/// Instantiate attribute objects.
	/// </summary>
	/// <param name="ammount">Ammount of objects created.</param>
	public void CreateAttributes()
	{
		int size = randomPlacesToInstantiateAttributes.Length;
		List<int> positions = new List<int>();
		for (int i = 0; i < size; i++)
		{
			positions.Add(i);
		}

		for(int i = 0; i < PlayerData.totalSkillTypes; i++)
		{
			int index = Random.Range(0, positions.Count);
			int position = positions[index];
			positions.RemoveAt(index);

			CreateAttribute(i, position);
		}
	}

	/// <summary>
	/// Create a production object in a random place inside the screen.
	/// </summary>
	public void CreateProduction()
	{
		if (objectProduction == null)
		{
			Debug.LogError("Need to assign objectProduction.");
			return;
		}

		if (randomPlacesToInstantiateProductions.Length <= 0)
		{
			Debug.LogError("No random places to instantiate productions.");
			return;
		}

		int index = Random.Range(
			0, randomPlacesToInstantiateProductions.Length);

		GameObject objNewProduction = Instantiate<GameObject>(objectProduction);

		objNewProduction.transform.SetParent(this.transform, false);

		objNewProduction.transform.position = 
		    randomPlacesToInstantiateProductions[index].position;
	}

	/// <summary>
	/// Verify all folders to see if an attribute object was dropped inside a corresponding folder.
	/// </summary>
	/// <returns><c>true</c>, if dropped the attribute on a corresponding folder, <c>false</c> otherwise.</returns>
	/// <param name="attributeObject">Attribute object.</param>
	public bool VerifyDropAttributeInFolder(
		AttributeObject attributeObject)
	{
		foreach(FolderObject folder in folders)
		{
			if (folder.VerifyInsideFolder(attributeObject))
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Starts the game, creating the first production.
	/// </summary>
	public void StartRecording()
	{
		if (recording == false && finished == false)
		{
			btnRecord.gameObject.SetActive(false);
			finishRecordingTime = Time.time + totalRecordingTime;
			recording = true;

			CreateProduction();
		}
	}

	//###########################################################
	// Initialization
	void Awake()
	{
		int attrs = randomPlacesToInstantiateAttributesContainer.childCount;
		randomPlacesToInstantiateAttributes = new Transform[attrs];
		for (int i = 0; i < attrs; i++)
		{
			randomPlacesToInstantiateAttributes[i] = 
				randomPlacesToInstantiateAttributesContainer.GetChild(i);
		}

		int prods = randomPlacesToInstantiateProductionContainer.childCount;
		randomPlacesToInstantiateProductions = new Transform[prods];
		for (int i = 0; i < prods; i++)
		{
			randomPlacesToInstantiateProductions[i] = 
				randomPlacesToInstantiateProductionContainer.GetChild(i);
		}

		PlayerData.Create();
	}
	/// <summary>
	/// Enable the record button.
	/// </summary>
	void OnEnable()
	{
		btnRecord.gameObject.SetActive(true);
		statusScreen.SetActive(false);
	}

	//###########################################################
	// Update methods
	void Update()
	{
		if (recording)
		{
			if (!finished)
			{
				if (Time.time < finishRecordingTime)
				{
					UpdateTimerBar();
				}
				else
				{
					FinishRecording();
				}
			}
		}
	}

	/// <summary>
	/// Set the timer bar size to match the remaining time
	/// </summary>
	void UpdateTimerBar()
	{
		if (objTimerBar != null)
		{
			objTimerBar.fillAmount = 
				Mathf.Min(1, 1 - ((finishRecordingTime - Time.time) / totalRecordingTime));
		}
	}

	/// <summary>
	/// Set the recording end tag and clear the objects.
	/// </summary>
	void FinishRecording()
	{
		objTimerBar.fillAmount = 1;
		recording = false;
		finished = true;
		BroadcastMessage("EndRecording");
		statusScreen.SetActive(true);

		int [] levels = new int[PlayerData.totalSkillTypes];
		
		for(int i = 0; i < PlayerData.totalSkillTypes; i++)
		{
			levels[i] = folders[i].Level() * 11;
		}

		txtVideoScore.text = "" + CalculateVideoScore(levels);
		PlayerData.EarnXP(levels);

		for(int i = 0; i < PlayerData.totalSkillTypes; i++)
		{
			int index = i * 3;
			skillStatus[index].text = "" + PlayerData.skills[i].Level();
			skillStatus[index + 1].text = "" + levels[i];
			skillStatus[index + 2].text = "" + PlayerData.skills[i].XPNextLevel();
		}
	}

	int CalculateVideoScore(int [] levels)
	{
		return PlayerData.CalculateVideoScore(levels);
	}

	//###########################################################
	// Private methods

	/// <summary>
	/// Instantiate the selected attribute object.
	/// </summary>
	/// <param name="attribute">Type of the object instantiated.</param>
	void CreateAttribute(int attribute, int position)
	{
		if (objectAttribute.Length > attribute &&
		    objectAttribute[attribute] != null)
		{
			GameObject att = Instantiate<GameObject>(objectAttribute[attribute]);
			att.transform.SetParent(transform, false);
			att.transform.position = 
				randomPlacesToInstantiateAttributes[position].position;
		}
		else
		{
			Debug.LogError("Error creating attribute object.");
		}
	}

	/// <summary>
	/// Event used to nullify the production object's reference to this script.
	/// </summary>
	void OnDestroy()
	{
		ProductionObject.MakeProductionManagerNull();
	}
}
