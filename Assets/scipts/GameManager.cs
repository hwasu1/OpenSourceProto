using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ���� ���� ����: ���� �޽��� ����, ���� ����, UI/�÷��̾� ���� ����
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public Text systemMessageText;     // �ý��� �޽��� ǥ�� (����, ���� ��� ��)
    public InputField chatInput;       // ä�� �Է�
    public GameObject cardButtonPrefab;
    public Transform cardContainer;
    public Button trialButton;         // ����� ���� ���� ��ư
    public Text countdownText;

    [Header("Player Info")]
    public string myRole;              // �÷��̾� ����: �ù�/�����
    public string mySlot;              // ���� ����

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ���� �޽��� ���� �� ȣ��
    public void OnServerMessage(string json)
    {
        string eventType = NetworkMessageHelper.GetEventType(json); // JSON���� eventType ����

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

    // ���� ���� ī��Ʈ�ٿ� �� �� ��ȯ
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
        // TODO: SceneManager.LoadScene("GameScene") �� �� ��ȯ
    }
}
