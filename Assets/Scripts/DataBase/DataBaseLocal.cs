using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
using Newtonsoft.Json;

public class DataEventTrigger : UnityEvent<object>
{

}

public static class DataTrigger
{
    public static Dictionary<string, DataEventTrigger> dicOnValueChange = new Dictionary<string, DataEventTrigger>();
    public static void RegisterValueChange(string s, UnityAction<object> delegateDataChange)
    {
        if(dicOnValueChange.ContainsKey(s))
        {
            dicOnValueChange[s].AddListener(delegateDataChange);
        }
        else
        {
            dicOnValueChange.Add(s, new DataEventTrigger());
            dicOnValueChange[s].AddListener(delegateDataChange);
        }
    }

    public static void TriggerEventData(this object data, string path)
    {
        if (dicOnValueChange.ContainsKey(path))
            dicOnValueChange[path].Invoke(data);
    }
}

public class DataBaseLocal : MonoBehaviour
{
    private PlayerData playerData;

    public bool LoadData()
    {
        if(PlayerPrefs.HasKey("DATA"))
        {
            GetData();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GetData()
    {
        string s = PlayerPrefs.GetString("DATA");
        playerData = JsonConvert.DeserializeObject<PlayerData>(s);
    }

    private void SaveData()
    {
        string s = JsonConvert.SerializeObject(playerData, Formatting.None);
        PlayerPrefs.SetString("DATA", s);
    }

    public void CreateNewData(PlayerData data)
    {
        playerData = data;
        SaveData();
    }

    #region ---------------------READ DATA -----------------------------
    public T ReadData<T>(string path)
    {
        object data = null;
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        ReadDataByPath(paths, playerData, out data);

        return (T)data;
    }

    public T ReadData<T>(string path, object key)
    {
        object data = null;
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        ReadDataByPath(paths, playerData, out data);
        Dictionary<string, T> newDic = (Dictionary<string, T>)data;

        return newDic[key.ToKey()];
    }

    private void ReadDataByPath(List<string> paths, object data, out object dataOut)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo fieldInfo = t.GetField(p);
        object dataChild = fieldInfo.GetValue(data);
        if(paths.Count == 1)
        {
            dataOut = dataChild;
        }
        else
        {
            paths.RemoveAt(0);
            ReadDataByPath(paths, dataChild, out dataOut);
        }
    }
    #endregion

    #region ----------------------UPDATE DATA ---------------------------------
    public void UpdateData(string path, object dataNew, Action callback)
    {
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        UpdateDataByPath(paths, playerData, dataNew, callback);
        SaveData();
        dataNew.TriggerEventData(path);
    }

    private void UpdateDataByPath(List<string> paths, object data, object dataNew, Action callback)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo fieldInfo = t.GetField(p);      
        if(paths.Count == 1)
        {
            fieldInfo.SetValue(data, dataNew);
            if (callback != null)
                callback();
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataByPath(paths, fieldInfo.GetValue(data), dataNew, callback);
        }
    }

    public void UpdateDataDic<TValue>(string path, object key, TValue dataNew, Action callback)
    {
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        UpdateDataDicByPath(paths, playerData, key, dataNew, callback);
        SaveData();
        dataNew.TriggerEventData(path);
    }

    private void UpdateDataDicByPath<TValue>(List<string> paths, object data, object key, TValue dataNew, Action callback)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo fieldInfo = t.GetField(p);       
        if(paths.Count == 1)
        {
            object dic = fieldInfo.GetValue(data);
            Dictionary<string, TValue> newDic = (Dictionary<string, TValue>)dic;
            newDic[key.ToKey()] = dataNew;
            fieldInfo.SetValue(data, newDic);
            if (callback != null)
                callback();
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataDicByPath(paths, fieldInfo.GetValue(data), key, dataNew, callback);
        }
    }

    #endregion
}
