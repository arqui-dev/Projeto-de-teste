using UnityEngine;
using System.Collections;

/// <summary>
/// This script holds all the player data, like level, experience, money, etc.
/// </summary>
public class PlayerData
{
	/// <summary>
	/// Skill types.
	/// </summary>
	public enum SkillType {
		Innovation, Quality, Content
	}

	/// <summary>
	/// Total skill types.
	/// </summary>
	public const int totalSkillTypes = 3;

	/// <summary>
	/// Skill types.
	/// </summary>
	public enum KnowledgeType {
		Innovation, Quality, Content
	}

	/// <summary>
	/// Total skill types.
	/// </summary>
	public const int totalKnowledgeTypes = 18;

	/// <summary>
	/// Player skills, quality, innovation and content.
	/// </summary>
	static public Skill [] skills = new Skill[totalSkillTypes];

	/// <summary>
	/// The SEBRAE are of knowledges
	/// </summary>
	static public Knowledge [] knowledges = 
		new Knowledge[totalKnowledgeTypes];

	/// <summary>
	/// The production level.
	/// </summary>
	/// <returns>Production level.</returns>
	static public int Production()
	{
		int sum = 0;
		foreach(Knowledge knowledge in knowledges)
		{
			if (knowledge.Learned()) sum++;
		}
		return sum;
	}


}

/// <summary>
/// Class to manage the player skills
/// </summary>
public class Skill
{
	/// <summary> The skill title. </summary>
	string title = "";

	/// <summary> Skill description. </summary>
	string description = "";

	/// <summary> Skill level. </summary>
	int level = 1;

	/// <summary> Skill total experience. </summary>
	float xp = 0;

	/// <summary>
	/// Verifies if the skill is on the next level and raise it if it does.
	/// </summary>
	void VerifyLevelUp()
	{
		if (xp > XPNextLevel())
		{
			level++;
		}
	}

	/// <summary>
	/// Necessary experience for the next level.
	/// </summary>
	/// <returns>Necessary experience for the next level.</returns>
	float XPNextLevel()
	{
		return level * (level + 1f) / 2f;
	}

	//###############################################################
	// Public methods

	/// <summary>
	/// Earn experience and verify if the skill leveled up.
	/// </summary>
	/// <param name="xp">Experience received.</param>
	public void EarnXP(float xp)
	{
		this.xp += xp;
		VerifyLevelUp();
	}

	/// <summary> The level of the skill. </summary>
	/// <returns>Level of the skill.</returns>
	public int Level()
	{
		return level;
	}

	/// <summary> The skill title. </summary>
	/// <returns>Skill title.</returns>
	public string Title()
	{
		return title;
	}

	/// <summary> The skill description. </summary>
	/// <returns>Skill description.</returns>
	public string Description()
	{
		return description;
	}
}

/// <summary>
/// The SEBRAE area of knowledge.
/// </summary>
public class Knowledge
{

	string title = "";

	string description = "";

	bool learned = false;

	public string Title()
	{
		return title;
	}

	public string Description()
	{
		return description;
	}

	public bool Learned()
	{
		return learned;
	}

}