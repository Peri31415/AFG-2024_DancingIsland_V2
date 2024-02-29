using UnityEngine;
using TMPro;
using System.Data.Common;

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

        MyGameManager.instance.targetCount += 1;

        if (MyGameManager.instance.currentGameStage == "First Trial")
            targetCount.text = "Target Count: " + MyGameManager.instance.targetCount;         

        if (MyGameManager.instance.currentGameStage == "Third Trial")
            targetCount.text = "Enemies Down: " + MyGameManager.instance.targetCount;         
    }
}
