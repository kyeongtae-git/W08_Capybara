using UnityEngine;

public class PlayerManager
{
    //스킬 종류
    int[] _skillLevelArray = new int[System.Enum.GetValues(typeof(SkillType)).Length];

    //치확
    float _baseCritRate = 10f;
    float _curCritRate;

    //치피
    float _baseCritDamage = 100f;
    float _curCritDamage;

    //공격력
    float _baseATKDamage = 10f;
    float _curATKDamage;

    //공격 속도
    float _baseATKSpeed = 1.00f;
    float _curATKSpeed;

    //데미지 보너스
    //float _baseDamageBonus = 0;
    //float _curDamageBonus;

    //의지
    float _baseWillPoint = 100f;
    float _willPoint;
    float _maxWillPoint;

    //스태미나
    float _baseStaminaPoint = 100f;
    float _staminaPoint;
    float _maxStaminaPoint;

    //적중 횟수
    int _hitStack = 0;

    //치명타 확률 보정
    int _noCritStack = 0;
    //치명타 초과분 전환 비율
    float _overCritCoeff = 1f;

    //의지 감소 속도
    float _willDownSpeed = 8f;
    //스태미나 감소 속도
    float _staminaDownSpeed = 12f;

    //효과별 계수
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

    //index번째 스킬 레벨업
    public void LevelUpSkill(int index)
    {
        _skillLevelArray[index]++;

        PlayerStatus();
        UpdateUI();
    }

    //현재 (치확, 치피, 공격력, 공격속도, 최대 의지, 최대 스태미나) 계산
    void PlayerStatus()
    {
        //스탯 계산
        _curCritRate    
            = SumCalc(_baseCritRate, 7.8f, _skillLevelArray[0], 13f, _skillLevelArray[4], 0.49f, _skillLevelArray[8]);
        
        _curCritDamage  
            = MultiplyCalc(_baseCritDamage, 0.18f, _skillLevelArray[1], 0.3f, _skillLevelArray[5], 0.0108f, _skillLevelArray[9]);
        _curATKDamage   
            = MultiplyCalc(_baseATKDamage, 0.15f, _skillLevelArray[2], 0.25f, _skillLevelArray[6], 0.009f, _skillLevelArray[10]);
        
        _curATKSpeed    
            = MultiplyCalc(_baseATKSpeed, 0.15f, _skillLevelArray[3], 0.25f, _skillLevelArray[7], 0.009f, _skillLevelArray[11]);
        
        _maxWillPoint
            = MultiplyCalc(_baseWillPoint, 0.10f, _skillLevelArray[12], 0, 0, 0, 0);
        _maxStaminaPoint
            = MultiplyCalc(_baseStaminaPoint, 0.15f, _skillLevelArray[13], 0, 0, 0, 0);

        //치명타 확률 100% 초과 시 초과 분의 _overCritCoeff배만큼 치명타 피해로 전환
        if (_curCritRate > 100f)
        {
            float oveCritRate = (_curCritRate - 100f) * _overCritCoeff;
            _curCritDamage += oveCritRate;
          
        }

        //최대값 제한
        //_curCritRate = Mathf.Clamp(_curCritRate, _baseCritRate, 100f);
        _curATKSpeed = Mathf.Clamp(_curATKSpeed, _baseATKSpeed, 12f);
        //Debug.Log($"{_curCritRate}, {_curCritDamage}, {_curATKDamage}, {_curATKSpeed}");
    }

    //합연산
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

    //곱연산
    float MultiplyCalc(float baseStatus, float passiveCoeff, float passiveLevel, float conditionCoeff, float conditionLevel, float hitCoeff, float hitLevel)
    {
        int stamina = (_staminaPoint > 0) ? 1 : 0;

        float result =
            (
            baseStatus
            * (Mathf.Pow((1 + passiveCoeff), passiveLevel))
            * (Mathf.Pow((1 + (conditionCoeff * stamina)), conditionLevel))
            * (1+hitCoeff * hitLevel * _hitStack)
            );
            
        //* (1 + (hitCoeff * hitLevel * _hitStack));

        return result;
    }

    //의지 감소
    public void DecreaseWill()
    {
        _willPoint -= _willDownSpeed * Time.deltaTime;

        Managers.UIManager.OnUpdateWillPointUIEvent?.Invoke(_willPoint, _maxWillPoint);
    }

    //의지가 0인지 확인
    public bool RunOutOfWill()
    {
        return (_willPoint <= 0);
    }

    //공격 시간 계산
    public float GetAttackTime()
    {
        float atkTime = (1 / _curATKSpeed);
        return atkTime;
    }

    //최종 피해량 계산
    public float GetFinalDamage()
    {
        //기본 데미지
        float damage = _curATKDamage;
        Managers.UIManager.OnCritUIEvent?.Invoke(false);

        //치명타 데미지
        float finalCrit = _curCritRate * 0.01f * (_noCritStack + 1);
        if (Managers.Utils.RandomSuccess(finalCrit))
        {
            _noCritStack = 0;
            damage *= (1 + (_curCritDamage * 0.01f));
            Managers.UIManager.OnCritUIEvent?.Invoke(true);
        }
        else
        {
            //확률 보정
            _noCritStack++;
        }

        //---데미지 계산 후 나머지---

        //적중 스택 증가
        _hitStack++;
        //스태미나 소모
        _staminaPoint -= _staminaDownSpeed;
        _staminaPoint = Mathf.Clamp(_staminaPoint, 0, _maxStaminaPoint);

        PlayerStatus();
        UpdateUI();

        return damage;
    }

    void UpdateUI()
    {
        //UI 갱신
        Managers.UIManager.OnUpdateCritRateUIEvent?.Invoke(_curCritRate);
        Managers.UIManager.OnUpdateCritDamageUIEvent?.Invoke(_curCritDamage);
        Managers.UIManager.OnUpdateATKDamageUIEvent?.Invoke(_curATKDamage);
        Managers.UIManager.OnUpdateATKSpeedUIEvent?.Invoke(_curATKSpeed);
        Managers.UIManager.OnUpdateWillPointUIEvent?.Invoke(_willPoint, _maxWillPoint);
        Managers.UIManager.OnUpdateStaminaPointUIEvent?.Invoke(_staminaPoint, _maxStaminaPoint);
    }
}
