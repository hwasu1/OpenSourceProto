using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 게임 전반 관리: 서버 메시지 수신, 라운드 진행, UI/플레이어 상태 관리
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public Text systemMessageText;     // 시스템 메시지 표시 (라운드, 심판 결과 등)
    public InputField chatInput;       // 채팅 입력
    public GameObject cardButtonPrefab;
    public Transform cardContainer;
    public Button trialButton;         // 배신자 심판 시작 버튼
    public Text countdownText;

    [Header("Player Info")]
    public string myRole;              // 플레이어 역할: 시민/배신자
    public string mySlot;              // 현재 슬롯

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 서버 메시지 수신 후 호출
    public void OnServerMessage(string json)
    {
        string eventType = NetworkMessageHelper.GetEventType(json); // JSON에서 eventType 추출

        switch (eventType)
        {
            case "GAME_START":
                StartCoroutine(CountdownAndLoadGameScene());
                break;
            case "ROUND_START":
                RoundManager.Instance.HandleRoundStart(json);
                break;
            case "CARD_SELECTION_CONFIRMED":
                RoundManager.Instance.HandleCardSelectionConfirmed();
                break;
            case "PLAYER_ACTION_UPDATE":
                RoundManager.Instance.HandlePlayerActionUpdate(json);
                break;
            case "INTERPRETATION_END":
                RoundManager.Instance.HandleInterpretationEnd(json);
                break;
            case "ROUND_RESULT":
                RoundManager.Instance.HandleRoundResult(json);
                break;
        }
    }

    // 게임 시작 카운트다운 후 씬 전환
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
        // TODO: SceneManager.LoadScene("GameScene") 등 씬 전환
    }
}
