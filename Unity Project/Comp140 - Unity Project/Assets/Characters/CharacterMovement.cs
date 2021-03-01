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
        Dictionary<int, bool> spacesDictionary = GetAbilitySpaces();

        foreach (var item in spacesDictionary)
        {
            if (item.Value)
            {
                board.HighlightSpace(item.Key, Color.red);
            }
            else
            {
                board.HighlightSpace(item.Key, Color.green);
            }
        }
    }

    #endregion

    #region Attack

    public void Attack()
    {
        Dictionary<int, bool> spacesDictionary = GetAbilitySpaces();

        foreach (var item in spacesDictionary)
        {
            Transform attackTransform = board.GetSpace(item.Key);

            if (item.Value)
            {
                //attack
                attackTransform = board.GetSpace(item.Key);
                Instantiate(attackEffect, transform);
            }
            else
            {
                //heal
                attackTransform = board.GetSpace(item.Key);
                //Instantiate(attackEffect, transform);
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

    Dictionary<int, bool> GetAbilitySpaces()
    {
        TargetSpace[] targetSpaces = GetAbility();

        Dictionary<int, bool> spacesDictionary = new Dictionary<int, bool>();
        int n = 0;

        foreach (var space in targetSpaces)
        {
            int spaceNum = space.space;
            bool damage = space.damage;

            spacesDictionary.Add(spaceNum, damage);

            n++;
        }

        return spacesDictionary;
    }

    #endregion
}