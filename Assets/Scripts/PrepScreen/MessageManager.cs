using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageManager : MonoBehaviour {

    public GameObject MessageBox;
    public Button CloseButton;
    public Button HelpButton;
    public Text Message;

    [HideInInspector] public string FinsError = "Tu ne dois enfiler les palmes que lorsque tu es prêt a plonger, sinon tu risquerais de tomber!";
    [HideInInspector] public string MaskError = "Tu n'as pas besoin d'enfiler ton masque avant d'avoir fini de preparer ton scaphandre.";
    [HideInInspector] public string WeightsError = "Tu ne vas pas porter ta ceinture de poids a même la peau, si?";
    [HideInInspector] public string RegulatorError = "Si tu installes le detendeur en premier, tu auras beaucoup de mal a passer le gilet de plongee.";
    [HideInInspector] public string DivingGearError = "Habilles toi avant d'enfiler ton scaphandre.";
    [HideInInspector] public string DivingGearError2 = "Tu n'as pas fini d'equiper ton scaphandre.";
    [HideInInspector] public string Instructions = "Prepare ton equipement avant la plongee en cliquant sur les differentes pièces dans le bon ordre! Cliquer sur une pièce permet de l'equiper ou de l'enfiler.\nTu perds des points a chaque fois que tu cliques sur un equipement en dehors de l'ordre de preparation habituel.";
    [HideInInspector] public string Congratz = "Bravo, tu es fin prêt a plonger!";

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
