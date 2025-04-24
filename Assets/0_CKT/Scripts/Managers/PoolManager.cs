using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    List<GameObject> prefabs = new List<GameObject>(); //����� ������
    List<GameObject>[] pools; //������ ������ �� ����Ʈ

    public void Init()
    {
        SetPrefab("Prefabs/Rock");
        SetPrefab("Prefabs/Emerald");

        pools = new List<GameObject>[prefabs.Count]; //����Ʈ�� ���̴� ����� ������� 1:1
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

    //������Ʈ ����
    public GameObject GetPrefabID(int index, Transform parent, Vector3 position)
    {
        if (index < 0) return null;
        if (index >= prefabs.Count)
        {
            Debug.Log("PrefabID : out of prefabs.Count");
        }

        GameObject select = null;

        foreach (GameObject item in pools[index]) //������ Ǯ�� ��Ȱ��ȭ�� ���ӿ�����Ʈ�� ����
        {
            if (!item.activeSelf) //�߰��ϸ�? select ������ �Ҵ�
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select) //�� ã����? ���Ӱ� �����ϰ� >> select ������ �Ҵ�
        {
            select = GameObject.Instantiate(prefabs[index]);
            pools[index].Add(select);
        }

        select.transform.parent = null;
        select.transform.position = position;

        return select; //select ��ȯ
    }

    //Ư�� ������Ʈ ��ü ��Ȱ��ȭ
    public void DeleteAllPrefabID(int index)
    {
        if (index < 0) return;

        foreach (GameObject item in pools[index]) //������ Ǯ�� ��Ȱ��ȭ�� ���ӿ�����Ʈ�� ����
        {
            if (item.activeSelf) //�߰��ϸ�? select ������ �Ҵ�
            {
                item.SetActive(false);
            }
        }
    }
}
