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
    float _maxWillPoint = 100f;
    float _willPoint;

    //���¹̳�
    float _maxStaminaPoint = 100f;
    float _staminaPoint;

    //���� Ƚ��
    int _hitStack = 0;

    //���� ���� �ӵ�
    float _willDownSpeed = 9f;
    //���¹̳� ���� �ӵ�
    float _staminaDownSpeed = 10f;

    //ȿ���� ���
    float _passiveCoeff = 0.15f;
    float _conditionCoeff = 0.4f;
    float _hitCoeff = 0.04f;

    public void Init()
    {
        _willPoint = _maxWillPoint;
        _staminaPoint = _maxStaminaPoint;
        _hitStack = 0;

        PlayerStatus();
        Managers.UIManager.OnUpdateWillPointUIEvent?.Invoke(_willPoint / _maxWillPoint);
        Managers.UIManager.OnUpdateStaminaPointUIEvent?.Invoke(_staminaPoint / _maxStaminaPoint);
    }

    //index��° ��ų ������
    public void LevelUpSkill(int index)
    {
        _skillLevelArray[index]++;
        PlayerStatus();
    }

    //���� (ġȮ, ġ��, ���ݷ�, ���ݼӵ�) ��� && UI ����
    void PlayerStatus()
    {
        //���� ���
        _curCritRate    
            = CalcCurStatus(_baseCritRate, 0.15f, _skillLevelArray[0], 0.4f, _skillLevelArray[4], 0.4f, _skillLevelArray[8]);
        _curCritDamage  
            = CalcCurStatus(_baseCritDamage, 0.15f, _skillLevelArray[1], 0.4f, _skillLevelArray[5], 0.08f, _skillLevelArray[9]);
        _curATKDamage   
            = CalcCurStatus(_baseATKDamage, 0.15f, _skillLevelArray[2], 0.4f, _skillLevelArray[6], 0.04f, _skillLevelArray[10]);
        _curATKSpeed    
            = CalcCurStatus(_baseATKSpeed, 0.15f, _skillLevelArray[3], 0.4f, _skillLevelArray[7], 0.02f, _skillLevelArray[11]);

        //�ִ밪 ����
        _curCritRate = Mathf.Clamp(_curCritRate, _baseCritRate, 100f);
        _curATKSpeed = Mathf.Clamp(_curATKSpeed, _baseATKSpeed, 20f);

        //UI ����
        Managers.UIManager.OnUpdateCritRateUIEvent?.Invoke(_curCritRate);
        Managers.UIManager.OnUpdateCritDamageUIEvent?.Invoke(_curCritDamage);
        Managers.UIManager.OnUpdateATKDamageUIEvent?.Invoke(_curATKDamage);
        Managers.UIManager.OnUpdateATKSpeedUIEvent?.Invoke(_curATKSpeed);
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

        Managers.UIManager.OnUpdateWillPointUIEvent?.Invoke(_willPoint / _maxWillPoint);
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
        //ġ��Ÿ ������
        if (Managers.Utils.RandomSuccess(_curCritRate * 0.01f))
        {
            damage *= (1 + (_curCritDamage * 0.01f));
        }

        //---������ ��� �� ������---

        //���� ���� ����
        _hitStack++;
        //���¹̳� �Ҹ�
        _staminaPoint -= _staminaDownSpeed;
        Managers.UIManager.OnUpdateStaminaPointUIEvent?.Invoke(_staminaPoint / _maxStaminaPoint);

        PlayerStatus();

        return damage;
    }
}
