using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogGunSelect : BaseDialog
{
    public Transform parentLayout;
    public ItemGunSelect itemGunSelectPrefab;

    private List<ConfigWeaponRecord> configGun;
    private List<ItemGunSelect> lsItem = new List<ItemGunSelect>();

    public override void OnSetup(DialogParam param)
    {
        if(configGun == null)
        {
            configGun = ConfigManager.instance.configWeapon.GetAll();
            foreach(ConfigWeaponRecord e in configGun)
            {
                ItemGunSelect item = Instantiate(itemGunSelectPrefab);
                item.transform.SetParent(parentLayout);
                lsItem.Add(item);
            }
        }

        for(int i = 0; i < configGun.Count; i++)
        {
            lsItem[i].Setup(configGun[i]);
        }
    }

    public void OnClose()
    {
        DialogManager.instance.HideDialog(this.index);
    }
}
