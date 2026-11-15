using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private StageInfo stageInfo;
    [SerializeField] private GameObject enterPortal;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AnimationManager.instance.StartCoroutine(AnimationManager.instance.TransitionCoroutine(enterPortal.transform.position));
        }
    }

}
