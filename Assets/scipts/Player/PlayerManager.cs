using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string playerId;
    public string nickname;
    public string role;         
    public List<string> cards;
    public bool actionCompleted = false;

    public void SetRoleAndCards(string assignedRole, List<string> cardList)
    {
        role = assignedRole;
        cards = cardList;
        actionCompleted = false;
        UIManager.Instance.SetupCardButtons(cards, this);
    }

    public void SelectCard(string card)
    {
        if (actionCompleted) return;
        NetworkManager.Instance.SendCardSelection(playerId, card);
        actionCompleted = true;
        UIManager.Instance.DisableMyCards();
        Debug.Log($"Player {playerId} 선택 완료: {card}");
    }

    public void MarkActionCompleted()
    {
        actionCompleted = true;
        UIManager.Instance.DisableMyCards();
    }
   
}
