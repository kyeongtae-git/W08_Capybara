using UnityEngine;

public class PlayerManager
{
    //��ų ����
    int[] _skillLevelArray = new int[System.Enum.GetValues(typeof(SkillType)).Length];

    //ġȮ
    float _baseCritRate = 33f;
    float _curCritRate;

    //ġ��
    float _baseCritDamage = 100f;
    float _curCritDamage;

    //���ݷ�
    float _baseATKDamage = 10f;
    float _curATKDamage;

    //���� �ӵ�
    float _baseATKSpeed = 0.78f;
    float _curATKSpeed;

    //����
    float _baseWillPoint = 100f;
    float _willPoint;
    float _maxWillPoint;

    //���¹̳�
    float _baseStaminaPoint = 100f;
    float _staminaPoint;
    float _maxStaminaPoint;

    //���� Ƚ��
    int _hitStack = 0;

    //���� ���� �ӵ�
    float _willDownSpeed = 8f;
    //���¹̳� ���� �ӵ�
    float _staminaDownSpeed = 5f;

    //ȿ���� ���
    float _passiveCoeff = 0.15f;
    float _conditionCoeff = 0.4f;
    float _hitCoeff = 0.04f;

    public void Init()
    {
        _hitStack = 0;

        PlayerStatus();
        _willPoint = _maxWillPoint;
        _staminaPoint = _maxStaminaPoint;
        UpdateUI();
    }

    //index��° ��ų ������
    public void LevelUpSkill(int index)
    {
        _skillLevelArray[index]++;

        PlayerStatus();
        UpdateUI();
    }

    //���� (ġȮ, ġ��, ���ݷ�, ���ݼӵ�, �ִ� ����, �ִ� ���¹̳�) ���
    void PlayerStatus()
    {
        //���� ���
        _curCritRate    
            = CalcCurStatus(_baseCritRate, 0.16f, _skillLevelArray[0], 0.7f, _skillLevelArray[4], 0.8f, _skillLevelArray[8]);
        _curCritDamage  
            = CalcCurStatus(_baseCritDamage, 0.16f, _skillLevelArray[1], 0.7f, _skillLevelArray[5], 0.16f, _skillLevelArray[9]);
        _curATKDamage   
            = CalcCurStatus(_baseATKDamage, 0.16f, _skillLevelArray[2], 0.7f, _skillLevelArray[6], 0.12f, _skillLevelArray[10]);
        _curATKSpeed    
            = CalcCurStatus(_baseATKSpeed, 0.16f, _skillLevelArray[3], 0.7f, _skillLevelArray[7], 0.04f, _skillLevelArray[11]);
        _maxWillPoint
            = CalcCurStatus(_baseWillPoint, 0.16f, _skillLevelArray[12], 0, 0, 0, 0);
        _maxStaminaPoint
            = CalcCurStatus(_baseStaminaPoint, 0.16f, _skillLevelArray[13], 0, 0, 0, 0);

        //�ִ밪 ����
        _curCritRate = Mathf.Clamp(_curCritRate, _baseCritRate, 100f);
        _curATKSpeed = Mathf.Clamp(_curATKSpeed, _baseATKSpeed, 15f);
        //Debug.Log($"{_curCritRate}, {_curCritDamage}, {_curATKDamage}, {_curATKSpeed}");
    }

    //���� ����
    public float CalcCurStatus(float baseStatus, float passiveCoeff, float passiveLevel, float conditionCoeff, float conditionLevel, float hitCoeff, float hitLevel)
    {
        int stamina = (_staminaPoint > 0) ? 1 : 0;
        
        float result =
            (
            baseStatus 
            * (1 + (passiveCoeff * passiveLevel)) 
            * (1 + (conditionCoeff * conditionLevel * stamina))
            ) 
            + (hitCoeff * hitLevel * _hitStack);

        return result;
    }

    //���� ����
    public void DecreaseWill()
    {
        _willPoint -= _willDownSpeed * Time.deltaTime;

        Managers.UIManager.OnUpdateWillPointUIEvent?.Invoke(_willPoint, _maxWillPoint);
    }

    //������ 0���� Ȯ��
    public bool RunOutOfWill()
    {
        return (_willPoint <= 0);
    }

    //���� �ð� ���
    public float GetAttackTime()
    {
        float atkTime = (1 / _curATKSpeed);
        return atkTime;
    }

    //���� ���ط� ���
    public float GetFinalDamage()
    {
        //�⺻ ������
        float damage = _curATKDamage;
        Managers.UIManager.OnCritUIEvent?.Invoke(false);

        //ġ��Ÿ ������
        if (Managers.Utils.RandomSuccess(_curCritRate * 0.01f))
        {
            damage *= (1 + (_curCritDamage * 0.01f));
            Managers.UIManager.OnCritUIEvent?.Invoke(true);
        }

        //---������ ��� �� ������---

        //���� ���� ����
        _hitStack++;
        //���¹̳� �Ҹ�
        _staminaPoint -= _staminaDownSpeed;
        _staminaPoint = Mathf.Clamp(_staminaPoint, 0, _maxStaminaPoint);

        PlayerStatus();
        UpdateUI();

        return damage;
    }

    void UpdateUI()
    {
        //UI ����
        Managers.UIManager.OnUpdateCritRateUIEvent?.Invoke(_curCritRate);
        Managers.UIManager.OnUpdateCritDamageUIEvent?.Invoke(_curCritDamage);
        Managers.UIManager.OnUpdateATKDamageUIEvent?.Invoke(_curATKDamage);
        Managers.UIManager.OnUpdateATKSpeedUIEvent?.Invoke(_curATKSpeed);
        Managers.UIManager.OnUpdateWillPointUIEvent?.Invoke(_willPoint, _maxWillPoint);
        Managers.UIManager.OnUpdateStaminaPointUIEvent?.Invoke(_staminaPoint, _maxStaminaPoint);
    }
}
