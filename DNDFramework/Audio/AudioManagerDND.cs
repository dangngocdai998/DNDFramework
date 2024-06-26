using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using CarterGames.Assets.AudioManager;
using UnityEngine;

namespace DNDFramework
{
    public class AudioManagerDND : SingletonMonoBehaviour<AudioManagerDND>
    {
        [Header("Setup")]
        [SerializeField] protected MusicPlayer music;
        [SerializeField] protected AudioManager audioSound;

        #region Vol

        float _volMusic = -100;
        public float volMusic
        {
            get
            {
                if (_volMusic < 0)
                {
                    _volMusic = PlayerPrefs.GetFloat("Volume_Music", 1);
                }
                return _volMusic;
            }
            private set
            {
                _volMusic = Mathf.Clamp01(value);
                PlayerPrefs.SetFloat("Volume_Music", _volMusic);
            }
        }
        float _volSound = -100;
        public float volSound
        {
            get
            {
                if (_volSound < 0)
                {
                    _volSound = PlayerPrefs.GetFloat("Volume_Sound", 1);
                }
                return _volSound;
            }
            private set
            {
                _volSound = Mathf.Clamp01(value);
                PlayerPrefs.SetFloat("Volume_Sound", _volSound);
            }
        }
        #endregion

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Tesst();
            }
            // if (Input.GetKeyDown(KeyCode.S))
            // {
            //     PlayMusic();
            // }
        }

        void Tesst()
        {
            PlaySound("Button");
        }
#endif
        public void PlaySound(string nameSound)
        {
            audioSound.Play(nameSound, volSound, 1);
        }

        public virtual void PlayMusic()
        {
            music.SetVolume(volMusic);
            music.PlayTrack();
        }
        public void SetVolumeMusic(float value)
        {
            volMusic = value;
            music.SetVolume(volMusic);
        }

        public void SetVolumeSound(float value)
        {
            volSound = value;
        }
    }
}
