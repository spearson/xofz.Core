namespace xofz.Startup
{
    using System.Collections.Generic;
    using System.Linq;

    public class CommandExecutor
    {
        public CommandExecutor()
        {
            this.executedCommands = new List<Command>(0x100);
        }

        public virtual T Get<T>() where T : Command
        {
            return this.executedCommands.OfType<T>().FirstOrDefault();
        }

        public virtual CommandExecutor Execute(Command command)
        {
            command.Execute();
            this.executedCommands.Add(command);
            return this;
        }

        private readonly IList<Command> executedCommands;
    }
}
