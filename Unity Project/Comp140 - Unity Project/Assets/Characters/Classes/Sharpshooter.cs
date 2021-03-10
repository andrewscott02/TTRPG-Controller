using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharpshooter : CharacterAttacks
{
    private void Start()
    {
        ability1 = SerpentineStrike;
        ability2 = VenomBite;
        ability3 = Flamethrower;
        ability4 = HealingElixir;
    }
}