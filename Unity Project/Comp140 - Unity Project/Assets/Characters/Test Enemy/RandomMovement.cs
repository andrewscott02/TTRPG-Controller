using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public FullBoard board;
    public Board teamBoard;
    private int currentSpace;

    private void Start()
    {
        RandomMoveSpace();
    }

    public void RandomMoveSpace()
    {
        int space = Random.Range(0, 6);

        Space script = board.spaces[space].GetComponent<Space>();

        if (board.IsSpaceValid(space) && script.GetSpace())
        {
            Move(space);
        }
        else
        {
            RandomMoveSpace();
        }
    }

    public void Move(int spaceIndex)
    {
        SetSpace(currentSpace, null);

        Transform setTransform = teamBoard.GetSpace(spaceIndex);
        currentSpace = spaceIndex;
        this.transform.position = setTransform.position;

        SetSpace(currentSpace, this.gameObject);
    }

    public void SetSpace(int space, GameObject newCharacter)
    {
        Space spaceScript = board.spaces[space].GetComponent<Space>();

        spaceScript.SetSpace(newCharacter);
    }
}