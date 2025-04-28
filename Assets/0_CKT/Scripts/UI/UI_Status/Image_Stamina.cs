using UnityEngine;
using UnityEngine.UI;

public class Image_Stamina : MonoBehaviour
{
    Image _staminaImage;

    private void OnEnable()
    {
        _staminaImage = GetComponent<Image>();
        Managers.UIManager.OnUpdateStaminaPointUIEvent += UpdateImage;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUpdateStaminaPointUIEvent -= UpdateImage;
    }

    void UpdateImage(float cur, float max)
    {
        _staminaImage.fillAmount = (cur/max);
    }
}
