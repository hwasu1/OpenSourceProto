using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ���� ����, ī�� ����, �ؼ� ����, ���� ��� ó�� ���
public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ���� ���� �޽��� ó��
    public void HandleRoundStart(string json)
    {
        RoundStartMessage msg = JsonUtility.FromJson<RoundStartMessage>(json);

        // ���Ұ� ���� ���� ����
        GameManager.Instance.myRole = msg.myRole;
        GameManager.Instance.mySlot = msg.mySlot;

        // ī�� UI ����
        UIManager.Instance.SetupCardButtons(msg.cards);

        // ����� ���� ǥ��
        if (msg.myRole == "traitor")
            UIManager.Instance.ShowTraitorInfo(msg.godPersonality);

        // ä�� �Է� Ȱ��ȭ/��Ȱ��ȭ
        GameManager.Instance.chatInput.interactable = msg.chatEnabled;

        // ���� ���� �ȳ�
        GameManager.Instance.systemMessageText.text = $"���� {msg.roundNumber} ����: {msg.mission}";
    }

    // ī�� ���� Ȯ�� �޽���
    public void HandleCardSelectionConfirmed()
    {
        UIManager.Instance.DisableMyCards(); // ī�� ��ư ��Ȱ��ȭ
        GameManager.Instance.systemMessageText.text = "ī�� ������ Ȯ�εǾ����ϴ�.";
    }

    // �ٸ� �÷��̾� �ൿ ������Ʈ
    public void HandlePlayerActionUpdate(string json)
    {
        PlayerActionUpdate msg = JsonUtility.FromJson<PlayerActionUpdate>(json);
        GameManager.Instance.systemMessageText.text = $"{msg.playerId}�� �ൿ�� �Ϸ��߽��ϴ�.";
    }

    // �ؼ� �ܰ� ���� �޽��� ó��
    public void HandleInterpretationEnd(string json)
    {
        InterpretationEnd msg = JsonUtility.FromJson<InterpretationEnd>(json);
        GameManager.Instance.chatInput.interactable = msg.chatEnabled;
        GameManager.Instance.systemMessageText.text = msg.message;
    }

    // ���� ��� ó��
    public void HandleRoundResult(string json)
    {
        RoundResult msg = JsonUtility.FromJson<RoundResult>(json);

        GameManager.Instance.systemMessageText.text = $"���� ���: {msg.finalSentence} (+{msg.scoreChange})";

        // �ð��� ȿ�� ����
        UIManager.Instance.PlayVisualCue(msg.visualCue);

        // ����� ���� ��ư Ȱ��ȭ
        GameManager.Instance.trialButton.interactable = msg.trialProposalPhase.active;
        if (msg.trialProposalPhase.active)
            StartCoroutine(UIManager.Instance.TrialCountdown(msg.trialProposalPhase.timeLimit));
    }
}
