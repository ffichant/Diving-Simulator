using UnityEngine;
using UnityEngine.UI;

public class ProximityTrigger : MonoBehaviour {

    public BoxCollider2D PlayerCollider;
    public SignLanguageManager SLManager;

    private void OnTriggerEnter2D (Collider2D other) {
        if (other == PlayerCollider) {
            SLManager.PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other == PlayerCollider) {
            SLManager.PlayerInRange = false;
        }
    }

}
