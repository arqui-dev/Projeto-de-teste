using UnityEngine;
using System.Collections;

/// <summary>
/// This script manage the production mechanic: the production on the scenery, new productions, what happens when a production is over.
/// </summary>
public class ProductionManager : MonoBehaviour
{
	//###########################################################
	// Public attributes

	/// <summary>
	/// Attribute objects, in the order: Content, Quality and Innovation.
	/// </summary>
	public GameObject [] objectAttribute;

	//###########################################################
	// Public methods

	/// <summary>
	/// Instantiate attribute objects.
	/// </summary>
	/// <param name="ammount">Ammount of objects created.</param>
	public void CreateAttributes(int ammount)
	{
		for(int i = 0; i < ammount; i++)
		{
			AttributeObject.Type type = (AttributeObject.Type)
				Random.Range(0, AttributeObject.totalTypes);

			CreateAttribute(type);
		}
	}

	//###########################################################
	// Private methods

	/// <summary>
	/// Instantiate the selected attribute object.
	/// </summary>
	/// <param name="attribute">Type of the object instantiated.</param>
	void CreateAttribute(AttributeObject.Type attribute)
	{
		Instantiate(objectAttribute[(int)attribute]);
	}

	/// <summary>
	/// Event used to nullify the production object's reference to this script.
	/// </summary>
	void OnDestroy()
	{
		ProductionObject.MakeProductionManagerNull();
	}
}
