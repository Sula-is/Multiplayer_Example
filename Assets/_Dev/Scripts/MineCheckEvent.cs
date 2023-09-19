using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class MineCheckEvent : MonoBehaviour {
    public UnityEvent _OnIsMine = new();
    public UnityEvent _OnIsNotMine = new();
    private PhotonView _myPhotonView;

    private void OnEnable() {
        CheckIfMine();
    }

    [ContextMenu("CheckIfMine")]
    public void CheckIfMine() {
        if (_myPhotonView == null) {
            _myPhotonView = GetComponent<PhotonView>();
        }

        if (_myPhotonView.IsMine) {
            _OnIsMine?.Invoke();
            Debug.Log($"{name} Photonview is Mine");
            return;
        }

        _OnIsNotMine?.Invoke();
        Debug.Log($"{name} Photonview is NOT Mine");
    }
}