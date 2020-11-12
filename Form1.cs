using System;
using System.Windows.Forms;

namespace poc
{

    public partial class Form1 : Form
    {
        Audiogram Audiogram = new Audiogram();
        bool playing = false;
        public Form1()
        {
            InitializeComponent();
        }

        private async void OnClick(object sender, EventArgs e)
        {
            if (!playing)
            {
                var stopped = await Audiogram.Play();
            }
            else Audiogram.Confirm();
            playing = true;
        }

    }
}
