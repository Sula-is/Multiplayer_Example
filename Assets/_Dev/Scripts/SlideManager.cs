using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
public class SlideManager : MonoBehaviour {
    public int _currentSlide;
    public List<GameObject> _slides = new();

    private readonly UnityEvent<int> _intSlideChanged = new();

    private PhotonView _myPhotonView;

    private void Awake() {
        UpdateSlide(_currentSlide);
        _intSlideChanged.AddListener(value => {
            //local
            UpdateSlide(value);
            //RPC
            if (_myPhotonView == null) {
                _myPhotonView = GetComponent<PhotonView>();
            }

            _myPhotonView.RPC("UpdateSlideRPC", RpcTarget.Others, value);
        });
    }

    [ContextMenu("Next")]
    public void Next() {
        _currentSlide++;

        if (_currentSlide >= _slides.Count) {
            _currentSlide = _slides.Count - 1;
        }

        _intSlideChanged?.Invoke(_currentSlide);
    }

    [ContextMenu("Previous")]
    public void Previous() {
        _currentSlide--;
        if (_currentSlide < 0) {
            _currentSlide = 0;
        }

        _intSlideChanged?.Invoke(_currentSlide);
    }

    private void UpdateSlide(int slideToActivate) {
        for (var i = 0; i < _slides.Count; i++) {
            _slides[i].SetActive(false);
        }

        _slides[slideToActivate].SetActive(true);
    }

    [PunRPC]
    private void UpdateSlideRPC(int slideToActivate) {
        for (var i = 0; i < _slides.Count; i++) {
            _slides[i].SetActive(false);
        }

        _currentSlide = slideToActivate;
        _slides[slideToActivate].SetActive(true);
    }
}