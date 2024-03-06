using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float playerHealth = 100;
    
    private void Awake()
    {
       if (instance != null && instance != this) Destroy(this);
       else instance = this;
    }
}
