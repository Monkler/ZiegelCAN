namespace ZiegelCAN
{
    using System;
    using System.Windows;

    /// <summary>
    /// An generic exception dialog used to display fatal errors.
    /// Only use this dialog if the user really needs to be worried about what is going on.
    /// The rest of the GUI will be in freeze mode until the exception dialog will be closed.
    /// </summary>
    public partial class ExceptionDialog : Window
    {
        internal ExceptionDialog(string title, string message, Exception exception)
        {
            this.InitializeComponent();

            this.Title = title;
            this.MessageLabel.Text = message;
            this.ExceptionTextBox.Text = exception.ToString();
        }

        /// <summary>
        /// Opens the exception dialog as window and returns only when closed.
        /// </summary>
        /// <param name="exception">The exception which should be displayed</param>
        /// <param name="title">The title of the Window. Will be displayed in the title bar.</param>
        /// <param name="message">The message of the exception which has occured. 
        /// Will be displayed in the gray text field above the exception.</param>
        public static void ShowDialog(Exception exception, string title = "Unknown Exception occured!", string message = "Exception:")
        {
            ExceptionDialog dialog = new ExceptionDialog(title, message, exception);
            dialog.ShowDialog();
        }

        private void OnCopyToClipboardClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.MessageLabel.Text + "\n" + this.ExceptionTextBox.Text);
        }

        private void OnOKButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
