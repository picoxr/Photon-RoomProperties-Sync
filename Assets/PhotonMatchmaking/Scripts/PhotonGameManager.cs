using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject playerPrefab;

    void Start()
    {
        if (PhotonPlayerManager.LocalPlayerInstance == null)
        {
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(Random.Range(45, 55), 1.5f, 50f), Quaternion.identity, 0);
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("TankerLobby");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
