using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.CompareTag("Bullet") && this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<FireControl>()._activeBullets.Remove(this.gameObject);
            GameObject.Find("Player").GetComponent<FireControl>()._bulletArchive.Add(this.gameObject);
        }
    }
}
