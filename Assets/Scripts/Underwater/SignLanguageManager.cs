using UnityEngine;
using UnityEngine.UI;

public class SignLanguageManager : MonoBehaviour {

    public Image Instructor;
    public Button Player1st;
    public Button Player2nd;
    public Button Player3rd;

    public GameObject InstructorPanel;
    public GameObject PlayerPanel;

    public Sprite Sign_Ok;
    public Sprite Sign_HalfGauge;
    public Sprite Sign_Reserve;
    public Sprite Sign_Manometer;
    public Sprite Sign_Up;
    public Sprite Sign_Stop;
    public Sprite Sign_No;

    public bool WaitingForTrigger;
    public ProximityTrigger Trigger;

    private void Start(){

        Instructor = Instructor.GetComponent<Image>();
        InstructorPanel.SetActive(false);

        Player1st = Player1st.GetComponent<Button>();
        Player2nd = Player2nd.GetComponent<Button>();
        Player3rd = Player3rd.GetComponent<Button>();
        PlayerPanel.SetActive(false);

        WaitingForTrigger = false;
    }

    public void SetupFirstQuiz(){

        InstructorPanel.SetActive(true);
        Instructor.sprite = Sign_Ok;

        WaitingForTrigger = true;

        /*
        Player1st.image.sprite = Sign_Up;
        Player2nd.image.sprite = Sign_Ok;
        Player3rd.image.sprite = Sign_Stop;
        */
    }

}
