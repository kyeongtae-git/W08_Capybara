using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_RockHealth : MonoBehaviour
{
    Image _rockHealthImage;

    private void OnEnable()
    {
        _rockHealthImage = GetComponent<Image>();
        Managers.UIManager.OnUpdateRockHealthUIEvent += UpdateImage;
    }

    private void OnDestroy()
    {
        Managers.UIManager.OnUpdateRockHealthUIEvent -= UpdateImage;
    }

    void UpdateImage(float percent)
    {
        _rockHealthImage.fillAmount = percent;
    }
}
