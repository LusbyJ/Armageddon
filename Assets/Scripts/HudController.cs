using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HudController : MonoBehaviour
{
    public GameObject character;
    public GameObject hudArmMain;
    public GameObject hudArmBack;
    public GameObject emergencyLight1;
    public GameObject emergencyLight2;
    public GameObject hudArmBarMain;
    public GameObject hudArmBarBack;
    private Weapon arm1;
    private Weapon arm2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

      if(PickUp.left){
        hudArmMain.SetActive(true);
        arm1=character.GetComponent<PickUp>().item1.GetComponent<Weapon>();
        float perc=Mathf.Clamp((float)arm1.integrity/(float)arm1.maxIntegrity,0,1);
        hudArmBarMain.transform.localScale=new Vector3(perc,1,1);
      }else{
        hudArmMain.SetActive(false);
      }
      if(PickUp.right){
        hudArmBack.SetActive(true);
        arm2=character.GetComponent<PickUp>().item2.GetComponent<Weapon>();
        float perc=Mathf.Clamp((float)arm2.integrity/(float)arm2.maxIntegrity,0,1);
        hudArmBarBack.transform.localScale=new Vector3(perc,1,1);
      }else{
        hudArmBack.SetActive(false);
      }

    }
}
