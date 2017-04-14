﻿using Leap.Unity;
using Leap.Unity.UI.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionBehaviour))]
public class SimpleHoverGlow : MonoBehaviour {

  private Material _material;

  private InteractionBehaviour _intObj;

  void Start() {
    _intObj = GetComponent<InteractionBehaviour>();

    Renderer renderer = GetComponent<Renderer>();
    if (renderer == null) {
      renderer = GetComponentInChildren<Renderer>();
    }
    if (renderer != null) {
      _material = renderer.material;
    }
  }

  void Update() {
    if (_material != null) {
      if (_intObj.isSuspended) {
        _material.color = new Color(0.7F, 0F, 0F, 1F);
      }
      else {
        _material.color = new Color(0.0F, 0.0F, 0.0F, 1F);
      }
      
      if (_intObj.isHovered) {
        float glow = Vector3.Distance(_intObj.closestHoveringHand.PalmPosition.ToVector3(), this.transform.position).Map(0F, 0.2F, 1F, 0.0F);
        _material.color = new Color(glow, glow, glow, 1F);
      }
    }
  }

}
