using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    //��ų ȿ�� ����Ʈ
    List<ISkill> _skillList = new List<ISkill>();

    //i��° ��ų ��ø Ƚ�� ����Ʈ
    List<int> _skillOverlapList = new List<int>();

    public void Init()
    {
        _skillList = new List<ISkill>();
        //_skillList.Add();


        _skillOverlapList = new List<int>();
        for (int i = 0; i < _skillList.Count; i++)
        {
            _skillOverlapList.Add(0);
        }
    }
}
