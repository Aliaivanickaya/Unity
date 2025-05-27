using UnityEngine;

public class RocketCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            GameManager.Instance.GameOver();
            Time.timeScale = 0f; 
        }
    }
}
