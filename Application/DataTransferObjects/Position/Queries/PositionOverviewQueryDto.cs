using Personaltool.Application.DataTransferObjects.Position.Common;
using System.Collections.Generic;

namespace Personaltool.Application.DataTransferObjects.Position.Queries
{
    public class PositionOverviewQueryDto
    {
        public PositionOverviewQueryDto()
        {
            Positions = new HashSet<PositionDto>();
        }

        public IEnumerable<PositionDto> Positions { get; set; }
    }
}
