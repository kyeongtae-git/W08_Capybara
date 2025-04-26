using UnityEngine;
using UnityEngine.EventSystems;

public class AqSkillExplain : MonoBehaviour
{
    //[SerializeField] private
    public GameObject panel; // 표시할 패널
    private RectTransform panelRectTransform;
    private RectTransform imageRectTransform;

    void Start()
    {
        // 초기 설정
        imageRectTransform = GetComponent<RectTransform>();
        panelRectTransform = panel.GetComponent<RectTransform>();
        panel.SetActive(false); // 패널 비활성화
    }

    // 마우스가 이미지 위에 올라갔을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true); // 패널 활성화

        // 이미지의 화면 좌표를 기준으로 패널 위치 설정
        Vector2 imagePos = imageRectTransform.position;
        panelRectTransform.position = imagePos;

        // 필요하면 패널 위치 조정 (예: 이미지 오른쪽 위로)
        // panelRectTransform.anchoredPosition += new Vector2(imageRectTransform.rect.width / 2, imageRectTransform.rect.height / 2);
    }

    // 마우스가 이미지 밖으로 나갔을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false); // 패널 비활성화
    }
}
