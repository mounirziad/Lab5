using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab5Script : MonoBehaviour
{

    public string charactername;
    public int characterlevel;
    public int conscore;
    public string characterclass;
    public bool hilldwarf;
    public bool toughfeat;
    public bool ishpavg;

    public List<string> classes = new List<string>()
    {
        "Artificer",
        "Barbarian",
        "Bard",
        "Cleric",
        "Druid",
        "Fighter",
        "Monk",
        "Ranger",
        "Rogue",
        "Paladin",
        "Sorcerer",
        "Wizard",
        "Warlock"
    };

    Dictionary<string, int> hitdice = new Dictionary<string, int>()
    {
        {"Artificer", 8 },
        {"Barbarian", 12 },
        {"Bard", 8 },
       {"Cleric", 8 },
       {"Druid", 8 },
       {"Fighter", 10 },
       {"Monk", 8 },
       {"Ranger", 10 },
       {"Rogue", 8 },
       {"Paladin", 10 },
       {"Sorcerer", 6 },
       {"Wizard", 6 },
       {"Warlock", 8 }
    };

    private int[] modifiers = {-5,-4,-4,-3,-3,-2,-2,-1,-1,0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10 };

    int calculatehp()
    {
        int totalhp = 0;

        if (ishpavg)
        {
            float avgVal;
            for(int i = 0; i < characterlevel; i++)
            {
                avgVal = (hitdice[characterclass] + 1) / 2;
                avgVal = Mathf.FloorToInt(avgVal);
                totalhp += (int)avgVal + modifiers[conscore - 1];
            }
        }
        else if (!ishpavg)
        {
            int rolledVal;
            int dice = hitdice[characterclass];
            for (int i = 0; i < characterlevel; i++)
            {
                rolledVal = Random.Range(1, dice + 1);
                totalhp += rolledVal + modifiers[conscore];
            }
                
        }

        if (hilldwarf)
        {
            for (int i = 0;i < characterlevel; i++)
            {
                totalhp++;
            }
        }

        if (toughfeat)
        {
            for(int i = 0; i < characterlevel; i++)
            {
                totalhp += 2;
            }
        }

        return totalhp;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!classes.Contains(characterclass))
        {
            error();
        }
        else
        {
            Display();
        }
 
    }

    void Display()
    {
        string isHillDwarf;
        if (hilldwarf)
        {
            isHillDwarf = "is";
        }
        else
        {
            isHillDwarf = "is not";
        }

        string hasToughFeat;
        if (toughfeat)
        {
            hasToughFeat = "does have";
        }
        else
        {
            hasToughFeat = "does not have";
        }

        string isAveraged;
        if (ishpavg)
        {
            isAveraged = "averaged. My hp is " + calculatehp();
        }
        else
        {
            isAveraged = "rolled. My hp is " + calculatehp();
        }

        Debug.Log($"My character {charactername} is a level {characterlevel} {characterclass} with a CON score of {conscore} and {isHillDwarf} a Hill Dwarf and {hasToughFeat} the tough feat. I want the hp {isAveraged}.");
    }

    void error()
    {
        Debug.LogError("Invalid class");
        Debug.Log("Valid classes are: Artificer, Barbarian, Bard, Cleric, Druid, Fighter, Monk, Ranger, Rogue, Paladin, Sorcerer, Wizard, Warlock");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
