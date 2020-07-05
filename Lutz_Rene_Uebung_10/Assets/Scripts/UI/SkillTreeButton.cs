using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{

    [SerializeField] GameObject _skillTreeObject;

    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    private Canvas _treeCanvas;
    private SkillTreeUI _treeUI;

    private void Awake()
    {
        _treeCanvas = _skillTreeObject.GetComponent<Canvas>();
        _treeUI = _skillTreeObject.GetComponent<SkillTreeUI>();
    }

    void Update()
    {
        SetActive(_treeUI.LevelCount > 0 || _treeCanvas.enabled);
    }

    private void SetActive(bool active)
    {
        _button.enabled = active;
        _image.enabled = active;
    }

    public void OnButtonClick()
    {
        _treeCanvas.enabled = !_treeCanvas.enabled;
    }
}
