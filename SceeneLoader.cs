using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceeneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public int Scene;

    public void LoadScene() {

        SceneManager.LoadScene(Scene);    
    }
}
