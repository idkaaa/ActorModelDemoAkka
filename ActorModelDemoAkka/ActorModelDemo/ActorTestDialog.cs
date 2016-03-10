using ActorModelDemo.Actors;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActorModelDemo
{
    /// <summary>
    /// This dialog helps to demonstrate the actor model in
    /// this example.
    /// </summary>
    public partial class ActorTestDialog : Form
    {
        /// <summary>
        /// The file writer actor.
        /// </summary>
        private IActorRef p_WriterActor;

        /// <summary>
        /// The message number, each message is unique
        /// </summary>
        private int p_MessageNumber { get; set; } = 0;

        /// <summary>
        /// The file number, each file is unique
        /// </summary>
        private int p_FileNumber { get; set; } = 0;

        public ActorTestDialog()
        {
            InitializeComponent();
            c_TimerMessageTimer.Interval = 1000;  // 1000ms timer for now
            var system = ActorSystem.Create("MyActorSystem");
            p_WriterActor = system.ActorOf<FileWriter>("fileWriter");
        }

        /// <summary>
        /// Enable the timer.
        /// </summary>
        private void c_ButtonEnableTimer_Click(object sender, EventArgs e)
        {
            Debug.Print("Enabling the timer, messages will start to be sent");
            c_TimerMessageTimer.Start();
            c_ButtonStartFile.Enabled = true;
            c_ButtonEnableTimer.Enabled = false;
            c_ButtonDisableTimer.Enabled = true;
        }

        /// <summary>
        /// Disable the timer
        /// </summary>
        private void c_ButtonDisableTimer_Click(object sender, EventArgs e)
        {
            Debug.Print("Disabling the timer, no  more messages will be coming");
            c_ButtonDisableTimer.Enabled = false;
            c_ButtonStartFile.Enabled = false;
            c_ButtonEnableTimer.Enabled = true;
            c_TimerMessageTimer.Stop();
        }

        /// <summary>
        /// This should be called when the display is done
        /// This function is private.   
        /// This function is ran on the UI thread.
        /// </summary>
        private void f_UpdateDisplayWhenDone(string Filename, int MessageCount)
        {
            this.Invoke(new Action(() =>
           {
               Debug.Print($"UI notified (on UI thread) of finished file {Filename}, messagecount={MessageCount}");
               c_TextBoxResults.AppendText($"Adding file {Filename}, MessageCount={MessageCount}{Environment.NewLine}");
           }));
        }

        /// <summary>
        /// Send each message to the actor
        /// </summary>
        private void c_TimerMessageTimer_Tick(object sender, EventArgs e)
        {
            string Message = $"Message {p_MessageNumber++}";
            Debug.Print($"Sending: {Message}");
            FileWriter.Content ContentMessage = new FileWriter.Content(Message);
            p_WriterActor.Tell(ContentMessage);
        }

        /// <summary>
        /// When starting a file, record the file name
        /// </summary>
        private void c_ButtonStartFile_Click(object sender, EventArgs e)
        {
            string FileName = $"File-{p_FileNumber++}.txt";
            c_ButtonStopFile.Enabled = true;
            c_ButtonStartFile.Enabled = false;
            c_ButtonDisableTimer.Enabled = false;

            Debug.Print($"Starting file: {FileName}");
            FileWriter.Start StartMessage = new FileWriter.Start(FileName);
            p_WriterActor.Tell(StartMessage);
        }

        /// <summary>
        /// Stop the file
        /// </summary>
        private void c_ButtonStopFile_Click(object sender, EventArgs e)
        {
            c_ButtonStartFile.Enabled = true;
            c_ButtonStopFile.Enabled = false;
            c_ButtonDisableTimer.Enabled = true;

            Debug.Print($"Stopping the file.");

            //assign callback when telling writer to stop
            Action<string, int> Callback =
                new Action<string, int>(f_UpdateDisplayWhenDone);
            FileWriter.Stop StopMessage = new FileWriter.Stop(Callback);
            p_WriterActor.Tell(StopMessage);
        }
    }
}
