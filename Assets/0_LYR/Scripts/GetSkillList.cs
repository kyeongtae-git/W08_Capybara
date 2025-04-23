using System.Collections.Generic;
using UnityEngine;

public class GetSkillList : MonoBehaviour
{
    private void Awake()
    {
            LoadSkillDataFromJson();

    }

    private void Start()
    {
        //PrintSkillData();
    }


    [System.Serializable]
    public class Skill
    {
        public string name;
        public string type;
        public string explaintext;
        public int increaseValue;
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
            //Debug.Log("JSON 내용: " + jsonData.text); // 디버깅용

            // JSON을 SkillData 형태로 변환
            SkillData skillData = JsonUtility.FromJson<SkillData>(jsonData.text);
            if (skillData != null && skillData.skills != null)
            {
                foreach (Skill skill in skillData.skills)
                {
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
            Debug.Log($"이름: {skill.name}, 스킬 타입 : {skill.type}, 명중률: {skill.explaintext}, 스텟 증가량: {skill.increaseValue}");
        }
        Debug.Log("==========================");
    }

    public bool TryGetSkill(string skillName, out Skill skill)
    {
        return skillDataMap.TryGetValue(skillName, out skill);
    }
}
