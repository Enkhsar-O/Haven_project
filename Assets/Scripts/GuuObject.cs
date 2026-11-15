using UnityEngine;

public class GuuObject : MonoBehaviour
{
    [SerializeField] private int maxUseCount;

    private int currentUseCount;
    void OnEnable()
    {
        currentUseCount = maxUseCount;
    }
    public bool UseGuu()
    {
        currentUseCount--;

        if (currentUseCount <= 0)
        {
            Destroy(gameObject);
            return false;
        }

        transform.localScale *= (1f - 1f / maxUseCount);

        return true;
    }
}
