using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    private void Awake() { if (Instance == null) Instance = this; else Destroy(gameObject); }

    [Header("Card UI")]
    public GameObject cardButtonPrefab;
    public Transform cardContainer;
    private List<GameObject> cardButtons = new List<GameObject>();

    [Header("Oracle & Role")] 
    public GameObject oraclePanel; 
    public Text oracleText; 
    public Text roleText;

    [Header("Traitor Info")] 
    public GameObject traitorPanel; 
    public Text traitorText;

    [Header("Visual Cue")] 
    public Animator visualCueAnimator;

    [Header("Card Selection Timer")]
    public Text countdownText;       
    public Image timerCircle;        


    public void SetupCardButtons(List<string> cards, PlayerManager player)
    {
        foreach (var btn in cardButtons) Destroy(btn);
        cardButtons.Clear();

        foreach (string card in cards)
        {
            string cardCopy = card;
            GameObject newBtn = Instantiate(cardButtonPrefab, cardContainer);
            newBtn.GetComponentInChildren<Text>().text = cardCopy;
            cardButtons.Add(newBtn);

            newBtn.GetComponent<Button>().onClick.AddListener(() => player.SelectCard(cardCopy));
        }
    }

    public void DisableMyCards() 
    {
        foreach (var btn in cardButtons) btn.GetComponent<Button>().interactable = false;
    }

    // ��Ź�� ����(1���忡��) ǥ��
    public void ShowOracleAndRole(string oracle, string role, int round)
    {
        oraclePanel.SetActive(true);
        oracleText.text = oracle;
        // ù ���忡�� ���� ǥ��
        if (round == 1)
            roleText.text = role;
        else
            roleText.text = "";

        StartCoroutine(HideOraclePanelAfterSeconds(3f));
    }

    // ��Ź�� ����(1���忡��) ǥ�� ����
    private IEnumerator HideOraclePanelAfterSeconds(float seconds) 
    {
        yield return new WaitForSeconds(seconds);
        oraclePanel.SetActive(false); 
    }

    // ����� ���ҿ��Ը� ���̴� 
    public void ShowTraitorInfo(string godPersonality)
    {
        traitorPanel.SetActive(true);
        traitorText.text = $"���� �丣�ҳ�: {godPersonality}";
    }

    public void AutoSelectRandomCard()
    {
        if (cardButtons.Count == 0) return;
        int index = UnityEngine.Random.Range(0, cardButtons.Count);
        string card = cardButtons[index].GetComponentInChildren<Text>().text;
        cardButtons[index].GetComponent<Button>().onClick.Invoke();
    }

    public void PlayVisualCue(VisualCue cue)
    {
        if (visualCueAnimator != null) visualCueAnimator.SetTrigger(cue.effect);
    }

    //ī�� ���ÿ� Ÿ�̸�����, ����� ��ǥ���� ��?�� �����մϴ�.
    public IEnumerator StartTimer(float totalTime, Action onTimerEnd)
    {
        float timer = totalTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            if (countdownText != null)
                countdownText.text = Mathf.Ceil(timer).ToString();

            if (timerCircle != null)
                timerCircle.fillAmount = timer / totalTime;

            yield return null;
        }

        if (countdownText != null)
            countdownText.text = "0";

        if (timerCircle != null)
            timerCircle.fillAmount = 0;

        onTimerEnd?.Invoke();
    }

}
