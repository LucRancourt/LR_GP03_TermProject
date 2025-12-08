using _Project.Code.Core.MVC;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WaveSkipper : BaseView
{
    [SerializeField] private Button skipWaveButton;

    //public bool IsAutoSkipActive { get; private set; } = false;

    public event Action OnWaveSkipped;


    public override void Initialize()
    {
        base.Initialize();

        skipWaveButton.onClick.RemoveAllListeners();
        skipWaveButton.onClick.AddListener(() => OnWaveSkipped?.Invoke());
    }
}
