using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Toca automaticamente um som ao clicar em qualquer Button carregado nas cenas.
public class UIButtonSoundManager : MonoBehaviour
{
    // Impede que mais de um gerenciador de som de UI exista ao trocar de cena.
    private static UIButtonSoundManager instance;

    [Header("Audio")]

    // Fonte dedicada aos sons de interface, configurada para o grupo SFX do mixer.
    [SerializeField] private AudioSource uiAudioSource;

    // Áudio reproduzido quando um botão interagível é clicado.
    [SerializeField] private AudioClip buttonClickClip;

    private void Awake()
    {
        // Mantém somente uma instância do gerenciador durante todo o jogo.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Registra esta instância como o gerenciador ativo.
        instance = this;

        // Mantém este objeto ao trocar entre Menu Principal e Gameplay.
        DontDestroyOnLoad(gameObject);

        // Permite que botões do pause emitam som mesmo com AudioListener.pause ativo.
        if (uiAudioSource != null)
        {
            uiAudioSource.ignoreListenerPause = true;
        }
    }

    private void OnEnable()
    {
        // Detecta quando uma nova cena termina de carregar.
        SceneManager.sceneLoaded += RegisterSceneButtons;
    }

    private void Start()
    {
        // Registra os botões da primeira cena, normalmente o MainMenu.
        RegisterSceneButtons(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        // Remove o evento para evitar referências inválidas.
        SceneManager.sceneLoaded -= RegisterSceneButtons;
    }

    // Encontra e conecta todos os botões da cena carregada.
    private void RegisterSceneButtons(Scene scene, LoadSceneMode loadSceneMode)
    {
        // Inclui botões dentro de painéis inicialmente desativados, como Pause e Settings.
        Button[] buttons = FindObjectsByType<Button>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        // Adiciona o som de clique ao evento de cada botão encontrado.
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayButtonClick);
        }
    }

    // Reproduz o efeito sonoro de clique pelo canal de efeitos do mixer.
    private void PlayButtonClick()
    {
        // Evita erro caso o AudioSource ou o clip não tenham sido atribuídos.
        if (uiAudioSource == null || buttonClickClip == null)
        {
            return;
        }

        // Toca o efeito sem interromper outros sons de interface.
        uiAudioSource.PlayOneShot(buttonClickClip);
    }
}