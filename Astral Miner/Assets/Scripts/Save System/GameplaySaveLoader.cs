using UnityEngine;

// Restaura os dados da partida depois que o menu solicita a opção Continuar.
public class GameplaySaveLoader : MonoBehaviour
{
    [Header("References")]
    // Controla a quantidade de dinheiro atual.
    [SerializeField] private MoneyController moneyController;

    // Controla níveis e atributos comprados pelo jogador.
    [SerializeField] private ProgressionManager progressionManager;

    // Controla o nível de dificuldade do mundo.
    [SerializeField] private WorldDificultyManager worldDifficultyManager;

    // Informa quando a nave foi criada para posicioná-la após o carregamento.
    [SerializeField] private PlayerRespawnManager respawnManager;

    // Posição que será aplicada somente no primeiro spawn da sessão carregada.
    private Vector3 savedPlayerPosition;

    // Indica se há uma posição pendente para aplicar no player recém-criado.
    private bool shouldRestorePlayerPosition;

    private void Awake()
    {
        // Escuta o spawn antes dos métodos Start dos objetos da cena.
        if (respawnManager != null)
        {
            respawnManager.OnPlayerSpawned.AddListener(RestorePlayerPosition);
        }

        // Só restaura dados quando a entrada na cena veio do botão Continuar.
        if (GameSessionManager.StartMode == GameStartMode.ContinueGame)
        {
            LoadSavedGame();
        }
    }

    private void OnDestroy()
    {
        // Remove o listener para evitar referências antigas ao trocar de cena.
        if (respawnManager != null)
        {
            respawnManager.OnPlayerSpawned.RemoveListener(RestorePlayerPosition);
        }
    }

    // Aplica dinheiro, dificuldade, upgrades e prepara a posição salva do player.
    private void LoadSavedGame()
    {
        // Interrompe o carregamento com segurança se não houver save disponível.
        if (SaveManager.Instance == null || !SaveManager.Instance.HasSave())
        {
            return;
        }

        // Obtém os dados que o SaveManager já leu do arquivo local.
        SaveData data = SaveManager.Instance.Data;

        if (moneyController != null)
        {
            moneyController.SetMoney(data.money);
        }

        if (worldDifficultyManager != null)
        {
            worldDifficultyManager.SetWorldLevel(data.worldLevel);
        }

        if (progressionManager != null)
        {
            progressionManager.LoadSaveData(data);
        }

        // Guarda a posição até que o PlayerRespawnManager crie a nave.
        savedPlayerPosition = new Vector3(
            data.playerPositionX,
            data.playerPositionY,
            0f
        );
        shouldRestorePlayerPosition = true;
    }

    // Move apenas o primeiro player criado para a posição que estava salva.
    private void RestorePlayerPosition(GameObject player)
    {
        if (!shouldRestorePlayerPosition)
        {
            return;
        }

        player.transform.position = savedPlayerPosition;
        shouldRestorePlayerPosition = false;
    }
}
