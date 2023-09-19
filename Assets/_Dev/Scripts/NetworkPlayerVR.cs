using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class NetworkPlayerVR : MonoBehaviour {
    public static NetworkPlayerVR _localPlayerInstance;
    private PhotonView _myPhotonView;

    private void Start() {
        if (_myPhotonView == null) {
            _myPhotonView = GetComponent<PhotonView>();
        }

        if (_myPhotonView.IsMine) {
            _localPlayerInstance = this;
        }

        name = _myPhotonView.Owner.NickName;
    }
}