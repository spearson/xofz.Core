namespace xofz.UI
{
    using System;

    public interface LoginUi : PopupUi
    {
        event Action BackspaceKeyTapped;

        event Action LoginKeyTapped;

        event Action CancelKeyTapped;

        event Action LogOutKeyTapped;

        AccessLevel CurrentAccessLevel { get; set; }

        string CurrentPassword { get; set; }

        string TimeRemaining { get; set; }

        bool LogOutKeyEnabled { get; set; }
    }
}
