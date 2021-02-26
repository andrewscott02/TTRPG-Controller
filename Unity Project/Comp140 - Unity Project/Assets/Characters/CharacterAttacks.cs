using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour
{
    #region Variables

    private FullBoard board;

    public delegate Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> Ability(int currentSpace);

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

    public Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> Disintegration(int currentSpace)
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        int[] line = GetColumn(currentSpace, 3);

        for (int n = 0; n < 3; n++)
        {
            Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
            basicEffect.Add(true, 4);
            //basicEffect.Add(true, 6 / (n + 1));

            Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
            effect.Add(basicEffect, false);
            
            targetSpaces.Add(line[n], effect);
        }
        return targetSpaces;
    }

    public Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> WallOfFire(int currentSpace)
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        int[] line = GetRow(currentSpace, 2);

        for (int n = 0; n < 3; n++)
        {
            Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
            basicEffect.Add(true, 2);

            Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
            effect.Add(basicEffect, false);

            targetSpaces.Add(line[n], effect);
        }
        return targetSpaces;
    }

    public Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> ConeOfCold(int currentSpace)
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        int[] line = GetColumn(currentSpace, 2);

        for (int n = 0; n < 2; n++)
        {
            Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
            basicEffect.Add(true, 6 / (n + 1));

            Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
            effect.Add(basicEffect, false);

            targetSpaces.Add(line[n], effect);
        }

        int[] sides = GetSides(currentSpace, 2);

        for (int n = 0; n < 3; n++)
        {
            if (board.IsSpaceValid(sides[n]))
            {
                Space space = board.spaces[line[n]].GetComponent<Space>();

                if (space.GetSpace())
                {
                    Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
                    basicEffect.Add(true, 3);

                    Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
                    effect.Add(basicEffect, false);

                    targetSpaces.Add(line[n], effect);
                }
            }
        }

        return targetSpaces;
    }

    #endregion

    #region Control

    public Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> FreezingGrasp(int currentSpace)
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        int[] line = GetColumn(currentSpace, 3);

        for (int n = 0; n < 3; n++)
        {
            if (board.IsSpaceValid(line[n]))
            {
                Space space = board.spaces[line[n]].GetComponent<Space>();

                if (space.GetSpace())
                {
                    Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
                    basicEffect.Add(true, 4);
                    //basicEffect.Add(true, 6 / (n + 1));

                    Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
                    effect.Add(basicEffect, true);

                    targetSpaces.Add(line[n], effect);
                }
                else
                {
                    Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
                    basicEffect.Add(true, 4);
                    //basicEffect.Add(true, 6 / (n + 1));

                    Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
                    effect.Add(basicEffect, true);

                    targetSpaces.Add(line[n], effect);

                    return targetSpaces;
                }
            }
        }
        return targetSpaces;
    }

    #endregion

    #region Healing

    public Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> HealingBurst(int currentSpace)
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        int[] line = GetColumn(currentSpace, 1);

        for (int n = 0; n < 1; n++)
        {
            Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
            basicEffect.Add(false, 3);

            Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
            effect.Add(basicEffect, false);

            targetSpaces.Add(line[n], effect);
        }

        int[] sides = GetSides(currentSpace, 2);

        for (int n = 0; n < 3; n++)
        {
            if (board.IsSpaceValid(sides[n]))
            {
                Space space = board.spaces[line[n]].GetComponent<Space>();

                if (space.GetSpace())
                {
                    Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
                    basicEffect.Add(false, 2);

                    Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
                    effect.Add(basicEffect, false);

                    targetSpaces.Add(line[n], effect);
                }
            }
        }

        return targetSpaces;
    }

    public Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> FrozenArmour(int currentSpace)
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
        basicEffect.Add(false, 4);
        //basicEffect.Add(true, 6 / (n + 1));

        Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
        effect.Add(basicEffect, false);

        targetSpaces.Add(currentSpace, effect);

        return targetSpaces;
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