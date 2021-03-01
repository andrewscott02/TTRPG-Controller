using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpace
{
    public int space;
    public bool damage;
    public float value;
    public bool effect;
}

public class CharacterAttacks : MonoBehaviour
{
    #region Variables

    private FullBoard board;

    public delegate TargetSpace[] Ability(int currentSpace);

    public Ability ability1;
    public Ability ability2;
    public Ability ability3;

    #endregion

    private void Awake()
    {
        GameObject reference = GameObject.Find("Board");
        board = reference.GetComponent<FullBoard>();
    }

    #region Abilities

    #region Damage

    public TargetSpace[] Disintegration(int currentSpace)
    {
        int[] line = GetColumn(currentSpace, 3);
        TargetSpace[] targetArea = new TargetSpace[line.Length];

        for (int n = 0; n < line.Length; n++)
        {
            targetArea[n].space = line[n];
            targetArea[n].damage = true;
            targetArea[n].value = 0.4f;
            targetArea[n].effect = false;
        }

        return targetArea;
    }

    public TargetSpace[] WallOfFire(int currentSpace)
    {
        int[] line = GetRow(currentSpace, 2);
        TargetSpace[] targetArea = new TargetSpace[line.Length];

        for (int n = 0; n < line.Length; n++)
        {
            targetArea[n].space = line[n];
            targetArea[n].damage = true;
            targetArea[n].value = 0.2f;
            targetArea[n].effect = false;
        }

        return targetArea;
    }

    public TargetSpace[] ConeOfCold(int currentSpace)
    {
        int[] line = GetColumn(currentSpace, 2);
        int[] sides = GetSides(currentSpace, 2);

        int[] spaces = new int[line.Length + sides.Length];

        for (int n = 0; n < line.Length; n++)
        {
            spaces[n] = line[n];
        }

        for (int n = 0; n < sides.Length; n++)
        {
            spaces[n + line.Length] = sides[n];
        }

        TargetSpace[] targetArea = new TargetSpace[spaces.Length];

        bool first = true;

        for (int n = 0; n < spaces.Length; n++)
        {
            if (first)
            {
                targetArea[n].value = 0.4f;
            }
            else
            {
                targetArea[n].value = 0.2f;
            }

            targetArea[n].space = line[n];
            targetArea[n].damage = true;
            targetArea[n].effect = false;
        }

        return targetArea;
    }

    #endregion

    #region Control

    public TargetSpace[] FreezingGrasp(int currentSpace)
    {
        int[] line = GetColumn(currentSpace, 3);

        List<int> targetSpaces = new List<int>();

        for (int n = 0; n < line.Length; n++)
        {
            if (board.IsSpaceValid(line[n]))
            {
                Space space = board.spaces[line[n]].GetComponent<Space>();

                targetSpaces.Add(line[n]);

                if (!space.GetSpace())
                {
                    break;
                }
            }
        }

        TargetSpace[] targetArea = new TargetSpace[targetSpaces.Count];

        for (int n = 0; n < line.Length; n++)
        {
            targetArea[n].space = targetSpaces[n];
            targetArea[n].damage = true;
            targetArea[n].value = 0.2f;
            targetArea[n].effect = false;
        }

        return targetArea;
    }

    #endregion

    #region Healing

    public TargetSpace[] HealingBurst(int currentSpace)
    {
        int[] line = GetRow(currentSpace, 0);
        TargetSpace[] targetArea = new TargetSpace[line.Length];

        for (int n = 0; n < line.Length; n++)
        {
            targetArea[n].space = line[n];
            targetArea[n].damage = false;
            targetArea[n].value = 0.2f;
            targetArea[n].effect = false;
        }

        return targetArea;
    }

    public TargetSpace[] FrozenArmour(int currentSpace)
    {
        TargetSpace[] targetArea = new TargetSpace[1];

        targetArea[0].space = currentSpace;
        targetArea[0].damage = true;
        targetArea[0].value = 0.5f;
        targetArea[0].effect = false;

        return targetArea;
    }

    #endregion

    #endregion

    #region Target Spaces

    int[] GetColumn(int currentSpace, int spaces)
    {
        int[] lineSpaces = new int[spaces];

        for (int n = 0; n < spaces; n++)
        {
            lineSpaces[n] = currentSpace - (n+1)*3;
        }

        return lineSpaces;
    }

    int[] GetRow(int currentSpace, int spaceOffset)
    {
        int[] lineSpaces = new int[3];

        int centerSpace = currentSpace;

        if ((currentSpace - 1) % 3 == 0)
            centerSpace = currentSpace;

        if ((currentSpace) % 3 == 0)
            centerSpace = currentSpace + 1;

        if ((currentSpace - 2) % 3 == 0)
            centerSpace = currentSpace - 1;

        lineSpaces[0] = centerSpace - (3 * spaceOffset);
        lineSpaces[1] = centerSpace - (spaceOffset * 3) + 1;
        lineSpaces[2] = centerSpace - (spaceOffset * 3) - 1;

        return lineSpaces;
    }

    int[] GetSides(int currentSpace, int spaceOffset)
    {
        List<int> spacesList = new List<int>();

        if ((currentSpace - 1) % 3 == 0)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset) + 1);
            spacesList.Add(currentSpace - (3 * spaceOffset) - 1);
        }

        if ((currentSpace) % 3 == 0)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset) + 1);
        }

        if ((currentSpace - 2) % 3 == 0)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset) - 1);
        }

        int[] lineSpaces = new int[spacesList.Count];

        int i = 0;
        foreach (var item in spacesList)
        {
            lineSpaces[i] = item;

            i++;
        }

        return lineSpaces;
    }

    int[] GetRadius(int currentSpace, int spaceOffset, bool getCenter)
    {
        List<int> spacesList = new List<int>();

        if (getCenter)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset));
        }

        //Center
        if ((currentSpace - 1) % 3 == 0)
        {
            for (int n = 1; n <= 4; n++)
            {
                spacesList.Add(currentSpace - (3 * spaceOffset) + n);
                spacesList.Add(currentSpace - (3 * spaceOffset) - n);
            }
        }

        //Left
        if ((currentSpace) % 3 == 0)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset) + 1);
            spacesList.Add(currentSpace - (3 * spaceOffset) + 3);
            spacesList.Add(currentSpace - (3 * spaceOffset) + 4);

            spacesList.Add(currentSpace - (3 * spaceOffset) - 2);
            spacesList.Add(currentSpace - (3 * spaceOffset) - 3);
        }

        //Right
        if ((currentSpace - 2) % 3 == 0)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset) - 1);
            spacesList.Add(currentSpace - (3 * spaceOffset) - 3);
            spacesList.Add(currentSpace - (3 * spaceOffset) - 4);

            spacesList.Add(currentSpace - (3 * spaceOffset) + 2);
            spacesList.Add(currentSpace - (3 * spaceOffset) + 3);
        }

        int[] lineSpaces = new int[spacesList.Count];

        int i = 0;
        foreach (var item in spacesList)
        {
            lineSpaces[i] = item;

            i++;
        }

        return lineSpaces;
    }

    /*
    int[] GetLine(int currentSpace, int spacesAcross, int spaceOffset)
    {
        int[] lineSpaces = new int[spacesAcross];

        lineSpaces[0] = currentSpace - (3 * spaceOffset);

        for (int n = 1; n <= spacesAcross; n++)
        {
            lineSpaces[n] = currentSpace - (spaceOffset * 3) + n;
            lineSpaces[n] = currentSpace - (spaceOffset * 3) - n;
        }

        return lineSpaces;
    }
    */

    #endregion
}