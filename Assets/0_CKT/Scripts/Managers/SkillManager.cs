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

    float _startMagni = 0.4f;
    float _hitMagni = 0.04f;
    float _passiveMagni = 0.15f;
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
        float cdMagni = 1 + (_startMagni * _skillOverlapList[(int)SkillType.Start_CD]);
        Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, cdMagni);

        float adMagni = 1 + (_startMagni * _skillOverlapList[(int)SkillType.Start_AD]);
        Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, adMagni);

        float asMagni = 1 + (_startMagni * _skillOverlapList[(int)SkillType.Start_AS]);
        Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, asMagni);

        Managers.PlayerManager.UpdateUI_Status();

        yield return new WaitForSeconds(_holdingTime);

        //버프 해제
        Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, (1 / cdMagni));

        Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, (1 / adMagni));

        Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, (1 / asMagni));

        Managers.PlayerManager.UpdateUI_Status();
    }

    public void HitBuff()
    {
        if (Managers.Utils.RandomSuccess(_hitSuccess))
        {
            float magni;

            magni = 1 + (_hitMagni * _skillOverlapList[(int)SkillType.Hit_CD]);
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, magni);

            magni = 1 + (_hitMagni * _skillOverlapList[(int)SkillType.Hit_AD]);
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, _hitMagni);

            magni = 1 + (1.02f * _skillOverlapList[(int)SkillType.Hit_AS]);
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, magni);

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
            Managers.PlayerManager.ChangeStatus((int)StatusType.Will, _willRecoveryPoint);

            magni = _recoveryPoint * _skillOverlapList[(int)SkillType.Hit_Stamina];
            Managers.PlayerManager.ChangeStatus((int)StatusType.Stamina, _recoveryPoint);
        }

        Managers.PlayerManager.UpdateUI_Status();
    }

    public void MaxBuff()
    {
        float magni;

        magni = 1 + (_passiveMagni * _skillOverlapList[(int)SkillType.P_CD]);
        Managers.PlayerManager.UpdateMaxStatus((int)StatusType.CritDamage, magni);

        magni = 1 + (_passiveMagni * _skillOverlapList[(int)SkillType.P_AD]);
        Managers.PlayerManager.UpdateMaxStatus((int)StatusType.ATKDamage, magni);

        magni = 1 + (_passiveMagni * _skillOverlapList[(int)SkillType.P_AS]);
        Managers.PlayerManager.UpdateMaxStatus((int)StatusType.ATKSpeed, magni);

        magni = 1 + (_passiveMagni * _skillOverlapList[(int)SkillType.P_Will]);
        Managers.PlayerManager.UpdateMaxStatus((int)StatusType.Will, magni);

        magni = 1 + (_passiveMagni * _skillOverlapList[(int)SkillType.P_Stamina]);
        Managers.PlayerManager.UpdateMaxStatus((int)StatusType.Stamina, magni);

        Managers.PlayerManager.UpdateUI_Status();
    }
}
