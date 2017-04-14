﻿using UnityEngine;
using UnityEngine.Events;

namespace Leap.Unity.UI.Interaction {

  /// <summary>
  /// A physics-enabled toggle. Toggling is triggered by physically pushing the toggle to its compressed position. 
  /// </summary>
  public class InteractionToggle : InteractionButton {

    [Tooltip("The height that this button rests at; this value is a lerp in between the min and max height.")]
    [Range(0f, 1f)]
    ///<summary> The height that this toggle rests at when it is toggled. </summary>
    public float toggledRestingHeight = 0.25f;
    
    [SerializeField]
    private bool _toggled = false;

    ///<summary> Whether or not this toggle is currently toggled. </summary>
    public bool toggled {
      get {
        return _toggled;
      }
      set {
        if (_toggled != value) {
          _toggled = value;
          if (_toggled) {
            toggleEvent.Invoke();
          }
          restingHeight = toggled ? toggledRestingHeight : _originalRestingHeight;
          rigidbody.WakeUp();
          depressedThisFrame = value;
          unDepressedThisFrame = !value;
        }
      }
    }

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    ///<summary> Triggered when this toggle is togggled. </summary>
    public UnityEvent toggleEvent = new UnityEvent();

    ///<summary> The minimum and maximum heights the button can exist at. </summary>
    private float _originalRestingHeight;

    protected override void Start() {
      base.Start();
      _originalRestingHeight = restingHeight;
      if (toggled) {
        restingHeight = toggledRestingHeight;
        toggleEvent.Invoke();
      }
    }

    protected virtual void OnEnable() {
      OnPress.AddListener(OnPressed);
    }

    protected virtual void OnDisable() {
      OnPress.RemoveListener(OnPressed);
    }

    private void OnPressed() {
      toggled = !toggled;
    }
  }
}
