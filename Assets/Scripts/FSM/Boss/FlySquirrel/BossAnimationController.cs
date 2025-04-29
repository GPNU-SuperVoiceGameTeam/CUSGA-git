using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private Animator animator;
    private ParticleSystem effectParticleSystem;

    void Start()
    {
        animator = GetComponent<Animator>();
        effectParticleSystem = GetComponent<ParticleSystem>();
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDamageEffect()
    {
        effectParticleSystem.Play();
    }

    // ������������Ч���Ʒ���...
}
