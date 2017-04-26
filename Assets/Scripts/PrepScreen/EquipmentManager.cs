using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {

    public Button Mask;
    public Button Suit;
    public Button Fins;
    public Button Regulator;
    public Button BuoyancyDevice;
    public Button Weights;
    public Button DivingGear;

    public Sprite MaskEquipped;
    public Sprite SuitEquipped;
    public Sprite FinsEquipped;
    public Sprite RegulatorEquipped;
    public Sprite BuoyancyDeviceEquipped;
    public Sprite WeightsEquipped;
    public Sprite CylinderWithDevice;
    public Sprite CylinderWithDeviceAndRegulator;
    public Sprite CylinderWithDeviceAndRegulatorEquipped;

    private bool MaskOn = false;
    private bool SuitOn = false;
    private bool FinsOn = false;
    private bool RegulatorOn = false;
    private bool BuoyancyDeviceOn = false;
    private bool WeightsOn = false;
    private bool DivingGearOn = false;

    public void Start() {
        Mask = Mask.GetComponent<Button>();
        Suit = Suit.GetComponent<Button>();
        Fins = Fins.GetComponent<Button>();
        Regulator = Regulator.GetComponent<Button>();
        BuoyancyDevice = BuoyancyDevice.GetComponent<Button>();
        Weights = Weights.GetComponent<Button>();
        DivingGear = DivingGear.GetComponent<Button>();

        Mask.onClick.AddListener(MaskOnClick);
        Suit.onClick.AddListener(SuitOnClick);
        Fins.onClick.AddListener(FinsOnClick);
        Regulator.onClick.AddListener(RegulatorOnClick);
        BuoyancyDevice.onClick.AddListener(BuoyancyOnClick);
        Weights.onClick.AddListener(WeightsOnClick);
        DivingGear.onClick.AddListener(DivingGearOnClick);
    }

    // Ordre dans lesquels les boutons doivent être actionnés pour gagner:
    //
    // Gilet de plongée
    // Détendeur
    //
    // Combinaison
    // Poids
    //
    // Après Poids + Détendeur:
    // Scaphandre
    // Palmes
    // Masque

    private void BuoyancyOnClick(){
        if (!BuoyancyDeviceOn) {
            BuoyancyDeviceOn = true;
            DivingGear.image.sprite = CylinderWithDevice;
            BuoyancyDevice.image.sprite = BuoyancyDeviceEquipped;
        }
    }

    private void RegulatorOnClick(){
        if (BuoyancyDeviceOn) {
            if (!RegulatorOn){
              RegulatorOn = true;
              DivingGear.image.sprite = CylinderWithDeviceAndRegulator;
              Regulator.image.sprite = RegulatorEquipped;
            }
        }
        else {
            Debug.Log("Regulator error");
        }
    }

    private void SuitOnClick(){
        if (!SuitOn){
            SuitOn = true;
            Suit.image.sprite = SuitEquipped;
        }

    }

    private void WeightsOnClick(){
        if (SuitOn) {
            if (!WeightsOn) {
                WeightsOn = true;
                Weights.image.sprite = WeightsEquipped;
            }
        }
        else {
            Debug.Log("Weights error");
        }
    }

    private void DivingGearOnClick(){
        if ((WeightsOn) && (RegulatorOn)) {
            if (!DivingGearOn) {
              DivingGearOn = true;
              DivingGear.image.sprite = CylinderWithDeviceAndRegulatorEquipped;
            }
        }
        else {
            Debug.Log("Gear error");
        }
    }

    private void FinsOnClick(){
        if (DivingGearOn) {
            if (!FinsOn) {
                FinsOn = true;
                Fins.image.sprite = FinsEquipped;
            }
        }
        else {
            Debug.Log("Fins error");
        }
    }

    private void MaskOnClick(){
      if (FinsOn) {
          if (!MaskOn) {
              MaskOn = true;
              Mask.image.sprite = MaskEquipped;
          }
      }
      else {
          Debug.Log("Mask error");
      }
    }
}
