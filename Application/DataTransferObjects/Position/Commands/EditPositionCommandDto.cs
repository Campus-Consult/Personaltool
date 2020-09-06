namespace Personaltool.Application.DataTransferObjects.Position.Commands
{
    public class EditPositionCommandDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
