﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region Setup

    #region Variables

    public FullBoard board;
    public Board teamBoard;
    protected int currentSpace;

    protected int boardSize = 9;

    [Range(9, 12)]
    public int idleSpace;

    public CharacterAttacks character;
    protected int abilityNum = 1;

    #endregion

    public virtual void Start()
    {
        character = GetComponent<CharacterAttacks>();

        IdlePosition();
    }

    #endregion

    #region Actions

    #region Movement

    public virtual void Move(int spaceIndex)
    {
        if (currentSpace == spaceIndex + boardSize)
            return;

        SetSpace(currentSpace, null);

        Transform setTransform = teamBoard.GetSpace(spaceIndex);
        currentSpace = spaceIndex + boardSize;
        this.transform.position = setTransform.position;

        SetSpace(currentSpace, this.gameObject);
    }

    public virtual void IdlePosition()
    {
        if (currentSpace == idleSpace)
            return;

        SetSpace(currentSpace, null);

        Transform setTransform = teamBoard.GetSpace(idleSpace);
        currentSpace = idleSpace + boardSize;
        this.transform.position = setTransform.position;

        SetSpace(currentSpace, this.gameObject);
    }

    public virtual void SetSpace(int space, GameObject newCharacter)
    {
        Space spaceScript = board.spaces[space].GetComponent<Space>();

        spaceScript.SetSpace(newCharacter);
    }

    #endregion

    #region Attack

    public virtual void Attack()
    {
        TargetSpace[] targetSpaces = GetAbility();

        foreach (var item in targetSpaces)
        {
            if (item.damage)
            {
                //attack
                board.Attack(item.space, item.value);
            }
            else
            {
                //heal
                board.Heal(item.space, item.value);
            }

            if (item.stun)
            {
                //stun
                board.Stun(item.space);
            }
        }
    }

    public virtual void Highlight()
    {
        Dictionary<int, bool> spacesDictionary = GetAbilitySpaces();

        foreach (var item in spacesDictionary)
        {
            if (item.Value)
            {
                board.HighlightSpace(item.Key, 1, 0);
            }
            else
            {
                board.HighlightSpace(item.Key, 0, 1);
            }
        }
    }

    public void SelectAbility(int ability)
    {
        abilityNum = ability;

        board.ResetHighlight();

        Invoke("Highlight", 0.15f);
    }

    #endregion

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
        if (abilityNum == 4)
        {
            targetSpaces = character.ability4(currentSpace);
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