using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 라운드 진행, 카드 선택, 해석 종료, 심판 결과 처리 담당
public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 라운드 시작 메시지 처리
    public void HandleRoundStart(string json)
    {
        RoundStartMessage msg = JsonUtility.FromJson<RoundStartMessage>(json);

        // 역할과 슬롯 정보 저장
        GameManager.Instance.myRole = msg.myRole;
        GameManager.Instance.mySlot = msg.mySlot;

        // 카드 UI 생성
        UIManager.Instance.SetupCardButtons(msg.cards);

        // 배신자 정보 표시
        if (msg.myRole == "traitor")
            UIManager.Instance.ShowTraitorInfo(msg.godPersonality);

        // 채팅 입력 활성화/비활성화
        GameManager.Instance.chatInput.interactable = msg.chatEnabled;

        // 라운드 시작 안내
        GameManager.Instance.systemMessageText.text = $"라운드 {msg.roundNumber} 시작: {msg.mission}";
    }

    // 카드 선택 확인 메시지
    public void HandleCardSelectionConfirmed()
    {
        UIManager.Instance.DisableMyCards(); // 카드 버튼 비활성화
        GameManager.Instance.systemMessageText.text = "카드 선택이 확인되었습니다.";
    }

    // 다른 플레이어 행동 업데이트
    public void HandlePlayerActionUpdate(string json)
    {
        PlayerActionUpdate msg = JsonUtility.FromJson<PlayerActionUpdate>(json);
        GameManager.Instance.systemMessageText.text = $"{msg.playerId}가 행동을 완료했습니다.";
    }

    // 해석 단계 종료 메시지 처리
    public void HandleInterpretationEnd(string json)
    {
        InterpretationEnd msg = JsonUtility.FromJson<InterpretationEnd>(json);
        GameManager.Instance.chatInput.interactable = msg.chatEnabled;
        GameManager.Instance.systemMessageText.text = msg.message;
    }

    // 라운드 결과 처리
    public void HandleRoundResult(string json)
    {
        RoundResult msg = JsonUtility.FromJson<RoundResult>(json);

        GameManager.Instance.systemMessageText.text = $"심판 결과: {msg.finalSentence} (+{msg.scoreChange})";

        // 시각적 효과 연출
        UIManager.Instance.PlayVisualCue(msg.visualCue);

        // 배신자 심판 버튼 활성화
        GameManager.Instance.trialButton.interactable = msg.trialProposalPhase.active;
        if (msg.trialProposalPhase.active)
            StartCoroutine(UIManager.Instance.TrialCountdown(msg.trialProposalPhase.timeLimit));
    }
}
