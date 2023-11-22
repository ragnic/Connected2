using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    public float offset = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if sandra is in portal area
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds (0.3f);
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y - offset);
    }
}
