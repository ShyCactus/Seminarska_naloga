using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathoptions : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void RestartLevel() {
        Cursor.lockState = CursorLockMode.Locked;
        if (coinkeep.coins > 200) { coinkeep.coins -= 200; } else { coinkeep.coins = 0; }
        SceneManager.LoadScene(3);
    
    }
    public void RestartGame()
    {
        GameObject kl = GameObject.Find("KeepLevel");
        KeepLevels kle = kl.GetComponent<KeepLevels>();
        KeepLevels.level = 1;
        
        SceneManager.LoadScene(0);
        
    }
}
