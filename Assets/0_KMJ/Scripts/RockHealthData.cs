using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

//딕셔너리는 직렬화가 불가능하므로 구조체를 직렬화하고 이 구조체를 딕셔너리에 넣어주는 형식
[System.Serializable]
public struct StageHP
{
    public int stageNumber;
    public float HP;
}

[CreateAssetMenu(fileName ="RockHealthDataTable",menuName ="Scriptable Object/RockHealthDataTable",order =1 )]
public class RockHealthData : ScriptableObject
{
    //값넣으면 그걸 반환해줄 딕셔너리
    private Dictionary<int, float> HPTableDict;

    //딕셔너리 초기화해주는 함수.
    public void MakeDictionary(List<StageHP> HPList)
    {
        HPTableDict = new();
        foreach(StageHP entry in HPList)
        {
            if(!HPTableDict.ContainsKey(entry.stageNumber))
            {
                HPTableDict.Add(entry.stageNumber, entry.HP);
            }
        }
    }

    //hp배율 가져올 함수. 스테이지 넘버 넣어주시면 최대 체력 몇인지 나와요
    public float GetHP(int stage)
    {
        //만약 오류가 발생하면 1f 돌려보내요.
        return HPTableDict.ContainsKey(stage) ? HPTableDict[stage] : 1f;
    }
}
