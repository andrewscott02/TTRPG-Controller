using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public GameObject character;
    public bool idle = false;

    public bool GetSpace()
    {
        if (character == null)
            return true;

        return false;
    }

    public void SetSpace(GameObject newCharacter)
    {
        character = newCharacter;
    }
}
