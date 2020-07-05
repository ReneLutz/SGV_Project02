using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeNodeUI : MonoBehaviour
{
    [SerializeField] public Skill Skill;
    [SerializeField] private SkillTreeUI _tree;

    [SerializeField] private List<SkillTreeNodeUI> _previousNodes;
    [SerializeField] private List<Image> _line;

    public bool Selected;

    private bool _changeColor;

    private Image _image;

    public SkillTreeNodeUI Init()
    {
        _changeColor = true;

        _image.sprite = Skill.Icon;

        Color color = _image.color;
        _image.color = new Color(color.r, color.g, color.b, 0.5f);

        return this;
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        
        Init();
    }

    private void Update()
    {
        // Change color only of needed. This saves color allocations
        if (Selected && _changeColor)
        {
            Color color = _image.color;
            _image.color = new Color(color.r, color.g, color.b, 1f);
        }
    }

    public void ActivateLine()
    {
        foreach (Image image in _line)
        {
            image.color = Constants.TREE_LINE_COLOR;
        }
    }

    public void OnTreeNodeSelected()
    {
        if (Selected) return;

        foreach (SkillTreeNodeUI node in _previousNodes)
        {
            if (!node.Selected) return;
        }

        _tree.AddSkillToBar(this);
    }
}
