using Personaltool.Application.DataTransferObjects.Position.Commands;
using Personaltool.Application.DataTransferObjects.Position.Queries;

namespace Personaltool.Application.Interfaces
{
    /// <summary>
    /// Provides commands and queries for positions
    /// </summary>
    public interface IPositionService
    {
        // Queries
        public PositionOverviewQueryDto GetOverview();
        public PositionListQueryDto GetList();
        public PositionDetailsQueryDto GetDetails(int id);
        public PositionHistoryQueryDto GetHistory(int id);

        // Commands
        public PositionDetailsQueryDto Create(CreatePositionCommandDto dto);
        public void Edit(EditPositionCommandDto dto);
        public void Deactivate(DeactivatePositionCommandDto dto);
        public void Reactivate(ReactivatePositionCommandDto dto);
        public void Assign(AssignPositionCommandDto dto);
        public void Dismiss(DismissPositionCommandDto dto);
        public void Reassign(ReassignPositionCommandDto dto);
    }
}
