using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinBehaviour : MonoBehaviour
{
    public GameObject coinPrefab;
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 1.38f, Random.Range(-10, 10));
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 1.38f, Random.Range(-10, 10));
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }
    }
}
