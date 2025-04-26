using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GetSkillList : MonoBehaviour
{
    private void Awake()
    {
        LoadSkillDataFromJson();
    }

    private void Start()
    {
        PrintSkillData();
    }

    [System.Serializable]
    public class Skill
    {
        public string name;
        public string type;
        public string explaintext;
        public float increaseValue; // int -> float로 변경
    }

    [System.Serializable]
    public class SkillData
    {
        public Skill[] skills;
    }

    Dictionary<string, Skill> skillDataMap = new Dictionary<string, Skill>();

    void LoadSkillDataFromJson()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("LYR/SkillList");
        if (jsonData != null)
        {
            SkillData skillData = JsonUtility.FromJson<SkillData>(jsonData.text);
            if (skillData != null && skillData.skills != null)
            {
                foreach (Skill skill in skillData.skills)
                {
                    // explaintext에서 increaseValue 추출
                    skill.increaseValue = ParseIncreaseValue(skill.explaintext, skill.increaseValue);
                    skillDataMap[skill.type] = skill;
                }
                Debug.Log("스킬 데이터 로드 완료!");
            }
            else
            {
                Debug.LogError("JSON 파싱 실패: 데이터가 null입니다.");
            }
        }
        else
        {
            Debug.LogError("SkillList.json 파일을 찾을 수 없습니다.");
        }
    }

    float ParseIncreaseValue(string explaintext, float defaultValue)
    {
        // 퍼센트 값 추출 (예: "15%")
        Match percentMatch = Regex.Match(explaintext, @"(\d+)%");
        // 고정 숫자 값 추출 (예: "0.4")
        Match numberMatch = Regex.Match(explaintext, @"(\d+\.\d+)");

        if (percentMatch.Success)
        {
            // 퍼센트를 소수점으로 변환 (예: 15% -> 0.15)
            return float.Parse(percentMatch.Groups[1].Value) / 100f;
        }
        else if (numberMatch.Success)
        {
            // 고정 숫자 값을 그대로 사용
            return float.Parse(numberMatch.Groups[1].Value);
        }
        PrintSkillData();
        // 매칭 실패 시 JSON의 기본값 사용
        return defaultValue;
    }

    void PrintSkillData()
    {
        Debug.Log("===== 로드된 스킬 목록 =====");
        if (skillDataMap.Count == 0)
        {
            Debug.Log("스킬 데이터가 없습니다.");
            return;
        }

        foreach (var skillPair in skillDataMap)
        {
            Skill skill = skillPair.Value;
            Debug.Log($"이름: {skill.name}, 스킬 타입: {skill.type}, 설명: {skill.explaintext}, 스텟 증가량: {skill.increaseValue}");
        }
        Debug.Log("==========================");
    }

    public bool TryGetSkill(string skillName, out Skill skill)
    {
        return skillDataMap.TryGetValue(skillName, out skill);
    }
}