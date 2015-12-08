using UnityEngine;
using System.Collections;

/// <summary>
/// This script holds all the player data, like level, experience, money, etc.
/// </summary>
public class PlayerData
{
	/// <summary>
	/// Player skills.
	/// </summary>
	static public Skills skills = new Skills();
}

/// <summary>
/// The player skills.
/// </summary>
public class Skills
{
	/// <summary>
	/// Production skill, used to determine the maximum level of the productions.
	/// </summary>
	public int production = 1;

	/// <summary> Innovation skill. </summary>
	public int innovation = 1;

	/// <summary> Content skill. </summary>
	public int content = 1;

	/// <summary> Quality skill. </summary>
	public int quality = 1;
}