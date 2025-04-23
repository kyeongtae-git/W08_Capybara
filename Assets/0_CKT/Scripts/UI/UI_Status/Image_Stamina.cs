using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_Stamina : MonoBehaviour
{
    Image _staminaImage;

    void Start()
    {
        _staminaImage = GetComponent<Image>();
        Managers.UIManager.OnUpdateUIEvent += UpdateImage;
    }

    void UpdateImage(List<float> list)
    {
        _staminaImage.fillAmount = list[(int)StatusType.Stamina];
    }
}
