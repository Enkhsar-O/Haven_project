using UnityEngine;

public class GuuAct : MonoBehaviour
{
    [SerializeField] private GameObject guuPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GuuUse(float guuUseVelocity, Vector2 guuUseDirection)
    {
        this.GetComponent<Rigidbody2D>().linearVelocity = guuUseDirection * guuUseVelocity;
        
        
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        Createguu();
    }
    void Createguu()
    {
        Vector2 currentLocation = this.GetComponent<Transform>().position;
        Instantiate(guuPrefab, currentLocation, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
