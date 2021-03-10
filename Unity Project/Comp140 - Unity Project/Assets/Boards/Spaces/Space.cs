using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    #region Setup

    #region Variables

    public GameObject character;
    private Health health;

    public bool idle = false;

    private FullBoard board;

    public int damage = 0;
    public int heal = 0;

    public Transform anchor;

    public Object attackHighlight;
    public Object healHighlight;
    public Object bothHighlight;

    public Object attackEffect;
    public Object healEffect;

    private List<GameObject> highlights = new List<GameObject>();

    #endregion

    private void Start()
    {
        board = GameObject.Find("Board").GetComponent<FullBoard>();

        if (character != null)
        {
            health = character.GetComponent<Health>();
        }
    }

    #endregion

    #region Space Selection

    public bool GetSpace()
    {
        if (character == null)
            return true;

        return false;
    }

    public void SetSpace(GameObject newCharacter)
    {
        character = newCharacter;

        if (character != null)
        {
            health = character.GetComponent<Health>();
        }
    }

    #endregion

    #region Highlight

    public void SetHighlight(int newDamage, int newHeal)
    {
        //ResetHighlight();

        damage += newDamage;

        heal += newHeal;
        
        if (heal > 0 && damage > 0)
        {
            ResetHighlight();

            int lowest = heal;

            if (damage < heal)
                lowest = damage;

            for (int n = 0; n < lowest; n++)
            {
                //add heal highlight
                GameObject effect = Instantiate(bothHighlight, anchor) as GameObject;
                effect.transform.position = anchor.position;
                highlights.Add(effect);
            }
        }
        else if (heal > 0 && !(damage > 0))
        {
            for (int n = 0; n < heal; n++)
            {
                //add heal highlight
                GameObject health = Instantiate(healHighlight, anchor) as GameObject;
                health.transform.position = anchor.position;
                highlights.Add(health);
            }
        }
        else if (damage > 0 && !(heal > 0))
        {
            for (int n = 0; n < damage; n++)
            {
                //add damage highlight
                GameObject attack = Instantiate(attackHighlight, anchor) as GameObject;
                attack.transform.position = anchor.position;
                highlights.Add(attack);
            }
        }
    }

    public void ResetHighlight()
    {
        foreach (var item in highlights)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }

        highlights.Clear();
    }

    public void HighlightColour(Color colour)
    {
        Material mat = GetComponentInChildren<MeshRenderer>().material;
        mat.SetColor("_Color", colour);
    }

    #endregion

    #region Activate Abilities

    public void Attack(float damage)
    {
        Instantiate(attackEffect, transform);

        if (character != null)
        {
            health = character.GetComponent<Health>();

            health.TakeDamage(damage);
        }
    }

    public void Heal(float heal)
    {
        Instantiate(healEffect, transform);

        if (character != null)
        {
            health = character.GetComponent<Health>();

            health.Heal(heal);
        }
    }

    public void Stun()
    {
        Instantiate(healEffect, transform);

        if (character != null)
        {
            EnemyController controller = character.GetComponent<EnemyController>();

            controller.Stun();
        }
    }

    #endregion
}