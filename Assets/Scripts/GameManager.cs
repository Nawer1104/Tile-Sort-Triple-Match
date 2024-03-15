using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Level> levels;

    private int startIndex = 0;

    private int currentIndex;

    public GameObject vfxLevelUp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentIndex = startIndex;

        levels[currentIndex].gameObject.SetActive(true);
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public void CheckLevelUp()
    {
        if (levels[currentIndex].gameObjects.Count == 0)
        {
            
            currentIndex += 1;

            GameObject vfx = Instantiate(vfxLevelUp, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);

            StartCoroutine("LevelUp");
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LevelUp()
    {
        yield return new WaitForSeconds(2);

/*        for (int i = 0; i < levels[currentIndex].transform.childCount - 1; i++)
        {
            levels[currentIndex].transform.GetChild(i).gameObject.SetActive(false);
        }*/

        levels[currentIndex-1].gameObject.SetActive(false);

        if (currentIndex >= levels.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            currentIndex = 0;
        }

        levels[currentIndex].gameObject.SetActive(true);
    }

    public void ReSetCurrentLevel()
    {
        //levels[currentIndex].AddComponents();
    }
}