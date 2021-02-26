using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 3;
    public int height = 3;
    private int boardSize;

    public GameObject[] spaces;

    private void Awake()
    {
        boardSize = height * width;
    }

    public Transform GetSpace(int space)
    {
        return spaces[space].transform.Find("Anchor");
    }
}