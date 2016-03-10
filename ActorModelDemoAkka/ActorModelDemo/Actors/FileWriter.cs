using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActorModelDemo.Actors
{
    /// <summary>
    /// This class is an actor that writes files and sends
    /// results.
    /// </summary>
    class FileWriter :
        TypedActor, 
        IHandle<FileWriter.Start>, 
        IHandle<FileWriter.Stop>, 
        IHandle<FileWriter.Content>
    {

        #region Messages

        /// 03/09/2016 - CLH
        /// <summary>
        /// The message to start the fileWriter.
        /// </summary>
        internal class Start : Message
        {
            /// 03/09/2016 - CLH
            /// <summary>
            /// The name of the file to be written.
            /// </summary>
            public string p_FileName { get; private set; }

            public Start(string FileName)
            {
                p_FileName = FileName;
            }
        }

        /// 03/09/2016 - CLH
        /// <summary>
        /// The message containing content to be written to a
        /// previously started file.
        /// </summary>
        internal class Content : Message
        {
            /// 03/09/2016 - CLH
            /// <summary>
            /// The content to be written to the file.
            /// </summary>
            public string p_Content { get; private set; }

            public Content(string Content)
            {
                p_Content = Content;
            }
        }

        /// 03/09/2016 - CLH
        /// <summary>
        /// Message to stop writing and call a callback from the 
        /// calling thread.
        /// </summary>
        internal class Stop : Message
        {
            /// 03/09/2016 - CLH
            /// <summary>
            /// The callback to be executed when finished writing.
            /// </summary>
            public Action<string, int> p_DoneCallback;

            public Stop(Action<string, int> Callback)
            {
                p_DoneCallback = Callback;
            }
        }

        #endregion

        /// 03/09/2016 - CLH
        /// <summary>
        /// The file being written.
        /// </summary>
        private StreamWriter p_Writer { get; set; }

        /// 03/09/2016 - CLH
        /// <summary>
        /// The count of messages received.
        /// </summary>
        private int p_MessageCount { get; set; }

        /// 03/09/2016 - CLH
        /// <summary>
        /// The name of the file being written.
        /// </summary>
        private string p_FileName { get; set; }

        /// 03/09/2016 - CLH
        /// <summary>
        /// Whether the actor is currently writing or not
        /// </summary>
        private bool p_IsWriting { get; set; } = false;

        /// 03/09/2016 - CLH
        /// <summary>
        /// Handles start messages.
        /// </summary>
        public void Handle(Start StartMessage)
        {
            if (p_IsWriting == true)
            {
                Debug.Print($"Start Message disregarded. Already writing to file: {p_FileName?.ToString()}");
                return;
            }
            p_FileName = StartMessage.p_FileName;
            p_MessageCount = 0;
            Debug.Print($"Opening file to write: {p_FileName}");
            try
            {
                p_Writer = new StreamWriter(p_FileName);
            }
            catch (Exception Ex)
            {
                Debug.Print($"Failed openeing file to write because: {Ex.Message}");
            }
            p_IsWriting = true;
        }

        /// 03/09/2016 - CLH
        /// <summary>
        /// Handles incoming stuff to write to file.
        /// </summary>
        public void Handle(Content ContentMessage)
        {
            if (p_IsWriting == false)
            {
                Debug.Print($"Content Message disregarded. Writing has not started.");
                return;
            }
            Debug.Print($"Writing file content: {ContentMessage.p_Content}");
            try
            {
                p_Writer.WriteLine(ContentMessage.p_Content);
                p_MessageCount++;
            }
            catch (Exception Ex)
            {
                Debug.Print($"Failed writing to file because: {Ex.Message}");
            }
        }

        /// 03/09/2016 - CLH
        /// <summary>
        /// Handles stopping the file writing process and
        /// calling the passed callback.
        /// </summary>
        public void Handle(Stop StopMessage)
        {
            if (p_IsWriting == false)
            {
                Debug.Print($"Stop Message disregarded. Writing has not started.");
                return;
            }
            Debug.Print($"Stop message recieved. Closing file: {p_FileName}");
            try
            {
                p_Writer.Close();
            }
            catch (Exception Ex)
            {
                Debug.Print($"Failed closing file because: {Ex.Message}");
            }
            try
            {
                Debug.Print($"Calling callback from FileWriter thread: {Thread.CurrentThread.ManagedThreadId}");
                StopMessage.p_DoneCallback(p_FileName, p_MessageCount);
            }
            catch (Exception Ex)
            {
                Debug.Print($"Couldn't call callback because: {Ex.Message}");
            }
            p_IsWriting = false;
        }
    }
}
