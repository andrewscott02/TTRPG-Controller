using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyromancer : CharacterAttacks
{
    private void Start()
    {
        ability1 = Disintegration;
        ability2 = WallOfFire;
        ability3 = Flamethrower;
        ability4 = HealingFlames;
    }
}
