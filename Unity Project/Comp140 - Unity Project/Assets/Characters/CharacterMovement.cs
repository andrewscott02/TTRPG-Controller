using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Setup

    #region Variables

    public FullBoard board;
    public Board teamBoard;
    private int currentSpace;

    [Range(6, 8)]
    public int idleSpace;

    public Object attackEffect;

    private CharacterAttacks character;
    int abilityNum = 1;

    #endregion

    private void Start()
    {
        character = GetComponent<CharacterAttacks>();

        IdlePosition();
    }

    #endregion

    #region Actions

    #region Movement

    public void Move(int spaceIndex)
    {
        SetSpace(currentSpace, null);

        Transform setTransform = teamBoard.GetSpace(spaceIndex);
        currentSpace = spaceIndex + 6;
        this.transform.position = setTransform.position;

        SetSpace(currentSpace, this.gameObject);
    }

    public void IdlePosition()
    {
        SetSpace(currentSpace, null);

        Transform setTransform = teamBoard.GetSpace(idleSpace);
        currentSpace = idleSpace + 6;
        this.transform.position = setTransform.position;

        SetSpace(currentSpace, this.gameObject);
    }

    public void SetSpace(int space, GameObject newCharacter)
    {
        Space spaceScript = board.spaces[space].GetComponent<Space>();

        spaceScript.SetSpace(newCharacter);
    }

    public void Highlight()
    {
        int[] attackSpaces = GetAbilitySpaces();

        for (int n = 0; n < attackSpaces.Length; n++)
        {
            board.HighlightSpace(attackSpaces[n], Color.red);
        }
    }

    #endregion

    #region Attack

    public void Attack()
    {
        int[] attackSpaces = GetAbilitySpaces();
        Transform[] attackTransform = new Transform[attackSpaces.Length];

        for (int n = 0; n < attackSpaces.Length; n++)
        {
            attackTransform[n] = board.GetSpace(attackSpaces[n]);
        }

        foreach (var transform in attackTransform)
        {
            if (transform != null)
            {
                Instantiate(attackEffect, transform);
            }
        }
    }

    #endregion

    public void SelectAbility(int ability)
    {
        abilityNum = ability;

        board.ResetHighlight();

        Invoke("Highlight", 0.15f);
    }

    #endregion

    #region Helper Functions

    TargetSpace[] GetAbility()
    {
        TargetSpace[] targetSpaces = new TargetSpace[10];

        if (abilityNum == 1)
        {
            targetSpaces = character.ability1(currentSpace);
        }
        if (abilityNum == 2)
        {
            targetSpaces = character.ability2(currentSpace);
        }
        if (abilityNum == 3)
        {
            targetSpaces = character.ability3(currentSpace);
        }

        return targetSpaces;
    }

    int[] GetAbilitySpaces()
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();
        targetSpaces = GetAbility();

        int[] spacesArray = new int[targetSpaces.Keys.Count];
        int n = 0;

        foreach (var space in targetSpaces)
        {
            int spaceNum = space.Key;

            spacesArray[n] = spaceNum;

            n++;
        }

        return spacesArray;
    }

    Dictionary<int, bool> GetAbilitySpacesDictionary()
    {
        TargetSpace[] targetSpaces = new TargetSpace[10];

        targetSpaces = GetAbility();

        Dictionary<int, bool> spacesDictionary = new Dictionary<int, bool>();
        int n = 0;

        foreach (var space in targetSpaces)
        {
            int spaceNum = space.Key;
            bool damage = spaceEffect.Key;

            spaceDictionary = space.Value;

            spaceEffect = spaceDictionary.Key;

            bool damage = spaceEffect.Key;

            spacesDictionary.Add(spaceNum, false);

            n++;
        }

        return spacesDictionary;
    }

    #endregion
}