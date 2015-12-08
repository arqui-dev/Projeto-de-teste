using UnityEngine;
using System.Collections;

/// <summary>  </summary>

/// <summary>
/// This script controls the behavior of the a production object.
/// </summary>
public class ProductionObject : MonoBehaviour
{
	//###########################################################
	// Static attributes

	/// <summary>
	/// Static attribute to store the production manager of the scene.
	/// </summary>
	static ProductionManager productionManager = null;

	//###########################################################
	// Public attributes

	/// <summary> How hard is to destroy this object </summary>
	public int life = 9;


	//###########################################################
	// Inicialization
	void Awake()
	{
		if (productionManager == null)
		{
			productionManager = GameObject.
				FindObjectOfType<ProductionManager>();
		}
	}

	//###########################################################
	// Public Methods

	/// <summary> 
	/// Reduces the life of this object and call defeat the object if the condition is fulfilled.
	/// </summary>
	public void OnClick()
	{
		this.life -= 1;
		if (this.life <= 0)
		{
			Defeated();
		}
	}

	//###########################################################
	// Private methods

	/// <summary>
	/// Create new attributes and destroys this object.
	/// </summary>
	void Defeated()
	{
		CreateAttributes();
		Destroy (gameObject);
	}

	/// <summary>
	/// Create attributes objects based on the player current skill levels.
	/// </summary>
	void CreateAttributes()
	{
		productionManager.CreateAttributes();
	}

	//###########################################################
	// Static methods

	/// <summary>
	/// Nullify the productionManager attribute that when the new scene is created it can get the right one.
	/// </summary>
	static public void MakeProductionManagerNull()
	{
		productionManager = null;
	}

}