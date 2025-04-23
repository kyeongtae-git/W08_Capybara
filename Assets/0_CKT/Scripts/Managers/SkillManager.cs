using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    //스킬 효과 리스트
    List<ISkill> _skillList = new List<ISkill>();

    //i번째 스킬 중첩 횟수 리스트
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
