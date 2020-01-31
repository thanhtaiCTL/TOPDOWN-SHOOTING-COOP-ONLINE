using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MatchView : BaseView
{
    [SerializeField]
    private Text info;

    public PUNConnectControl punConnectControl;
    [NonSerialized]
    public MatchViewParam matchViewParam;

    public override void OnSetup(ViewParam param)
    {
        matchViewParam = (MatchViewParam)param;
        punConnectControl.Connect();
    }

    public void SetInfo(string _message)
    {
        info.text = _message;
    }
}
