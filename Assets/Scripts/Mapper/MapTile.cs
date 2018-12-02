using System;
using UnityEngine;

namespace Mapper {
    public class MapTile<T> {
        public T Value {
            get { return Map.GetTile(MapCoordinate); }
            set { Map.SetTile(MapCoordinate, value); }
        }

        public MapTile<T> Up {
            get {
                if (_up == null) {
                    _up = GetNeighbour(0, -1);
                }
                return _up;
            }
        }

        public MapTile<T> Right {
            get {
                if (_right == null) {
                    _right = GetNeighbour(0, -1);
                }
                return _right;
            }
        }


        public MapTile<T> Down {
            get {
                if (_down == null) {
                    _down = GetNeighbour(0, -1);
                }
                return _down;
            }
        }

        public MapTile<T> Left {
            get {
                if (_left == null) {
                    _left = GetNeighbour(0, -1);
                }
                return _left;
            }
        }
        public Map<T> Map {
            get;
            private set;
        }

        public Vector2Int MapCoordinate {
            get;
            private set;
        }

        private MapTile<T> _up;
        private MapTile<T> _right;
        private MapTile<T> _down;
        private MapTile<T> _left;

        public MapTile(Map<T> map, Vector2Int coordinate) {
            MapCoordinate = coordinate;
            Map = map;
        }

        public MapTile<T> GetNeighbour(int x, int y) {
            if (Map == null) {
                throw new Exception("Tried to get neighbour but no map is associated.");
            }

            var neighbourCoordinate = MapCoordinate + new Vector2Int(x, y);
            return new MapTile<T>(Map, neighbourCoordinate);
        }
    }
}