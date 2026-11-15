using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject player;
    static public AnimationManager instance;

    void Start()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator TransitionCoroutine(Vector2 targetPosition)
    {
        transitionAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        float length = transitionAnimator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(length / 2f);
        player.transform.position = targetPosition;
        
        
    }
    public void OnWalk()
    {
        playerAnimator.SetTrigger("OnWalk");
    }
    public void OnStopWalking()
    {
        playerAnimator.SetTrigger("OnStop");
    }
    public void OnJump()
    {
        playerAnimator.SetTrigger("OnJump");
    }
    public void OnStopJumping()
    {
        playerAnimator.SetTrigger("OnStop");
    }
}
