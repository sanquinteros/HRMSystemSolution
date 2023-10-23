namespace HRMSystemModel
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string TradingName { get; set; }
        public string Address { get; set; }
        public string CNPJ { get; set; }

        public override string ToString()
        {
            return $"Company[CompanyID='{CompanyID}', CompanyName='{CompanyName}', TradingName='{TradingName}', Address='{Address}', CNPJ='{CNPJ}']";
        }
    }
}
