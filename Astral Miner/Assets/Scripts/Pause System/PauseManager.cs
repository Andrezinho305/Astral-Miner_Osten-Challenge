using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    // Painel visual exibido enquanto o jogo estiver pausado.
    [SerializeField] private GameObject pausePanel;

    // Guarda se o jogo está pausado ou em execução.
    private bool isPaused;

    private void Start()
    {
        // Garante que o jogo comece despausado
        // e que o painel fique escondido.
        SetPause(false);
    }

    private void Update()
    {
        // Detecta se a tecla ESC foi pressionada neste frame.
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Alterna entre pausar e continuar o jogo.
            SetPause(!isPaused);
        }
    }

    // Define o estado de pausa do jogo.
    public void SetPause(bool pause)
    {
        // Atualiza o estado interno.
        isPaused = pause;

        // Pausa ou retoma o tempo do jogo.
        // Com 0, movimento, física e spawners param.
        Time.timeScale = isPaused ? 0f : 1f;

        // Pausa ou retoma todos os áudios do jogo.
        AudioListener.pause = isPaused;

        // Mostra ou oculta o painel de pause,
        // apenas se ele tiver sido atribuído no Inspector.
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
    }

    // Método público para conectar ao botão "Continuar" da UI.
    public void ResumeGame()
    {
        // Retoma o jogo.
        SetPause(false);
    }

    private void OnDestroy()
    {
        // Evita que o jogo fique travado se este objeto for destruído.
        Time.timeScale = 1f;

        // Garante que o áudio volte ao normal.
        AudioListener.pause = false;
    }
}
