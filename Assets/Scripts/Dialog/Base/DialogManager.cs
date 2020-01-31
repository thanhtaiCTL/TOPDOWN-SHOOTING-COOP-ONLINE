﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogManager : Singleton<DialogManager>
{
    public RectTransform anchorParent;

    private Dictionary<DialogIndex, BaseDialog> dicDialog = new Dictionary<DialogIndex, BaseDialog>();
    private List<BaseDialog> lsDialog = new List<BaseDialog>();

    private void Start()
    {
        foreach(DialogIndex e in DialogConfig.dialogIndexes)
        {
            string nameDialog = e.ToString();
            GameObject goDialog = Instantiate(Resources.Load("Dialog/" + nameDialog, typeof(GameObject))) as GameObject;
            goDialog.transform.SetParent(anchorParent, false);
            BaseDialog dialog = goDialog.GetComponent<BaseDialog>();
            dicDialog[e] = dialog;
            dialog.gameObject.SetActive(false);
        }
    }

    public void ShowDialog(DialogIndex index, DialogParam param = null, Action callback = null)
    {
        BaseDialog dialog = dicDialog[index];
        dialog.gameObject.SetActive(true);
        dialog.GetComponent<RectTransform>().SetAsFirstSibling();
        dialog.OnSetup(param);
        dialog.OnShow(callback);
        lsDialog.Add(dialog);
    }

    public void HideDialog(DialogIndex index, Action callback = null)
    {
        BaseDialog dialog = dicDialog[index];
        dialog.OnHide(callback);
        lsDialog.Remove(dialog);
        dialog.gameObject.SetActive(false);
    }

    public void HideAllDialog(DialogIndex index, Action callback = null)
    {
        foreach(BaseDialog e in lsDialog)
        {
            e.OnHide(callback);
            e.gameObject.SetActive(false);
        }
        lsDialog.Clear();
    }
}
