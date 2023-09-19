using UnityEngine;

public class InputPlayerStorage : MonoBehaviour {
    public static InputPlayerStorage _instance;

    //Rig references
    public Transform _head_rig;
    public Transform _rHand_rig;
    public Transform _lHand_rig;

    private void Start() {
        _instance = this;
    }
}