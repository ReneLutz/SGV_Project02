﻿using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [SerializeField] private GameObject _levelUp;

    public int Level;
   
    public Skill Skill;

    private bool _active;
    private bool _changeColor;
    private int _index;

    private SkillBarUI _bar;
    private Image _image;

    public SkillSlotUI Init(Skill skill, SkillBarUI bar, int index)
    {
        _changeColor = true;
        _active = false;
        _index = index;
        Skill = skill;

        _image.sprite = skill.Icon;
        _bar = bar;

        Level = Constants.SKILL_DEFAULT_LEVEL;

        return this;
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        if (!_changeColor) return;

        Color current = _image.color;
        
        if (_active)
        {
            _image.color = new Color(current.r, current.g, current.b, 1f);
        }
        else
        {
            _image.color = new Color(current.r, current.g, current.b, 0.6f);
        }

        _changeColor = false;
    }

    public void SetActive(bool active)
    {
        _active = active;
        _changeColor = true;
    }

    public void OnButtonClick()
    {
        _bar.ActivateSkill(_index);
    }

    public void OnLevelUpClick()
    {
        Level++;
        _bar.LevelUpSkill(_index, Level);
    }

    public void SetPlusActive(bool active)
    {
        _levelUp.SetActive(active);
    }
}
