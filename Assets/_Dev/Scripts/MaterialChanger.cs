using UnityEngine;

public class MaterialChanger : MonoBehaviour {
    public Material[] _materials;
    public int _matIndex;
    private MeshRenderer _myRenderer;

    private void Awake() {
        _myRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterial() {
        _matIndex += 1;
        if (_matIndex >= _materials.Length) {
            _matIndex = 0;
        }

        _myRenderer.material = _materials[_matIndex];
    }
}