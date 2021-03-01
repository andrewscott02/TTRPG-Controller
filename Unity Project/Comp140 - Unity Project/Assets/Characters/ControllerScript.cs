using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    #region Setup

    #region Variables

    public GameObject[] characters;
    private GameObject currentCharacter;

    public FullBoard board;

    #endregion

    private void Start()
    {
        //currentCharacter = characters[0];
    }

    #endregion

    public GameObject CheckCharacter()
    {
        return currentCharacter;
    }

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
            TryEndTurn();
        }

        //Select Character
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("choose character q");

            //select first character

            SelectCharacter(null, 0);
        }

        if (Input.GetKeyDown("w"))
        {
            Debug.Log("choose character w");

            //select first character

            SelectCharacter(null, 1);
        }

        /*
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("choose character e");

            //select first character

            currentCharacter = characters[2];
        }
        */

        //Select ability
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("choose ability a");

            SelectSpell(1);
        }

        if (Input.GetKeyDown("s"))
        {
            Debug.Log("choose ability s");

            SelectSpell(2);
        }

        if (Input.GetKeyDown("d"))
        {
            Debug.Log("choose ability d");

            SelectSpell(3);
        }
    }

    #endregion

    #region Actions

    void Highlight()
    {
        foreach (var space in board.spaces)
        {
            Space spaceScript = space.GetComponent<Space>();

            if (spaceScript.character != null)
            {
                CharacterMovement moveScript = spaceScript.character.GetComponent<CharacterMovement>();
                if (!spaceScript.idle)
                {
                    moveScript.Highlight();
                }
            }
        }
    }

    #region Movement

    public void Move(int spaceIndex)
    {
        if (currentCharacter != null)
        {
            currentCharacter.GetComponentInChildren<CharacterMovement>().Move(spaceIndex);
        }

        board.ResetHighlight();

        Highlight();
    }

    public void IdlePosition()
    {
        if (currentCharacter != null)
        {
            currentCharacter.GetComponentInChildren<CharacterMovement>().IdlePosition();
        }

        board.ResetHighlight();

        Highlight();
    }

    #endregion

    #region Character & Spell Selection

    public void SelectCharacter(GameObject characterRef, int character)
    {
        if (characterRef != null)
        {
            foreach (var item in characters)
            {
                if (item == characterRef)
                {
                    currentCharacter = item;
                }
            }
        }
        else
        {
            currentCharacter = characters[character];
        }
    }

    public void SelectSpell(int spell)
    {
        if (currentCharacter != null)
        {
            currentCharacter.GetComponentInChildren<CharacterMovement>().SelectAbility(spell);
        }

        board.ResetHighlight();

        Highlight();
    }

    #endregion

    #region End Turn

    void TryEndTurn()
    {
        bool canEndTurn = true;
        foreach (var space in board.spaces)
        {
            Space spaceScript = space.GetComponent<Space>();

            if (spaceScript.character != null)
            {
                if (spaceScript.idle)
                {
                    canEndTurn = false;
                }
            }
        }

        if (canEndTurn)
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("EndTurn");
        foreach (var item in characters)
        {
            item.GetComponentInChildren<CharacterMovement>().Attack();
        }
    }

    #endregion

    #endregion
}