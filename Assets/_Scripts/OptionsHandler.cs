using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
// public:
    public static OptionsHandler instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void SetSensitivity(Slider slider)
    {
        PlayerPrefs.SetFloat("Sensitivity", slider.value);
    }
}
