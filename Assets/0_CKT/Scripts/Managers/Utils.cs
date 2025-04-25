using System.Collections.Generic;
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

    public List<int> GetRandomNumbers(int min, int max, int count)
    {
        // ��ȿ�� �˻�
        if ((count > (max - min + 1)) || (count < 0))
        {
            Debug.LogError("���� ���� ������ ������ �ʰ��ϰų� ��ȿ���� �ʽ��ϴ�.");
            return new List<int>();
        }

        List<int> numbers = new List<int>();
        List<int> results = new List<int>();

        // ���� ����Ʈ ����
        for (int i = min; i <= max; i++)
        {
            numbers.Add(i);
        }

        // �������� ���� ����
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, numbers.Count);
            results.Add(numbers[randomIndex]);
            numbers.RemoveAt(randomIndex); // �ߺ� ����
        }

        return results;
    }
}
