using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AcquiredSkills : MonoBehaviour
{
    List<GameObject> Aqskill = new List<GameObject>();

    GetSkillImage _skillImage;


    //GetSprite(스킬 타입) 스킬 타입의 스프라이트 가져와짐
    private Dictionary<string, GameObject> assignedSkillObjects = new Dictionary<string, GameObject>();
    // 이미 사용된(할당된) 오브젝트 집합
    private HashSet<GameObject> usedObjects = new HashSet<GameObject>();


    private Dictionary<string, int> skillCallCounts = new Dictionary<string, int>();


    void Start()
    {
        _skillImage = FindAnyObjectByType<GetSkillImage>();
        foreach (Transform child in this.transform)
        {
            Aqskill.Add(child.gameObject);
        }

    }

    public void AssignSkill(string skillType)
    {
        Debug.Log("실행된 스킬 타입" + skillType);

        if (skillCallCounts.ContainsKey(skillType))
        {
            skillCallCounts[skillType]++;
        }
        else
        {
            skillCallCounts[skillType] = 1;
        }
        Debug.Log($"스킬 '{skillType}' 호출 횟수: {skillCallCounts[skillType]}");


        // 이미 같은 스킬 타입이 할당된 적 있으면 무시
        if (assignedSkillObjects.ContainsKey(skillType))
        {
            Debug.Log($"Skill '{skillType}' is already assigned.");
            return;
        }

        // 사용되지 않은 첫 번째 슬롯 탐색
        GameObject slot = Aqskill.Find(go => !usedObjects.Contains(go));
        if (slot == null)
        {
            Debug.LogWarning("모든 슬롯이 이미 사용 중입니다!");
            return;
        }

        // 해당 슬롯을 사용 표시하고 맵에 등록
        usedObjects.Add(slot);
        assignedSkillObjects[skillType] = slot;

        // 자식 오브젝트에서 Image 컴포넌트 가져와 스프라이트 세팅
        Image img = slot.GetComponentInChildren<Image>();
        TextMeshProUGUI text = slot.GetComponentInChildren<TextMeshProUGUI>();
        if (img != null)
        {
            img.enabled = true;
            text.enabled = true;
            img.sprite = _skillImage.GetSprite(skillType+"_0");
            text.text = $"{ skillCallCounts[skillType]}";
        }
        else
        {
            Debug.LogWarning($"'{slot.name}'의 자식에서 Image 컴포넌트를 찾을 수 없습니다.");
        }
    }


}
