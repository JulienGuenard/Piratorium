using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject winCanvas;
    public string nextLevel;

    static public GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void Win()
    {
        if (winCanvas.activeInHierarchy) return;

        winCanvas.SetActive(true);
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(nextLevel);
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
