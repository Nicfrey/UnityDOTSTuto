using UnityEngine;

public class ShootPopup : MonoBehaviour
{
    private float destroyTimer = 1f;
    
    void Update()
    {
        float moveSpeed = 2f;
        transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        
        destroyTimer -= Time.deltaTime;
        if(destroyTimer <= 0)
            Destroy(gameObject);
    }
}
