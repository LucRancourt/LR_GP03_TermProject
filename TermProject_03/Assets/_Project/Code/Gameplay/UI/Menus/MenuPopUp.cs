using UnityEngine;
using UnityEngine.UI;

public abstract class MenuPopUp : Menu
{
    [SerializeField] private Button clickAwayPanel;


    protected override void Awake()
    {
        base.Awake();

        if (clickAwayPanel.TryGetComponent(out TweenedButton tweener))
        {
            Destroy(tweener);
        }
    }
}
