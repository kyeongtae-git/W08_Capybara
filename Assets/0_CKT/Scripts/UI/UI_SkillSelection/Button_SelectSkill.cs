using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_SelectSkill : MonoBehaviour
{
    int _skillID;
    Button _selectSkillButton;

    void Start()
    {
        _selectSkillButton = GetComponent<Button>();
        SetSkill();

        Managers.UIManager.OnButton_SelectSkillSetEvent += SetSkill;
    }

    void SetSkill()
    {
        _skillID = Random.Range(0, System.Enum.GetValues(typeof(SkillType)).Length);
        TextMeshProUGUI TMP = GetComponentInChildren<TextMeshProUGUI>();
        TMP.text = $"Skill {(SkillType)_skillID}";

        _selectSkillButton.onClick.RemoveAllListeners();
        _selectSkillButton.onClick.AddListener(() => Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent?.Invoke(false));
        _selectSkillButton.onClick.AddListener(() => Managers.SkillManager.OverlapSkill(_skillID));
        _selectSkillButton.onClick.AddListener(() => StartCoroutine(Managers.GameManager.StartStage()));
    }
}
