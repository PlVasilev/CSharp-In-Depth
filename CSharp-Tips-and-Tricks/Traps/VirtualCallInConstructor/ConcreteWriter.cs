namespace VirtualCallInConstructor
{
    using System.Text;

    public class ConcreteWriter : BaseWriter
    {
        private readonly StringBuilder stringBuilder;

        public ConcreteWriter()
        {
            this.stringBuilder = new StringBuilder();
        }

        protected override void WriteHeader()
        {
            this.stringBuilder.AppendLine("--------------");
        }
    }
}
