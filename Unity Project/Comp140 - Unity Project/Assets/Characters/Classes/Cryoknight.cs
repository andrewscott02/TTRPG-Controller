using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cryoknight : CharacterAttacks
{
    private void Start()
    {
        ability1 = ConeOfCold;
        ability2 = FrostBite;
        ability3 = FrozenArmour;
    }
}
