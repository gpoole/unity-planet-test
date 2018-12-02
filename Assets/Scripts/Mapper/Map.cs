using System;
using UnityEngine;

namespace Mapper {
    public class Map<T> {
        public int Width {
            get;
            private set;
        }

        public int Height {
            get;
            private set;
        }

        private T[] _tiles;

        public Map(int width, int height, T[] tiles) {
            if (width * height != tiles.Length) {
                throw new ArgumentException($"Dimensions don't match provided tile set ({width * height } != {tiles.Length})");
            }
            Width = width;
            Height = height;
            _tiles = tiles;
        }

        public Map(int width, int height) : this(width, height, new T[width * height]) { }

        public T GetTile(int x, int y) {
            return _tiles[GetIndex(x, y)];
        }

        public T GetTile(Vector2Int coordinate) {
            return GetTile(coordinate.x, coordinate.y);
        }

        public void SetTile(int x, int y, T tile) {
            _tiles[GetIndex(x, y)] = tile;
        }

        public void SetTile(Vector2Int coordinate, T tile) {
            SetTile(coordinate.x, coordinate.y, tile);
        }

        private int GetIndex(int x, int y) {
            int index = (y * Width) + x;
            if (index > _tiles.Length) {
                throw new ArgumentException($"Requested coordinate out of bounds (x: {x}, y: {y}, width: {Width}, height: {Height})");
            }

            return index;
        }
    }
}