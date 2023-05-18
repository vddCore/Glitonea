namespace Glitonea.Utilities
{
    public record EnumDescription
    {
        public object Value { get; }
        
        public string? Description { get; set; }
        public string? Hint { get; set; }

        public EnumDescription(object value)
        {
            Value = value;
        }
        
        public override string ToString()
        {
            return Description ?? "<none>";
        }
    }
}