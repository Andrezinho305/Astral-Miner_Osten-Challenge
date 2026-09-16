using UnityEngine;

// Define como a próxima partida deve ser inicializada.
public enum GameStartMode
{
    // Cria uma partida sem dados anteriores.
    NewGame,

    // Restaura os dados do save existente.
    ContinueGame
}

// Mantém a escolha do menu enquanto uma nova cena é carregada.
public static class GameSessionManager
{
    // Modo usado ao entrar na cena de gameplay; uma nova partida é o padrão seguro.
    public static GameStartMode StartMode { get; private set; } = GameStartMode.NewGame;

    // Armazena a escolha feita pelo jogador no menu principal.
    public static void SetStartMode(GameStartMode startMode)
    {
        StartMode = startMode;
    }
}
