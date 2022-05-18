using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

namespace CW_ThoughtsOutLoud
{
   
    public class ChainForBinary 
    {
        public int size;   // Размер списка
        public Node pBegin, pTail; // Начало списка/хвост

        // Конструктор создает список (цепочку)
        // Формальные параметры: отсутствуют
        // Входные данные: отсутствуют
        // Выходные данные: список
        public ChainForBinary()
        {
            size = 0;
            pBegin = null;
            pTail = null;
        }
        public class Node  // Запись об элементe
        {
            public Node pNext;  // Следующий узел
            public App data;    // Ключ

            // Конструктор создает элемент списка
            // Формальные параметры: data - значение ключа
            // Входные данные: значение элемента и список
            // Выходные данные: список
            public Node(App data)
            {
                this.data = data;
                this.pNext = null;
            }
        }

        // Процедура создаёт элемент списка
        // Формальные параметры: key - значение ключа
        // Входные данные: значение ключа и список
        // Выходные данные: список
        public void Add(App key)
        {
            Node current = pBegin;
            Node new_node = new Node(key);

            // Case 1 of the above algo
            if (current == null)
            {
                new_node.pNext = new_node;
                pBegin = new_node;
                size++;
            }

            // Case 2 of the above algo
            else if (String.Compare(current.data.Audio, new_node.data.Audio) >= 0)
            {

                /* If value is smaller than
                    head's value then we need
                    to change next of last node */
                while (current.pNext != pBegin)
                    current = current.pNext;

                current.pNext = new_node;
                new_node.pNext = pBegin;
                pBegin = new_node;
                size++;
            }

            // Case 3 of the above algo
            else
            {

                /* Locate the node before
                the point of insertion */
                while (current.pNext != pBegin && String.Compare(current.pNext.data.Audio, new_node.data.Audio) < 0)
                    current = current.pNext;

                new_node.pNext = current.pNext;
                current.pNext = new_node;
                size++;
            }
        }


        // Процедура удаляет элемент списка
        // Формальные параметры: key - значение ключа
        // Входные данные: значение ключа и список
        // Выходные данные: список
        public void Delete(App key)
        {
            Node Temp = pBegin;
            bool is_deleted = false;
            Node For_search2 = pBegin;

            while ((is_deleted == false) && (pBegin != null) && (Temp.pNext != pBegin)) // Ищем, пока не пусто, не нашли и не конец
            {
                if (Temp.data != key) // Нашли 
                {

                    Temp = Temp.pNext;
                }
                while (For_search2.pNext != Temp)
                {
                    For_search2 = For_search2.pNext;
                }
                if (Temp == pBegin) // Сдвиг метки начала, если нужен
                {
                    pBegin = Temp.pNext;
                    pTail.pNext = pBegin;
                    is_deleted = true;
                    Debug.WriteLine(key + " element was removed");
                    size--;
                }
                else if ((is_deleted == false) && (Temp.data == key))
                {
                    For_search2.pNext = Temp.pNext;
                    is_deleted = true;
                    Debug.WriteLine(key + " element was removed");
                    size--;
                }
            }
            if ((Temp.pNext == pBegin) && (is_deleted == false) && (Temp.data == key))
            {
                For_search2.pNext = Temp.pNext;
                is_deleted = true;
                Debug.WriteLine(key + " element was removed");
                size--;

            }
            else if ((pBegin != null) && (Temp.pNext == pBegin) && (pBegin == Temp) && (is_deleted == false) && (Temp.data == key))
            {
                pBegin = null;
                is_deleted = true;
                Debug.WriteLine(key + " element was removed");
                size--;
            }
            if (is_deleted == false)
            {
                Debug.WriteLine(key + " isn't in the hash-table");
            }
        }

        // Процедура очищает список
        // Формальные параметры: отсутствуют
        // Входные данные: список
        // Выходные данные: список
        public void Clear()
        {
            while (pBegin != null)
            {
                Delete(pBegin.data);
            }
        }

        // Процедура ищет элемент с указанным ключем
        // Формальные параметры: key - значение ключа
        // Входные данные: значение ключа и список
        // Выходные данные: искомый элемент или null
        public Node Search(App key)
        {
            Node Temp = pBegin;
            while ((pBegin != null) && (Temp.pNext != pBegin))
            {
                if (Temp.data.My.Category == key.My.Color) return Temp;
                Temp = Temp.pNext;
            }
            if ((pBegin != null) && (Temp.pNext == pBegin) && (Temp.data.My.Category == key.My.Category))
            {
                return Temp;
            }
            return null;
        }

        // Процедура выводит на экран все элементы списка
        // Формальные параметры: отсутствуют
        // Входные данные: список
        // Выходные данные: ключи элементов в цепочке 
        public void Print()
        {
            Node P = pBegin;
            for (int i = 0; i < size; i++)
            {
                Debug.Write(P.data.My.Category + "+" + P.data.Audio + " -> ");
                P = P.pNext;
            }
            Console.WriteLine();
        }
        //// Реализация foreach
        //IEnumerator IEnumerable<App>.GetEnumerator()
        //{
        //    Node pointer = pBegin;
        //    while (pointer != null)
        //    {
        //        yield return pointer.data;
        //        pointer = pointer.pNext;
        //    }
        //}

        //// Реализация foreach
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    Node pointer = pBegin;
        //    while (pointer != null)
        //    {
        //        yield return pointer.data;
        //        pointer = pointer.pNext;
        //    }
        //}
    }
}
