using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeapon : MonoBehaviour
{
    public UnitMethod parentUnit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {   
           parentUnit.HitEnemy(collision);
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {   
           parentUnit.ExitEnemy(collision);
        } 
    }

}
