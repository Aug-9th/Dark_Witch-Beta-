using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;  
using UnityEngine.UI;
public class Spell_CoolDown : MonoBehaviour
{
    public Image imageCoolDown;
    public SkillsManager curSkill;
    public float Timer = 0.0f;
    public bool isCoolDown;
    void Start()
    {
        imageCoolDown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        ApplyCD();
    }

    public void ApplyCD()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0.0f)
        {
            imageCoolDown.fillAmount = 0.0f;
        }
        else
        {
            imageCoolDown.fillAmount = Timer / curSkill.cooldownTime;
        }
    }
}
