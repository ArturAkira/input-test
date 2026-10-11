using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{
    Collider2D col;
    public string sceneName;
    public float localX, localy;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.Instance.coordX = localX;
        gameManager.Instance.coordY = localy;
        SceneManager.LoadScene(sceneName);
    }
}
