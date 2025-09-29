using System;
using System.Collections.Generic;

// 서버에서 받는 메시지 구조 정의
// JSON으로 전달되므로 JsonUtility.FromJson으로 변환 가능

[Serializable]
public class RoundStartMessage
{
    public int roundNumber;
    public int timeLimit;
    public string mission;
    public string myRole;            // "citizen" or "traitor"
    public string mySlot;            // "주체", "어떻게" 등
    public List<string> cards;       // 플레이어에게 보여줄 카드 목록
    public bool chatEnabled;         // 채팅 입력 가능 여부
    public string godPersonality;    // 배신자에게만 있는 정보
}

[Serializable]
public class PlayerActionUpdate
{
    public string playerId;
    public string actionStatus;      // "card_selected" 등
}

[Serializable]
public class InterpretationEnd
{
    public string message;
    public bool chatEnabled;         // false로 채팅 비활성화
}

[Serializable]
public class RoundResult
{
    public string finalSentence;     // AI 심판 결과 문장
    public int scoreChange;          // HP 변동
    public VisualCue visualCue;      // 연출 정보
    public TrialProposalPhase trialProposalPhase; // 배신자 심판 단계
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
    public int timeLimit;            // 심판 제안 단계 제한 시간
}
