namespace xofz.UI
{
    public interface Messenger
    {
        object Subscriber { get; set; }

        Response Question(string question);

        void Inform(string message);

        void Warn(string warning);

        void GiveError(string error);
    }
}
