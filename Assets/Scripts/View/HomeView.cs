using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : BaseView
{
    [Header("----------SKIN----------")]
    public Text nameSkin;
    public Text hpSkin;

    [Header("----------GUN----------")]
    public Text gunName;
    public Text damage;
    public Image damageAmount;
    public Text clipSize;
    public Image clipSizeAmount;
    public Text rof;
    public Image rofAmount;

    private ConfigWeaponRecord cfGun;
    private ConfigSkinRecord cfSkin;

    public override void OnSetup(ViewParam param)
    {
        PlayerInfo playerInfo = DataAPIControler.instance.GetPlayerInfo();

        cfGun = ConfigManager.instance.configWeapon.GetRecordByKeySearch(playerInfo.idGun);
        ChangeGunInfoUI(cfGun);
        DataTrigger.RegisterValueChange(DataPath.PLAYER_INFO_WEAPON, (idGun) =>
        {
            cfGun = ConfigManager.instance.configWeapon.GetRecordByKeySearch(idGun);
            ChangeGunInfoUI(cfGun);
        });

        cfSkin = ConfigManager.instance.configSkin.GetRecordByKeySearch(playerInfo.idSkin);
        ChangeSkinInfoUI(cfSkin);
        DataTrigger.RegisterValueChange(DataPath.PLAYER_INFO_SKIN, (idSkin) =>
        {
            cfSkin = ConfigManager.instance.configSkin.GetRecordByKeySearch(idSkin);
            ChangeSkinInfoUI(cfSkin);
        });
    }

    public void OnGunSelect()
    {
        DialogManager.instance.ShowDialog(DialogIndex.DialogGunSelect);
    }

    public void OnSkinSelect()
    {
        DialogManager.instance.ShowDialog(DialogIndex.DialogSkinSelect);
    }

    public void SetGun(int id)
    {
        DataAPIControler.instance.ChangeGun(id, null);
    }

    public void SetSkin(int id)
    {
        DataAPIControler.instance.ChangeSkin(id, null);
    }

    private void ChangeGunInfoUI(ConfigWeaponRecord cfWeapon)
    {
        this.cfGun = cfWeapon;
        gunName.text = cfWeapon.name;
        damage.text = cfWeapon.damage.ToString();
        damageAmount.fillAmount = (float)cfWeapon.damage / 30.0f;

        clipSize.text = cfWeapon.clipsize.ToString();
        clipSizeAmount.fillAmount = (float)cfWeapon.clipsize / 100.0f;

        rof.text = cfWeapon.rof.ToString();
        rofAmount.fillAmount = 0.1f / cfWeapon.rof;
    }

    private void ChangeSkinInfoUI(ConfigSkinRecord configSkin)
    {
        this.cfSkin = configSkin;
        nameSkin.text = configSkin.name;
        hpSkin.text = configSkin.hp.ToString();
    }

    public void OnFight()
    {
        MatchViewParam param = new MatchViewParam();
        param.configGun = this.cfGun;
        ViewManager.instance.OnSwitchView(ViewIndex.MatchView, param);
    }
}
