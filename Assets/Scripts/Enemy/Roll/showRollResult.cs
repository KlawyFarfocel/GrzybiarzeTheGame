using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject d20=GameObject.Find("d20");
        rollResult = int.Parse(Variables.Object(d20).Get("result").ToString());
        rollEffect = Variables.Object(d20).Get("effect").ToString();
        showResult(rollResult, rollEffect);
    }

}
