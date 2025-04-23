using UnityEngine;

public class BalanceTestScript : MonoBehaviour
{
    float totalDamage = 0;
    float basicDamage = 1;
    float damageScale = 1.04f;
    int currentBuff = 0;
    int attackNumber = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i<attackNumber; i++)
        {
            totalDamage += basicDamage * Mathf.Pow(damageScale, currentBuff);
            currentBuff++;
        }
        Debug.Log(totalDamage / (basicDamage * attackNumber));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
