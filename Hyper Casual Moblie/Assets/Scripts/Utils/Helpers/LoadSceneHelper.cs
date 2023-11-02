using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Load(string s)
    {
        SceneManager.LoadScene(s);
     }

        /*public void Load(int i)
        {
            Debug.Log("Load(int) chamado com o argumento: " + i);
            SceneManager.LoadScene(i);
        }

        public void Load(string s)
        {
            Debug.Log("Load(string) chamado com o argumento: " + s);
            SceneManager.LoadScene(s);
        }*/
    }
