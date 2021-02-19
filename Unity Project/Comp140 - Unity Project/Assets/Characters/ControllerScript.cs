﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public FullBoard board;
    public Board teamBoard;
    private int currentSpace;

    public Object attackEffect;

    private CharacterAttacks character;
    int abilityNum = 1;

    private void Start()
    {
        character = GetComponent<CharacterAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Numbers

        if (Input.GetKeyDown("0"))
        {
            Debug.Log("KeyPressed");

            Move(0);
        }

        if (Input.GetKeyDown("1"))
        {
            Debug.Log("KeyPressed");

            Move(1);
        }

        if (Input.GetKeyDown("2"))
        {
            Debug.Log("KeyPressed");

            Move(2);
        }

        if (Input.GetKeyDown("3"))
        {
            Debug.Log("KeyPressed");

            Move(3);
        }

        if (Input.GetKeyDown("4"))
        {
            Debug.Log("KeyPressed");

            Move(4);
        }

        if (Input.GetKeyDown("5"))
        {
            Debug.Log("KeyPressed");

            Move(5);
        }

        if (Input.GetKeyDown("6"))
        {
            Debug.Log("KeyPressed");

            Move(6);
        }

        if (Input.GetKeyDown("7"))
        {
            Debug.Log("KeyPressed");

            Move(7);
        }

        if (Input.GetKeyDown("8"))
        {
            Debug.Log("KeyPressed");

            Move(8);
        }

        #endregion

        if (Input.GetKeyDown("9"))
        {
            Debug.Log("End Turn");

            Attack();
        }

        //Select ability
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("choose ability a");

            abilityNum = 1;
        }

        if (Input.GetKeyDown("a"))
        {
            Debug.Log("choose ability a");

            abilityNum = 2;
        }
    }

    void Move(int spaceIndex)
    {
        Transform setTransform = teamBoard.GetSpace(spaceIndex);
        currentSpace = spaceIndex + 9;
        this.transform.position = setTransform.position;

        board.ResetHighlight();

        Invoke("Highlight", 0.15f);
    }

    void Highlight()
    {
        int[] attackSpaces = new int[3];

        attackSpaces[0] = currentSpace - 3;
        attackSpaces[1] = currentSpace - 6;
        attackSpaces[2] = currentSpace - 9;

        foreach (var space in attackSpaces)
        {
            board.HighlightSpace(space, Color.red);
        }
    }

    void Attack()
    {
        Transform[] attackTransform = GetLineTransforms();

        foreach (var transform in attackTransform)
        {
            if (transform != null)
            {
                Instantiate(attackEffect, transform);
            }
        }
    }

    Transform[] GetLineTransforms()
    {
        Transform[] lineTransform = new Transform[3];

        lineTransform[0] = board.GetSpace(currentSpace - 3);
        lineTransform[1] = board.GetSpace(currentSpace - 6);
        lineTransform[2] = board.GetSpace(currentSpace - 9);

        return lineTransform;
    }

    Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> GetAbility()
    {
        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

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
        List<int> spaces = new List<int>();

        Dictionary<int, Dictionary<Dictionary<bool, int>, bool>> targetSpaces = new Dictionary<int, Dictionary<Dictionary<bool, int>, bool>>();

        targetSpaces = GetAbility();

        //get first int from target space dictionaries, add them to a list, add list to array

        int[] spacesArray = new int[spaces.Count];

        return spacesArray;
    }
}