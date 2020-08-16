﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.VisaNS;
using Xu;

namespace TestFSQ
{
    public abstract class VisaClient : IDisposable, IEquatable<VisaClient>
    {
        public VisaClient(string resourceName) => Open(resourceName);

        public void Dispose() => Session?.Dispose();

        private MessageBasedSession Session { get; set; }

        public string ResourceName { get; private set; }

        public string VendorName { get; private set; } = "Unknown";

        public string Model { get; private set; } = "Unknown";

        public string SerialNumber { get; private set; } = "Unknown";

        public string DeviceVersion { get; private set; } = "Unknown";

        protected void Open(string resourceName)
        {
            try
            {
                ResourceName = resourceName;
                Session = ResourceManager.GetLocalManager().Open(ResourceName) as MessageBasedSession;
                Reset();
                string[] result = Query("*IDN?\n").Split(',');
                if (result.Length > 3)
                {
                    VendorName = result[0].Trim();
                    Model = result[1].Trim();
                    SerialNumber = result[2].Trim();
                    DeviceVersion = result[3].Trim();
                }
            }
            catch (InvalidCastException iexp)
            {
                Session = null;
                MessageBox.Show("Type must be \"MessageBasedSession\": " + iexp.Message);
            }
            catch (Exception exp)
            {
                Session = null;
                MessageBox.Show(exp.Message);
            }
            finally
            {
                // Send message connection is established
            }
        }

        public void Close() => Session.Dispose();

        public void Write(string cmd)
        {
            try
            {
                lock (Session)
                    Session.Write(ReplaceCommonEscapeSequences(cmd));
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public string Read()
        {
            try
            {
                string res = null;

                lock (Session)
                {
                    res = Session.ReadString();
                }

                return res;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return null;
            }
            finally
            {
                // Send message
            }
        }

        public string Query(string cmd)
        {
            try
            {
                string res = null;

                lock (Session)
                {
                    res = Session.Query(ReplaceCommonEscapeSequences(cmd));
                }

                return res;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return null;
            }
            finally
            {
                // Send message
            }
        }

        public void WriteAsync(string cmd)
        {
            try
            {
                string textToWrite = ReplaceCommonEscapeSequences(cmd);
                lock (Session)
                {
                    AsyncHandle = Session.BeginWrite(
                    textToWrite,
                    new AsyncCallback(OnWriteComplete),
                    textToWrite.Length as object);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void OnWriteComplete(IAsyncResult result)
        {
            try
            {
                Session.EndWrite(result);
                string elementsTransferredTextBoxText = ((int)result.AsyncState).ToString();
                string lastIOStatusTextBoxText = Session.LastStatus.ToString();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public void ReadAsync()
        {
            try
            {
                lock (Session)
                {
                    AsyncHandle = Session.BeginRead(
                    Session.DefaultBufferSize,
                    new AsyncCallback(OnReadComplete),
                    null);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void OnReadComplete(IAsyncResult result)
        {
            try
            {
                string responseString = Session.EndReadString(result);
                string elementsTransferredTextBoxText = responseString.Length.ToString();
                string lastIOStatusTextBoxText = Session.LastStatus.ToString();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public void TerminateAsync()
        {
            try
            {
                if (AsyncHandle is IAsyncResult res)
                    Session.Terminate(res);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        public double GetNumber(string cmd) => double.Parse(Query(cmd).Trim());

        public void SyncWait() => Write("INIT;*WAI\n");

        public bool IsReady => Query("*OPC?\n").Trim() == "1";

        public void Reset() => Write("*RST\n");

        public static string[] FindResources() => ResourceManager.GetLocalManager().FindResources("?*");

        private static string ReplaceCommonEscapeSequences(string s) => s.Replace("\\n", "\n").Replace("\\r", "\r");

        private IAsyncResult AsyncHandle { get; set; } = null;

        public override string ToString() => ResourceName + " | " + VendorName + " | " + Model + " | " + SerialNumber + " | " + DeviceVersion;

        public bool Equals(VisaClient other) => ResourceName == other.ResourceName;

        public override int GetHashCode() => ResourceName.GetHashCode();
    }
}
