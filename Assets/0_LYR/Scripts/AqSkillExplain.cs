using UnityEngine;
using UnityEngine.EventSystems;
using static GetSkillList;

public class AqSkillExplain : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    string _desName; // 스킬 또는 스텟
    string _description;
    int _skillCallCount;
    float _skillValue;
    DescriptionPanel _descriptionPanel; // 패널 프리팹 참조
    AcquiredSkills _InfoData;
    GetSkillList _skillList;

    GameObject instantiatedPanel; // 인스턴스화된 패널



    void Start()
    {
        _descriptionPanel = Resources.Load<DescriptionPanel>("LYR/SkillPanel");
        _InfoData = FindAnyObjectByType<AcquiredSkills>();
        _skillList = FindAnyObjectByType<GetSkillList>();
    }


    string CheckParentSkill()
    {
        // 스크립트가 붙은 오브젝트의 부모 가져오기
        Transform parent = transform.parent;
        if (parent == null)
        {
            Debug.LogWarning("이 오브젝트는 부모가 없습니다!");
            return null;
        }

        // 부모 오브젝트 자체를 확인
        Skill skill;
        string skillType = _InfoData.GetSkillType(parent.gameObject);
        _skillCallCount = _InfoData.GetSkillCallCount(skillType);
        _skillList.TryGetSkill(skillType, out skill);

        _desName = skill.name;
        _description = skill.explaintext;
        _skillValue = skill.increaseValue;

        if (skillType != null)
        {
            //Debug.Log($"부모 오브젝트 '{parent.name}'에 할당된 스킬 타입: {skillType}, 호출 횟수: {_skillCallCount}");
            return skillType;
        }
        else
        {
            Debug.Log($"부모 오브젝트 '{parent.name}'은 할당된 스킬이 없습니다.");
            return null;
        }
    }





    public void OnPointerEnter(PointerEventData eventData)
    {
        // 패널 인스턴스화
        instantiatedPanel = Instantiate(_descriptionPanel.gameObject, transform);
        var panel = instantiatedPanel.GetComponent<DescriptionPanel>();

        CheckParentSkill();

        // 패널을 이미지/텍스트 하단에 표시
        Vector2 panelPosition = new Vector2(transform.position.x, transform.position.y - 85f); // 하단 오프셋 조정
        panel.ShowPanel(_desName,$"증가한 수치 : {_skillValue} X {_skillCallCount} = {_skillValue* _skillCallCount}", panelPosition);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (instantiatedPanel != null)
        {
            instantiatedPanel.GetComponent<DescriptionPanel>().HidePanel();
            Destroy(instantiatedPanel);
        }
    }
}
