using UnityEngine;
using UnityEngine.UI;

public class Image_Stamina : MonoBehaviour
{
    Image _staminaImage;

    void Start()
    {
        _staminaImage = GetComponent<Image>();
        Managers.UIManager.OnUpdateStaminaPointUIEvent += UpdateImage;
    }

    void UpdateImage(float amount)
    {
        _staminaImage.fillAmount = amount;
    }
}
