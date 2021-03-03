using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : CharacterAttacks
{
    #region Target Spaces

    public override int[] GetColumn(int currentSpace, int spaces)
    {
        int[] lineSpaces = new int[spaces];

        for (int n = 0; n < spaces; n++)
        {
            lineSpaces[n] = currentSpace + (n + 1) * 3;
        }

        return lineSpaces;
    }

    public override int[] GetRow(int currentSpace, int spaceOffset)
    {
        int[] lineSpaces = new int[3];

        int centerSpace = currentSpace;

        if ((currentSpace - 1) % 3 == 0)
            centerSpace = currentSpace;

        if ((currentSpace) % 3 == 0)
            centerSpace = currentSpace + 1;

        if ((currentSpace - 2) % 3 == 0)
            centerSpace = currentSpace - 1;

        lineSpaces[0] = centerSpace + (3 * spaceOffset);
        lineSpaces[1] = centerSpace + (spaceOffset * 3) + 1;
        lineSpaces[2] = centerSpace + (spaceOffset * 3) - 1;

        return lineSpaces;
    }

    public override int[] GetSides(int currentSpace, int spaceOffset)
    {
        List<int> spacesList = new List<int>();

        if ((currentSpace - 1) % 3 == 0)
        {
            spacesList.Add(currentSpace + (3 * spaceOffset) + 1);
            spacesList.Add(currentSpace + (3 * spaceOffset) - 1);
        }

        if ((currentSpace) % 3 == 0)
        {
            spacesList.Add(currentSpace + (3 * spaceOffset) + 1);
        }

        if ((currentSpace - 2) % 3 == 0)
        {
            spacesList.Add(currentSpace + (3 * spaceOffset) - 1);
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

    public override int[] GetRadius(int currentSpace, int spaceOffset, bool getCenter, bool diagonal)
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
                spacesList.Add(currentSpace + (3 * spaceOffset) + n);
                spacesList.Add(currentSpace + (3 * spaceOffset) - n);
            }
        }

        //Left
        if ((currentSpace) % 3 == 0)
        {
            spacesList.Add(currentSpace + (3 * spaceOffset) + 1);
            spacesList.Add(currentSpace + (3 * spaceOffset) + 3);
            spacesList.Add(currentSpace + (3 * spaceOffset) + 4);

            if (diagonal)
            {
                spacesList.Add(currentSpace + (3 * spaceOffset) - 2);
                spacesList.Add(currentSpace + (3 * spaceOffset) - 3);
            }
        }

        //Right
        if ((currentSpace - 2) % 3 == 0)
        {
            spacesList.Add(currentSpace + (3 * spaceOffset) - 1);
            spacesList.Add(currentSpace + (3 * spaceOffset) - 3);
            spacesList.Add(currentSpace + (3 * spaceOffset) - 4);

            if (diagonal)
            {
                spacesList.Add(currentSpace + (3 * spaceOffset) + 2);
                spacesList.Add(currentSpace + (3 * spaceOffset) + 3);
            }
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