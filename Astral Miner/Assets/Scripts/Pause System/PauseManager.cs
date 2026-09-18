using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Painel visual exibido enquanto o jogo estiver pausado.
    [SerializeField] private GameObject pausePanel;

    // Painel com as configurações acessadas durante a partida.
    [SerializeField] private GameObject settingsPanel;

    // Nome exato da cena que contém o menu principal.
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    // Gerencia os dados que precisam ser salvos antes de voltar ao menu.
    [SerializeField] private ProgressionManager progressionManager;

    // Guarda se o jogo está pausado ou em execução.
    private bool isPaused;

    // Indica se o jogador está navegando pelo painel de configurações.
    private bool isSettingsOpen;

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
            // Fecha as configurações e retorna ao painel de pausa antes de despausar.
            if (isSettingsOpen)
            {
                ShowPauseMenu();
                return;
            }

            // Alterna entre pausar e continuar o jogo.
            SetPause(!isPaused);
        }
    }

    // Define o estado de pausa do jogo.
    public void SetPause(bool pause)
    {
        // Atualiza o estado interno.
        isPaused = pause;

        // Fecha o estado de configurações ao pausar ou despausar normalmente.
        isSettingsOpen = false;

        // Pausa ou retoma o tempo do jogo.
        // Com 0, movimento, física e spawners param.
        Time.timeScale = isPaused ? 0f : 1f;

        if(!isPaused)
        {
            BlockGameplayShootingUntilRelease();
        }

        // Pausa ou retoma todos os áudios do jogo.
        AudioListener.pause = isPaused;

        // Mostra ou oculta o painel de pause,
        // apenas se ele tiver sido atribuído no Inspector.
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }

        // Esconde as configurações quando o menu de pausa não está aberto.
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Método público para conectar ao botão "Continuar" da UI.
    public void ResumeGame()
    {
        // Retoma o jogo.
        SetPause(false);
    }

    // Abre as configurações sem retomar o tempo do jogo.
    public void OpenSettings()
    {
        // Mantém o jogo pausado enquanto o jogador altera configurações.
        isPaused = true;
        isSettingsOpen = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;

        // Esconde os botões de pausa enquanto as configurações são exibidas.
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        // Exibe o painel configurado no Inspector.
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    // Fecha as configurações e volta aos botões do menu de pausa.
    public void ShowPauseMenu()
    {
        // Mantém a partida parada, mas troca o painel visível.
        isPaused = true;
        isSettingsOpen = false;

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    // Salva a partida atual e retorna para a cena do menu principal.
    public void ReturnToMainMenu()
    {
        // Registra dinheiro, upgrades, dificuldade e posição antes de sair da partida.
        if (progressionManager != null)
        {
            progressionManager.SaveCurrentProgress();
        }

        // Restaura tempo e áudio para que o menu não abra pausado.
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Carrega a cena de menu configurada no Inspector.
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void BlockGameplayShootingUntilRelease()
    {
        // Procura o script PlayerShoot no jogador ativo na cena.
        PlayerShoot playerShoot = FindObjectOfType<PlayerShoot>();

        if (playerShoot != null)
        {
            // Bloqueia o tiro até que o jogador solte a tecla de disparo.
            playerShoot.BlockShootingUntilRelease();
        }
    }   

    private void OnDestroy()
    {
        // Evita que o jogo fique travado se este objeto for destruído.
        Time.timeScale = 1f;

        // Garante que o áudio volte ao normal.
        AudioListener.pause = false;
    }
}
