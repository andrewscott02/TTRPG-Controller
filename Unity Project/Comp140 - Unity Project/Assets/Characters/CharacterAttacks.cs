using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpace
{
    public int space;
    public bool damage;
    public float value;
    public bool stun;
    public bool shield = false;
}

public class CharacterAttacks : MonoBehaviour
{
    #region Setup

    #region Variables

    protected FullBoard board;

    public delegate TargetSpace[] Ability(int currentSpace);

    public Ability ability1;
    public Ability ability2;
    public Ability ability3;
    public Ability ability4;

    #endregion

    protected void Awake()
    {
        GameObject reference = GameObject.Find("Board");
        board = reference.GetComponent<FullBoard>();
    }

    #endregion

    #region Abilities

    #region Damage

    public TargetSpace[] Disintegration(int currentSpace)
    {
        int[] line = GetColumn(currentSpace, 4);
        TargetSpace[] targetArea = new TargetSpace[line.Length];

        for (int n = 0; n < line.Length; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = line[n],
                damage = true,
                value = 0.4f,
                stun = false
            };

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] WallOfFire(int currentSpace)
    {
        int[] line = GetRow(currentSpace, 3);
        TargetSpace[] targetArea = new TargetSpace[line.Length];

        for (int n = 0; n < line.Length; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = line[n],
                damage = true,
                value = 0.2f,
                stun = false
            };

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] Flamethrower(int currentSpace)
    {
        int[] line = GetColumn(currentSpace, 3);
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
            TargetSpace targetSpace = new TargetSpace
            {
                space = spaces[n],
                damage = true,
                stun = false
            };

            if (first)
            {
                targetSpace.value = 0.3f;
            }
            else
            {
                targetSpace.value = 0.2f;
            }

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] GleamingBlade(int currentSpace)
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
            TargetSpace targetSpace = new TargetSpace
            {
                space = spaces[n],
                damage = true,
                stun = false
            };

            if (first)
            {
                targetSpace.value = 0.4f;
            }
            else
            {
                targetSpace.value = 0.3f;
            }

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] AcidBomb(int currentSpace)
    {
        int[] radius = GetRadius(currentSpace - 9, 0, true, false);
        TargetSpace[] targetArea = new TargetSpace[radius.Length];

        for (int n = 0; n < radius.Length; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = radius[n],
                damage = true,
                value = 0.15f,
                stun = false
            };

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] SerpentineStrike(int currentSpace)
    {
        int[] line = GetColumn(currentSpace, 4);
        TargetSpace[] targetArea = new TargetSpace[line.Length];

        for (int n = 0; n < line.Length; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = line[n],
                damage = true,
                value = 0.4f,
                stun = false
            };

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    #endregion

    #region Control

    public TargetSpace[] SearingStrike(int currentSpace)
    {
        int[] line = GetColumn(currentSpace, 2);

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

        for (int n = 0; n < targetSpaces.Count; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = targetSpaces[n],
                damage = true,
                value = 0.4f,
                stun = true
            };

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] DrainVitality(int currentSpace)
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

        TargetSpace[] targetArea = new TargetSpace[targetSpaces.Count + 1];

        for (int n = 0; n < targetSpaces.Count; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = targetSpaces[n],
                damage = true,
                value = 0.2f,
                stun = false
            };

            targetArea[n] = targetSpace;
        }

        TargetSpace healingSpace = new TargetSpace
        {
            space = currentSpace,
            damage = false,
            value = 0.2f,
            stun = false
        };

        targetArea[targetArea.Length - 1] = healingSpace;

        return targetArea;
    }

    public TargetSpace[] Entangle(int currentSpace)
    {
        TargetSpace[] targetArea = new TargetSpace[1];

        TargetSpace targetSpace = new TargetSpace
        {
            space = currentSpace - 9,
            damage = true,
            value = 0.2f,
            stun = true
        };

        targetArea[0] = targetSpace;

        return targetArea;
    }

    public TargetSpace[] VenomBite(int currentSpace)
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

        for (int n = 0; n < targetSpaces.Count; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = targetSpaces[n],
                damage = true,
                value = 0.3f,
                stun = true
            };

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    #endregion

    #region Healing

    public TargetSpace[] HealingFlames(int currentSpace)
    {
        TargetSpace[] targetArea = new TargetSpace[1];

        TargetSpace targetSpace = new TargetSpace
        {
            space = currentSpace,
            damage = false,
            value = 0.2f,
            stun = false
        };

        targetArea[0] = targetSpace;

        return targetArea;
    }

    public TargetSpace[] CleanseWounds(int currentSpace)
    {
        int[] line = GetColumn(currentSpace + 6, 2);
        int[] sides = GetSides(currentSpace + 6, 1);

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
            TargetSpace targetSpace = new TargetSpace
            {
                space = spaces[n],
                damage = false,
                stun = false
            };

            if (first)
            {
                targetSpace.value = 0.05f;
            }
            else
            {
                targetSpace.value = 0.15f;
            }

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] FaithfulWard(int currentSpace)
    {
        int[] line = GetColumn(currentSpace + 6, 2);
        int[] sidesRow1 = GetSides(currentSpace + 3, 1);
        int[] sidesRow2 = GetSides(currentSpace + 6, 1);

        int[] spaces = new int[line.Length + sidesRow1.Length + sidesRow2.Length];

        for (int n = 0; n < line.Length; n++)
        {
            spaces[n] = line[n];
        }

        for (int n = 0; n < sidesRow1.Length; n++)
        {
            spaces[n + line.Length] = sidesRow1[n];
        }

        for (int n = 0; n < sidesRow2.Length; n++)
        {
            spaces[n + line.Length + sidesRow1.Length] = sidesRow2[n];
        }

        TargetSpace[] targetArea = new TargetSpace[spaces.Length];

        bool first = true;

        for (int n = 0; n < spaces.Length; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = spaces[n],
                damage = false,
                stun = false
            };

            if (first)
            {
                targetSpace.value = 0f;
            }
            else
            {
                targetSpace.value = 0f;
            }

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] HealingMist(int currentSpace)
    {
        int[] line = GetRow(currentSpace, 0);
        TargetSpace[] targetArea = new TargetSpace[line.Length];

        for (int n = 0; n < line.Length; n++)
        {
            TargetSpace targetSpace = new TargetSpace
            {
                space = line[n],
                damage = false,
                value = 0.15f,
                stun = false
            };

            targetArea[n] = targetSpace;
        }

        return targetArea;
    }

    public TargetSpace[] HealingElixir(int currentSpace)
    {
        TargetSpace[] targetArea = new TargetSpace[1];

        TargetSpace targetSpace = new TargetSpace
        {
            space = currentSpace,
            damage = false,
            value = 0.18f,
            stun = false
        };

        targetArea[0] = targetSpace;

        return targetArea;
    }

    #endregion

    #endregion

    #region Target Spaces

    public virtual int[] GetColumn(int currentSpace, int spaces)
    {
        int[] lineSpaces = new int[spaces];

        for (int n = 0; n < spaces; n++)
        {
            lineSpaces[n] = currentSpace - (n+1)*3;
        }

        return lineSpaces;
    }

    public virtual int[] GetRow(int currentSpace, int spaceOffset)
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

    public virtual int[] GetSides(int currentSpace, int spaceOffset)
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

    public virtual int[] GetRadius(int currentSpace, int spaceOffset, bool getCenter, bool diagonal)
    {
        List<int> spacesList = new List<int>();

        if (getCenter)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset));
        }

        //Center
        if ((currentSpace - 1) % 3 == 0)
        {
            if (diagonal)
            {
                for (int n = 1; n <= 4; n++)
                {
                    spacesList.Add(currentSpace - (3 * spaceOffset) + n);
                    spacesList.Add(currentSpace - (3 * spaceOffset) - n);
                }
            }
            else
            {
                for (int n = 1; n <= 4; n += 2)
                {
                    spacesList.Add(currentSpace - (3 * spaceOffset) + n);
                    spacesList.Add(currentSpace - (3 * spaceOffset) - n);
                }
            }
        }

        //Left
        if ((currentSpace) % 3 == 0)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset) + 1);
            spacesList.Add(currentSpace - (3 * spaceOffset) + 3);

            spacesList.Add(currentSpace - (3 * spaceOffset) - 3);

            if (diagonal)
            {
                spacesList.Add(currentSpace - (3 * spaceOffset) + 4);

                spacesList.Add(currentSpace - (3 * spaceOffset) - 2);
            }
        }

        //Right
        if ((currentSpace - 2) % 3 == 0)
        {
            spacesList.Add(currentSpace - (3 * spaceOffset) - 1);
            spacesList.Add(currentSpace - (3 * spaceOffset) - 3);

            spacesList.Add(currentSpace - (3 * spaceOffset) + 3);

            if (diagonal)
            {
                spacesList.Add(currentSpace - (3 * spaceOffset) - 4);

                spacesList.Add(currentSpace - (3 * spaceOffset) + 2);
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