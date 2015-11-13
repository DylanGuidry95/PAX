using UnityEngine;
using System.Collections;

public class QuickTest : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameStateManager.ToPlay();
        }
    }
}
