using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private int coins = 0;
    public GameObject coinPrefab;

    [SerializeField] private Text coinsText;
    [SerializeField] private Text chestsText;
    [SerializeField] private GameObject chestPrefab;
    private int chest = 0;
    private GameObject instantiatedObj1;
    private GameObject instantiatedObj2;
    private GameObject instantiatedObj3;
    [SerializeField] private GameObject LevelFailed;
    [SerializeField] private GameObject UI;
    [SerializeField] AudioSource coin_Collect;
    [SerializeField] AudioSource chest_Collect;

    //[SerializeField] private AudioSource collect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coin_Collect.Play();
            coins++;
            //collect.SoundEffect.Play();
            coinsText.text = "Coins: " + coins.ToString();
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 1.38f, Random.Range(-10, 10));
            Vector3 randomPositionChest = new Vector3(Random.Range(-20, 20), 1.38f, Random.Range(-20, 20));
            if (coins < 5)
            {
                Instantiate(coinPrefab, randomPosition, Quaternion.identity);
            }
            else if(coins>=5)
            {
                instantiatedObj1 = Instantiate(chestPrefab, randomPositionChest, transform.rotation * Quaternion.Euler(-90f, 90f, 0f));
                randomPositionChest = new Vector3(Random.Range(-20, 20), 1.38f, Random.Range(-20, 20));
                instantiatedObj2 = Instantiate(chestPrefab, randomPositionChest, transform.rotation * Quaternion.Euler(-90f, 90f, 0f));
                randomPositionChest = new Vector3(Random.Range(-20, 20), 1.38f, Random.Range(-20, 20));
                instantiatedObj3 = Instantiate(chestPrefab, randomPositionChest, transform.rotation * Quaternion.Euler(-90f, 90f, 0f));
                Invoke("DestroyChests", 10f);
            }           
        }
        if (other.gameObject.CompareTag("Chest"))
        {
            chest_Collect.Play();
            Vector3 randomPositionChest = new Vector3(Random.Range(-20, 20), 1.38f, Random.Range(-20, 20));
            Destroy(other.gameObject);
            chest++;
            chestsText.text = "Chests: " + chest.ToString();
            if (chest == 3)
            {
                Invoke("CompleteLevel", 2f);
            }
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void DestroyChests()
    {
        Debug.Log("Game Over");
        Instantiate(LevelFailed, transform.position, Quaternion.identity);
        Destroy(UI);
        Destroy(instantiatedObj3);
        Destroy(instantiatedObj2);
        Destroy(instantiatedObj1);
        Invoke("RestartLevel", 5f);
    }
    private void Timer(float timeRemaining)
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

}
