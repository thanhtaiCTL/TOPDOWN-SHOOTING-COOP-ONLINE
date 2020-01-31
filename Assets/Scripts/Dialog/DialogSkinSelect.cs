using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSkinSelect : BaseDialog
{
    public Transform parentLayout;
    public ItemSkinSelect itemSkinPrefab;

    private List<ConfigSkinRecord> configSkin;
    private List<ItemSkinSelect> lsItem = new List<ItemSkinSelect>();

    public override void OnSetup(DialogParam param)
    {
        if(configSkin == null)
        {
            configSkin = ConfigManager.instance.configSkin.GetAll();
            foreach(ConfigSkinRecord e in configSkin)
            {
                ItemSkinSelect item = Instantiate(itemSkinPrefab);
                item.transform.SetParent(parentLayout);
                item.Setup(e);
                lsItem.Add(item);
            }
        }       
    }

    public void OnClose()
    {
        DialogManager.instance.HideDialog(this.index);
    }
}
