using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGunSelect : MonoBehaviour
{
    public Image icon;
    public Text gunName;
    public Text damage;

    private ConfigWeaponRecord configGun;

   public void Setup(ConfigWeaponRecord cfGun)
    {
        if(this.configGun != cfGun)
        {
            this.configGun = cfGun;
            gunName.text = cfGun.name;
            damage.text = cfGun.damage.ToString();
            icon.overrideSprite = Resources.Load("Icon/" + cfGun.prefab.ToString(), typeof(Sprite)) as Sprite;            
        }
    }

    public void OnSelectGun()
    {
        HomeView homeView = (HomeView)ViewManager.instance.currentView;
        homeView.SetGun(configGun.id);        
    }
}
