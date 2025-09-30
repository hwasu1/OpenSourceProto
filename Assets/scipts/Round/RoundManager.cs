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

        // ����/���� �ʱ�ȭ
        GameManager.Instance.myRole = msg.myRole;
        GameManager.Instance.mySlot = msg.mySlot;

        // UI ǥ��
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
        GameManager.Instance.systemMessageText.text = "ī�� ������ Ȯ�εǾ����ϴ�.";
    }

    public void HandlePlayerActionUpdate(PlayerActionUpdate msg)
    {
        GameManager.Instance.systemMessageText.text = $"{msg.playerId}�� �ൿ�� �Ϸ��߽��ϴ�.";
    }

    public void HandleInterpretationEnd(InterpretationEnd msg)
    {
        GameManager.Instance.chatInput.interactable = msg.chatEnabled;
        GameManager.Instance.systemMessageText.text = msg.message;
    }

    public void HandleRoundResult(RoundResult msg)
    {
        GameManager.Instance.systemMessageText.text = $"���� ��: {msg.finalSentence} (+{msg.scoreChange})";
        UIManager.Instance.PlayVisualCue(msg.visualCue);


    }
}
