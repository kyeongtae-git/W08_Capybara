using System.Collections.Generic;
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

    public List<int> GetRandomNumbers(int min, int max, int count)
    {
        // 유효성 검사
        if ((count > (max - min + 1)) || (count < 0))
        {
            Debug.LogError("뽑을 숫자 개수가 범위를 초과하거나 유효하지 않습니다.");
            return new List<int>();
        }

        List<int> numbers = new List<int>();
        List<int> results = new List<int>();

        // 숫자 리스트 생성
        for (int i = min; i <= max; i++)
        {
            numbers.Add(i);
        }

        // 무작위로 숫자 선택
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, numbers.Count);
            results.Add(numbers[randomIndex]);
            numbers.RemoveAt(randomIndex); // 중복 방지
        }

        return results;
    }
}
