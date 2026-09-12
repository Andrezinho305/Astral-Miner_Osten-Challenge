using Unity.Cinemachine;
using UnityEngine;

public class CameraBinder : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;

    public void SetPlayer(GameObject player)
    {
        Debug.Log(
                   "Player recebido: " +
                   player.name +
                   " | Instance ID: " +
                   player.GetInstanceID()
               );

        cinemachineCamera.Follow = player.transform;

        Debug.Log(
            "Follow configurado para: " +
            cinemachineCamera.Follow.name +
            " | Instance ID: " +
            cinemachineCamera.Follow.gameObject.GetInstanceID()
        );
    }
}
