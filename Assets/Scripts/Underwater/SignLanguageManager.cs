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

    public bool PlayerInRange;
    public ProximityTrigger Trigger;
    public bool WaitingForAnswer;

    private int ExpectedAnswer;

    public GameObject oxygenBar;

    public ScoreManager Score;

    private void Start(){

        Score = GameObject.FindWithTag("Score").GetComponent<ScoreManager>();

        Instructor = Instructor.GetComponent<Image>();
        InstructorPanel.SetActive(false);

        Player1st = Player1st.GetComponent<Button>();
        Player2nd = Player2nd.GetComponent<Button>();
        Player3rd = Player3rd.GetComponent<Button>();
        Player1st.onClick.AddListener(Button1OnClick);
        Player2nd.onClick.AddListener(Button2OnClick);
        Player3rd.onClick.AddListener(Button3OnClick);
        PlayerPanel.SetActive(false);

        WaitingForAnswer = false;

        //SetupThirdQuiz();
    }

    private void Update(){
        if (WaitingForAnswer){
            if (PlayerInRange && !PlayerPanel.activeSelf){
               PlayerPanel.SetActive(true);
            }
            if (!PlayerInRange && PlayerPanel.activeSelf){
               PlayerPanel.SetActive(false);
            }
        }
        else if (PlayerPanel.activeSelf) PlayerPanel.SetActive(false);
    }

    public void SetupFirstQuiz(){

        InstructorPanel.SetActive(true);
        Instructor.sprite = Sign_Ok;

        Player1st.image.sprite = Sign_Up;
        Player2nd.image.sprite = Sign_Ok;
        Player3rd.image.sprite = Sign_Stop;

        ExpectedAnswer = 2;
        WaitingForAnswer = true;
    }

    public void SetupSecondQuiz(){

        InstructorPanel.SetActive(true);
        Instructor.sprite = Sign_Manometer;

        Player1st.image.sprite = Sign_Ok;
        Player2nd.image.sprite = Sign_HalfGauge;
        Player3rd.image.sprite = Sign_Reserve;

        float currentAir = oxygenBar.GetComponent<Scrollbar>().size;
        Debug.Log(currentAir);
        if (currentAir>0.6f) {
            ExpectedAnswer = 1;
        }
        else if (currentAir>0.35f) {
            ExpectedAnswer = 2;
        }
        else {
          ExpectedAnswer = 3;
        }
        WaitingForAnswer = true;
    }

    public void SetupThirdQuiz(){

        InstructorPanel.SetActive(true);
        Instructor.sprite = Sign_Up;

        Player1st.image.sprite = Sign_Up;
        Player2nd.image.sprite = Sign_Ok;
        Player3rd.image.sprite = Sign_No;

        ExpectedAnswer = 2;
        WaitingForAnswer = true;
    }

    public void Button1OnClick(){
        if (ExpectedAnswer != 1) {
            RemovePointsBecause(5, "Mauvaise r�ponse �� l'instructeur.");
        }
        ResumeDiving();
    }

    public void Button2OnClick(){
        if (ExpectedAnswer != 2) {
            RemovePointsBecause(5, "Mauvaise r�ponse �� l'instructeur.");
        }
        ResumeDiving();
    }

    public void Button3OnClick(){
        if (ExpectedAnswer != 3) {
            RemovePointsBecause(5, "Mauvaise r�ponse �� l'instructeur.");
        }
        ResumeDiving();
    }

    private void ResumeDiving(){
        WaitingForAnswer = false;
        InstructorPanel.SetActive(false);
    }

    private void RemovePointsBecause(int val, string reason){
          Score.RegisterLossOfPointsDive(val, reason);
    }
}
