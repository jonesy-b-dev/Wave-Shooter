using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Hit();
    void Death();
    void Shoot();
}