using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class EventSync : MonoBehaviour {
    private PhotonView _myPhotonView;
    public UnityEvent _syncEventOnline = new UnityEvent(); 
    #region RPC

    [ContextMenu("SendEvent")]
    private void SendEventRPC() {
        //sent from local
        if (_myPhotonView == null)
            _myPhotonView = GetComponent<PhotonView>();
        
        _myPhotonView.RPC("EventRPC", RpcTarget.All);
        //_myPhotonView.RPC("EventRPC", RpcTarget.All,(string)"u", (int) 1);
    }

    [PunRPC]
    private void EventRPC() {
        //client or target
        _syncEventOnline?.Invoke();
    }


    #endregion
}
