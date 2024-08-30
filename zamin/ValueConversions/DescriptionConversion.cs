// Global using directives

using SSF.EFCore.zamin.asl.ValueObjects;

namespace SSF.EFCore.zamin.ValueConversions
{
    public class DescriptionConversion : ValueConverter<Description, string>
    {
        public DescriptionConversion() : base(c => c.Value, c => Description.FromString(c))
        {

        }
    }
}
