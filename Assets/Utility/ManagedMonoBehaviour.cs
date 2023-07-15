using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagedMonoBehaviour : MonoBehaviour
{
    public virtual void Initialize() {}
    public virtual void ManagedUpdate() {} 
}