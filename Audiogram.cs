using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace poc
{
    public class Result {
        public Result(string type, string ear, int frequency, float volume = 0f) {
            Type = type;
            Ear = ear;
            Frequency = frequency;
            Volume = volume;
        }
        public string Type { set; get; }  // air, bone
        public string Ear { set; get; }  // left, right
        public int Frequency { set; get; }
        public float Volume { set; get; }
    }

    public class Audiogram
    {
        public Audiogram()
        {
            Type = "air";
            Ear = "left";
            Volume = 1f;
        }

        public List<Result> Results = new List<Result>();
        int[] recipe = { 1000, 2000, 4000, 8000, 500, 250, 125 };
        int step = 0;
        private readonly Beep Beep = new Beep();
        bool stopped = false;

        public string Ear { set; get; }
        public string Type { set; get; }
        public float Volume {
            set {
                if (Ear == "left") Beep.Volume.LeftOnly = value;
                else Beep.Volume.RightOnly = value;
            }
            get {
                if (Ear == "left") return Beep.Volume.Left;
                return Beep.Volume.Right;
            }
        }


        public void Confirm() {
            Results.Add(new Result(Type, Ear, Beep.Frequency, Volume));
            stopped = true;
            PlayNextFrequency(Beep.Frequency);
        }

        public bool IsComplete(int frequency) {
            var found = Results.Find(element => {
                return element.Frequency == frequency
                && element.Type == Type
                && element.Ear == Ear;
                });
            return found != null;
        }

        public void PlayNextFrequency(int frequency) {
            for (int i = 0; i < recipe.Length; i++)
            {
                if (recipe[i] != frequency && !IsComplete(recipe[i]))
                {
                    step = i;
                    Play(recipe[i]);
                }
            }
        }

        public bool Play(int frequency = 1000) {
            Beep.Frequency = frequency;
            Beep.Play();
            Thread.Sleep(5000);
            if (stopped)
            {
                stopped = false;
                return stopped;
            }
            PlayNextFrequency(frequency);
            return true;
        }

        public void Stop() {
            stopped = true;
        }

    }
}
