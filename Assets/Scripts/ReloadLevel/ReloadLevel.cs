using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    public void ReloadScene()
    {
        StartCoroutine(ReloadLevel());

        IEnumerator ReloadLevel()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
}
