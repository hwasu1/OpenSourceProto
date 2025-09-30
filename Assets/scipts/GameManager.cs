using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public Text systemMessageText;
    public InputField chatInput;
    public Button trialButton;
    public Text countdownText;

    [Header("Player Info")]
    public string myRole;
    public string mySlot;

    private void Awake()
    { if (Instance == null)
            Instance = this;

      else Destroy(gameObject); 
    }

    public void OnServerMessage(string json)
    {
        string eventType = NetworkMessageHelper.GetEventType(json);

        switch (eventType)
        {
            case "GAME_START":
                StartCoroutine(CountdownAndLoadGameScene());
                break;

            case "ROUND_START":
                RoundManager.Instance
                    .HandleRoundStart(JsonUtility.FromJson<RoundStartMessage>(json));
                break;

            case "CARD_SELECTION_CONFIRMED":
                RoundManager.Instance
                    .HandleCardSelectionConfirmed();
                break;

            case "PLAYER_ACTION_UPDATE":
                RoundManager.Instance
                    .HandlePlayerActionUpdate(JsonUtility.FromJson<PlayerActionUpdate>(json));
                break;

            case "INTERPRETATION_END":
                RoundManager.Instance
                    .HandleInterpretationEnd(JsonUtility.FromJson<InterpretationEnd>(json));
                break;

            case "ROUND_RESULT":
                RoundManager.Instance
                    .HandleRoundResult(JsonUtility.FromJson<RoundResult>(json));
                break;
        }
    }

    private IEnumerator CountdownAndLoadGameScene()
    {
        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }
        countdownText.text = "";
        // SceneManager.LoadScene("GameScene"); // ¾À ÀüÈ¯
    }
    public PlayerManager GetPlayerManager() { return FindObjectOfType<PlayerManager>(); }
}
