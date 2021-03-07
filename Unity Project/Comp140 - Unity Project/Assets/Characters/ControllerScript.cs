using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    #region Setup

    #region Variables

    public GameObject[] characters;
    private GameObject currentCharacter;

    public GameObject[] enemies;

    public FullBoard board;

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
            TryEndTurn();
        }

        //Select Character
        if (Input.GetKeyDown("q"))
        {
            //select first character

            SelectCharacter(null, 0);
        }

        if (Input.GetKeyDown("w"))
        {
            //select second character

            SelectCharacter(null, 1);
        }

        
        if (Input.GetKeyDown("e"))
        {
            //select third character

            SelectCharacter(null, 2);
        }
        

        //Select ability
        if (Input.GetKeyDown("a"))
        {
            SelectSpell(1);
        }

        if (Input.GetKeyDown("s"))
        {
            SelectSpell(2);
        }

        if (Input.GetKeyDown("d"))
        {
            SelectSpell(3);
        }
    }

    #endregion

    #region Actions

    public GameObject CheckCharacter()
    {
        return currentCharacter;
    }

    void Highlight()
    {
        foreach (var space in board.spaces)
        {
            Space spaceScript = space.GetComponent<Space>();

            if (spaceScript.character != null)
            {
                if (spaceScript.character.GetComponent<CharacterController>() != null)
                {
                    CharacterController moveScript = spaceScript.character.GetComponent<CharacterController>();
                    if (!spaceScript.idle)
                    {
                        moveScript.Highlight();
                    }
                }
            }
        }
    }

    #region Movement

    public void Move(int spaceIndex)
    {
        if (currentCharacter != null)
        {
            currentCharacter.GetComponentInChildren<CharacterController>().Move(spaceIndex);

            board.ResetHighlight();

            Highlight();
        }
    }

    public void IdlePosition()
    {
        if (currentCharacter != null)
        {
            currentCharacter.GetComponentInChildren<CharacterController>().IdlePosition();

            board.ResetHighlight();

            Highlight();
        }
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
            currentCharacter.GetComponentInChildren<CharacterController>().SelectAbility(spell);

            board.ResetHighlight();

            Highlight();
        }
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
            EndTurn();
        }
    }

    void EndTurn()
    {
        //get enemies to attack before these are called
        Invoke("EnemyAttack", 0);

        //delay
        Invoke("Attack", 0.01f);

        //delay
        Invoke("EnemyMovement", 0.2f);

        //Enemy change skills
        Invoke("EnemySpellSelection", 0.2f);
    }

    void Attack()
    {
        foreach (var item in characters)
        {
            if (item != null)
                item.GetComponentInChildren<CharacterController>().Attack();
        }
    }

    void EnemyMovement()
    {
        foreach (var item in enemies)
        {
            if (item != null)
            {
                item.GetComponentInChildren<EnemyController>().RandomMoveSpace();

                Debug.Log("EnemyMove");
            }
        }
    }

    void EnemyAttack()
    {
        foreach (var item in enemies)
        {
            if (item != null)
            {
                item.GetComponentInChildren<EnemyController>().Attack();

                Debug.Log("EnemyAttack");
            }
        }
    }

    void EnemySpellSelection()
    {
        foreach (var item in enemies)
        {
            if (item != null)
            {
                item.GetComponentInChildren<EnemyController>().RandomSpellSelection();

                Debug.Log("EnemySelectSpell");
            }
        }

        board.ResetHighlight();

        Highlight();
    }

    #endregion

    #endregion
}