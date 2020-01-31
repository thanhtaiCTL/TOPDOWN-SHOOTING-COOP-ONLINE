using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogIndex
{
    DialogPause = 1,
    DialogGunSelect = 2,
    DialogSkinSelect = 3
}

public class DialogConfig
{
    public static DialogIndex[] dialogIndexes = { 
        DialogIndex.DialogGunSelect,
        DialogIndex.DialogSkinSelect
    };
}
