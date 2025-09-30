using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    private Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>();

    public PlayerManager SpawnPlayer(string playerId, string nickname, int spawnIndex)
    {
        GameObject playerObj = Instantiate(playerPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        PlayerManager pm = playerObj.AddComponent<PlayerManager>();
        pm.playerId = playerId;
        pm.nickname = nickname;
        players.Add(playerId, pm);
        return pm;
    }
}
