namespace HRMSystemModel
{
    public class BenefitType
    {
        public int BenefitTypeId { get; set; }
        public string TypeName { get; set; }

        public override string ToString()
        {
            return $"BenefitTypes[BenefitTypeId='{BenefitTypeId}', TypeName='{TypeName}']";
        }
    }
}
