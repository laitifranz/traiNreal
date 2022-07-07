/****************************************************************************
* Copyright 2019 Nreal Techonology Limited. All rights reserved.
*                                                                                                                                                          
* This file is part of NRSDK.                                                                                                          
*                                                                                                                                                           
* https://www.nreal.ai/        
* 
*****************************************************************************/

namespace NRKernal.Record
{
    using UnityEngine;
    using System;
    using AOT;
    using System.Runtime.InteropServices;

    /// <summary> A video encoder. </summary>
    public class AudioEncoder : IEncoderBase
    {
        private NativeEncoder mNativeEncoder;
        public NativeEncodeConfig EncodeConfig;
        private IntPtr androidMediaProjection { get; set; }
                
        private bool m_IsStarted = false;
        private AudioDataCallBack mDataCallBack = null;

        /// <summary> Default constructor. </summary>
        public AudioEncoder()
        {
#if !UNITY_EDITOR
            mNativeEncoder = NativeEncoder.GetInstance();
            mNativeEncoder.Register(this);
#endif
        }

        /// <summary> Configurations the given parameter. </summary>
        /// <param name="param"> The parameter.</param>
        public void Config(CameraParameters param)
        {
            EncodeConfig = new NativeEncodeConfig(param);
            androidMediaProjection = (param.mediaProjection != null) ? param.mediaProjection.GetRawObject() : IntPtr.Zero;
        }

        /// <summary> Adjust the volume of encoder.</summary>
        /// <param name="recordIdx"> Recorder index.</param>
        /// <param name="factor"> The factor of volume.</param>
        public void AdjustVolume(RecorderIndex recordIdx, float factor)
        {
#if !UNITY_EDITOR
            mNativeEncoder.AdjustVolume(recordIdx, factor);
#endif
        }

        /// <summary> Starts this object. </summary>
        public void Start(AudioDataCallBack onAudioDataCallback = null)
        {
            if (m_IsStarted)
            {
                return;
            }
            NRDebugger.Info("[AudioEncoder] Start");
            NRDebugger.Info("[AudioEncoder] Config {0}", EncodeConfig.ToString());
            mDataCallBack = onAudioDataCallback;
#if !UNITY_EDITOR
            mNativeEncoder.SetConfigration(EncodeConfig, androidMediaProjection);
            mNativeEncoder.StartAudioRecorder(mDataCallBack);
#endif
            m_IsStarted = true;
        }

        /// <summary> Stops this object. </summary>
        public void Stop()
        {
            if (!m_IsStarted)
            {
                return;
            }

            NRDebugger.Info("[AudioEncoder] Stop");
#if !UNITY_EDITOR
            mNativeEncoder.StopAudioRecorder(mDataCallBack);
#endif
            m_IsStarted = false;
        }

        /// <summary> Releases this object. </summary>
        public void Release()
        {
            NRDebugger.Info("[AudioEncoder] Release...");
#if !UNITY_EDITOR
            mNativeEncoder.UnRegister(this);
            mNativeEncoder.Destroy();
#endif
        }
    }
}
