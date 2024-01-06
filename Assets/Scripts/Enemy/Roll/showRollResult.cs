using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class showRollResult : StateMachineBehaviour 
{
    public int rollResult;
    public string rollEffect;

    public void showResult(int roll,string result)
    {
        string path = $"D20/{roll}-{result}";
        GameObject.Find("d20").GetComponent<Animator>().enabled = false;
        GameObject.Find("d20").GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        var DMGPanel = GameObject.Find("DMGPanel").GetComponent<RectTransform>();
        DMGPanel.localPosition = new Vector3(0, -200, 0);

        if(rollEffect == "SUCCESS" ) //trafiony
        {
            if (rollResult == 20)//kryt
            {
                GameObject attackPrefab = Resources.Load<GameObject>("Prefabs/Attacks/AttackCrit");
                GameObject attackNormal = Instantiate(attackPrefab);
                GameObject.Find("DMGText").GetComponent<TextMeshProUGUI>().text = "KRYT!";

                attackNormal.transform.SetParent(GameObject.Find("Enemy(Clone)").transform, true);
                attackNormal.transform.localPosition = new Vector3(0, 0.2f, 0);
                attackNormal.transform.localScale = new Vector3(1, 1, 1);
                attackNormal.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 2);

                GameObject Enemy = GameObject.Find("Enemy(Clone)");
                Enemy.GetComponent<EnemyClick>().HandleDMGCalc(Enemy);

                Destroy(attackNormal, 0.51f);
            }
            else //zwykly
            {
                GameObject attackPrefab = Resources.Load<GameObject>("Prefabs/Attacks/AttackNormal");
                GameObject attackNormal = Instantiate(attackPrefab);
                GameObject.Find("DMGText").GetComponent<TextMeshProUGUI>().text = "TRAFIENIE!";


                attackNormal.transform.SetParent(GameObject.Find("Enemy(Clone)").transform, true);
                attackNormal.transform.localPosition = new Vector3(0, 0.2f, 0);
                attackNormal.transform.localScale = new Vector3(1, 1, 1);
                attackNormal.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 2);

                GameObject Enemy = GameObject.Find("Enemy(Clone)");
                Enemy.GetComponent<EnemyClick>().HandleDMGCalc(Enemy);

                Destroy(attackNormal, 0.68f);
            }
        }
        else //miss
        {
            GameObject attackPrefab = Resources.Load<GameObject>("Prefabs/Attacks/AttackMiss");
            GameObject attackNormal = Instantiate(attackPrefab);
            GameObject.Find("DMGText").GetComponent<TextMeshProUGUI>().text = "PUD£O!";

            attackNormal.transform.SetParent(GameObject.Find("Enemy(Clone)").transform, true);
            attackNormal.transform.localPosition = new Vector3(-1f, 0.2f, 0);
            attackNormal.transform.localScale = new Vector3(1, 1, 1);
            attackNormal.GetComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 2);
            Destroy(attackNormal, 0.68f);
            
        }
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject d20=GameObject.Find("d20");
        rollResult = int.Parse(Variables.Object(d20).Get("result").ToString());
        rollEffect = Variables.Object(d20).Get("effect").ToString();
        showResult(rollResult, rollEffect);
    }

}
