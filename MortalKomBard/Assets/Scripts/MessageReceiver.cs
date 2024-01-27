using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageReceiver : MonoBehaviour
{
    public GMArena arenaGameManager;
    public int numberOfFaces;

    [ProButton]
    public void PlayerSmiled()
    {
        arenaGameManager.PlayerSmiled(string.Empty);
    }

    [ProButton]
    public void SetNumberOfFaces(int value)
    {
        numberOfFaces = value;
    }
}
