using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    public int currentRound = 0;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else Destroy(gameObject);
    }

    public void HandleRoundStart(RoundStartMessage msg)
    {
        currentRound++;

        // 역할/슬롯 초기화
        GameManager.Instance.myRole = msg.myRole;
        GameManager.Instance.mySlot = msg.mySlot;

        // UI 표시
        UIManager.Instance.ShowOracleAndRole(msg.mission, msg.myRole, currentRound);
        if (msg.myRole == "traitor") UIManager.Instance.ShowTraitorInfo(msg.godPersonality);
        GameManager.Instance.chatInput.interactable = msg.chatEnabled;

        StartCoroutine(StartCardSelection(msg.cards, msg.timeLimit));
    }

    private IEnumerator StartCardSelection(List<string> cards, int selectionTime)
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.SetupCardButtons(cards, GameManager.Instance.GetPlayerManager());


        StartCoroutine(
        UIManager.Instance.StartTimer(selectionTime, () =>

        UIManager.Instance.AutoSelectRandomCard()
        )
        );

    }

    public void HandleCardSelectionConfirmed()
    {
        UIManager.Instance.DisableMyCards();
        GameManager.Instance.systemMessageText.text = "카드 선택이 확인되었습니다.";
    }

    public void HandlePlayerActionUpdate(PlayerActionUpdate msg)
    {
        GameManager.Instance.systemMessageText.text = $"{msg.playerId}가 행동을 완료했습니다.";
    }

    public void HandleInterpretationEnd(InterpretationEnd msg)
    {
        GameManager.Instance.chatInput.interactable = msg.chatEnabled;
        GameManager.Instance.systemMessageText.text = msg.message;
    }

    public void HandleRoundResult(RoundResult msg)
    {
        GameManager.Instance.systemMessageText.text = $"신의 평가: {msg.finalSentence} (+{msg.scoreChange})";
        UIManager.Instance.PlayVisualCue(msg.visualCue);


    }
}
