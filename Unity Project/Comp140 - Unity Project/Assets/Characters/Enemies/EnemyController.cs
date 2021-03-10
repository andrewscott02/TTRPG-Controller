using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    public bool highlightSpaces = true;

    bool stun = false;

    public override void Start()
    {
        character = GetComponent<CharacterAttacks>();

        RandomMoveSpace();
        RandomSpellSelection();
    }

    public override void IdlePosition()
    {

    }

    public void RandomMoveSpace()
    {
        if (!stun)
        {
            int space = Random.Range(0, boardSize);

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
    }

    public override void Move(int spaceIndex)
    {
        if (!stun)
        {
            SetSpace(currentSpace, null);

            Transform setTransform = teamBoard.GetSpace(spaceIndex);
            currentSpace = spaceIndex;
            this.transform.position = setTransform.position;

            SetSpace(currentSpace, this.gameObject);
        }
    }

    public override void SetSpace(int space, GameObject newCharacter)
    {
        Space spaceScript = board.spaces[space].GetComponent<Space>();

        spaceScript.SetSpace(newCharacter);
    }

    public void RandomSpellSelection()
    {
        if (!stun)
        {
            abilityNum = Random.Range(1, 4);

            board.ResetHighlight();

            Invoke("Highlight", 0.15f);
        }
    }

    public override void Highlight()
    {
        if (highlightSpaces &! stun)
            base.Highlight();
    }

    public void Stun()
    {
        stun = true;
    }

    public override void Attack()
    {
        if (!stun)
            base.Attack();
    }
}
