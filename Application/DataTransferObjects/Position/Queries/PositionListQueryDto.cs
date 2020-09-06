using Personaltool.Application.DataTransferObjects.Position.Common;
using System.Collections.Generic;

namespace Personaltool.Application.DataTransferObjects.Position.Queries
{
    public class PositionListQueryDto
    {
        public IEnumerable<PositionDto> Positions { get; set; }
    }
}
