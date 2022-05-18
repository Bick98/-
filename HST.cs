using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CW_ThoughtsOutLoud
{

    // Класс хеш-таблицы
    public class HST
    {
        static int size_h = 10;
        // Размер таблицы
        public LSR[] h_table;  // Массив списков (цепочек)

        public int ComparisonsNumber { get; private set; }

        // Конструктор создает таблицу (массив списков)
        // Формальные параметры: отсутствуют
        // Входные данные: отсутствуют
        // Выходные данные: таблица
        public HST()
        {
            h_table = new LSR[size_h]; // Создаём массив списков
            for (int i = 0; i < size_h; i++) h_table[i] = new LSR();
        }
   

        // Вывод информации об элементах хеш-таблицы в массив строк************
        // Формальные параметры: пусто
        // Входные данные: хеш-таблица
        // Выходные данные: хеш-таблица, массив строк
        public string[] Info()
        {
            
            string[] result = new string[size_h*size_h];
            int k = 0;


            for (int i = 0; i < size_h; i++)
            {
                LSR.Node Tmp = h_table[i].pBegin;
                if (h_table[i].pBegin != null)
                {
                    
                    //if (!h_table[i].)
                    //{

                    do
                        {
                            result[k] = $"Значение: {Tmp.data.Color}\n" +
                                $"Ключ: {Tmp.data.Category,-14} ({ConvertToNumber(Tmp.data)})\n" +
                                //$"Индекс: {i}\n" +
                                $"Хеш: {Hash_func(Tmp.data)}\n";
                            Tmp = Tmp.pNext;
                            k++;
                        }
                        while (Tmp != h_table[i].pBegin);


                    //}
                    //else
                    //{
                    //    do
                    //    {
                    //        result[k] = $"Удалено!\n/Значение: {Tmp.data.Color}\n" +
                    //        $"/Ключ: {Tmp.data.Category,-14} ({ConvertToNumber(Tmp.data)})\n" +
                    //        //$"/Индекс: {i}/\n" +
                    //        $"/Хеш: {Hash_func(Tmp.data)}\n";
                    //        k++;
                    //    }
                    //    while (Tmp != h_table[i].pBegin);
                    //}
                    ////k++;
                }
            }

            return result;
        }

        // Вывод информации об элементах хеш-таблицы в строку************************
        // с разделителями для работы в проекте
        // Формальные параметры: пусто
        // Входные данные: хеш-таблица
        // Выходные данные: хеш-таблица, строка
        public string InfoToFile()
        {
            string result = string.Empty;

            for (int i = 0; i < size_h; i++)
            {
                LSR.Node Temp = h_table[i].pBegin;
                if (h_table[i].pBegin != null)
                {
                   do 
                   { 
                        result += $"{Temp.data.Color,-50} |{Temp.data.Category}\n";
                        Temp = Temp.pNext;

                   } while (Temp != h_table[i].pBegin) ;
                }

            }

            return result;
        }



        // Преобразование ключа в число типа double
        // Формальные параметры: ключ key
        // Входные данные: хеш-таблица
        // Выходные данные: вещественное число - представление ключа
        private double ConvertToNumber(CC key)
        {
            double result = 0;
            string keyString = key.Category.ToString();


            foreach (char symbol in keyString)
                result += symbol;


            return result;
        }

        // Вычисление середины квадрата
        // Формальные параметры: ключ key
        // Входные данные: хеш-таблица
        // Выходные данные: целое число - хеш для ключа
        private int Hash_func(CC key)
        {
            return ((int)(ConvertToNumber(key) * ConvertToNumber(key) % Math.Pow(10, Convert.ToString(ConvertToNumber(key) * ConvertToNumber(key)).Length - 1) / size_h) % size_h);
        }


        // Процедура добавления элемента в таблицу
        // Формальные параметры:  num (ключ)
        // Входные данные: ключ, хеш-таблица
        // Выходные данные: хеш-таблица
        public bool Add(CC key)
        {
           

            if (h_table[Hash_func(key)].Search(key) != null);
            return h_table[Hash_func(key)].Add(key); // Добавляем число *********
        }

        // Процедура удаления элемента из таблицы
        // Формальные параметры: num (ключ)
        // Входные данные: ключ, хеш-таблица
        // Выходные данные: хеш-таблица
        public void Delete(CC key)
        {
            CC key1 = new CC("Категория1", "Красный");

            if (h_table[Hash_func(key)] != null)
            {
                h_table[Hash_func(key)].Delete(key);
                //h_table[Hash_func(key)].pBegin. = true;
            }// Если ячейка таблицы не пуста, удаляем из списка
        }

        // Процедура поиска элемента в таблице
        // Формальные параметры: num (ключ)
        // Входные данные: ключ, хеш-таблица
        // Выходные данные: сообщение о наличии или отсутствии ключа в таблице
        public LSR.Node SearchInList(CC key) //**************
        {

            LSR.Node Tmp = h_table[Hash_func(key)].Search(key);
            if (Tmp == null)
            {
                //Debug.WriteLine(key + " isn't in the hash-table");
                return null;
            }
            //Debug.WriteLine(key + " hash-index: " + Hash_func(key));
            return Tmp;
        }

        //Процедура поиска элемента в таблице
        //Формальные параметры: num(ключ)
        // Входные данные: ключ, хеш-таблица
        // Выходные данные: сообщение о наличии или отсутствии ключа в таблице
        //public LSR.Node Search(string key) //**************
        //{
        //    CC key1 = new CC(key);
        //    LSR.Node Tmp = h_table[Hash_func(key)].Search(key);
        //    if (Tmp == null)
        //    {
        //        Debug.WriteLine(key + " isn't in the hash-table");
        //        return null;
        //    }
        //    Debug.WriteLine(key + " hash-index: " + Hash_func(key));
        //    return Tmp;
        //}

        public int Compress(CC key)
        {
            return h_table[Hash_func(key)].SearchCount(key);
        }

        // Процедура очищает цепочки хеш-таблицы
        // Формальные параметры: отсутствуют
        // Входные данные: хеш-таблица
        // Выходные данные: хеш-таблица
        public void ClearTable()
        {
            for (int i = 0; i < size_h; i++) h_table[i].Clear();
        }

        // Процедура выводит на экран хеш-таблицу
        // Формальные параметры: отсутствуют
        // Входные данные: хеш-таблица
        // Выходные данные: содержание таблицы
        public void PrintList()
        {
            Debug.WriteLine(" ");
            for (int i = 0; i < size_h; i++)

            {
                if (h_table[i] != null)
                {
                    Debug.WriteLine("Hash-index: " + i + "  num: ");
                    h_table[i].Print();
                }
                else
                {
                    Debug.WriteLine("Hash-index: " + i + " ");
                }
            }
            Debug.WriteLine(" ");
        }
    }
}