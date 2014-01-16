namespace SnagitJiraOutputAccessory.Models
{
    using System;
    using System.Drawing;
    using System.Timers;
    using System.Windows.Forms;
    using SnagitJiraOutputAccessory.Commands;

    public class UploadCompleteNotification
    {
        private const int _duration = 5000;
        private NotifyIcon _notifyIcon = null;
        private ICommand _clickAction = null;

        public void Notify(string title, string body, ICommand onClickCommand)
        {
            _clickAction = onClickCommand;

            _notifyIcon = new NotifyIcon();
            _notifyIcon.Visible = true;
            _notifyIcon.Icon = SystemIcons.Information;
            _notifyIcon.BalloonTipTitle = title;
            _notifyIcon.BalloonTipText = body;
            _notifyIcon.ShowBalloonTip(_duration);
            _notifyIcon.BalloonTipClicked += onNotificationClicked;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += onTimerElapsed;
            timer.Interval = _duration;
            timer.Enabled = true;
        }

        private void onTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _notifyIcon.Dispose();
        }

        private void onNotificationClicked(object sender, EventArgs e)
        {
            if (_clickAction != null)
            {
                _clickAction.Execute();
            }
        }
    }
}
