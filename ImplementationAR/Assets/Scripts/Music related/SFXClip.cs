using System;

namespace Assets.Scripts.Manager
{
    /// <summary>
    /// Simple Enum to keep track of the sound effect to call out too.
    /// </summary>
    [Serializable]
    public enum SFXClip
    {//make sure to only add linearly and dont add anything in between to mess up the thing
        ButtonClick,
        ReturnButtonClick,
        NotificationSound,
        ShelfOpening,
        ShelfClosing,
        GotSomethingSFX,
        MetalDoorOpening,
        VaultDoorOpening,
        UnlockingDoor,
        WinSound,
        LoseSound
    }
}