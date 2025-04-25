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
        Debug.Log("실행된 스킬 타입: " + skillType);

        // 스킬 호출 횟수 증가
        if (skillCallCounts.ContainsKey(skillType))
        {
            skillCallCounts[skillType]++;
        }
        else
        {
            skillCallCounts[skillType] = 1;
        }
        Debug.Log($"스킬 '{skillType}' 호출 횟수: {skillCallCounts[skillType]}");

        // slot 변수를 한 번만 선언
        GameObject slot = null;
        // text 변수를 한 번만 선언
        TextMeshProUGUI text = null;

        // 이미 같은 스킬 타입이 할당된 경우, 기존 슬롯 가져와 텍스트 업데이트
        if (assignedSkillObjects.ContainsKey(skillType))
        {
            slot = assignedSkillObjects[skillType];
            text = slot.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.enabled = true;
                text.text = skillCallCounts[skillType].ToString();
            }
            else
            {
                Debug.LogWarning($"'{slot.name}'의 자식에서 TextMeshProUGUI 컴포넌트를 찾을 수 없습니다.");
            }
            Debug.Log($"Skill '{skillType}' is already assigned. Text updated.");
            return;
        }

        // 사용되지 않은 첫 번째 슬롯 탐색
        slot = Aqskill.Find(go => !usedObjects.Contains(go));
        if (slot == null)
        {
            Debug.LogWarning("모든 슬롯이 이미 사용 중입니다!");
            return;
        }

        // 해당 슬롯을 사용 표시하고 맵에 등록
        usedObjects.Add(slot);
        assignedSkillObjects[skillType] = slot;

        // 자식 오브젝트에서 Image와 TextMeshProUGUI 컴포넌트 가져와 설정
        Image img = slot.GetComponentInChildren<Image>();
        text = slot.GetComponentInChildren<TextMeshProUGUI>();
        if (img != null && text != null)
        {
            img.enabled = true;
            text.enabled = true;
            img.sprite = _skillImage.GetSprite(skillType + "_0");
            text.text = skillCallCounts[skillType].ToString();
        }
        else
        {
            Debug.LogWarning($"'{slot.name}'의 자식에서 Image 또는 TextMeshProUGUI 컴포넌트를 찾을 수 없습니다.");
        }
    }


}
