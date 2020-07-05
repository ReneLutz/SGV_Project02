using System.Collections.Generic;
using UnityEngine;

public class SkillBarUI : MonoBehaviour
{
    [SerializeField] private SkillSlotUI _slotPrefab;
    [SerializeField] private Controller _player;

    private List<SkillSlotUI> _slots;

    private int _activeSkillIndex = 0;
    private int _levelUpCount = 0;

    public SkillBarUI Init(List<Skill> skills)
    {
        SkillSlotUI slot;

        foreach(Skill skill in skills)
        {
            slot = Instantiate(_slotPrefab, transform);

            _slots.Add(slot);

            slot.Init(skill.Icon, this, _slots.IndexOf(slot));
        }

        return this;
    }

    private void Awake()
    {
        _slots = new List<SkillSlotUI>();
    }

    private void Start()
    {
        Init(_player.Skills);

        // Init player with skill 0
        _slots[_activeSkillIndex].SetActive(true);
        _player.SetSkill(_activeSkillIndex, Constants.SKILL_DEFAULT_LEVEL);

        _player._onLevelUp += OnLevelUp;
    }

    private void Update()
    {
        int newActiveSkillIndex = _activeSkillIndex;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            newActiveSkillIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            newActiveSkillIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            newActiveSkillIndex = 2;
        }

        ActivateSkill(newActiveSkillIndex);
    }

    public void ActivateSkill(int index)
    {
        if (index >= _slots.Count) return;

        if (_activeSkillIndex == index) return;

        _slots[_activeSkillIndex].SetActive(false);
        _slots[index].SetActive(true);

        _activeSkillIndex = index;

        _player.SetSkill(_activeSkillIndex, _slots[_activeSkillIndex].Level);
    }

    public void LevelUpSkill(int index, int level)
    {
        _levelUpCount--;

        if (_levelUpCount <= 0) SetSlotPlusesActive(false);

        if (_activeSkillIndex == index) _player.SetSkill(_activeSkillIndex, level);
    }

    public void OnLevelUp()
    {
        _levelUpCount++;
        SetSlotPlusesActive(true);
    }

    private void SetSlotPlusesActive(bool active)
    {
        foreach (SkillSlotUI slot in _slots)
        {
            slot.SetPlusActive(active);
        }
    }
}
