using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    #region Setup

    #region Variables

    public GameObject[] characters;

    private GameObject currentCharacter;

    #endregion

    #endregion

    #region Inputs

    // Update is called once per frame
    void Update()
    {
        #region Numbers
        /*
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
        */
        #endregion

        if (Input.GetKeyDown("j"))
        {
            Debug.Log("End Turn");

            Attack();
        }

        //Select Character
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("choose character q");

            //select first character

            currentCharacter = characters[0];
        }

        //Select ability
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("choose ability a");

            currentCharacter.GetComponentInChildren<CharacterMovement>().SelectAbility(1);
        }

        if (Input.GetKeyDown("s"))
        {
            Debug.Log("choose ability s");

            currentCharacter.GetComponentInChildren<CharacterMovement>().SelectAbility(2);
        }
    }

    #endregion

    #region Actions

    #region Movement

    public void Move(int spaceIndex)
    {
        currentCharacter.GetComponentInChildren<CharacterMovement>().Move(spaceIndex);
    }

    public void IdlePosition()
    {
        currentCharacter.GetComponentInChildren<CharacterMovement>().IdlePosition();
    }

    #endregion

    #region Attack

    void Attack()
    {
        foreach (var item in characters)
        {
            item.GetComponentInChildren<CharacterMovement>().Attack();
        }
    }

    #endregion

    #endregion
}