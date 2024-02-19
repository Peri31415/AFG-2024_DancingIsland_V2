using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    public float health = 10f;

    [SerializeField] private TextMeshProUGUI targetCount; 

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();            
        }
    }

    void Die()
    {
        Destroy(gameObject);

        MyGameManager.Instance.targetCount += 1;

        targetCount.text = "Target Count: " + MyGameManager.Instance.targetCount;         
    }
}
