using UnityEngine;

public class BasicRock : MonoBehaviour, IRock
{
    public int HP { get; private set; } //현재 체력
    public int FullHP { get; private set; } = 12; // 최대 체력
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP = FullHP; //현재 체력 최대 체력으로 초기화
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
