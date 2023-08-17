using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_System : MonoBehaviour
{
    [SerializeField] 
    private GameObject player;
    public GameObject CD;
    public void ChooseSkill(SkillsManager skill)
    {
        player.GetComponent<PlayerAttack>().curSkill = skill;
        CD.GetComponent<Spell_CoolDown>().curSkill = skill;
    }
}
