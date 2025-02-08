using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterStats characterData;

    private void Start()
    {
        if (!HitDice.Dice.ContainsKey(characterData.Class))
        {
            Debug.LogError("Invalid class. Valid classes are: " + string.Join(", ", HitDice.Dice.Keys));
            return;
        }

        // Create a new GameObject to attach the script to
        GameObject characterObject = new GameObject("Character");
        NewBehaviourScript playerCharacter = characterObject.AddComponent<NewBehaviourScript>();

        // Initialize the character
        playerCharacter.Initialize(characterData);
        playerCharacter.Display();
    }
}