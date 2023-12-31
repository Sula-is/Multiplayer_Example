using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class NetworkManager : MonoBehaviourPunCallbacks {
    public GameObject _playerPrefab;
    public UnityEvent _OnJoinRoom = new();
    public UnityEvent _OnLeaveRoom = new();
    private readonly string _roomID = "01";
    private readonly string _serverID = "fd2c8671-47ce-41ab-95af-b1729f4658bb";

    private void Start() {
        ConnectToServer();
    }

    private void ConnectToServer() {
        Debug.Log("Try connect to server. .");

        PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime =
            _serverID; //è utile per cambiare server, ad esempio per testing

        //Name
        var randomIndex = Random.Range(0, 100).ToString();
        PhotonNetwork.NickName = "Sula" + randomIndex;

        PhotonNetwork.ConnectUsingSettings(); //Connects using deafult settings
    }

    private void CreateRoom() {
        var createdOptions = new RoomOptions();
        createdOptions.MaxPlayers = 10;
        createdOptions.IsVisible = true;
        createdOptions.PublishUserId = true;

        createdOptions.CustomRoomProperties = new Hashtable();
        createdOptions.CustomRoomProperties.Add("IndexSlide", 0);
        createdOptions.CustomRoomPropertiesForLobby = new string[1] { "IndexSlide" };

        Debug.Log($"Try create room {_roomID}");
        PhotonNetwork.CreateRoom(_roomID, createdOptions);
    }

    private void JoinRoom() {
        if (PhotonNetwork.IsConnectedAndReady) {
            PhotonNetwork.JoinRoom(_roomID);
            Debug.Log($"Joining session: {_roomID}");
        }
    }

    public void SimulateDisconnection() {
        if (PhotonNetwork.IsConnectedAndReady) {
            Debug.Log("Disconnect");
            PhotonNetwork.Disconnect();
        }
    }

    public void SimulateLeaveRoom() {
        if (PhotonNetwork.IsConnectedAndReady) {
            Debug.Log("Leave room");
            PhotonNetwork.LeaveRoom();
        }
    }

    private void InstantiatePlayer() {
        var randomVector = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));

        if (NetworkPlayer._localPlayerInstance == null) {
            PhotonNetwork.Instantiate(_playerPrefab.name, randomVector, Quaternion.identity);
        }
    }

    #region Overrides

    public override void OnConnectedToMaster() {
        Debug.Log("Connect to server");

        if (PhotonNetwork.IsConnectedAndReady) {
            JoinRoom();
        }
    }

    public override void OnCreatedRoom() {
        Debug.Log("Room created");
    }

    public override void OnJoinedRoom() {
        Debug.Log("Room joined");
        InstantiatePlayer();
        _OnJoinRoom?.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log($"Room creation failed: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.Log($"Room join failed: {message}");
        CreateRoom();
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log($"Disconnection cause by: {cause}");
        _OnLeaveRoom?.Invoke();
        PhotonNetwork.ConnectUsingSettings();
        //ConnectToServer();
    }

    public override void OnLeftRoom() {
        Debug.Log("Player left room");
    }

    #endregion
}