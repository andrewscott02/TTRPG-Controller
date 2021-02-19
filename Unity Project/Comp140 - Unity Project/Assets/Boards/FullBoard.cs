using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullBoard : MonoBehaviour
{
    public Board playerBoard;
    public Board enemyBoard;

    public GameObject[] spaces;

    private void Start()
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

    public Transform GetSpace(int space)
    {
        if (spaces[space].transform.Find("Anchor") != null)
        {
            return spaces[space].transform.Find("Anchor");
        }
        return null;
    }

    public void HighlightSpace(int space, Color colour)
    {
        Material mat = spaces[space].GetComponentInChildren<MeshRenderer>().material;
        Debug.Log(space);
        mat.SetColor("_Color", colour);
    }

    public void ResetHighlight()
    {
        foreach (var space in spaces)
        {
            Material mat = space.GetComponentInChildren<MeshRenderer>().material;
            mat.SetColor("_Color", Color.white);
        }
    }
}