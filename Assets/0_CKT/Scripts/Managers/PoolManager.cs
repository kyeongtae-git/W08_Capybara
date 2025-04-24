using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    List<GameObject> prefabs = new List<GameObject>(); //등록한 프리펩
    List<GameObject>[] pools; //생성한 프리펩 별 리스트

    public void Init()
    {
        SetPrefab("Prefabs/Rock");
        SetPrefab("Prefabs/Emerald");

        pools = new List<GameObject>[prefabs.Count]; //리스트의 길이는 등록한 프리펩과 1:1
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    void SetPrefab(string name)
    {
        GameObject obj = Resources.Load<GameObject>(name);
        if (obj == null)
        {
            Debug.LogError($"{name} prefab not found in Resources folder.");
            return;
        }
        else
        {
            prefabs.Add(obj);
        }
    }

    //오브젝트 생성
    public GameObject GetPrefabID(int index, Transform parent, Vector3 position)
    {
        if (index < 0) return null;
        if (index >= prefabs.Count)
        {
            Debug.Log("PrefabID : out of prefabs.Count");
        }

        GameObject select = null;

        foreach (GameObject item in pools[index]) //선택한 풀의 비활성화된 게임오브젝트에 접근
        {
            if (!item.activeSelf) //발견하면? select 변수에 할당
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select) //못 찾으면? 새롭게 생성하고 >> select 변수에 할당
        {
            select = GameObject.Instantiate(prefabs[index]);
            pools[index].Add(select);
        }

        select.transform.parent = null;
        select.transform.position = position;

        return select; //select 반환
    }

    //특정 오브젝트 전체 비활성화
    public void DeleteAllPrefabID(int index)
    {
        if (index < 0) return;

        foreach (GameObject item in pools[index]) //선택한 풀의 비활성화된 게임오브젝트에 접근
        {
            if (item.activeSelf) //발견하면? select 변수에 할당
            {
                item.SetActive(false);
            }
        }
    }
}
