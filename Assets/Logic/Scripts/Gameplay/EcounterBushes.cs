using UnityEngine;

public class EcounterBushes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bush Encounterd");
        if (collision.tag == "Player")
        {
            GameManager.instance.startEnemyEncounter();
        }
    }
}
