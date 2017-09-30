﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A button always consists of a text field and a background image. 
/// It can also show an icon (some gameobject).
/// The icon and the text content are stored in the given ButtonData
/// </summary>
namespace MRUI
{
    [ExecuteInEditMode]
    public class Button : MonoBehaviour
    {
        public ButtonData data;
        private ButtonData oldData;

        public enum Transition { None, Material };
        public Transition transition;

        public Material normalMaterial;
        public Material highlightedMaterial;
        public Material pressedMaterial;

        public float fadeDuration = .1f;

        [Tooltip("defines if this should behave like a toggle button")]
        public bool toggle;

        [Tooltip("Tapped-event (air tap or clicker, simulated by touch or left click)")]
        public UnityEvent OnPressed;

        // "Data has been changed"
        [HideInInspector]
        public UnityEvent OnDataChanged;

        [Tooltip("Gaze on Button (hover with mouse).")]
        public UnityEvent OnHighlighted;

#if UNITY_EDITOR
        private bool forceUpdate = false;
#endif

        public bool dataChanged()
        {
            return (oldData != data || oldData.icon != data.icon || oldData.title != data.title);
        }

        void Start()
        {
            updateData();
        }

        
        private void updateChanged()
        {
            if (dataChanged() && OnDataChanged != null)
            {
                OnDataChanged.Invoke();
                oldData = data.copy();
            }
        }

        public void updateData()
        {
#if UNITY_EDITOR
            forceUpdate = true;
#else
            updateChanged();
#endif
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            updateData();
        }

        public void OnEnable()
        {
            updateData();
        }

        private void Update()
        {
            if (forceUpdate)
            {
                updateChanged();
                forceUpdate = false;
            }
        }
#endif

    }
}