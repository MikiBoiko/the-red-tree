namespace NPLTV.Console
{
    public class Command 
    {
        public readonly string name, shortcut, details;
        public readonly bool accessible;

        protected static Command[] __availableCommands = {
            new NoneCommand(),
            new ClearCommand(),
            new HelpCommand(),
            new CloseCommand()
        };

        public Command(string name, string shortcut, string details, bool accessible)
        {
            this.name = name;
            this.shortcut = shortcut;
            this.details = details;
            this.accessible = accessible;
        }

        public static void RunConsoleCommand(string command)
        {
            string _lowerCommand = command.ToLower();
            bool _found = false;
            foreach(Command c in __availableCommands)
            {
                if(c.Parse(_lowerCommand.Split(' ')))
                {
                    _found = true;
                    break;
                }
            }

            if(!_found)
            {
                ConsoleController.PrintLine($"Command '{ command }' not found.");
            }
        }

        public virtual bool Parse(string[] arg)
        {
            if(arg[0] == name || arg[0] == shortcut)
            {
                this.ExecuteCommand();
                return true;
            }
            return false;
        }

        public virtual void ExecuteCommand()
        {
            ConsoleController.PrintLine($"Executing command [{ name }]");
        }

        public string GetHelp()
        {
            string _showDetails = accessible ? details : "Not accessible";
            return $"{ shortcut }, { name }: { _showDetails }";
        }
    }

    public class NoneCommand : Command
    {
        public NoneCommand() : base("none", "", "just prints an empty line", true) {  }

        public override void ExecuteCommand()
        {
            ConsoleController.NextLine();
        }
    }

    public class ClearCommand : Command
    {
        public ClearCommand() : base("clear", "c", "clear the console log", true) {  }

        public override void ExecuteCommand()
        {
            ConsoleController.Clear();
        }
    }

    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", "h", "prints all available commands", true) {  }

        public override void ExecuteCommand()
        {
            ConsoleController.PrintLine("All available commands:");
            foreach (Command _command in __availableCommands)
            {
                ConsoleController.PrintLine($"\t•\t{ _command.GetHelp() }");
            }
            ConsoleController.NextLine();
        }
    }

    public class CloseCommand : Command
    {
        public CloseCommand() : base("close", "x", "to close the console", true) {  }

        public override void ExecuteCommand()
        {
            ConsoleController.CloseConsole();
        }
    }
}