namespace xofz.UI
{
    using System;

    public interface LoginUi : PopupUi
    {
        event Action BackspaceKeyTapped;

        event Action LoginKeyTapped;

        event Action CancelKeyTapped;

        AccessLevel CurrentAccessLevel { get; set; }

        string CurrentPassword { get; set; }

        string TimeRemaining { get; set; }

        void FocusPassword();
    }
}
