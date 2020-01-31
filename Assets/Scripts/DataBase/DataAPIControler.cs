using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataAPIControler : Singleton<DataAPIControler>
{
    [SerializeField]
    private DataBaseLocal dataModel;

    public void OnInit(Action callback)
    {
        if(dataModel.LoadData())
        {
            callback?.Invoke();
        }
        else
        {            
            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.username = "TAI";
            playerInfo.exp = 0;
            playerInfo.level = 1;
            playerInfo.idSkin = 1;
            playerInfo.idGun = 1;            

            PlayerInventory playerInventory = new PlayerInventory();
            playerInventory.cash = 100;
            playerInventory.gold = 50;

            WeaponData weaponData = new WeaponData();
            weaponData.id = 1;
            weaponData.level = 1;

            PlayerData playerData = new PlayerData();
            playerData.playerInfo = playerInfo;
            playerData.playerInventory = playerInventory;            

            dataModel.CreateNewData(playerData);
            dataModel.UpdateDataDic<WeaponData>(DataPath.PLAYER_WEAPON, 1, weaponData, callback);
        }
    }

    public PlayerInfo GetPlayerInfo()
    {
        return dataModel.ReadData<PlayerInfo>(DataPath.PLAYER_INFO);
    }

    public PlayerInventory GetPlayerInventory()
    {
        return dataModel.ReadData<PlayerInventory>(DataPath.PLAYER_INVENTORY);
    }

    public WeaponData GetWeaponData(int id)
    {
        return dataModel.ReadData<WeaponData>(DataPath.PLAYER_WEAPON, id);
    }

    public void ChangeGun(int id, Action callback)
    {
        dataModel.UpdateData(DataPath.PLAYER_INFO_WEAPON, id, callback);
    }

    public void ChangeSkin(int id, Action callback)
    {
        dataModel.UpdateData(DataPath.PLAYER_INFO_SKIN, id, callback);
    }
}
