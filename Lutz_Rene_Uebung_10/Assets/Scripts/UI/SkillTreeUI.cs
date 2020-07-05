using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    [SerializeField] private SkillBarUI _bar;

    [SerializeField] private Controller _player;

    [SerializeField] private List<SkillTreeNodeUI> _treeNodes;

    public int LevelCount { get; private set; } = 0;

    private SkillTreeNodeUI _root;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        _root = _treeNodes[0];
        _bar.AddSkill(_root.Skill);

        _player._onLevelUp += OnLevelUp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            _canvas.enabled = !_canvas.enabled;
        }
    }

    private void OnLevelUp()
    {
        LevelCount++;
    }

    public void AddSkillToBar(SkillTreeNodeUI node)
    {
        // Only when allowed to activate skill
        if (LevelCount <= 0) return;
        LevelCount--;

        _bar.AddSkill(node.Skill);

        node.Selected = true;
        node.ActivateLine();
    }
}
