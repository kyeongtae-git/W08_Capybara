using UnityEngine;
using UnityEngine.UI;

public class Button_SelectEvent : MonoBehaviour
{
    Button _selectEventButton;

    private void Start()
    {
        _selectEventButton = GetComponent<Button>();
        _selectEventButton.onClick.AddListener(() => Managers.UIManager.OnUI_EventCanvasEnableEvent?.Invoke(false));
        _selectEventButton.onClick.AddListener(() => Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent?.Invoke(true, false));
    }

    void SetEvent()
    {

    }
}
