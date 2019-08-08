using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam
{
    public class Coordinate
    {
        public int X, Y;
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class Segment
    {
        public Coordinate Start, End;
        public Segment(Coordinate s, Coordinate e)
        {
            Start = s;
            End = e;
        }
        
    }
    interface IProperty
    {
        bool GetProperty(Segment Segment1, Segment Segment2, out string name);
    }

    class Perpendicular : IProperty
    {
        Alignment line = new Alignment();
        Utility util = new Utility();
        public bool GetProperty(Segment Segment1, Segment Segment2, out string name)
        {
             
            if ((line.IsParallelToX(Segment1) && line.IsParallelToY(Segment2)) || (line.IsParallelToY(Segment1) && line.IsParallelToX(Segment2)))
            {
                name = GetIntersection(Segment1, Segment2);
                return true;
            }
            name = "NO";
            return false;
        }

        private string GetIntersection(Segment segment1, Segment segment2)
        {
            if(line.IsParallelToX(segment1))
            {
                if (util.GetMinX(segment1) <= segment2.Start.X && util.GetMaxX(segment1) >= segment2.Start.X
                    && segment1.Start.Y >= util.GetMinY(segment2) && segment1.End.Y <= util.GetMaxY(segment2))
                    return "POINT";
                return "NO";
            }
            
             return GetIntersection(segment2, segment1);          
        }
    }

    class Parallel : IProperty
    { 
        public bool GetProperty(Segment Segment1, Segment Segment2, out string name)
        {
            var line = new Alignment();
            
            if ( (line.IsParallelToX(Segment1) && line.IsParallelToX(Segment2) ) || (line.IsParallelToY(Segment1) && line.IsParallelToY(Segment2)) )
            {
                name = GetIntersection(Segment1, Segment2);
                return true;
            }
            name = "NO";
            return false;  
        }

        private string GetIntersection(Segment segment1, Segment segment2)
        {
            Utility u = new Utility();
            Alignment line = new Alignment();
            if (line.IsParallelToX(segment1))   // if parallel to x-axis 
            {   //overlapping
                if(segment1.Start.Y == segment2.Start.Y)
                {
                    if (u.GetMaxX(segment1) == u.GetMinX(segment2) || u.GetMaxX(segment2) == u.GetMinX(segment1))
                    {
                        return "POINT";
                    }
                    else if ((u.GetMinX(segment1) <= u.GetMinX(segment2) && u.GetMaxX(segment1) >= u.GetMinX(segment2))
                             || (u.GetMinX(segment2) <= u.GetMinX(segment1) && u.GetMaxX(segment2) >= u.GetMinX(segment1)))
                    {
                        return "SEGMENT";
                    }
                }
                return "NO";
            }
            else
            {

                // case overlapping
                if(segment1.Start.X == segment2.Start.X)
                {
                    if (u.GetMaxY(segment1) == u.GetMinY(segment2) || u.GetMaxY(segment2) == u.GetMinY(segment1))
                    {
                        return "POINT";
                    }
                    else if ((u.GetMaxY(segment1) >= u.GetMinY(segment2) && u.GetMinY(segment1) <= u.GetMinY(segment2) ) 
                        || (u.GetMaxY(segment2) >= u.GetMinY(segment1) && u.GetMinY(segment2) <= u.GetMinY(segment1)))
                    {
                        return "SEGMENT";
                    }

                }

                return "NO";
            }
        }
    }

    class PointLine
    {
        public string GetIntersection(Segment point, Segment segment)
        {
            
            var util = new Utility();

            if (util.GetMaxX(segment) >= point.Start.X && util.GetMinX(segment) <= point.Start.X
                && util.GetMaxY(segment) >= point.Start.Y && util.GetMinY(segment) <= point.Start.Y)
                return "POINT";
            return "NO";
        }
    }
    class Utility
    {
        public int GetMinX(Segment segment)
        {
            return Math.Min(segment.Start.X, segment.End.X);
        }

        public int GetMaxX(Segment segment)
        {
            return Math.Max(segment.Start.X, segment.End.X);
        }

        public int GetMaxY(Segment segment)
        {
            return Math.Max(segment.Start.Y, segment.End.Y);
        }
        public int GetMinY(Segment segment)
        {
            return Math.Min(segment.Start.Y, segment.End.Y);
        }
    }

    class Alignment
    {
        public bool IsParallelToX(Segment segment)
        {
            return segment.Start.Y == segment.End.Y;
        }
        public bool IsParallelToY(Segment segment)
        {
            return segment.Start.X == segment.End.X;
        }
    }
    class Point
    {
        public bool Ispoint(Segment segment)
        {
            return (segment.Start.X == segment.End.X && segment.Start.Y == segment.End.Y);
        }

        public bool IsSamePoint(Segment segment1, Segment segment2)
        {
            return (segment1.Start.X == segment2.Start.X && segment1.End.X == segment2.End.X
                   && segment1.Start.Y == segment2.Start.Y && segment2.End.Y == segment1.End.Y);
        }
    }
    class NoPointSegment
    {
        public string Intersection(int[] seg1, int[] seg2)
        {
            var Line1 = new Segment(new Coordinate(seg1[0], seg1[1]), new Coordinate(seg1[2], seg1[3]));
            var Line2 = new Segment(new Coordinate(seg2[0], seg2[1]), new Coordinate(seg2[2], seg2[3]));
            var point = new Point();
            // if both represent point, return no explicitly.

            if(point.Ispoint(Line1) && point.Ispoint(Line2))
            {
                if (point.IsSamePoint(Line1, Line2)) return "POINT";
                return "NO";
            }
            else if(point.Ispoint(Line1) || point.Ispoint(Line2))
            {
                var pointLine = new PointLine();
                if (point.Ispoint(Line1)) return pointLine.GetIntersection(Line1, Line2);
                return pointLine.GetIntersection(Line2, Line1);
            }
            
            Parallel parallel = new Parallel();
            Perpendicular perpendicular = new Perpendicular();
            string name = string.Empty;

           
            if (parallel.GetProperty(Line1, Line2, out name)) return name;
            else if (perpendicular.GetProperty(Line1, Line2, out name)) return name;

            return "NO";

        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            string input = Console.ReadLine();
            NoPointSegment solver = new NoPointSegment();
            do
            {
                var segments = input.Split('|');
                var segParts = segments[0].Split(',');
                var seg1 = new int[4] { int.Parse(segParts[0]), int.Parse(segParts[1]), int.Parse(segParts[2]), int.Parse(segParts[3]) };
                segParts = segments[1].Split(',');
                var seg2 = new int[4] { int.Parse(segParts[0]), int.Parse(segParts[1]), int.Parse(segParts[2]), int.Parse(segParts[3]) };
                Console.WriteLine(solver.Intersection(seg1, seg2));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}
