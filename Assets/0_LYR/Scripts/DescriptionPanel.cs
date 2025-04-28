using TMPro;
using UnityEngine;

public class DescriptionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI desName; // 설명 텍스트 UI
    [SerializeField] private TextMeshProUGUI descriptionText;



    public void ShowPanel(string desName, string description, Vector2 position)
    {
        this.desName.text = desName;
        this.descriptionText.text = description;
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(position.x, 0, Screen.width - rect.sizeDelta.x/2),
            Mathf.Clamp(position.y, rect.sizeDelta.y, Screen.height)
        );
        transform.position = clampedPosition;
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
