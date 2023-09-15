using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(PhotonView))]
public class NetworkPlayer : MonoBehaviour {
    public static NetworkPlayer _localPlayerInstance;

    private PhotonView _myPhotonView;
    private void Start() {
        if (_myPhotonView==null) {
            _myPhotonView = GetComponent<PhotonView>();
        }

        if (_myPhotonView.IsMine) {
            _localPlayerInstance = this;
        }

        this.name = _myPhotonView.Owner.NickName;

    }
}
