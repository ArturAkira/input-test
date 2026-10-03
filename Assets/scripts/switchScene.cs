using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{
    Collider2D col;
    public string sceneName;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(sceneName);
        
    }
}
