using System;
using System.Collections.Generic;

// �������� �޴� �޽��� ���� ����
// JSON���� ���޵ǹǷ� JsonUtility.FromJson���� ��ȯ ����

[Serializable]
public class RoundStartMessage
{
    public int roundNumber;
    public int timeLimit;
    public string mission;
    public string myRole;            // "citizen" or "traitor"
    public string mySlot;            // "��ü", "���" ��
    public List<string> cards;       // �÷��̾�� ������ ī�� ���
    public bool chatEnabled;         // ä�� �Է� ���� ����
    public string godPersonality;    // ����ڿ��Ը� �ִ� ����
}

[Serializable]
public class PlayerActionUpdate
{
    public string playerId;
    public string actionStatus;      // "card_selected" ��
}

[Serializable]
public class InterpretationEnd
{
    public string message;
    public bool chatEnabled;         // false�� ä�� ��Ȱ��ȭ
}

[Serializable]
public class RoundResult
{
    public string finalSentence;     // AI ���� ��� ����
    public int scoreChange;          // HP ����
    public VisualCue visualCue;      // ���� ����
    public TrialProposalPhase trialProposalPhase; // ����� ���� �ܰ�
}

[Serializable]
public class VisualCue
{
    public string subject;
    public string target;
    public string effect;
    public string action;
}

[Serializable]
public class TrialProposalPhase
{
    public bool active;
    public int timeLimit;            // ���� ���� �ܰ� ���� �ð�
}
