using UnityEngine;

public enum GameState { Idle, Ready, Fight }

public enum SkillType
{
    Passive_CR,
    Passive_CD,
    Passive_AD,
    Passive_AS,
    Condition_CR,
    Condition_CD,
    Condition_AD,
    Condition_AS,
    Hit_CR,
    Hit_CD,
    Hit_AD,
    Hit_AS,
    Max_Will,
    Max_Stamina
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
