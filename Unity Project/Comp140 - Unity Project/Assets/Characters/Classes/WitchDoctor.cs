using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoctor : CharacterAttacks
{
    private void Start()
    {
        ability1 = AcidBomb;
        ability2 = DrainVitality;
        ability3 = Entangle;
        ability4 = HealingMist;
    }
}
