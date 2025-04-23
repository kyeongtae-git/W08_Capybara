using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    //i번째 스킬 중첩 횟수 리스트
    List<int> _skillOverlapList = new List<int>();

    float _holdingTime = 5f;
    float _hitSuccess = 1f;

    float _startMagni = 1.4f;
    float _hitMagni = 1.04f;
    float _passiveMagni = 1.15f;
    float _recoveryPoint = 1f;

    public void Init()
    {
        _skillOverlapList = new List<int>();
        for (int i = 0; i < System.Enum.GetValues(typeof(SkillType)).Length; i++)
        {
            _skillOverlapList.Add(0);
        }
    }

    public void OverlapSkill(int index)
    {
        _skillOverlapList[index]++;
    }

    /// <summary>
    /// 전투시작 시 일정 시간 동안 버프 적용
    /// </summary>
    public IEnumerator CoStartBuff()
    {
        //버프
        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_CD]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, _startMagni);
        }
        if (_skillOverlapList[(int)SkillType.Start_CD] > 0)
            Debug.Log($"전투 시작 : 치명타피해 증가 버프 적용 +{_skillOverlapList[(int)SkillType.Start_CD]}");

        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_AD]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, _startMagni);
        }
        if (_skillOverlapList[(int)SkillType.Start_AD] > 0)
            Debug.Log($"전투 시작 : 공격력 증가 버프 적용 +{_skillOverlapList[(int)SkillType.Start_AD]}");

        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_AS]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, _startMagni);
        }
        if (_skillOverlapList[(int)SkillType.Start_AS] > 0)
            Debug.Log($"전투 시작 : 공격속도 증가 버프 적용 +{_skillOverlapList[(int)SkillType.Start_AS]}");

        Managers.PlayerManager.UpdateUI_Status();
        yield return new WaitForSeconds(_holdingTime);

        //버프 해제
        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_CD]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, (1/ _startMagni));
        }
        if (_skillOverlapList[(int)SkillType.Start_CD] > 0)
            Debug.Log($"지속 시간 종료 : 치명타피해 증가 버프 해제 +{_skillOverlapList[(int)SkillType.Start_CD]}");

        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_AD]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, (1 / _startMagni));
        }
        if (_skillOverlapList[(int)SkillType.Start_AD] > 0)
            Debug.Log($"지속 시간 종료 : 공격력 증가 버프 해제 +{_skillOverlapList[(int)SkillType.Start_AD]}");

        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_AS]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, (1 / _startMagni));
        }
        if (_skillOverlapList[(int)SkillType.Start_AS] > 0)
            Debug.Log($"지속 시간 종료 : 공격속도 증가 버프 해제 +{_skillOverlapList[(int)SkillType.Start_AS]}");

        Managers.PlayerManager.UpdateUI_Status();
    }

    public void HitBuff()
    {
        if (Managers.Utils.RandomSuccess(_hitSuccess))
        {
            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_CD]; i++)
            {
                Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, _hitMagni);
            }
            if (_skillOverlapList[(int)SkillType.Hit_CD] > 0)
                Debug.Log($"적중 시 효과 : 치명타피해 증가 버프 적용 +{_skillOverlapList[(int)SkillType.Hit_CD]}");

            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_AD]; i++)
            {
                Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, _hitMagni);
            }
            if (_skillOverlapList[(int)SkillType.Hit_AD] > 0)
                Debug.Log($"적중 시 효과 : 공격력 증가 버프 적용 +{_skillOverlapList[(int)SkillType.Hit_AD]}");

            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_AS]; i++)
            {
                Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, _hitMagni);
            }
            if (_skillOverlapList[(int)SkillType.Hit_AS] > 0)
                Debug.Log($"적중 시 효과 : 공격속도 증가 버프 적용 +{_skillOverlapList[(int)SkillType.Hit_AS]}");
            float _willRecoveryPoint = 0;
            //한번 때릴때마다 채워질 의지 회복량 계산
            if(_skillOverlapList[(int)SkillType.Hit_Will]>0)
            {
                //스킬 몇번 찍었는지
                float n = _skillOverlapList[(int)SkillType.Hit_Will];
                //초당 공격 횟수
                float attackNum = Managers.PlayerManager.CurStatusList[3];
                _willRecoveryPoint = 4 / (n * attackNum) * (1 - Mathf.Pow((float)Math.E, -0.1f * n) * (1 - Mathf.Pow((float)Math.E, -0.1f * attackNum)));
            }
            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_Will]; i++)
            {
                Managers.PlayerManager.ChangeStatus((int)StatusType.Will, _willRecoveryPoint);
            }
            if (_skillOverlapList[(int)SkillType.Hit_Will] > 0)
                Debug.Log($"적중 시 효과 : 의지 획복 +{_skillOverlapList[(int)SkillType.Hit_Will]}");

            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_Stamina]; i++)
            {
                Managers.PlayerManager.ChangeStatus((int)StatusType.Stamina, _recoveryPoint);
            }
            if (_skillOverlapList[(int)SkillType.Hit_Stamina] > 0)
                Debug.Log($"적중 시 효과 : 스태미나 획복 +{_skillOverlapList[(int)SkillType.Hit_Stamina]}");
        }

        Managers.PlayerManager.UpdateUI_Status();
    }

    public void MaxBuff()
    {
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_CD]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.CritDamage, _passiveMagni);
        }
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_AD]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.ATKDamage, _passiveMagni);
        }
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_AS]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.ATKSpeed, _passiveMagni);
        }
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_Will]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.Will, _passiveMagni);
        }
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_Stamina]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.Stamina, _passiveMagni);
        }

        Managers.PlayerManager.UpdateUI_Status();
    }
}
