using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class EventSync : MonoBehaviour {
    public UnityEvent _syncEventOnline = new();

    private PhotonView _myPhotonView;

    [ContextMenu("SendEvent")]
    public void SendEventRPC() {
        //sent from local
        if (_myPhotonView == null) {
            _myPhotonView = GetComponent<PhotonView>();
        }

        //volendo posso mandarla in locale
        EventRPC();
        //e l'RPC solo agli altri
        _myPhotonView.RPC("EventRPC", RpcTarget.Others);
    }

    [PunRPC]
    private void EventRPC() {
        //client or target
        _syncEventOnline?.Invoke();
    }
}