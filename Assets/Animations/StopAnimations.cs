using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimations : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StopAnimation()
    {
        animator.speed = 0;
        Time.timeScale = 1f;
    }
}
