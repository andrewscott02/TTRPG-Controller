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
            Debug.Log("Both");
        }
        else if (heal && !damage)
        {
            HighlightColour(Color.green);
            Debug.Log("Heal");
        }
        else if (damage && !heal)
        {
            HighlightColour(Color.red);
            Debug.Log("Damage");
        }
        else
        {
            HighlightColour(Color.white);
            Debug.Log("None");
        }
    }

    public void HighlightColour(Color colour)
    {
        Debug.Log(colour);
        Material mat = GetComponentInChildren<MeshRenderer>().material;
        mat.SetColor("_Color", colour);
    }
}
