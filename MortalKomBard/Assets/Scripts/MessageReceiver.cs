using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageReceiver : MonoBehaviour
{
    public GMArena arenaGameManager;
    public int numberOfFaces;

    public static MessageReceiver Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    [ProButton]
    public void PlayerSmiled()
    {
        arenaGameManager?.PlayerSmiled(string.Empty);
    }

    [ProButton]
    public void SetNumberOfFaces(int value)
    {
        numberOfFaces = value;
    }
}
