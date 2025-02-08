using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Struct to hold character data
[System.Serializable]  // This tells Unity to show this struct in the Inspector
public struct CharacterStats
{
    public string Name;
    public int Level;
    public int Constitution;
    public string Class;
    public bool IsHillDwarf;
    public bool HasToughFeat;
    public bool UseAverageHP;
}

// Struct to store hit dice per class
public struct HitDice
{
    public static readonly Dictionary<string, int> Dice = new Dictionary<string, int>()
    {
        {"Artificer", 8}, {"Barbarian", 12}, {"Bard", 8}, {"Cleric", 8},
        {"Druid", 8}, {"Fighter", 10}, {"Monk", 8}, {"Ranger", 10},
        {"Rogue", 8}, {"Paladin", 10}, {"Sorcerer", 6}, {"Wizard", 6},
        {"Warlock", 8}
    };
}

// Character class to handle logic
public class NewBehaviourScript : MonoBehaviour
{
    private CharacterStats stats;
    private static readonly int[] Modifiers = { -5, -4, -4, -3, -3, -2, -2, -1, -1, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };

    // Instead of a constructor, use an initialization method
    public void Initialize(CharacterStats characterStats)
    {
        this.stats = characterStats;
    }

    // Calculate HP based on rules
    public int CalculateHP()
    {
        if (!HitDice.Dice.ContainsKey(stats.Class))
        {
            Debug.LogError($"Invalid class: {stats.Class}");
            return 0;
        }

        int totalHP = 0;
        int hitDie = HitDice.Dice[stats.Class];

        for (int i = 0; i < stats.Level; i++)
        {
            int hpGain = stats.UseAverageHP ? Mathf.FloorToInt((hitDie + 1) / 2f) : Random.Range(1, hitDie + 1);
            totalHP += hpGain + Modifiers[Mathf.Clamp(stats.Constitution - 1, 0, Modifiers.Length - 1)];
        }

        if (stats.IsHillDwarf)
            totalHP += stats.Level;

        if (stats.HasToughFeat)
            totalHP += stats.Level * 2;

        return totalHP;
    }

    public void Display()
    {
        string dwarfText = stats.IsHillDwarf ? "is" : "is not";
        string featText = stats.HasToughFeat ? "does have" : "does not have";
        string hpMethod = stats.UseAverageHP ? "averaged" : "rolled";
        int hp = CalculateHP();

        Debug.Log($"My character {stats.Name} is a level {stats.Level} {stats.Class} " +
                  $"with a CON score of {stats.Constitution} and {dwarfText} a Hill Dwarf " +
                  $"and {featText} the tough feat. I want the hp {hpMethod}. My hp is {hp}.");
    }
}