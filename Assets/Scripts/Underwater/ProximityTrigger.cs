using UnityEngine;
using UnityEngine.UI;

public class ProximityTrigger : MonoBehaviour {

    public BoxCollider2D PlayerCollider;
    public SignLanguageManager SLManager;

    public ScoreManager Score;
    private int NumberSanctions = 0;
    void Start()
    {
        Score = GameObject.FindWithTag("Score").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other == PlayerCollider) {
            SLManager.PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other == PlayerCollider) {
            SLManager.PlayerInRange = false;
            if (NumberSanctions > 3)
            {
                Score.RegisterLossOfPointsDive(30, "Il ne faut pas trop s'eloigner du moniteur !");
            }
            else
                NumberSanctions++;
        }
    }

}
