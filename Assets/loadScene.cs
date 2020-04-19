using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadscene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
