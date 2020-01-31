using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkinSelect : MonoBehaviour
{
    public Image icon;
    public Text skinName;
    public Text skinHP;

    private ConfigSkinRecord configSkin;

    public void Setup(ConfigSkinRecord cfSkin)
    {
        if(this.configSkin != cfSkin)
        {
            this.configSkin = cfSkin;
            skinName.text = cfSkin.name;
            skinHP.text = cfSkin.hp.ToString();
            icon.overrideSprite = Resources.Load("Icon/" + cfSkin.prefab, typeof(Sprite)) as Sprite;
        }
    }

    public void OnSelectSkin()
    {
        HomeView homeView = (HomeView)ViewManager.instance.currentView;
        homeView.SetSkin(configSkin.id);
    }
}
