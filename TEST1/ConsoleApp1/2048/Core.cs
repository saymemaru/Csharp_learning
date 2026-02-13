using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    internal class Core
    {
        private int[] moveZeroArray;
        private int[] mergeArray;
        private int[,] map;
        public int[,] Map 
        {
            get {  return map; }
        }
        private List<GridPosition> emptyGrids;
        private int gridSize;
        private Random random;

        public Core(int gridSize)
        {
            this.gridSize = gridSize;

            map = new int[gridSize, gridSize];
            UnmovedMap = new int[gridSize, gridSize];
            moveZeroArray = new int[gridSize];
            mergeArray = new int[gridSize];
            emptyGrids = new List<GridPosition>(gridSize * gridSize);
            random = new();
        }
        private void MoveZeroToEnd()
        {
            Array.Clear(moveZeroArray,0, gridSize);

            int index = 0;
            for (int j = 0; j < mergeArray.Length; j++)
            {
                if (mergeArray[j] != 0)
                {
                    moveZeroArray[index++] = mergeArray[j];
                }
            }
            moveZeroArray.CopyTo(mergeArray,0);
        }

        //合并相邻相同的数，并把0移动到末尾
        private void CombineSameNumbers()
        {
            MoveZeroToEnd();

            for (int i = 0; i < mergeArray.Length - 1; i++)
            {
                if (mergeArray[i] == 0)
                    continue;
                if (mergeArray[i] == mergeArray[i + 1])
                {
                    mergeArray[i] += mergeArray[i + 1];
                    mergeArray[i + 1] = 0;
                }
            }

            MoveZeroToEnd();
        }

        //上移
        private void MoveUp()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    mergeArray[y] = map[x, y];
                }

                CombineSameNumbers();

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = mergeArray[y];
                }
            }
        }

        //下移，按倒序获取1d数组合并后，再倒序返回到2d数组
        private void MoveDown()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = map.GetLength(1) - 1; y >= 0; y--)
                {
                    mergeArray[gridSize - 1 - y] = map[x, y];
                }
                CombineSameNumbers();

                for (int y = 0; y <= map.GetLength(1) - 1; y++)
                {
                    map[x, y] = mergeArray[gridSize - 1 - y];
                }
            }
        }

        private void MoveLeft()
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    mergeArray[x] = map[x, y];
                }
                CombineSameNumbers();

                for (int x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y] = mergeArray[x];
                }
            }

        }

        private void MoveRight()
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = map.GetLength(1) - 1; x >= 0; x--)
                {
                    mergeArray[gridSize - 1 - x] = map[x, y];
                }

                CombineSameNumbers();

                for (int x = 0; x <= map.GetLength(1) - 1; x++)
                {
                    map[x, y] = mergeArray[gridSize - 1 - x];
                }
            }

        }

        private int[,] UnmovedMap;
        public bool IsMapChanged { get; set; }
        public void Move(MoveDirection direction)
        {
            Array.Copy(Map, UnmovedMap,Map.Length);
            IsMapChanged = false;

            switch (direction)
            {
                case (MoveDirection.Up):
                    MoveUp();
                    break;
                case (MoveDirection.Down):
                    MoveDown();
                    break;
                case (MoveDirection.Left):
                    MoveLeft();
                    break;
                case (MoveDirection.Right):
                    MoveRight();
                    break;
            }

            for (int y = 0;y < map.GetLength(1);y++)
            {
                for(int x = 0;x < map.GetLength(0) ;x++)
                {
                    if (Map[x,y] != UnmovedMap[x,y])
                    {
                        IsMapChanged = true;
                        return;
                    }
                }
            }

        }

        private void GetEmptyGrid()
        {
            emptyGrids.Clear();

            for(int x = 0; x < map.GetLength(0);x++)
            {
                for(int y = 0; y < map.GetLength(1);y++)
                {
                    if(map[x, y] == 0)
                    {
                        emptyGrids.Add(new GridPosition(x, y));
                    }
                }
            }
        }

        public void GeneragteNumber()
        {
            GetEmptyGrid();

            if(emptyGrids.Count > 0)
            { 
                int randomIndex = random.Next(0, emptyGrids.Count);
                GridPosition position = emptyGrids[randomIndex];
                map[position.X, position.Y] = random.Next(0, 10) <= 1 ? 4 : 2;
            }
        }

        public bool IsGameEnd()
        {
            foreach(int num in Map)
            {
                if (num == 0)
                    return false;
            }

            for(int x = 0; x < map.GetLength(0) - 1;x++)
            {
                for(int y = 0;y < map.GetLength(1) - 1;y++)
                {
                    if (map[x, y] == map[x, y + 1])
                        return false;
                }
            }

            for (int y = 0; y < map.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < map.GetLength(0) - 1; x++)
                {
                    if (map[x, y] == map[x + 1, y])
                        return false;
                }
            }

            return true;
        }


        public static void PrintDoubleArray(Array array)
        {
            Console.Clear();
            for (int y = 0; y < array.GetLength(1); y++)
            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    Console.Write(array.GetValue(x, y) + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

