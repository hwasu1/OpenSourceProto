using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 서버에서 받은 JSON 메시지에서 eventType을 추출하는 헬퍼 클래스
public static class NetworkMessageHelper
{
    [System.Serializable]
    private class Wrapper { public string eventType; }

    public static string GetEventType(string json)
    {
        return JsonUtility.FromJson<Wrapper>(json).eventType;
    }
}
