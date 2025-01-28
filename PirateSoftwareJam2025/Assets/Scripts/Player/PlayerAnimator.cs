using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform gun;
    SpriteRenderer SR;
    bool isHit = false;
    bool isDead = false;
    void HandleRotation()
    {
        if(gun.transform.GetChild(0).position.x - gun.transform.parent.position.x > 0)
        {
            SR.flipX = false;
        }
        else
            SR.flipX = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        SR = GetComponent<SpriteRenderer>(); 
        transform.parent.GetComponent<PlayerHealth>().OnDamaged.AddListener(HitAnimation); 
        transform.parent.GetComponent<PlayerHealth>().OnDie.AddListener(DieAnimation); 
    }

    void HitAnimation(int value)
    {
        StartCoroutine("HitCoroutine");
    }

    void DieAnimation()
    {
        isDead = true;
        animator.Play("VIRU_DEAD");
    }
    IEnumerator HitCoroutine()
    {
        isHit = true;

        yield return new WaitForSeconds(1);

        isHit = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        HandleRotation();

        HandleAnimation();
        
    }
    
    void HandleAnimation()
    {
        if (isHit)
        {
            animator.Play("VIRU_HIT");
            return;
        }

        if(PlayerInputHandler.Instance.GetMoveInput.magnitude > 0.1f)
        {
            animator.Play("VIRU_RUN");
        }

        else
        {
            animator.Play("VIRU_IDLE");
        }

    }
}
