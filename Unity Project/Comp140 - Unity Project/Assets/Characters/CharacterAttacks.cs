using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacks : MonoBehaviour
{
    #region Variables

    public delegate Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> Ability(int currentSpace);

    public Ability ability1;
    public Ability ability2;
    public Ability ability3;

    #endregion

    #region Abilities

    #region Damage

    public Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> Disintegration(int currentSpace)
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        int[] line = GetColumn(currentSpace, 2);

        for (int n = 0; n < 2; n++)
        {
            Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
            basicEffect.Add(true, 6/(n+1));

            Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
            effect.Add(basicEffect, false);
            
            targetSpaces.Add(line[n], effect);
        }
        return targetSpaces;
    }

    public Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> WallOfFire(int currentSpace)
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        int[] line = GetRow(currentSpace, 3);

        for (int n = 0; n < 3; n++)
        {
            Dictionary<bool, int> basicEffect = new Dictionary<bool, int>();
            basicEffect.Add(true, 4);

            Dictionary<Dictionary<bool, int>, bool> effect = new Dictionary<Dictionary<bool, int>, bool>();
            effect.Add(basicEffect, false);

            targetSpaces.Add(line[n], effect);
        }
        return targetSpaces;
    }

    #endregion

    #region Control

    #endregion

    #region Healing

    #endregion

    #endregion

    #region Target Spaces

    int[] GetColumn(int currentSpace, int spaces)
    {
        int[] lineSpaces = new int[spaces];

        for (int n = 0; n < spaces; n++)
        {
            lineSpaces[n] = currentSpace - (n+2)*3;
            Debug.Log(lineSpaces[n]);
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