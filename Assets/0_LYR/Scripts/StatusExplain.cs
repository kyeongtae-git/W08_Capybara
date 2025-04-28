using UnityEngine;
using UnityEngine.EventSystems;
using static GetSkillList;

public class StatusExplain : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    string _desName; // 스킬 또는 스텟
    [SerializeField]
    string _description;
    DescriptionPanel _descriptionPanel; // 패널 프리팹 참조

    GameObject instantiatedPanel; // 인스턴스화된 패널



    void Start()
    {
        _descriptionPanel = Resources.Load<DescriptionPanel>("LYR/StatPanel");

    }





    public void OnPointerEnter(PointerEventData eventData)
    {
        // 패널 인스턴스화
        instantiatedPanel = Instantiate(_descriptionPanel.gameObject, transform);
        var panel = instantiatedPanel.GetComponent<DescriptionPanel>();


        // 패널을 이미지/텍스트 하단에 표시
        Vector2 panelPosition = new Vector2(transform.position.x, transform.position.y -55f); // 하단 오프셋 조정
        panel.ShowPanel(_desName, _description, panelPosition);

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
