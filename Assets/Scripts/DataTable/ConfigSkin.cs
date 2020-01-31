using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigSkinRecord
{
    public int id;
    public string name;
    public string head;
    public string extra;
    public string body;
    public string leg;
    public int hp;
    public string prefab;
}

public class ConfigSkin : BYDataTable<ConfigSkinRecord>
{
    public override void InitComparison()
    {
        recordCompare = new ConfigPrinaryKeyCompare<ConfigSkinRecord>("id");
    }
}
