using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controla os botões e painéis exibidos na cena de menu principal.
public class MainMenuManager : MonoBehaviour
{
    [Header("Scene")]
    // Nome exato da cena que contém a partida.
    [SerializeField] private string gameplaySceneName = "Prototype_ATT";

    [Header("Panels")]
    // Painel principal com os botões Novo Jogo, Continuar, Configurações e Sair.
    [SerializeField] private GameObject mainMenuPanel;

    // Painel aberto ao selecionar Configurações.
    [SerializeField] private GameObject settingsPanel;

    [Header("Buttons")]
    // Botão Continuar, desativado quando não existe uma partida salva.
    [SerializeField] private Button continueButton;

    private void Start()
    {
        // Garante que o menu não herde uma pausa de outra cena.
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Exibe somente o painel principal ao entrar no menu.
        ShowMainMenu();

        // Atualiza a disponibilidade do botão Continuar conforme o save local.
        RefreshContinueButton();
    }

    // Inicia uma partida limpa, substituindo o save anterior caso exista.
    public void StartNewGame()
    {
        // Remove os dados salvos para que a nova partida não herde progresso antigo.
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.DeleteSave();
        }

        // Registra que a próxima cena deve iniciar do zero.
        GameSessionManager.SetStartMode(GameStartMode.NewGame);

        // Carrega a cena jogável configurada no Inspector.
        SceneManager.LoadScene(gameplaySceneName);
    }

    // Carrega a partida salva, caso ela exista.
    public void ContinueGame()
    {
        // Impede continuar quando não há dados válidos para restaurar.
        if (SaveManager.Instance == null || !SaveManager.Instance.HasSave())
        {
            RefreshContinueButton();
            return;
        }

        // Registra que a próxima cena deve restaurar o save atual.
        GameSessionManager.SetStartMode(GameStartMode.ContinueGame);

        // Carrega a mesma cena jogável usada por uma nova partida.
        SceneManager.LoadScene(gameplaySceneName);
    }

    // Abre o painel de configurações e esconde os botões principais.
    public void OpenSettings()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    // Fecha as configurações e retorna aos botões principais.
    public void ShowMainMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Fecha a aplicação compilada. No Editor, apenas registra a intenção.
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo.");
        Application.Quit();
    }

    // Habilita o botão Continuar somente quando o SaveManager encontrar um save.
    private void RefreshContinueButton()
    {
        if (continueButton != null)
        {
            continueButton.interactable =
                SaveManager.Instance != null && SaveManager.Instance.HasSave();
        }
    }
}
