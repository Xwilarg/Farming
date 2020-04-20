using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    public void InstantiatePlayer(bool isMe, Vector3 position, Vector3 velocity)
    {
        GameObject go = Instantiate(_playerPrefab, position, Quaternion.identity);
        go.GetComponent<PlayerController>().SetIsMe(isMe);
    }
}
