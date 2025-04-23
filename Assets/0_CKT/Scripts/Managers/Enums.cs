using UnityEngine;

public enum GameState { Idle, Fight }

public enum SkillType
{
    Start_CD,
    Start_AD,
    Start_AS,
    Hit_CD,
    Hit_AD,
    Hit_AS,
    Hit_Will,
    Hit_Stamina,
    P_CD,
    P_AD,
    P_AS,
    P_Will,
    P_Stamina
}

public enum StatusType
{ 
    CritRate,
    CritDamage,
    ATKDamage,
    ATKSpeed,
    Will,
    Stamina
}


public class Enums : MonoBehaviour
{

}
