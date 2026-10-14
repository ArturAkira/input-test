using System.Collections;
using UnityEngine;

public class miniBoss1 : MonoBehaviour
{
    public int side;
    public Transform projectile;
    public Transform pivot;
    private bool bulletTime;

    void Start()
    {
        transform.right = Vector2.right * side;
    }
    private void Update()
    {
        if (!bulletTime) { StartCoroutine(shotFrequency()); }
    }

    IEnumerator shotFrequency()
    {
        bulletTime = true;
        Instantiate(projectile, pivot.position, transform.rotation);
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        bulletTime=false;
    }
}
