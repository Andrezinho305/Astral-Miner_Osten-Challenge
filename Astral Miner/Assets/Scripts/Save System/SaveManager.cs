using System.IO;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private string savePath;

    public SaveData Data { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) //sigleton desse manager
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(
            Application.persistentDataPath,
            "save.json"
        );

        LoadSaveFile();
    }

    private void LoadSaveFile()
    {
        // Nenhum save existente
        if (!File.Exists(savePath))
        {
            Data = new SaveData();

            Debug.Log("Nenhum save encontrado.");

            return;
        }

        string json =
            File.ReadAllText(savePath);

        Data =
            JsonUtility.FromJson<SaveData>(json);

        // Segurança caso o arquivo esteja vazio/inválido
        if (Data == null)
        {
            Data = new SaveData();
        }

        Debug.Log(
            "Save carregado de: " +
            savePath
        );
    }


    public void WriteSaveFile()
    {
        if (Data == null)
        {
            Data = new SaveData();
        }

        string json =
            JsonUtility.ToJson(
                Data,
                true
            );

        File.WriteAllText(
            savePath,
            json
        );

        Debug.Log(
            "Save realizado em: " +
            savePath
        );
    }


    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        Data = new SaveData();

        Debug.Log("Save apagado.");
    }


    public bool HasSave()
    {
        return Data != null &&
               Data.hasSave;
    }
}
