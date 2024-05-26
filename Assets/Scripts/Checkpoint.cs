using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private BallController ballController;

    private void Start()
    {
        ballController = FindObjectOfType<BallController>();
        if (ballController == null)
        {
            Debug.LogError("BallController bulunamadı!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuizManager.Instance.GenerateRandomMathQuestion();
            ballController.DisableControls();
        }
    }
}