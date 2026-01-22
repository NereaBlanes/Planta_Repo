using UnityEngine;
using UnityEngine.SceneManagement;


public class WinPortal : MonoBehaviour
{
    public int sceneToLoad;
    
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadScene(sceneToLoad);
        }
    }
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


}
