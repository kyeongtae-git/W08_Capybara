using UnityEngine;

public class Utils
{
    /// <summary>
    /// success 확률로 성공
    /// </summary>
    public bool RandomSuccess(float success)
    {
        success = Mathf.Clamp(success, 0, 1);
        float randomValue = UnityEngine.Random.value; // 0.0f에서 1.0f 사이의 랜덤 값을 생성

        if (randomValue <= success) return true; // 성공
        else return false; // 실패
    }
}
