using UnityEngine;
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
	/// Attribute objects, in the order: Content, Quality and Innovation.
	/// </summary>
	public GameObject [] objectAttribute;

	/// <summary> Array with the folder objects. </summary>
	public FolderObject [] folders;

	//###########################################################
	// Private attributes

	/// <summary> Random places to instantiate attribute objects. </summary>
	Transform [] randomPlacesToInstantiateAttributes;

	//###########################################################
	// Public methods

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

		for(int i = 0; i < AttributeObject.totalTypes; i++)
		{
			int index = Random.Range(0, positions.Count);
			int position = positions[index];
			positions.RemoveAt(index);

			CreateAttribute(i, position);
		}
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

	//###########################################################
	// Initialization
	void Awake()
	{
		randomPlacesToInstantiateAttributes = 
			randomPlacesToInstantiateAttributesContainer.
				GetComponentsInChildren<Transform>();
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
