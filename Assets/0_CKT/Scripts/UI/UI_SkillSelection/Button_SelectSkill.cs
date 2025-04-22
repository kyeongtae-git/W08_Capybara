using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_SelectSkill : MonoBehaviour
{
    [SerializeField] int _skillID;
    Button _selectSkillButton;

    void Start()
    {
        _selectSkillButton = GetComponent<Button>();
        _selectSkillButton.onClick.AddListener(() => Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent?.Invoke(false));
        _selectSkillButton.onClick.AddListener(() => StartCoroutine(Managers.GameManager.StartStage()));
        SetSkill();
    }

    void SetSkill()
    {
        _skillID = Random.Range(0, 30);
        TextMeshProUGUI TMP = GetComponentInChildren<TextMeshProUGUI>();
        TMP.text = $"Skill {_skillID}";
    }
}
