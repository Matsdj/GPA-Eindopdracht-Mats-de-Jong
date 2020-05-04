using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPA_Eindopdracht_Mats_de_Jong
{
    class PathfindingAI
    {
        public Vector2[] findPath(MapV2 map, BaseEntity obj, BaseEntity targetObj)
        {
            Point startPoint = map.GridLoc(obj);
            Point endPoint = map.GridLoc(targetObj);
            int maxPathRadius = 20,
                pathLength = 0;
            Vector2[] path = new Vector2[0];
            //Check if player is in range
            if ((endPoint - startPoint).ToVector2().Length() < maxPathRadius)
            {
                //Initialize map
                int[,] gridMap = new int[map.Columns, map.Rows];
                //Create gridMap
                gridMap[startPoint.X, startPoint.Y] = 1;
                for (int i = 1; (i <= maxPathRadius) && (pathLength == 0); i++)
                {
                    //loop through grid
                    for (int x = Math.Max(0, startPoint.X - maxPathRadius); x < Math.Min(map.Columns, startPoint.X + maxPathRadius); x++)
                        for (int y = Math.Max(0, startPoint.Y - maxPathRadius); y < Math.Min(map.Rows, startPoint.Y + maxPathRadius); y++)
                        {
                            if (gridMap[x, y] == i)
                            {
                                //North
                                if (map.Get(x, y - 1) is Floor && gridMap[x, y - 1] == 0) gridMap[x, y - 1] = i + 1;
                                //West
                                if (map.Get(x - 1, y) is Floor && gridMap[x - 1, y] == 0) gridMap[x - 1, y] = i + 1;
                                //South
                                if (map.Get(x, y + 1) is Floor && gridMap[x, y + 1] == 0) gridMap[x, y + 1] = i + 1;
                                //East
                                if (map.Get(x + 1, y) is Floor && gridMap[x + 1, y] == 0) gridMap[x + 1, y] = i + 1;
                                //Path found?
                                if (x == endPoint.X && y == endPoint.Y) pathLength = i;
                            }
                        }
                }

                //Create shortest path
                Point pathSpot = endPoint;
                Point[] gridPath = new Point[pathLength];
                if (pathLength > 0)
                {
                    gridPath[pathLength - 1] = endPoint;
                    for (int dist = pathLength - 1; dist > 0; dist += 0)
                    {
                        //North West South East
                        if (pathSpot.Y > 0 && gridMap[pathSpot.X, pathSpot.Y - 1] <= dist && gridMap[pathSpot.X, pathSpot.Y - 1] != 0)
                        {
                            pathSpot.Y--;
                            dist--;
                            gridPath[dist] = pathSpot;
                        }
                        if (pathSpot.X > 0 && gridMap[pathSpot.X - 1, pathSpot.Y] <= dist && gridMap[pathSpot.X - 1, pathSpot.Y] != 0)
                        {
                            pathSpot.X--;
                            dist--;
                            gridPath[dist] = pathSpot;
                        }
                        if (pathSpot.Y + 1 < map.Rows && gridMap[pathSpot.X, pathSpot.Y + 1] <= dist && gridMap[pathSpot.X, pathSpot.Y + 1] != 0)
                        {
                            pathSpot.Y++;
                            dist--;
                            gridPath[dist] = pathSpot;
                        }
                        if (pathSpot.X + 1 < map.Columns && gridMap[pathSpot.X + 1, pathSpot.Y] <= dist && gridMap[pathSpot.X + 1, pathSpot.Y] != 0)
                        {
                            pathSpot.X++;
                            dist--;
                            gridPath[dist] = pathSpot;
                        }
                    }
                }
                path = new Vector2[gridPath.Length];
                for (int i = 0; i < path.Length; i++)
                {
                    path[i] = gridPath[i].ToVector2() * map.TileSize;
                }
            }
            //Return
            return path;
        }
    }
}
