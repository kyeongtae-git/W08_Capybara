using System.Collections.Generic;
using UnityEngine;

public class RockHealthDataLoader : MonoBehaviour
{
    private TextAsset csvFile;
    private RockHealthData rockHealthData;
    public static RockHealthDataLoader _instance { get; private set; }

    public List<StageHP> LoadList()
    {
        List<StageHP> result = new();
        string[] lines = csvFile.text.Split("\n");

        for(int i=0; i<lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            string[] cells = lines[i].Split(',');

            if (cells.Length < 2) continue;

            int stage = int.Parse(cells[0].Trim());
            float hp = float.Parse(cells[1].Trim());

            result.Add(new StageHP { stageNumber = stage, HP = hp });
        }

        return result;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //로드해오기
        csvFile = Resources.Load<TextAsset>("KMJ/RockHealthcsvTable");
        rockHealthData = Resources.Load<RockHealthData>("KMJ/RockHealthDataTable");

        //csvFile로 데이터 초기화하기
        rockHealthData.MakeDictionary(LoadList());            
    }

    public float LoadHealthData(int stageNum)
    {
        return rockHealthData.GetHP(stageNum);
    }
}
