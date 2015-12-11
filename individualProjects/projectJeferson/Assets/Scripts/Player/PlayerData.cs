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
		Innovation, Quality, Content, Communication
	}

	/// <summary>
	/// Total skill types.
	/// </summary>
	public const int totalSkillTypes = 4;
	
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

	static public int [] skillBonus = new int[totalSkillTypes];
	
	/// <summary>
	/// The SEBRAE are of knowledges
	/// </summary>
	static public Knowledge [] knowledges = 
		new Knowledge[totalKnowledgeTypes];

	static public int scoreLastVideo = 0;
	static public int scoreVideoBefore = 0;

	static public int marketingBonus = 1;
	static public int marketingCost = 0;
	static public int marketingValue = 1;

	static public int lastMarketing = 0;

	/// <summary>
	/// The total number of money earned.
	/// </summary>
	static public int totalMoney = 1500;


	static public VideoData videoRelease = null;
	static public VideoData videoLast = null;

	static public int turn = 0;
	static public bool recordedThisTurn = false;

	static public MarketItem [] marketItens = new MarketItem[6];

	static public bool RemoveMoney(int cost)
	{
		if (cost > totalMoney)
			return false;

		totalMoney -= cost;
		return true;
	}

	static public bool HasMoney(int cost)
	{
		if (cost > totalMoney)
			return false;

		return true;
	}

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


	static public void Create()
	{
		for(int i = 0; i < skillBonus.Length; i++)
		{
			skills[i] = new Skill();
			skillBonus[i] = 0;
		}

		for(int i = 0; i < marketItens.Length; i++)
		{
			marketItens[i] = new MarketItem();
		} 
	}

	static public int CalculateVideoScore(int [] levels)
	{
		int sum = 0;
		for (int i = 0; i < levels.Length; i++)
		{
			sum += levels[i] + skillBonus[i];
		}

		scoreVideoBefore = scoreLastVideo;
		scoreLastVideo = sum;

		CalculateMarketingValue(1);

		return sum;
	}

	static public int CalculateMarketingValue(int bonus, int cost = 0)
	{
		marketingBonus = bonus;
		marketingCost = cost;
		marketingValue = skills[(int)SkillType.Communication].Level() * marketingBonus;

		return marketingValue;
	}

	static public void EarnXP(int [] levels)
	{
		for (int i = 0; i < totalSkillTypes; i++)
		{
			skills[i].EarnXP(levels[i]);
		}
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
	int xp = 0;

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
	public int XPNextLevel()
	{
		return (level * (level + 1) / 2) * 10;
	}

	//###############################################################
	// Public methods

	/// <summary>
	/// Earn experience and verify if the skill leveled up.
	/// </summary>
	/// <param name="xp">Experience received.</param>
	public void EarnXP(int xp)
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

public class MarketItem
{
	string title = "";
	
	string description = "";
	
	bool bought = false;

	int mensalCost = 0;
	
	public string Title()
	{
		return title;
	}
	
	public string Description()
	{
		return description;
	}
	
	public bool Bought()
	{
		return bought;
	}

	public void Buy()
	{
		bought = true;
	}
}