using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Eğer bitiş noktasına giren nesne oyuncuysa
        if (other.CompareTag("Player"))
        {
            // TimerController scriptini al ve oyunu bitir
            TimerController timerController = FindObjectOfType<TimerController>();
            if (timerController != null)
            {
                timerController.FinishGame();
            }
        }
    }
}