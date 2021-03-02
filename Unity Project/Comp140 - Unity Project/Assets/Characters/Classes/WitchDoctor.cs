using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoctor : CharacterAttacks
{
    private void Start()
    {
        ability1 = AcidBomb;
        ability2 = Leach;
        ability3 = HealingMist;
    }
}
