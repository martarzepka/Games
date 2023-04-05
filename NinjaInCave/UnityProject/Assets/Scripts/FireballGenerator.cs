using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGenerator : MonoBehaviour
{
    public GameObject fireballPrefab;
    [SerializeField] private float respawnTime = 5.0f;
    private Transform startPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = GetComponent<Transform>();
        StartCoroutine(fireballsWave());
    }

    private void spawn()
    {
        GameObject fireball = Instantiate(fireballPrefab) as GameObject;
        fireball.transform.position = new Vector3(startPoint.position.x, startPoint.position.y, 0.0f);
    }

    IEnumerator fireballsWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawn();
        }
        
    }
}
