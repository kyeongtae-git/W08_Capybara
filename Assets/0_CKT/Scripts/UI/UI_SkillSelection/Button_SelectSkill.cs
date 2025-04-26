using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GetSkillList;

public class Button_SelectSkill : MonoBehaviour
{
    Button _selectSkillButton;
    GetSkillList _skillList;

    AcquiredSkills _aqSkill;
    GetSkillImage _skillImage;

    void Start()
    {
        //SetSkill(Random.Range(0, System.Enum.GetValues(typeof(SkillType)).Length));

        //Managers.UIManager.OnButton_SelectSkillSetEvent += SetSkill;
    }

    public void SetSkill(int skillID)
    {
        TextMeshProUGUI skillname = transform.Find("SkillName").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillexplain = transform.Find("SkillExplain").GetComponent<TextMeshProUGUI>();
        Image image = transform.Find("Image").GetComponent<Image>();

        SkillType skillType = (SkillType)skillID;
        string skillTypestring =skillType.ToString();
        Skill skill;

        _selectSkillButton = GetComponent<Button>();
        _skillList = FindAnyObjectByType<GetSkillList>();
        _aqSkill = FindAnyObjectByType<AcquiredSkills>();
        _skillImage = FindAnyObjectByType<GetSkillImage>();

        Debug.Log(_skillList.name);
        if(_skillList.TryGetSkill(skillTypestring, out skill))
        {
            skillname.text = skill.name;
            skillexplain.text = skill.explaintext;
            image.sprite = _skillImage.GetSprite(skill.type+ "_0");
        }
        else
        {
            Debug.LogWarning($"스킬 키 '{skillTypestring}' 가 없음!");
        }

        _selectSkillButton.onClick.RemoveAllListeners();
        _selectSkillButton.onClick.AddListener(() => Managers.UIManager.OnUI_SkillSelectionCanvasEnableEvent?.Invoke(false, false));
        _selectSkillButton.onClick.AddListener(() => Managers.PlayerManager.LevelUpSkill(skillID));
        _selectSkillButton.onClick.AddListener(() => _aqSkill.AssignSkill(skill.type));
        _selectSkillButton.onClick.AddListener(() => StartCoroutine(Managers.GameManager.StartStage()));
    }
}
