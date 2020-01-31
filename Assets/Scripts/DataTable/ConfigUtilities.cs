using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class ConfigUtilities : MonoBehaviour
{
   
}

public class ConfigPrinaryKeyCompare<T2> : ConfigCompare<T2> where T2 : class, new()
{
    private FieldInfo fieldInfo;

    public ConfigPrinaryKeyCompare(string keyFieldName)
    {
        fieldInfo = typeof(T2).GetField(keyFieldName);
    }

    public override int ICompareHandle(T2 x, T2 y)
    {
        var xValue = fieldInfo.GetValue(x);
        var yValue = fieldInfo.GetValue(y);

        if (xValue == null && yValue == null)
            return 0;
        else if (xValue != null && yValue == null)
            return 1;
        else if (xValue == null && yValue != null)
            return -1;
        else
            return ((IComparable)xValue).CompareTo(yValue);
    }

    public override T2 SetKeySearch(object key)
    {
        T2 data = new T2();
        fieldInfo.SetValue(data, key);
        return data;
    }
}
