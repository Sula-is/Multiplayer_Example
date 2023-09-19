using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class EventSyncInt : MonoBehaviour {
    public int _myInt;
    public UnityEvent<int> _syncStringEventOnline = new();

    private PhotonView _myPhotonView;

    [ContextMenu("SendEvent")]
    private void SendEventRPC() {
        //sent from local
        if (_myPhotonView == null) {
            _myPhotonView = GetComponent<PhotonView>();
        }


        EventRPC(_myInt);
        _myPhotonView.RPC("EventRPC", RpcTarget.All, _myInt);
    }


    [PunRPC]
    private void EventRPC(int value) {
        //client or target
        _syncStringEventOnline?.Invoke(value);
    }
}