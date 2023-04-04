using Agava.Merge2.Core;
using System.Collections.Generic;

namespace Agava.Merge2UIView
{
    public interface IBoardSetup
    {
        IShape Shape { get; }
        IContourAlgorithm ContourAlgorithm { get; }
        
        IEnumerable<(MapCoordinate, (string, int))> Items();
        IEnumerable<MapCoordinate> OpenedPositions();
    }
}
