﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigWeaponRecord
{
    public int id;
    public string name;
    public string prefab;
    public int damage;
    public float rof;
    public int clipsize;
}

public class ConfigWeapon : BYDataTable<ConfigWeaponRecord>
{
    public override void InitComparison()
    {
        recordCompare = new ConfigPrinaryKeyCompare<ConfigWeaponRecord>("id");
    }
}
