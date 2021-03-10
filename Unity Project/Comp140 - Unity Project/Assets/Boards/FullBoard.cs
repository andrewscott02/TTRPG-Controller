using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullBoard : MonoBehaviour
{
    #region Setup

    #region Variables

    public Board playerBoard;
    public Board enemyBoard;

    public GameObject[] spaces;

    #endregion

    private void Awake()
    {
        GameObject[] playerSpaces = playerBoard.spaces;
        GameObject[] enemySpaces = enemyBoard.spaces;

        spaces = new GameObject[playerSpaces.Length + enemySpaces.Length];
        
        for (int n = 0; n < enemySpaces.Length; n++)
        {
            spaces[n] = enemySpaces[n];
        }

        for (int n = 0; n < playerSpaces.Length; n++)
        {
            spaces[n + enemySpaces.Length] = playerSpaces[n];
        }
    }

    #endregion

    #region Space Checks

    public Transform GetSpace(int space)
    {
        if (IsSpaceValid(space))
        {
            return spaces[space].transform.Find("Anchor");
        }
        return null;
    }

    public bool IsSpaceValid(int space)
    {
        return space >= 0 && space < (spaces.Length - 2);
    }

    #endregion

    #region Highlight Spaces

    public void HighlightSpace(int space, int damage, int heal)
    {
        //Debug.Log("highlight");
        if (IsSpaceValid(space))
        {
            Space spaceScript = spaces[space].GetComponent<Space>();
            spaceScript.SetHighlight(damage, heal);
        }
    }

    public void ResetHighlight()
    {
        //Debug.Log("resethighlight");
        foreach (var space in spaces)
        {
            Space spaceScript = space.GetComponent<Space>();
            spaceScript.damage = 0;
            spaceScript.heal = 0;

            spaceScript.ResetHighlight();
        }
    }

    #endregion

    public void Attack(int space, float damage)
    {
        if (IsSpaceValid(space))
        {
            Space spaceScript = spaces[space].GetComponent<Space>();
            spaceScript.Attack(damage);
        }
    }

    public void Heal(int space, float heal)
    {
        if (IsSpaceValid(space))
        {
            Space spaceScript = spaces[space].GetComponent<Space>();
            spaceScript.Heal(heal);
        }
    }

    public void Stun(int space)
    {
        if (IsSpaceValid(space))
        {
            Space spaceScript = spaces[space].GetComponent<Space>();
            spaceScript.Stun();
        }
    }
}