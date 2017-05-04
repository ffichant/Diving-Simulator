using UnityEngine;

public class AudioSourceMenu : MonoBehaviour {

  void Awake() {
      DontDestroyOnLoad(transform.gameObject);
  }
}
