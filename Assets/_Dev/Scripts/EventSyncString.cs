using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class EventSyncString : MonoBehaviour {
    public string _myString;
    public UnityEvent<string> _syncStringEventOnline = new();

    private PhotonView _myPhotonView;

    [ContextMenu("SendEvent")]
    private void SendEventRPC() {
        //sent from local
        if (_myPhotonView == null) {
            _myPhotonView = GetComponent<PhotonView>();
        }

        //STRING SYNC
        EventRPC(_myString);
        _myPhotonView.RPC("EventRPC", RpcTarget.All, _myString);
    }


    [PunRPC]
    private void EventRPC(string syncString) {
        //client or target
        _syncStringEventOnline?.Invoke(syncString);
    }
}