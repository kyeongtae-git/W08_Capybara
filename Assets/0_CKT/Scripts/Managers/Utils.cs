using UnityEngine;

public class Utils
{
    /// <summary>
    /// success Ȯ���� ����
    /// </summary>
    public bool RandomSuccess(float success)
    {
        success = Mathf.Clamp(success, 0, 1);
        float randomValue = UnityEngine.Random.value; // 0.0f���� 1.0f ������ ���� ���� ����

        if (randomValue <= success) return true; // ����
        else return false; // ����
    }
}
