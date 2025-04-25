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
    float _baseATKSpeed = 0.78f;
    float _curATKSpeed;

    //의지
    float _maxWillPoint = 100f;
    float _willPoint;

    //스태미나
    float _maxStaminaPoint = 100f;
    float _staminaPoint;

    //적중 횟수
    int _hitStack = 0;

    //의지 감소 속도
    float _willDownSpeed = 9f;
    //스태미나 감소 속도
    float _staminaDownSpeed = 10f;

    //효과별 계수
    float _passiveCoeff = 0.15f;
    float _conditionCoeff = 0.4f;
    float _hitCoeff = 0.1f;

    public void Init()
    {
        _willPoint = _maxWillPoint;
        _staminaPoint = _maxStaminaPoint;
        _hitStack = 0;

        PlayerStatus();
        Managers.UIManager.OnUpdateWillPointUIEvent?.Invoke(_willPoint / _maxWillPoint);
        Managers.UIManager.OnUpdateStaminaPointUIEvent?.Invoke(_staminaPoint / _maxStaminaPoint);
    }

    //index번째 스킬 레벨업
    public void LevelUpSkill(int index)
    {
        _skillLevelArray[index]++;
    }

    //현재 (치확, 치피, 공격력, 공격속도) 계산 && UI 갱신
    void PlayerStatus()
    {
        //스탯 계산
        _curCritRate    = CalcCurStatus(_baseCritRate, 1, _skillLevelArray[0], _skillLevelArray[4], _skillLevelArray[8]);
        _curCritDamage  = CalcCurStatus(_baseCritDamage, 1, _skillLevelArray[1], _skillLevelArray[5], _skillLevelArray[9]);
        _curATKDamage   = CalcCurStatus(_baseATKDamage, 1, _skillLevelArray[2], _skillLevelArray[6], _skillLevelArray[10]);
        _curATKSpeed    = CalcCurStatus(_baseATKSpeed, 1, _skillLevelArray[3], _skillLevelArray[7], _skillLevelArray[11]);

        //최대값 제한
        _curCritRate = Mathf.Clamp(_curCritRate, _baseCritRate, 100f);
        _curATKSpeed = Mathf.Clamp(_curATKSpeed, _baseATKSpeed, 15f);

        //UI 갱신
        Managers.UIManager.OnUpdateCritRateUIEvent?.Invoke(_curCritRate);
        Managers.UIManager.OnUpdateCritDamageUIEvent?.Invoke(_curCritDamage);
        Managers.UIManager.OnUpdateATKDamageUIEvent?.Invoke(_curATKDamage);
        Managers.UIManager.OnUpdateATKSpeedUIEvent?.Invoke(_curATKSpeed);
        //Debug.Log($"{_curCritRate}, {_curCritDamage}, {_curATKDamage}, {_curATKSpeed}");
    }

    //스탯 계산식
    public float CalcCurStatus(float baseStatus, float coeff, float passive, float condition, float hit)
    {
        int stamina = (_staminaPoint > 0) ? 1 : 0;
        
        float result =
            (
            baseStatus 
            * (1 + (_passiveCoeff * coeff * passive)) 
            * (1 + (_conditionCoeff * coeff * condition * stamina))
            ) 
            + (_hitCoeff * coeff * hit * _hitStack);

        return result;
    }

    //의지 감소
    public void DecreaseWill()
    {
        _willPoint -= _willDownSpeed * Time.deltaTime;

        Managers.UIManager.OnUpdateWillPointUIEvent?.Invoke(_willPoint / _maxWillPoint);
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
        PlayerStatus();

        //기본 데미지
        float damage = _curATKDamage;
        //치명타 데미지
        if (Managers.Utils.RandomSuccess(_curCritRate * 0.01f))
        {
            damage *= (1 + (_curCritDamage * 0.01f));
        }

        //---데미지 계산 후 나머지---

        //적중 스택 증가
        _hitStack++;
        //스태미나 소모
        _staminaPoint -= _staminaDownSpeed;
        Managers.UIManager.OnUpdateStaminaPointUIEvent?.Invoke(_staminaPoint / _maxStaminaPoint);

        return damage;
    }
}
