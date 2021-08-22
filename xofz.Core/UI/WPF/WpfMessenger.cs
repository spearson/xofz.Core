namespace xofz.UI.WPF
{
    using System.Windows;

    public sealed class WpfMessenger
        : Messenger
    {
        Ui Messenger.Subscriber { get; set; }

        string Messenger.InfoCaption { get; set; }
            = string.Empty;

        string Messenger.WarningCaption { get; set; }
            = @"Warning";

        string Messenger.ErrorCaption { get; set; }
            = @"Error";

        string Messenger.QuestionCaption { get; set; }
            = @"?";

        Response Messenger.Question(
            string question)
        {
            Messenger m = this;
            MessageBoxResult result;
            if (m.Subscriber is Window subscriber)
            {
                result = MessageBox.Show(
                    subscriber,
                    question,
                    m.QuestionCaption,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                goto handleResult;
            }

            result = MessageBox.Show(
                question,
                m.QuestionCaption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

        handleResult:
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return Response.Yes;
                default:
                    return Response.No;
            }
        }

        Response Messenger.QuestionWithCancel(
            string question)
        {
            Messenger m = this;
            MessageBoxResult result;
            if (m.Subscriber is Window subscriber)
            {
                result = MessageBox.Show(
                    subscriber,
                    question,
                    m.QuestionCaption,
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);

                goto handleResult;
            }

            result = MessageBox.Show(
                question,
                m.QuestionCaption,
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

        handleResult:
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return Response.Yes;
                case MessageBoxResult.No:
                    return Response.No;
                case MessageBoxResult.Cancel:
                    return Response.Cancel;
                default:
                    return Response.Cancel;
            }
        }

        Response Messenger.QuestionOKCancel(
            string question)
        {
            Messenger m = this;
            MessageBoxResult result;
            if (m.Subscriber is Window subscriber)
            {
                result = MessageBox.Show(
                    subscriber,
                    question,
                    m.QuestionCaption,
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question);

                goto handleResult;
            }

            result = MessageBox.Show(
                question,
                m.QuestionCaption,
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question);

        handleResult:
            switch (result)
            {
                case MessageBoxResult.OK:
                    return Response.OK;
                case MessageBoxResult.Cancel:
                    return Response.Cancel;
                default:
                    return Response.Cancel;
            }
        }

        void Messenger.Inform(
            string message)
        {
            this.sendMessage(
                message,
                MessageBoxImage.Information);
        }

        void Messenger.Warn(
            string warning)
        {
            this.sendMessage(
                warning,
                MessageBoxImage.Warning);
        }

        void Messenger.GiveError(
            string error)
        {
            this.sendMessage(
                error,
                MessageBoxImage.Error);
        }

        private void sendMessage(
            string message,
            MessageBoxImage icon)
        {
            string caption;
            Messenger m = this;
            switch (icon)
            {
                case MessageBoxImage.Warning:
                    caption = m.WarningCaption;
                    break;
                case MessageBoxImage.Error:
                    caption = m.ErrorCaption;
                    break;
                case MessageBoxImage.Information:
                    caption = m.InfoCaption;
                    break;
                default:
                    caption = string.Empty;
                    break;
            }

            
            if (m.Subscriber is Window subscriber)
            {
                MessageBox.Show(
                    subscriber,
                    message,
                    caption,
                    MessageBoxButton.OK,
                    icon);
                return;
            }

            MessageBox.Show(
                message,
                caption,
                MessageBoxButton.OK,
                icon);
        }
    }
}
