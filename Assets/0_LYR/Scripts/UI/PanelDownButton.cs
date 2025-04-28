using UnityEngine;
using UnityEngine.UI;

public class PanelDownButton : MonoBehaviour
{
    Button _panelDownButton;
    Canvas _skillCanvas;
    UI_SkillSeletctionDown _buttonUI;
    UI_SkillSeletctionUp _upButtonUI;

    void Start()
    {
        _skillCanvas = FindAnyObjectByType<UI_SkillSelection>().gameObject.GetComponent<Canvas>();
        _buttonUI = FindAnyObjectByType<UI_SkillSeletctionDown>();
        _upButtonUI = FindAnyObjectByType<UI_SkillSeletctionUp>();
        _panelDownButton = GetComponent<Button>();

        ButtonClick();
    }

    public void ButtonClick()
    {
        _panelDownButton.onClick.RemoveAllListeners();
        _panelDownButton.onClick.AddListener(() => _skillCanvas.enabled = false);
        _panelDownButton.onClick.AddListener(() => _buttonUI.CanvasOnOff(false));
        _panelDownButton.onClick.AddListener(() => _upButtonUI.CanvasOnOff(true));

    }




}
