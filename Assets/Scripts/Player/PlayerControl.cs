using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControl : MonoBehaviourPunCallbacks, IPunObservable
{
    public Animator anim;
    public float sensity = 0.1f;

    private Transform trans;
    private bool controlLocal = false;
    [SerializeField]
    private GameObject goLocalControl;
    private float localAngle = 0;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    private void Start()
    {
        trans = transform;
        if(PhotonNetwork.OfflineMode)
        {
            controlLocal = true;
        }
        else
        {
            if(photonView.IsMine)
            {
                controlLocal = true;
            }
            else
            {
                controlLocal = false;
            }
        }

        if(controlLocal)
        {
            ViewManager.instance.OnSwitchView(ViewIndex.EmptyView);
            goLocalControl.SetActive(true);
        }
    }

    private void Update()
    {
        if(controlLocal)
        {
            Vector2 moveDir = InputManager.moveDir;
            Vector3 dir = new Vector3(moveDir.x, 0, moveDir.y);
            anim.SetFloat("Move_X", dir.x);
            anim.SetFloat("Move_Y", dir.z);

            trans.Translate(dir * Time.deltaTime * 4);
            float x_Rotate = InputManager.mouseDelta.x;
            localAngle += x_Rotate * sensity;
            Quaternion quaternion = Quaternion.Euler(0, localAngle, 0);
            trans.localRotation = quaternion;
        }
    }
}
