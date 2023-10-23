namespace HRMSystemModel
{
    public class Position
    {
        public int PositionId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Positions[PositionId='{PositionId}', Name='{Name}']";
        }
    }
}
