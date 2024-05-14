using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RockEnemy : MonoBehaviour
{
    public GameObject RocksEnemy;
    public Light l;
    public bool lgrow;
   
    private void Awake()
    {
        lgrow = false;
        l.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "playerCball") {
            Drive.canEnterNextLevel = true;
            
            lgrow = true;
            StartCoroutine("Destroyo");
        }
    }
    private void FixedUpdate()
    {
        if (lgrow) {
            l.gameObject.SetActive(true);
        }
    }
    IEnumerator Destroyo() {

        yield return new WaitForSeconds(.77f);
        SceneManager.LoadScene(7);
        Destroy(RocksEnemy);
       
       

    }
}
