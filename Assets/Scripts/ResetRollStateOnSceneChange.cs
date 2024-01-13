using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRollStateOnSceneChange : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject enemy=GameObject.Find("Enemy(Clone)");
        if(enemy != null)
        {
            enemy.GetComponent<EnemyClick>().isRolling = false;
        }
    }
}
