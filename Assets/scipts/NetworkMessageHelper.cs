using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ���� JSON �޽������� eventType�� �����ϴ� ���� Ŭ����
public static class NetworkMessageHelper
{
    [System.Serializable]
    private class Wrapper { public string eventType; }

    public static string GetEventType(string json)
    {
        return JsonUtility.FromJson<Wrapper>(json).eventType;
    }
}
