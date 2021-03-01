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

    public void HighlightSpace(int space, Color colour)
    {
        if (IsSpaceValid(space))
        {
            Material mat = spaces[space].GetComponentInChildren<MeshRenderer>().material;
            mat.SetColor("_Color", colour);
        }
    }

    public void ResetHighlight()
    {
        foreach (var space in spaces)
        {
            Material mat = space.GetComponentInChildren<MeshRenderer>().material;
            mat.SetColor("_Color", Color.white);
        }
    }

    #endregion
}