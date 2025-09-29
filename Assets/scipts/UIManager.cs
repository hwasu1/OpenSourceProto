using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI ���� ó�� ���: ī�� ��ư, ����� ����, ���� ī��Ʈ�ٿ�, �ð� ȿ��
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

    // ī�� ��ư ����
    public void SetupCardButtons(List<string> cards)
    {
        // ���� ī�� ����
        foreach (var btn in cardButtons) Destroy(btn);
        cardButtons.Clear();

        foreach (string card in cards)
        {
            GameObject newBtn = Instantiate(cardButtonPrefab, cardContainer);
            newBtn.GetComponentInChildren<Text>().text = card;
            cardButtons.Add(newBtn);

            // Ŭ�� �̺�Ʈ ���
            newBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnCardClicked(card);
            });
        }
    }

    // ī�� Ŭ�� �� ������ ���� ����
    private void OnCardClicked(string card)
    {
        Debug.Log($"ī�� ����: {card}");
        // TODO: ������ ���� ����
    }

    // �� ī�� ��Ȱ��ȭ
    public void DisableMyCards()
    {
        foreach (var btn in cardButtons)
            btn.GetComponent<Button>().interactable = false;
    }

    // ����� ���� ǥ��
    public void ShowTraitorInfo(string godPersonality)
    {
        traitorPanel.SetActive(true);
        traitorText.text = $"����� ����: {godPersonality}";
    }

    // �ð��� ȿ�� ����
    public void PlayVisualCue(VisualCue cue)
    {
        // TODO: ���� ���� ����
        if (visualCueAnimator != null)
        {
            visualCueAnimator.SetTrigger(cue.effect);
        }
    }

    // ���� �ܰ� ī��Ʈ�ٿ�
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
