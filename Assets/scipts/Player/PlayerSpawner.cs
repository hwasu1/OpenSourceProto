using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints; // �÷��̾� 4�� �ڸ� ��ǥ(���� ����)

    // �÷��̾� �������� ������ ��ҷ� ��ȯ��
    public PlayerManager SpawnPlayer(string playerId, string nickname, int spawnIndex)
    {
        GameObject playerObj = Instantiate(playerPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        PlayerManager pm = playerObj.AddComponent<PlayerManager>();
        pm.playerId = playerId;
        pm.nickname = nickname;
        GameManager.Instance.AddPlayer(playerId, pm); 
        return pm;
    }
}