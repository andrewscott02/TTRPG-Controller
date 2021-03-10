using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : EnemyAttacks
{
    private void Start()
    {
        ability1 = WallOfFire;
        ability2 = DrainVitality;
        ability3 = GleamingBlade;
        ability4 = HealingMist;
    }
}
