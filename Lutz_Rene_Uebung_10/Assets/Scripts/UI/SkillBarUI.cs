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
        foreach (Skill skill in skills)
        {
            AddSkill(skill);
        }

        return this;
    }

    private void Awake()
    {
        _slots = new List<SkillSlotUI>();
    }

    private void Start()
    {
        _player._onLevelUp += OnLevelUp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateSkill(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateSkill(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateSkill(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateSkill(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActivateSkill(4);
        }
    }

    public void ActivateSkill(int index)
    {
        if (index >= _slots.Count) return;

        _slots[_activeSkillIndex].SetActive(false);
        _slots[index].SetActive(true);

        _activeSkillIndex = index;

        _player.SetSkill(_slots[_activeSkillIndex].Skill, _slots[_activeSkillIndex].Level);
    }

    public void LevelUpSkill(int index, int level)
    {
        _levelUpCount--;

        if (_levelUpCount <= 0) SetSlotPlusesActive(false);

        if (_activeSkillIndex == index) _player.SetSkill(_slots[_activeSkillIndex].Skill, level);
    }

    public void OnLevelUp()
    {
        _levelUpCount++;
        SetSlotPlusesActive(true);
    }

    public void AddSkill(Skill skill)
    {
        SkillSlotUI slot;

        slot = Instantiate(_slotPrefab, transform);

        _slots.Add(slot);

        slot.Init(skill, this, _slots.IndexOf(slot));

        if (_levelUpCount > 0)
        {
            slot.SetPlusActive(true);
        }

        if (_slots.Count == 1) ActivateSkill(0);
    }

    private void SetSlotPlusesActive(bool active)
    {
        foreach (SkillSlotUI slot in _slots)
        {
            slot.SetPlusActive(active);
        }
    }
}
