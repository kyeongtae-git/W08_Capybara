using UnityEngine;

public class PlayerManager
{
    //��ų ����
    int[] _skillLevelArray = new int[System.Enum.GetValues(typeof(SkillType)).Length];

    //ġȮ
    float _baseCritRate = 10f;
    float _curCritRate;

    //ġ��
    float _baseCritDamage = 100f;
    float _curCritDamage;

    //���ݷ�
    float _baseATKDamage = 10f;
    float _curATKDamage;

    //���� �ӵ�
    float _baseATKSpeed = 1.00f;
    float _curATKSpeed;

    //������ ���ʽ�
    //float _baseDamageBonus = 0;
    //float _curDamageBonus;

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

    //ġ��Ÿ Ȯ�� ����
    int _noCritStack = 0;
    //ġ��Ÿ �ʰ��� ��ȯ ����
    float _overCritCoeff = 2f;

    //���� ���� �ӵ�
    float _willDownSpeed = 8f;
    //���¹̳� ���� �ӵ�
    float _staminaDownSpeed = 8f;

    //ȿ���� ���
    //float _passiveCoeff = 0.15f;
    //float _conditionCoeff = 0.4f;
    //float _hitCoeff = 0.04f;

    public void Init()
    {
        _hitStack = 0;
        _noCritStack = 0;

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
            = SumCalc(_baseCritRate, 15f, _skillLevelArray[0], 30f, _skillLevelArray[4], 1.5f, _skillLevelArray[8]);
        
        _curCritDamage  
            = MultiplyCalc(_baseCritDamage, 0.30f, _skillLevelArray[1], 0.6000f, _skillLevelArray[5], 3f, _skillLevelArray[9]);
        _curATKDamage   
            = MultiplyCalc(_baseATKDamage, 0.150f, _skillLevelArray[2], 0.3000f, _skillLevelArray[6], 0.15f, _skillLevelArray[10]);
        
        _curATKSpeed    
            = MultiplyCalc(_baseATKSpeed, 0.1500f, _skillLevelArray[3], 0.3000f, _skillLevelArray[7], 0.015f, _skillLevelArray[11]);
        
        _maxWillPoint
            = MultiplyCalc(_baseWillPoint, 0.1000f, _skillLevelArray[12], 0, 0, 0, 0);
        _maxStaminaPoint
            = MultiplyCalc(_baseStaminaPoint, 0.1500f, _skillLevelArray[13], 0, 0, 0, 0);

        //ġ��Ÿ Ȯ�� 100% �ʰ� �� �ʰ� ���� _overCritCoeff�踸ŭ ġ��Ÿ ���ط� ��ȯ
        if (_curCritRate > 100f)
        {
            float oveCritRate = (_curCritRate - 100f) * _overCritCoeff;
            _curCritDamage += oveCritRate;
        }

        //�ִ밪 ����
        //_curCritRate = Mathf.Clamp(_curCritRate, _baseCritRate, 100f);
        _curATKSpeed = Mathf.Clamp(_curATKSpeed, _baseATKSpeed, 30f);
        //Debug.Log($"{_curCritRate}, {_curCritDamage}, {_curATKDamage}, {_curATKSpeed}");
    }

    //�տ���
    float SumCalc(float baseStatus, float passiveCoeff, float passiveLevel, float conditionCoeff, float conditionLevel, float hitCoeff, float hitLevel)
    {
        int stamina = (_staminaPoint > 0) ? 1 : 0;

        float result =
            baseStatus
            + (passiveCoeff * passiveLevel)
            + ((conditionCoeff * stamina) * conditionLevel)
            + (hitCoeff * hitLevel * _hitStack);

        return result;
    }

    //������
    float MultiplyCalc(float baseStatus, float passiveCoeff, float passiveLevel, float conditionCoeff, float conditionLevel, float hitCoeff, float hitLevel)
    {
        int stamina = (_staminaPoint > 0) ? 1 : 0;
        
        float result =
            (
            baseStatus 
            * (Mathf.Pow((1 + passiveCoeff), passiveLevel)) 
            * (Mathf.Pow((1 + (conditionCoeff * stamina)), conditionLevel))
            )
            + (hitCoeff * hitLevel * _hitStack);
        //* (1 + (hitCoeff * hitLevel * _hitStack));

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
        float finalCrit = _curCritRate * 0.01f * (_noCritStack + 1);
        if (Managers.Utils.RandomSuccess(finalCrit))
        {
            _noCritStack = 0;
            damage *= (1 + (_curCritDamage * 0.01f));
            Managers.UIManager.OnCritUIEvent?.Invoke(true);
        }
        else
        {
            //Ȯ�� ����
            _noCritStack++;
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
