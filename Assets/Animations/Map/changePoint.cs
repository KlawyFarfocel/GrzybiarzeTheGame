using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class changePoint : StateMachineBehaviour
{
    public string Point;
    public bool isEntering;
    private Animation targetAnimation;
    private int playCounter = 0;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (string.IsNullOrEmpty(Point)) return;

        string animationName = Point;
            if (isEntering)
            {
                animationName += "Enter";
            }
            else
            {
                animationName += "Exit";
            }
        targetAnimation = GameObject.Find(Point).GetComponent<Animation>();
        if(playCounter < 2 ) 
        {
            targetAnimation.Play(animationName);
            playCounter++;
        }
        else
        {
            animator.SetBool("hasPlayed", true);
        }
        
    }

}
