using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField, NaughtyAttributes.AnimatorParam("_animator", AnimatorControllerParameterType.Trigger)] private string _openTrigger;


    public void Open()
    {

    }


    public void Close()
    {

    }
}
