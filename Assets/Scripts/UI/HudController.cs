using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public GameObject character;
    public GameObject hudArmMain;
    public GameObject hudArmBack;
    public GameObject emergencyLight1;
    public GameObject emergencyLight2;
    public GameObject hudArmBarMain;
    public GameObject hudArmBarBack;
    public GameObject hudArmMainIcon;
    public GameObject hudArmBackIcon;
    private Weapon arm1;
    private Weapon arm2;
    private Image MainIcon;
    private Image BackIcon;
    private targetLerp mainPos;
    private targetLerp backPos;
    private Vector2 armActivePos=new Vector2(94,272);
    private Vector2 armInactivePos=new Vector2(85,280);
    // Start is called before the first frame update
    void Start()
    {
      MainIcon=hudArmMainIcon.GetComponent<Image>();
      BackIcon=hudArmBackIcon.GetComponent<Image>();
      mainPos=hudArmMain.GetComponent<targetLerp>();
      backPos=hudArmBack.GetComponent<targetLerp>();
    }

    // Update is called once per frame
    void Update()
    {
        //Right Side Arm
        if (character.GetComponent<PickUp>().item1 != null)
        {
            hudArmMain.SetActive(true);
            arm1 = character.GetComponent<PickUp>().item1.GetComponent<Weapon>();
            float perc = Mathf.Clamp((float)arm1.integrity / (float)arm1.maxIntegrity, 0, 1);
            hudArmBarMain.transform.localScale = new Vector3(perc, 1, 1);
            MainIcon.sprite=arm1.weaponIcon;
            //Move the UI arm if it's active
            if(arm1.holding==1){
              mainPos.target=new Vector2(armActivePos.x,armActivePos.y);
            }else{
              mainPos.target=new Vector2(armInactivePos.x,armInactivePos.y);
            }
        }
        else
        {
            hudArmMain.SetActive(false);
        }
        //Left Side Arm
        if (character.GetComponent<PickUp>().item2 != null)
        {
            hudArmBack.SetActive(true);
            arm2 = character.GetComponent<PickUp>().item2.GetComponent<Weapon>();
            //scale healthbar
            float perc = Mathf.Clamp((float)arm2.integrity / (float)arm2.maxIntegrity, 0, 1);
            hudArmBarBack.transform.localScale = new Vector3(perc, 1, 1);
            //change icon sprite
            BackIcon.sprite=arm2.weaponIcon;
            //Move the UI arm if it's active
            if(arm2.holding==1){
              backPos.target=new Vector2(-armActivePos.x,armActivePos.y);
            }else{
              backPos.target=new Vector2(-armInactivePos.x,armInactivePos.y);
            }
        }
        else
        {
            hudArmBack.SetActive(false);
        }

    }
}
