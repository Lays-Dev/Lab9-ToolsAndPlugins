using UnityEngine;
using UnityEngine.InputSystem;

// S saves. L loads.
public class SaveLoadInput : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.sKey.wasPressedThisFrame)
            SavingService.SaveGame();

        if (Keyboard.current.lKey.wasPressedThisFrame)
            SavingService.LoadGame();
    }
}
