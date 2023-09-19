using UnityEngine;

public class NetworkTracking : MonoBehaviour {
    //visual references
    [Space] public Transform _head_visual;
    public Transform _rHand_visual;
    public Transform _lHand_visual;

    //Rig references
    private Transform _head_rig;
    private Transform _lHand_rig;
    private Transform _rHand_rig;

    private void Start() {
        SetReferences();
    }

    private void FixedUpdate() {
        //passare valori
        if (_head_rig == null) {
            return;
        }

        SyncTransform(_head_visual, _head_rig);


        if (_rHand_rig == null) {
            return;
        }

        SyncTransform(_rHand_visual, _rHand_rig);

        if (_lHand_rig == null) {
            return;
        }

        SyncTransform(_lHand_visual, _lHand_rig);
    }

    private void SetReferences() {
        _head_rig = InputPlayerStorage._instance._head_rig;
        _rHand_rig = InputPlayerStorage._instance._rHand_rig;
        _lHand_rig = InputPlayerStorage._instance._lHand_rig;
    }

    private void SyncTransform(Transform start, Transform destination) {
        start.position = destination.position;
        start.rotation = destination.rotation;
    }
}