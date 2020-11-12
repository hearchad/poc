using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Threading;
using System.Media;
using System.Security.AccessControl;

namespace poc
{

    public class Volume
    {
        public Volume(float left = 0, float right = 0) {
            this.Left = left;
            this.Right = right;
        }
        public float Left { get; set; }
        public float Right { get; set; }

        public float LeftOnly {
            set {
                Left = value; Right = 0f;
            }
        }
        public float RightOnly
        {
            set
            {
                Right = value; Left = 0f;
            }
        }
    }
    public class Beep
    {
        private MonoToStereoSampleProvider stereo;
        private readonly WaveOutEvent waveOut = new WaveOutEvent();
        public Volume Volume = new Volume();
        public SignalGenerator signal = new SignalGenerator(44100, 1);

        public Beep(int freq = 500) {
            this.Frequency = freq;
        }

        public int Frequency {
            set {
                signal.Frequency = value;
                stereo = new MonoToStereoSampleProvider(signal);
            }
            get {
                return (int)signal.Frequency;
            }
        }

        public void Play(int duration = 500) {
            stereo.LeftVolume = Volume.Left; 
            stereo.RightVolume = Volume.Right;

            waveOut.Stop();
            waveOut.Init(stereo.Take(TimeSpan.FromMilliseconds(duration)));
            waveOut.Play();
        }
    }
}

