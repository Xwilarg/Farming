﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Material _green, _blue;

    public void InstantiatePlayer(NetworkManager net, bool isMe, Vector3 position, Vector3 velocity)
    {
        GameObject go = Instantiate(_playerPrefab, position, Quaternion.identity);
        go.GetComponent<PlayerController>().InitNetwork(net, isMe);
        go.GetComponent<Renderer>().material = isMe ? _green : _blue;
    }
}
