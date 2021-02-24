using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    #region Setup

    #region Variables

    public FullBoard board;
    public Board teamBoard;
    private int currentSpace;

    public Object attackEffect;

    private CharacterAttacks character;
    int abilityNum = 1;

    #endregion

    private void Start()
    {
        character = GetComponent<CharacterAttacks>();
    }

    #endregion

    #region Inputs

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

            board.ResetHighlight();

            Invoke("Highlight", 0.15f);
        }

        if (Input.GetKeyDown("s"))
        {
            Debug.Log("choose ability s");

            abilityNum = 2;

            board.ResetHighlight();

            Invoke("Highlight", 0.15f);
        }
    }

    #endregion

    #region Actions

    #region Movement

    public void Move(int spaceIndex)
    {
        Transform setTransform = teamBoard.GetSpace(spaceIndex);
        currentSpace = spaceIndex + 9;
        this.transform.position = setTransform.position;

        board.ResetHighlight();

        Invoke("Highlight", 0.15f);
    }

    void Highlight()
    {
        int[] attackSpaces = GetAbilitySpaces();

        for (int n = 0; n < attackSpaces.Length; n++)
        {
            board.HighlightSpace(attackSpaces[n], Color.red);
        }
    }

    #endregion

    #region Attack

    void Attack()
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

    #endregion

    #region Helper Functions

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

    #endregion
}