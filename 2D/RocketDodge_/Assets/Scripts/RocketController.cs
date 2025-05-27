using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Передвижение влево/вправо (A/D или стрелки ← →)
        float moveX = Input.GetAxis("Horizontal");
        Vector3 newPos = transform.position + new Vector3(moveX, 0, 0) * moveSpeed * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, -12f, 12f);
        transform.position = newPos;
    }
}