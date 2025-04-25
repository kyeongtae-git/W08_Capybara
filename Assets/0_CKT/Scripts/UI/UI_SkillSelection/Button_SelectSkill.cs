using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GetSkillList;

public class Button_SelectSkill : MonoBehaviour
{
    Button _selectSkillButton;
    GetSkillList _skillList;

    AcquiredSkills _aqSkill;

    void Start()
    {
        _selectSkillButton = GetComponent<Button>();
        _skillList = FindAnyObjectByType<GetSkillList>();
        _aqSkill = FindAnyObjectByType<AcquiredSkills>();
        //SetSkill(Random.Range(0, System.Enum.GetValues(typeof(SkillType)).Length));

        //Managers.UIManager.OnButton_SelectSkillSetEvent += SetSkill;
    }

    public void SetSkill(int skillID)
    {
        TextMeshProUGUI skillname = transform.Find("SkillName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillexplain = transform.Find("SkillExplain").GetComponent<TextMeshProUGUI>();

        SkillType skillType = (SkillType)skillID;
        string skillTypestring =skillType.ToString();
        Skill skill;

        if(_skillList.TryGetSkill(skillTypestring, out skill))
        {
            skillname.text = skill.name;
            skillexplain.text = skill.explaintext;
        }
        else
        {
            Debug.LogWarning($"스킬 키 '{skillTypestring}' 가 없음!");
        }

        _selectSkillButton.onClick.RemoveAllListeners();
        _selectSkillButton.onClick.AddListener(() => Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent?.Invoke(false));
        _selectSkillButton.onClick.AddListener(() => Managers.PlayerManager.LevelUpSkill(skillID));
        _selectSkillButton.onClick.AddListener(() => _aqSkill.AssignSkill(skill.type));
        _selectSkillButton.onClick.AddListener(() => StartCoroutine(Managers.GameManager.StartStage()));
    }
}
