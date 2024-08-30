using SSF.EFCore.zamin.asl.ValueObjects;

namespace SSF.EFCore.zamin.ValueConversions
{
    public class NationalCodeConversion : ValueConverter<NationalCode, string>
    {
        public NationalCodeConversion() : base(c => c.Value, c => NationalCode.FromString(c))
        {

        }
    }
}
