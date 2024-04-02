using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
// public:
    public static OptionsHandler instance;
    public float sensitivityMultiplier;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public void SetSensitivity(Slider slider)
    {
        sensitivityMultiplier = slider.value;
    }
}
