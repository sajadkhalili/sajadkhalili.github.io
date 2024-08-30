using SSF.EFCore.zamin.asl.ValueObjects;

namespace SSF.EFCore.zamin.ValueConversions
{
    public class TitleConversion : ValueConverter<Title, string>
    {
        public TitleConversion() : base(c => c.Value, c => Title.FromString(c))
        {

        }
    }
}
