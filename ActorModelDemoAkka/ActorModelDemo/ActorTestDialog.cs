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
        /// The actor system.
        /// </summary>
        private ActorSystem p_ActorSystem;

        /// <summary>
        /// The interface for communicating with the file writing actor.
        /// </summary>
        private Inbox p_FileWriterInbox;

        /// <summary>
        /// The file writer actor.
        /// </summary>
        private IActorRef p_FileWriter;

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
            p_ActorSystem = ActorSystem.Create("MyActorSystem");
            p_FileWriter = p_ActorSystem.ActorOf<FileWriter>("fileWriter");
            p_FileWriterInbox = Inbox.Create(p_ActorSystem);
        }

        /// <summary>
        /// Enable the timer.
        /// </summary>
        private void c_ButtonEnableTimer_Click(object sender, EventArgs e)
        {
            Debug.Print("UI Enabling the timer, messages will start to be sent");
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
            Debug.Print("UI Disabling the timer, no  more messages will be coming");
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
        private async void c_TimerMessageTimer_Tick(object sender, EventArgs e)
        {
            string Message = $"Message {p_MessageNumber++}";
            Debug.Print($"UI Sending: {Message}");
            FileWriter.Content ContentMessage = new FileWriter.Content(Message);
            p_FileWriterInbox.Send(p_FileWriter, ContentMessage);
            try
            {
                await p_FileWriterInbox.ReceiveAsync(TimeSpan.FromSeconds(2));
                Debug.Print($"UI Confirmed actor recieved message: {Message}");
            }
            catch (TimeoutException)
            {
                Debug.Print($"UI Failed to confirm Actor recieved message: {Message}");
            }
        }

        /// <summary>
        /// When starting a file, record the file name
        /// </summary>
        private async void c_ButtonStartFile_Click(object sender, EventArgs e)
        {
            string FileName = $"File-{p_FileNumber++}.txt";
            c_ButtonStopFile.Enabled = true;
            c_ButtonStartFile.Enabled = false;
            c_ButtonDisableTimer.Enabled = false;

            Debug.Print($"UI Starting file: {FileName}");
            FileWriter.Start StartMessage = new FileWriter.Start(FileName);
            p_FileWriterInbox.Send(p_FileWriter,StartMessage);
            try
            {
                await p_FileWriterInbox.ReceiveAsync(TimeSpan.FromSeconds(2));
                Debug.Print($"UI Confirmed actor started file: {FileName}");
            }
            catch (TimeoutException)
            {
                Debug.Print($"UI Failed to confirm Actor started file: {FileName}");
            }
        }

        /// <summary>
        /// Stop the file
        /// </summary>
        private async void c_ButtonStopFile_Click(object sender, EventArgs e)
        {
            c_ButtonStartFile.Enabled = true;
            c_ButtonStopFile.Enabled = false;
            c_ButtonDisableTimer.Enabled = true;

            Debug.Print($"UI Sending Stop Writing message.");

            //assign callback when telling writer to stop
            Action<string, int> Callback =
                new Action<string, int>(f_UpdateDisplayWhenDone);
            FileWriter.Stop StopMessage = new FileWriter.Stop(Callback);
            p_FileWriterInbox.Send(p_FileWriter, StopMessage);
            try
            {
                //can't use this because we aren't inside of an actor
                //bool ShutdownSuccess = await p_WriterActor.GracefulStop(TimeSpan.FromSeconds(2));

                //can use inbox
                await p_FileWriterInbox.ReceiveAsync(TimeSpan.FromSeconds(5));
                Debug.Print($"UI Confirmed Successfully stopped actor.");
            }
            catch (Exception)
            {
                Debug.Print($"UI Failed to stop actor.");
            }
        }
    }
}
