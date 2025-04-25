using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillSelection : MonoBehaviour
{
    Canvas _canvas;
    Button_SelectSkill[] _skills;

    void Start()
    {
        _canvas = GetComponent<Canvas>();

        Button[] buttons = GetComponentsInChildren<Button>();
        _skills = new Button_SelectSkill[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            _skills[i] = buttons[i].GetComponent<Button_SelectSkill>();
        }

        Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent += OnCanvasEnable;

        OnCanvasEnable(true);
    }

    void OnCanvasEnable(bool boolean)
    {
        _canvas.enabled = boolean;
        Debug.Log($"스킬 선택 패널 {boolean}");

        int max = System.Enum.GetValues(typeof(SkillType)).Length;
        List<int> list = Managers.Utils.GetRandomNumbers(0, max, _skills.Length);

        for (int i = 0; i < _skills.Length; i++)
        {
            _skills[i].SetSkill(list[i]);
        }
    }
}
