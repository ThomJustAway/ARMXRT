﻿using UnityEngine;

using System;

namespace Assets.Scripts.Manager
{
    [Serializable]
    public struct AmbientMusicClip
    {
        public AmbientClip ambientSFX;
        public AudioClip clip;
        [HideInInspector] public AudioSource source;
        [Range(0, 1)]
        public float volume;
        [Range(-3, 3)]
        public float pitch;
    }
}