using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
 public void OnDebug(InputAction.CallbackContext context)
    {
        Debug.Log($"OnDebug Fired with value: true", this);
    }

}
