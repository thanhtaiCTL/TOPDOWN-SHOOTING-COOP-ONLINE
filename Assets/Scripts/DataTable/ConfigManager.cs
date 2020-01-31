using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConfigManager : Singleton<ConfigManager>
{
    private ConfigWeapon configWeapon_;
    public ConfigWeapon configWeapon
    {
        get
        {
            return configWeapon_;
        }
    }

    private ConfigSkin configSkin_;
    public ConfigSkin configSkin
    {
        get
        {
            return configSkin_;
        }
    }    

    public void InitConfig(Action callback)
    {
        StartCoroutine(Init(callback));
    }

    IEnumerator Init(Action callback)
    {
        configWeapon_ = Resources.Load("DataTable/ConfigWeapon", typeof(ScriptableObject)) as ConfigWeapon;
        yield return new WaitUntil(() => configWeapon_ != null);
        configSkin_ = Resources.Load("DataTable/ConfigSkin", typeof(ScriptableObject)) as ConfigSkin;
        yield return new WaitUntil(() => configWeapon_ != null);

        callback?.Invoke();
    }
}
