using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUISkin : Singleton<PlayerUISkin>
{
    public Animator anim;
    public Transform gunAnchor;

    private Dictionary<string, GameObject> dicBody = new Dictionary<string, GameObject>();
    private GameObject head, body, extra, leg, gun;
    private Dictionary<string, GameObject> dicGun = new Dictionary<string, GameObject>();
    private ConfigWeaponRecord configGun;
    private ConfigSkinRecord configSkin;

    private void Start()
    {
        foreach(SkinnedMeshRenderer e in gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true))
        {
            dicBody.Add(e.name, e.gameObject);
            e.gameObject.SetActive(false);
        }

        PlayerInfo playerInfo = DataAPIControler.instance.GetPlayerInfo();

        configGun = ConfigManager.instance.configWeapon.GetRecordByKeySearch(playerInfo.idGun);
        SetGun(configGun);
        DataTrigger.RegisterValueChange(DataPath.PLAYER_INFO_WEAPON, (idGun) =>
        {
            configGun = ConfigManager.instance.configWeapon.GetRecordByKeySearch((int)idGun);
            SetGun(configGun);
        });

        configSkin = ConfigManager.instance.configSkin.GetRecordByKeySearch(playerInfo.idSkin);
        SetBody(configSkin);
        DataTrigger.RegisterValueChange(DataPath.PLAYER_INFO_SKIN, (idSkin) =>
        {
            configSkin = ConfigManager.instance.configSkin.GetRecordByKeySearch((int)idSkin);
            SetBody(configSkin);
        });
    }

    public void SetGun(ConfigWeaponRecord cfGun)
    {
        if(gun != null)
        {
            gun.SetActive(false);
        }

        if(dicGun.ContainsKey(cfGun.prefab))
        {
            gun = dicGun[cfGun.prefab];            
        }
        else
        {
            GameObject goGun = Instantiate(Resources.Load("Weapon/" + cfGun.prefab, typeof(GameObject))) as GameObject;
            gun = goGun;
            gun.transform.SetParent(gunAnchor);
            gun.transform.localPosition = Vector3.zero;
            gun.transform.localRotation = Quaternion.identity;
            gun.transform.localScale = Vector3.one;
            dicGun.Add(cfGun.prefab, gun);           
        }
        gun.SetActive(true);      
    }

    public void SetBody(ConfigSkinRecord cfSkin)
    {
        if (head != null)
            head.SetActive(false);
        if (extra != null)
            extra.SetActive(false);
        if (body != null)
            body.SetActive(false);
        if (leg != null)
            leg.SetActive(false);

        if (dicBody.ContainsKey(cfSkin.head))
            head = dicBody[cfSkin.head];
        if (dicBody.ContainsKey(cfSkin.extra))
            extra = dicBody[cfSkin.extra];
        if (dicBody.ContainsKey(cfSkin.body))
            body = dicBody[cfSkin.body];
        if (dicBody.ContainsKey(cfSkin.leg))
            leg = dicBody[cfSkin.leg];

        head.SetActive(true);
        extra.SetActive(true);
        body.SetActive(true);
        leg.SetActive(true);
    }
}
