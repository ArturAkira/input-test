
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class npcManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col;
    [SerializeField] public GameObject textCanvas;
    [SerializeField] private GameObject placeHolderBox;
    [SerializeField] private TMP_Text textBox;

    [SerializeField] private CinemachineCamera cine;
    [SerializeField] private CinemachinePositionComposer cinepos;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraTarget;

    [SerializeField] private Rigidbody2D plRg;

    [Header("Dialogos")]
    [TextArea(2, 5)]
    public string[] texts;

    [SerializeField] private float velocidadeDigitacao = 0.05f;

    public bool placeholder;
    private bool dialogComplete = false;

    private int count = 0;
    private bool digitando = false;
    private Coroutine coroutineDialogo;

    private void Start()
    {
        textCanvas.SetActive(false);
    }

    private void Update()
    {
        if (gameManager.Instance.dialogTime == true && placeholder)
        {
            cameraTarget.position = Vector3.MoveTowards(
                cameraTarget.position,
                new Vector3(3f, cameraTarget.position.y, cameraTarget.position.z),
                2f * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            placeholder = true;
            placeHolderBox.SetActive(true);

            

            if (!placeholder || dialogComplete == true)
                return;

            plRg.linearVelocity = Vector2.zero;

            if (!textCanvas.activeSelf)
            {
                count = 0;

                if (texts == null || texts.Length == 0)
                    return;

                textCanvas.SetActive(true);
                plRg.linearVelocityX = 0;
                gameManager.Instance.dialogTime = true;

                cine.Target.TrackingTarget = cameraTarget;
                cinepos.Composition.DeadZone.Enabled = false;

                MostrarDialogo();
                return;
            }

            if (digitando)
            {
                CompletarTexto();
            }
            else
            {
                MostrarDialogo();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {

            placeholder = false;
            placeHolderBox.SetActive(true);

            FecharDialogo();
        }
    }

    public void OnInteract(InputValue value)
    {
        if (!value.isPressed || !placeholder)
            return;

        if (!textCanvas.activeSelf)
        {
            count = 0;

            if (texts == null || texts.Length == 0)
                return;

            textCanvas.SetActive(true);
            plRg.linearVelocityX = 0;
            gameManager.Instance.dialogTime = true;

            MostrarDialogo();
            return;
        }

        if (digitando)
        {
            CompletarTexto();
        }
        else
        {
            MostrarDialogo();
        }
    }

    private void MostrarDialogo()
    {
        if (count >= texts.Length)
        {
            FecharDialogo();
            return;
        }

        if (coroutineDialogo != null)
        {
            StopCoroutine(coroutineDialogo);
        }

        coroutineDialogo = StartCoroutine(
            DigitarTexto(texts[count])
        );

        count++;
    }

    private IEnumerator DigitarTexto(string texto)
    {
        digitando = true;
        textBox.text = "";

        foreach (char letra in texto)
        {
            textBox.text += letra;

            yield return new WaitForSecondsRealtime(velocidadeDigitacao);
        }

        digitando = false;
        coroutineDialogo = null;
    }

    private void CompletarTexto()
    {
        if (coroutineDialogo != null)
        {
            StopCoroutine(coroutineDialogo);
            coroutineDialogo = null;
        }

        textBox.text = texts[count - 1];
        digitando = false;
    }

    private void FecharDialogo()
    {
        if (coroutineDialogo != null)
        {
            StopCoroutine(coroutineDialogo);
            coroutineDialogo = null;
        }

        digitando = false;
        count = 0;

        cine.Target.TrackingTarget = player;
        cinepos.Composition.DeadZone.Enabled = true;
        plRg.linearVelocityX = 0;

        textCanvas.SetActive(false);
        gameManager.Instance.dialogTime = false;
        plRg.linearVelocityX = 0;
        dialogComplete = true;
    }
}
