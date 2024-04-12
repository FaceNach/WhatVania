using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickup;
    [SerializeField] int PointsForCoinPickup = 100;

    bool wasCollected = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(PointsForCoinPickup);
            AudioSource.PlayClipAtPoint(CoinPickup, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
