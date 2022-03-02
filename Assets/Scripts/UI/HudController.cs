using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public GameObject character; //the character in the scene
    public GameObject hudArmMain; //Right arm object of HUD
    public GameObject hudArmBack; //Left arm object of HUD
    public GameObject emergencyLight1; //Right emergency Light
    public GameObject emergencyLight2; //Left emergency Light
    public GameObject hudArmBarMain; //Bar of hudArmMain
    public GameObject hudArmBarBack; //Bar of hudArmBack
    public GameObject hudArmMainIcon; //Icon of hudArmMain
    public GameObject hudArmBackIcon; //Icon of hudArmBack
    public Sprite emergencyLit; //Sprite of lit emergency light
    public Sprite emergencyBroken; //Sprite of broken emergency light.
    public GameObject pin;
    private Weapon arm1;
    private Weapon arm2;
    private Image MainIcon;
    private Image BackIcon;
    private targetLerp mainPos;
    private targetLerp backPos;
    private Vector2 armActivePos=new Vector2(192,256);
    private Vector2 armInactivePos=new Vector2(170,277);
    private targetLerp light1Pos;
    private targetLerp light2Pos;
    private targetLerp pinPos;
    private Vector2 lightActivePos=new Vector2(89,265);
    private Vector2 lightInactivePos=new Vector2(40,265);
    private Image emergency1Image;
    private Image emergency2Image;
    private Vector2 pinActivePos=new Vector2(0,223);
    private Vector2 pinInactivePos=new Vector2(0,350);
    public GameObject pinBar;


    // Start is called before the first frame update
    void Start()
    {
      MainIcon=hudArmMainIcon.GetComponent<Image>();
      BackIcon=hudArmBackIcon.GetComponent<Image>();
      mainPos=hudArmMain.GetComponent<targetLerp>();
      backPos=hudArmBack.GetComponent<targetLerp>();
      light1Pos=emergencyLight1.GetComponent<targetLerp>();
      light2Pos=emergencyLight2.GetComponent<targetLerp>();
      emergency1Image=emergencyLight1.GetComponent<Image>();
      emergency2Image=emergencyLight2.GetComponent<Image>();
      pinPos=pin.GetComponent<targetLerp>();
    }

    // Update is called once per frame
    void Update()
    {
        //if neither arm is active, move lights to their active positions.  Otherwise, move them to inactive positions.
        if(character.GetComponent<PickUp>().item1 == null&&character.GetComponent<PickUp>().item2 == null){
          light1Pos.target=new Vector2(lightActivePos.x,lightActivePos.y);
          light2Pos.target=new Vector2(-lightActivePos.x,lightActivePos.y);
        }else{
          light1Pos.target=new Vector2(lightInactivePos.x,lightInactivePos.y);
          light2Pos.target=new Vector2(-lightInactivePos.x,lightInactivePos.y);
        }
        int hullHealth=character.GetComponent<Health>().health;
        //Light the emergency lights
        if(hullHealth>2){
          emergency1Image.sprite=emergencyLit;
        }else{
          emergency1Image.sprite=emergencyBroken;
        }
        if(hullHealth>1){
          emergency2Image.sprite=emergencyLit;
        }else{
          emergency2Image.sprite=emergencyBroken;
        }
        //pin key active
        if(character.GetComponent<PickUp>().item1!=null&&character.GetComponent<PickUp>().item1.name=="PinKey"){
          pinPos.target=new Vector2(pinActivePos.x,pinActivePos.y);
          hudArmMain.SetActive(false);
          hudArmBack.SetActive(false);
          float perc = Mathf.Clamp((float)character.GetComponent<PickUp>().item1.GetComponent<Weapon>().integrity / 
                      (float)character.GetComponent<PickUp>().item1.GetComponent<Weapon>().maxIntegrity, 0, 1);
          pinBar.transform.localScale = new Vector3(perc, 1, 1);
        }else{
          pinPos.target=new Vector2(pinInactivePos.x,pinInactivePos.y);
          //Right Side Arm
          if (character.GetComponent<PickUp>().item2 != null)
          {
              hudArmMain.SetActive(true);
              arm1 = character.GetComponent<PickUp>().item2.GetComponent<Weapon>();
              float perc = Mathf.Clamp((float)arm1.integrity / (float)arm1.maxIntegrity, 0, 1);
              hudArmBarMain.transform.localScale = new Vector3(perc, 1, 1);
              MainIcon.sprite=arm1.weaponIcon;
              //Move the UI arm if it's active
              if(Weapon.holding2 == 1){
                mainPos.target=new Vector2(armActivePos.x,armActivePos.y);
              }else{
                mainPos.target=new Vector2(armInactivePos.x,armInactivePos.y);
              }
          }
          else
          {
              //make the arm invisible if nothing is there.
              hudArmMain.SetActive(false);
          }
          //Left Side Arm
          if (character.GetComponent<PickUp>().item1 != null)
          {
              hudArmBack.SetActive(true);
              arm2 = character.GetComponent<PickUp>().item1.GetComponent<Weapon>();
              //scale healthbar
              float perc = Mathf.Clamp((float)arm2.integrity / (float)arm2.maxIntegrity, 0, 1);
              hudArmBarBack.transform.localScale = new Vector3(perc, 1, 1);
              //change icon sprite
              BackIcon.sprite=arm2.weaponIcon;
              //Move the UI arm if it's active
              if(Weapon.holding1 == 1){
                backPos.target=new Vector2(-armActivePos.x,armActivePos.y);
              }else{
                backPos.target=new Vector2(-armInactivePos.x,armInactivePos.y);
              }
          }
          else
          {
              //make the arm invisible if nothing is there.
              hudArmBack.SetActive(false);
          }
        }

    }
}
