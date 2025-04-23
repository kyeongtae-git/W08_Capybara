using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    //i��° ��ų ��ø Ƚ�� ����Ʈ
    List<int> _skillOverlapList = new List<int>();

    float _holdingTime = 5f;
    float _hitSuccess = 0.3f;

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
    /// �������� �� ���� �ð� ���� ���� ����
    /// </summary>
    public IEnumerator CoStartBuff()
    {
        //����
        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_CD]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, 1.15f);
        }
        if (_skillOverlapList[(int)SkillType.Start_CD] > 0)
            Debug.Log($"���� ���� : ġ��Ÿ���� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Start_CD]}");

        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_AD]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, 1.15f);
        }
        if (_skillOverlapList[(int)SkillType.Start_AD] > 0)
            Debug.Log($"���� ���� : ���ݷ� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Start_AD]}");

        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_AS]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, 1.15f);
        }
        if (_skillOverlapList[(int)SkillType.Start_AS] > 0)
            Debug.Log($"���� ���� : ���ݼӵ� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Start_AS]}");

        yield return new WaitForSeconds(_holdingTime);

        //���� ����
        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_CD]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, (1/ 1.15f));
        }
        if (_skillOverlapList[(int)SkillType.Start_CD] > 0)
            Debug.Log($"���� �ð� ���� : ġ��Ÿ���� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Start_CD]}");

        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_AD]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, (1 / 1.15f));
        }
        if (_skillOverlapList[(int)SkillType.Start_AD] > 0)
            Debug.Log($"���� �ð� ���� : ���ݷ� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Start_AD]}");

        for (int i = 0; i < _skillOverlapList[(int)SkillType.Start_AS]; i++)
        {
            Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, (1 / 1.15f));
        }
        if (_skillOverlapList[(int)SkillType.Start_AS] > 0)
            Debug.Log($"���� �ð� ���� : ���ݼӵ� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Start_AS]}");
    }

    public void HitBuff()
    {
        if (Managers.Utils.RandomSuccess(_hitSuccess))
        {
            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_CD]; i++)
            {
                Managers.PlayerManager.UpdateCurStatus((int)StatusType.CritDamage, 1.15f);
            }
            if (_skillOverlapList[(int)SkillType.Hit_CD] > 0)
                Debug.Log($"���� �� ȿ�� : ġ��Ÿ���� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Hit_CD]}");

            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_AD]; i++)
            {
                Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKDamage, 1.15f);
            }
            if (_skillOverlapList[(int)SkillType.Hit_AD] > 0)
                Debug.Log($"���� �� ȿ�� : ���ݷ� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Hit_AD]}");

            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_AS]; i++)
            {
                Managers.PlayerManager.UpdateCurStatus((int)StatusType.ATKSpeed, 1.15f);
            }
            if (_skillOverlapList[(int)SkillType.Hit_AS] > 0)
                Debug.Log($"���� �� ȿ�� : ���ݼӵ� ���� ���� ���� +{_skillOverlapList[(int)SkillType.Hit_AS]}");

            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_Will]; i++)
            {
                Managers.PlayerManager.ChangeStatus((int)StatusType.Will, 10f);
            }
            if (_skillOverlapList[(int)SkillType.Hit_Will] > 0)
                Debug.Log($"���� �� ȿ�� : ���� ȹ�� +{_skillOverlapList[(int)SkillType.Hit_Will]}");

            for (int i = 0; i < _skillOverlapList[(int)SkillType.Hit_Stamina]; i++)
            {
                Managers.PlayerManager.ChangeStatus((int)StatusType.Stamina, 10f);
            }
            if (_skillOverlapList[(int)SkillType.Hit_Stamina] > 0)
                Debug.Log($"���� �� ȿ�� : ���¹̳� ȹ�� +{_skillOverlapList[(int)SkillType.Hit_Stamina]}");
        }
    }

    public void MaxBuff()
    {
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_CD]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.CritDamage, 1.15f);
        }
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_AD]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.ATKDamage, 1.15f);
        }
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_AS]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.ATKSpeed, 1.15f);
        }
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_Will]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.Will, 1.15f);
        }
        for (int i = 0; i < _skillOverlapList[(int)SkillType.P_Stamina]; i++)
        {
            Managers.PlayerManager.UpdateMaxStatus((int)StatusType.Stamina, 1.15f);
        }
    }
}
