namespace Glitonea.Utilities
{
    public record EnumDescription
    {
        public object Value { get; set; }
        
        public string Description { get; set; }
        public string Hint { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}