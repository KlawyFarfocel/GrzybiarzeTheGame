using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClick : MonoBehaviour
{
    
    public void EnemyClickAction(GameObject enemy)
    {
        //kod do obslugi walki po kliknieciu na przeciwnika
        Enemy enemyData = enemy.GetComponent<Enemy>();
        Debug.Log("Zadano dmg" + enemyData.Damage);
    }
}
