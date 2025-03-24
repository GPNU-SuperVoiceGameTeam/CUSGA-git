using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWave : MonoBehaviour
{
    public float currentLifetime;
    public float pulseForce = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (collision.CompareTag("Enemy"))
        {

            Vector3 direction = (this.transform.position - collision.transform.position).normalized;
            rb.AddForce(-direction * pulseForce, ForceMode2D.Impulse);
        }
    }
    public void Initialize(float baseLifetime)
    {
        currentLifetime = baseLifetime;
        StartCoroutine(DestroyAfterTime());
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(currentLifetime);
        Destroy(gameObject);
    }
}
