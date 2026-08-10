using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private int gemValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        //Check if overlapping object has score manager
        if (other.TryGetComponent<ScoreManager>(out var scoreManager))
        {
            //Call add gem function and destroy gem
            scoreManager.AddGems(gemValue);
            Destroy(gameObject);
        }
    }
}
