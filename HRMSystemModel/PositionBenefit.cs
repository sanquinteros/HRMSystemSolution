namespace HRMSystemModel
{
    public class PositionBenefit
    {
        public int PositionBenefitId { get; set; }
        public int PositionId { get; set; }
        public int BenefitTypeId { get; set; }
        public decimal Value { get; set; }

        public override string ToString()
        {
            return $"PositionBenefits[PositionBenefitId='{PositionBenefitId}', PositionId='{PositionId}', BenefitTypeId='{BenefitTypeId}', Value='{Value}']";
        }
    }
}
