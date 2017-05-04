using UnityEngine;

public class AudioSourceDiving : MonoBehaviour {

  void Awake() {
      GameObject AudioTest = GameObject.Find("AudioSourceMenu");
      if (AudioTest != null) {
          Destroy(AudioTest);
      }
  }
}
