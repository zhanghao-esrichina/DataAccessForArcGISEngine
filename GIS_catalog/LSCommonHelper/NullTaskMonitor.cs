using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace LSCommonHelper
{
    public class NullTaskMonitor : TaskMonitor
    {
        // Fields
        private bool m_canCanceld = true;
        private bool m_canceld = false;

        // Methods
        public void EnterWaitState()
        {
            this.EnterWaitState(false);
        }

        public void EnterWaitState(bool pUnlimited)
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public void ExitWaitState()
        {
            Cursor.Current = Cursors.Default;
        }

        public void PutTaskInfo(string paramCaption, int paramProgress)
        {
            Application.DoEvents();
        }

        // Properties
        public bool CanCanceld
        {
            get
            {
                return this.m_canCanceld;
            }
            set
            {
                this.m_canCanceld = value;
            }
        }

        public bool Canceld
        {
            get
            {
                return this.m_canceld;
            }
            set
            {
                this.m_canceld = value;
            }
        }

        public string TaskCaption
        {
            get
            {
                return "";
            }
            set
            {
                Application.DoEvents();
            }
        }

        public int TaskProgress
        {
            get
            {
                return 0;
            }
            set
            {
                Application.DoEvents();
            }
        }
    }
    public interface TaskMonitor
    {
        // Methods
        void EnterWaitState();
        void EnterWaitState(bool pUnlimited);
        void ExitWaitState();
        void PutTaskInfo(string paramCaption, int paramProgress);

        // Properties
        bool CanCanceld { get; set; }
        bool Canceld { get; set; }
        string TaskCaption { get; set; }
        int TaskProgress { get; set; }
    }

 



}
