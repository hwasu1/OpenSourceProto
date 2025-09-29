using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI 관련 처리 담당: 카드 버튼, 배신자 정보, 심판 카운트다운, 시각 효과
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [Header("Card UI")]
    public GameObject cardButtonPrefab;
    public Transform cardContainer;

    [Header("Traitor Info")]
    public GameObject traitorPanel;
    public Text traitorText;

    [Header("Visual Cue")]
    public Animator visualCueAnimator;

    [Header("Trial Countdown")]
    public Text countdownText;

    private List<GameObject> cardButtons = new List<GameObject>();

    // 카드 버튼 생성
    public void SetupCardButtons(List<string> cards)
    {
        // 기존 카드 삭제
        foreach (var btn in cardButtons) Destroy(btn);
        cardButtons.Clear();

        foreach (string card in cards)
        {
            GameObject newBtn = Instantiate(cardButtonPrefab, cardContainer);
            newBtn.GetComponentInChildren<Text>().text = card;
            cardButtons.Add(newBtn);

            // 클릭 이벤트 등록
            newBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnCardClicked(card);
            });
        }
    }

    // 카드 클릭 시 서버로 선택 전송
    private void OnCardClicked(string card)
    {
        Debug.Log($"카드 선택: {card}");
        // TODO: 서버로 선택 전송
    }

    // 내 카드 비활성화
    public void DisableMyCards()
    {
        foreach (var btn in cardButtons)
            btn.GetComponent<Button>().interactable = false;
    }

    // 배신자 정보 표시
    public void ShowTraitorInfo(string godPersonality)
    {
        traitorPanel.SetActive(true);
        traitorText.text = $"배신자 정보: {godPersonality}";
    }

    // 시각적 효과 연출
    public void PlayVisualCue(VisualCue cue)
    {
        // TODO: 실제 연출 구현
        if (visualCueAnimator != null)
        {
            visualCueAnimator.SetTrigger(cue.effect);
        }
    }

    // 심판 단계 카운트다운
    public IEnumerator TrialCountdown(int timeLimit)
    {
        int t = timeLimit;
        while (t > 0)
        {
            countdownText.text = t.ToString();
            yield return new WaitForSeconds(1f);
            t--;
        }
        countdownText.text = "";
    }
}
