using System;
using System.Collections;
using System.Collections.Generic;

namespace CW_ThoughtsOutLoud
{
	// Класс узла
	public class ListNode<TData>
	{
		// Ссылка на следующий узел списка
		public ListNode<TData> Next;
		// Значение узла
		public TData Data;

		// Конструктор узла по значение
		// Формальные параметры: значение data
		// Входные данные: пусто
		// Выходные данные: узел списка
		public ListNode(TData data)
		{
			Data = data;
		}
	}

	// Класс односвязного списка
	public class SingleLinkedList<TData> : IEnumerable<TData>
	{
		// Голова списка
		private ListNode<TData> head;
		// Количество узлов в списке
		public int Count { get; private set; } = 0;

		// Очищает список
		// Формальные параметры: список
		// Входные данные: пусто
		// Выходные данные: пустой список
		public void Clear() => head = null;

		// Возвращает строку, содержащую значения узлов списка по порядку
		// Формальные параметры: список
		// Входные данные: пусто
		// Выходные данные: список, строка со значениями узлов
		public string Info()
		{
			string result = string.Empty;
			ListNode<TData> current = head;
			while (current != null)
			{
				result += current.Data + " -> ";
				current = current.Next;
			}
			result += "null";

			return result;
		}

		// Возвращает строку, содержащую значения узлов списка на отдельных строках
		// Формальные параметры: список
		// Входные данные: пусто
		// Выходные данные: список, строка со значениями узлов
		public string ElementsInfo()
		{
			string result = string.Empty;
			ListNode<TData> current = head;
			while (current != null)
			{
				result += $"{current.Data}\n";
				current = current.Next;
			}

			return result;
		}

		// Возвращает адрес нужного узла по значению
		// Формальные параметры: значение key
		// Входные данные: список
		// Выходные данные: список, узел списка
		public ListNode<TData> Find(TData key)
		{
			if (Count == 0)
				return null;

			ListNode<TData> current = head;
			while (!current.Data.Equals(key))
			{
				current = current.Next;
				if (current == null)
					return null;
			}

			return current;
		}

		// Возвращает адрес нужного узла по индексу
		public ListNode<TData> FindByIndex(int index)
		{
			if (index > Count || index < 0)
				return null;

			ListNode<TData> node = head;
			int k = 0;

			while (k < index)
			{
				k++;
				node = node.Next;
			}

			return node;
		}

		// Возвращает последний узел, содержащий значение key
		// Формальные параметры: значение key
		// Входные данные: список
		// Выходные данные: список, узел списка
		public ListNode<TData> FindLast(TData key)
		{
			ListNode<TData> result_node = null;

			for (ListNode<TData> node = head; node != null; node = node.Next)
			{
				if (node.Data.Equals(key))
					result_node = node;
			}

			return result_node;
		}

		// Возвращает адрес последнего узла (хвоста)
		// Формальные параметры: пусто
		// Входные данные: список
		// Выходные данные: список, хвост списка
		private ListNode<TData> FindTail()
		{
			if (Count == 0)
			{
				return null;
			}

			ListNode<TData> current = head;
			while (current.Next != null)
			{
				current = current.Next;
			}

			return current;
		}

		// Возвращает true, если в списке есть узел с переданным значением, иначе false
		// Формальные параметры: значение key
		// Входные данные: список
		// Выходные данные: список, true или false
		public bool Contains(TData key)
		{
			return (Find(key) != null);
		}

		// Добавление узла в конец списка
		// Формальные параметры: значение key
		// Входные данные: спсок
		// Выходные данные: список
		public void PushBack(TData key)
		{
			ListNode<TData> newNode = new ListNode<TData>(key);
			if (Count == 0)
			{
				head = newNode;
			}
			else
			{
				ListNode<TData> tail = FindTail();
				tail.Next = newNode;
			}

			Count++;
		}

		// Добавление узла в начало списка
		public void PushFront(TData item)
		{
			ListNode<TData> newNode = new ListNode<TData>(item);

			if (Count != 0)
			{
				newNode.Next = head;
			}

			head = newNode;
			Count++;
		}

		// Добавление узла после указанного узла
		public void AddAfter(ListNode<TData> node, TData item)
		{
			if (node == null)
				return; // ничего не делаем, если адрес пустой

			ListNode<TData> newNode = new ListNode<TData>(item);

			newNode.Next = node.Next;
			node.Next = newNode;

			Count++;
		}

		private void AddAfter(ListNode<TData> nodeAfter, ListNode<TData> node)
		{
			if (nodeAfter == null)
				return; // ничего не делаем, если адрес пустой

			node.Next = nodeAfter.Next;
			nodeAfter.Next = node;

			Count++;
		}

		// Добавление узла перед указанным узлом
		public void AddBefore(ListNode<TData> node, TData item)
		{
			if (node == null)
				return; // ничего не делаем, если адрес пустой

			ListNode<TData> newNode = new ListNode<TData>(item);

			if (head == node)
			{
				newNode.Next = head;
				head = newNode;
			}
			else
			{
				ListNode<TData> current = head;

				while (current.Next != node)
					current = current.Next;
				current.Next = newNode;
				newNode.Next = node;
			}

			Count++;
		}

		// Добавляет элементы переданного массива в конец
		public void PushBackRange(TData[] array)
		{
			ListNode<TData> tail = FindTail();

			for (int i = 0; i < array.Length; i++)
			{
				ListNode<TData> newNode = new ListNode<TData>(array[i]);
				if (Count == 0)
					head = newNode;
				else
					tail.Next = newNode;
				tail = newNode;

				Count++;
			}
		}

		// Удаление первого узла
		// Формальные параметры: пусто
		// Входные данные: список
		// Выходные данные: список
		public void RemoveFirst()
		{
			if (Count == 0)
			{
				throw new Exception("Список пуст"); // Вызывает исключение
			}

			head = head.Next;
			Count--;
		}

		// Удаление последнего узла
		// Формальные параметры: пусто
		// Входные данные: список
		// Выходные данные: список
		public void RemoveLast()
		{
			if (Count == 0)
			{
				throw new Exception("Список пуст");
			}

			if (Count == 1)
			{
				head = null;
			}
			else
			{
				ListNode<TData> current = head;
				while (current.Next.Next != null)
				{
					current = current.Next;
				}

				current.Next = null;
			}

			Count--;
		}

		// Удаление произвольного узла
		// Формальные параметры: узел node
		// Входные данные: список
		// Выходные данные: список
		public void RemoveNode(ListNode<TData> node)
		{
			if (head == node)
			{
				head = node.Next;
			}
			else
			{
				ListNode<TData> current = head;
				while (current.Next != node)
				{
					current = current.Next;
				}

				current.Next = node.Next;
			}

			Count--;
		}

		// Возвращает true при удалении первого вхождения указанного элемента, иначе false
		// Формальные параметры: значение key
		// Входные данные: список
		// Выходные данные: список, true или false
		public bool Remove(TData key)
		{
			if (Count == 0)
				throw new Exception("Список пуст");
			if (Count == 1)
			{
				if (head.Data.Equals(key))
				{
					head = null;
					Count--;
					return true;
				}
				else
					return false;
			}
			else
			{
				ListNode<TData> current = head;

				if (head.Data.Equals(key))
				{
					head = head.Next;
					Count--;
					return true;
				}

				while (current.Next != null)
				{
					if (current.Next.Data.Equals(key))
					{
						current.Next = current.Next.Next;
						Count--;
						return true;
					}
					current = current.Next;
				}
				return false;
			}
		}

		// Возвращает true при удалении последнего вхождения указанного элемента, иначе false
		public bool RemoveLast(TData item)
		{
			if (Count == 0)
				throw new Exception("Список пуст");

			if (Count == 1 && !head.Data.Equals(item))
				return false;

			ListNode<TData> current = head;
			ListNode<TData> node = null;

			while (current.Next != null)
			{
				if (current.Next.Data.Equals(item))
					node = current;
				current = current.Next;
			}

			if (head.Data.Equals(item) && node == null)
			{
				head = head.Next;
				Count--;
				return true;
			}

			if (node != null)
			{
				node.Next = node.Next.Next;
				Count--;
				return true;
			}

			return false;
		}

		// Удаляет все элементы, равные переданному элементу и возвращает количество этих элементов
		public int RemoveAll(TData item)
		{
			int k = Count; // Количество элементов в переданном списке
			ListNode<TData> current = head;

			if (k > 2)
			{
				while (current.Next != null)
				{
					if (current.Next.Data.Equals(item))
					{
						current.Next = current.Next.Next;
						Count--;
					}
					else
						current = current.Next;
				}
			}

			if (head.Data.Equals(item))
			{
				head = head.Next;
				Count--;
			}

			return k - Count;
		}

		// Переворачивает все элементы в обратном порядке
		public void Reverse()
		{
			if (Count <= 1)
				return;

			ListNode<TData> prev = null;
			ListNode<TData> current = head;
			ListNode<TData> next = current.Next;

			while (next != null)
			{
				next = current.Next;
				current.Next = prev;
				prev = current;
				current = next;
			}

			head = prev;
		}

		// Реализация foreach
		IEnumerator<TData> IEnumerable<TData>.GetEnumerator()
		{
			ListNode<TData> pointer = head;
			while (pointer != null)
			{
				yield return pointer.Data;
				pointer = pointer.Next;
			}
		}

		// Реализация foreach
		IEnumerator IEnumerable.GetEnumerator()
		{
			ListNode<TData> pointer = head;
			while (pointer != null)
			{
				yield return pointer.Data;
				pointer = pointer.Next;
			}
		}
	}
}
