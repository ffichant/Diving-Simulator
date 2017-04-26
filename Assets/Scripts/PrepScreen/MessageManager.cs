using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageManager : MonoBehaviour {

    public GameObject MessageBox;
    public Button CloseButton;
    public Button HelpButton;
    public Text Message;

    [HideInInspector] public string FinsError = "Tu ne dois enfiler les palmes que lorsque tu es prêt à plonger, sinon tu risquerais de tomber!";
    [HideInInspector] public string MaskError = "Tu n'as pas besoin d'enfiler ton masque avant d'avoir fini de préparer ton scaphandre.";
    [HideInInspector] public string WeightsError = "Tu ne vas pas porter ta ceinture de poids à même la peau, si?";
    [HideInInspector] public string RegulatorError = "Si tu installes le détendeur en premier, tu auras beaucoup de mal à passer le gilet de plongée.";
    [HideInInspector] public string DivingGearError = "Habilles toi avant d'enfiler ton scaphandre.";
    [HideInInspector] public string DivingGearError2 = "Tu n'as pas fini d'équiper ton scaphandre.";
    [HideInInspector] public string Instructions = "Prépare ton équipement avant la plongée en cliquant sur les différentes pièces dans le bon ordre! Cliquer sur une pièce permet de l'équiper ou de l'enfiler.\nTu perds des points à chaque fois que tu cliques sur un équipement en dehors de l'ordre de préparation habituel.";
    [HideInInspector] public string Congratz = "Bravo, tu es fin prêt à plonger!";

    private void Start(){

        CloseButton = CloseButton.GetComponent<Button>();
        CloseButton.onClick.AddListener(CloseButtonOnClick);

        HelpButton = HelpButton.GetComponent<Button>();
        HelpButton.onClick.AddListener(HelpButtonOnClick);

        Message = Message.GetComponent<Text>();
        Message.text = Instructions;
    }

    private void CloseButtonOnClick(){
        MessageBox.SetActive(false);
    }

    private void HelpButtonOnClick(){
        OpenMessageBoxWithMessage(Instructions);
    }

    private void FinalButtonOnClick(){
        SceneManager.LoadScene("Underwater");
    }

    public void OpenMessageBoxWithMessage(string msg){
        MessageBox.SetActive(true);
        Message.text = msg;
    }

    public void OpenFinalMessageBox(){
        MessageBox.SetActive(true);
        Message.text = Congratz;
        CloseButton.onClick.AddListener(FinalButtonOnClick);
    }
}
