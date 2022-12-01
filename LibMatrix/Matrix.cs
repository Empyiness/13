using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LibMatrix
{
    /// <summary>
    /// Класс для работы с двумерным массивом
    /// </summary>
    /// <typeparam name="T">Обобщенный тип</typeparam>
    public class Matrix<T>
    {
        private T[,] _items;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="rowCount">Количество строк</param>
        /// <param name="columnCount">Количество столбцов</param>
        /// <param name="extension">Расширение</param>
        public Matrix(int rowCount = 5, int columnCount = 4, string extension = ".matrix")
        {
            _items = new T[rowCount, columnCount];
            Rows = rowCount;
            Columns = columnCount;
            Extension = extension;
        }
        /// <summary>
        /// Индексаотор
        /// </summary>
        /// <param name="rowIndex">Индекс строки элемента</param>
        /// <param name="columnIndex">Индекс столбца элемента</param>
        /// <returns></returns>
        public T this[int rowIndex, int columnIndex]
        {
            get => _items[rowIndex, columnIndex];
            set => _items[rowIndex, columnIndex] = value;
        }
        /// <summary>
        /// Свойство количество строк
        /// </summary>
        public int Rows { get; private set; }
        /// <summary>
        /// Свойство количество столбцов
        /// </summary>
        public int Columns { get; private set; }
        /// <summary>
        /// Свойство расширение
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// Установка всем элементам матрицы значения типа по-умолчанию
        /// </summary>
        public void DefaultInit()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {   
                    _items[i, j] = default;
                }
            }
        }
        /// <summary>
        /// Сохранение матрицы
        /// </summary>
        /// <param name="path">Путь</param>
        public void Save(string path)
        {
            BinaryFormatter formatter = new();
            using(FileStream stream = new(path, FileMode.Create))
            {
                formatter.Serialize(stream, _items);
            }
        }
        /// <summary>
        /// Открытие матрицы
        /// </summary>
        /// <param name="path">Путь</param>
        public void Load(string path)
        {
            BinaryFormatter formatter = new();
            using (FileStream stream = new(path, FileMode.Open))
            {
                _items = formatter.Deserialize(stream) as T[,];
            }
        }
    }
    public static class VisualArray
    {
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("Num" + (i + 1), typeof(T));
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }
                res.Rows.Add(row);
            }
            return res;
        }

        public static DataTable ToDataTable<T>(this Matrix<T> matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.Columns; i++)
            {
                res.Columns.Add("Num" + (i + 1), typeof(T));
            }
            for (int i = 0; i < matrix.Rows; i++)
            {
                var row = res.NewRow();
                for (int j = 0; j < matrix.Columns; j++)
                {
                    row[j] = matrix[i, j];
                }
                res.Rows.Add(row);
            }
            return res;
        }
    }
}
