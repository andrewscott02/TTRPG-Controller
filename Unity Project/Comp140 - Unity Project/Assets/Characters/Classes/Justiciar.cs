﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Justiciar : CharacterAttacks
{
    private void Start()
    {
        ability1 = GleamingBlade;
        ability2 = SearingStrike;
        ability3 = CleanseWounds;
        ability4 = FaithfulWard;
    }
}