using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager: MonoBehaviour
{
    public static BuffManager instance;
    private void Start()
    {
        instance = this;
    }
    private bool _isExtraDamageActive;
    private bool _isExtraDefenderActive;
    private bool _isExtraExtractionActive;
    private bool _isDoubleBuffTimeActive;
    private bool _isMegaPickaxeActive;
    private bool _isPirateActive;
    private bool _isKnightActive;

    public bool IsExtraDamageActive { get => _isExtraDamageActive; set => _isExtraDamageActive = value; }
    public bool IsExtraDefenderActive { get => _isExtraDefenderActive; set => _isExtraDefenderActive = value; }
    public bool IsExtraExtractionActive { get => _isExtraExtractionActive; set => _isExtraExtractionActive = value; }
    public bool IsDoubleBuffTimeActive { get => _isDoubleBuffTimeActive; set => _isDoubleBuffTimeActive = value; }
    public bool IsMegaPickaxeActive { get => _isMegaPickaxeActive; set => _isMegaPickaxeActive = value; }
    public bool IsPirateActive { get => _isPirateActive; set => _isPirateActive = value; }
    public bool IsKnightActive { get => _isKnightActive; set => _isKnightActive = value; }
}
