using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    public GameObject character;
    public bool idle = false;

    private FullBoard board;

    public bool damage = false;
    public bool heal = false;

    private void Start()
    {
        board = GameObject.Find("Board").GetComponent<FullBoard>();
    }

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

    public void SetHighlight(bool newDamage, bool newHeal)
    {
        if (newDamage)
        {
            damage = newDamage;
        }
        
        if (newHeal)
        {
            heal = newHeal;
        }

        if (heal && damage)
        {
            HighlightColour(Color.magenta);
        }
        else if (heal && !damage)
        {
            HighlightColour(Color.green);
        }
        else if (damage && !heal)
        {
            HighlightColour(Color.red);
        }
        else
        {
            HighlightColour(Color.white);
        }
    }

    public void HighlightColour(Color colour)
    {
        Material mat = GetComponentInChildren<MeshRenderer>().material;
        mat.SetColor("_Color", colour);
    }
}
