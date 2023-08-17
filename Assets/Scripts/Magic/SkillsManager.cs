using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skill")]
public class SkillsManager : ScriptableObject
{
    [SerializeField]
    public int damage;
    public float speed;
    public float cooldownTime;
    public int manaCost;
    public float timeLife;
    public GameObject Skill;
}
