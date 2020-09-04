using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonelCampbellController : MonoBehaviour
{
    public Animator animator;
    public float animationTimer;
    public float nextAnimation;

    // Start is called before the first frame update
    void Start()
    {
        if(animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
        animationTimer = 0;
        nextAnimation = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(animator)
        {
            animationTimer += Time.deltaTime;
            //Debug.Log("AnimatorTimer: " + animationTimer);
            //Debug.Log("nextAnimation: " + nextAnimation);
            if (nextAnimation <= animationTimer)
            {
                animationTimer = 0;
                nextAnimation = Random.Range(9, 14);
                switch (Random.Range(0, 9))
                {
                    case 1:
                        animator.SetTrigger("Squat");
                        break;
                    case 2:
                        animator.SetTrigger("TakeAKnee");
                        break;
                    case 3:
                        animator.SetTrigger("Chuckle");
                        break;
                    case 4:
                        animator.SetTrigger("CheckingOut");
                        break;
                    case 5:
                        animator.SetTrigger("Shrug");
                        break;
                    case 6:
                        animator.SetTrigger("Salute");
                        break;
                    case 7:
                        animator.SetTrigger("Thinking");
                        break;
                    case 8:
                        animator.SetTrigger("AllGood");
                        break;
                    case 9:
                        animator.SetTrigger("");
                        break;
                    default:
                        animator.SetTrigger("Shrug");
                        break;
                }
            }
        }
    }
}
