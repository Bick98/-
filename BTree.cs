using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_ThoughtsOutLoud
{
	// Класс узла дерева
	public class BNode<TKey, TData> where TKey : IComparable
	{
		
		// Ссылка на узел слева
		public BNode<TKey, TData> left;
		// Ссылка на узел справа
		public BNode<TKey, TData> right;
		// Ссылка на узел, являющийся родителем
		public BNode<TKey, TData> parent;
		// Список значений узла
		public SingleLinkedList<TData> Data { get; } = new SingleLinkedList<TData>();
		// Уникальное значение узла - его ключ
		public TKey key;

		public BNode()
		{
			
		}

		// Конструктор узла по ключу
		// Формальные параметры: ключ
		// Входные данные: пусто
		// Выходные данные: узел с заданным ключом
		public BNode(TKey key)
		{
			this.key = key;
		}

		// Конструктор узла по ключу и значению
		// Формальные параметры: ключ, значение
		// Входные данные: пусто
		// Выходные данные: узел с заданным ключом и списком значений с переданным значением
		public BNode(TKey key, TData data)
		{
			this.key = key;
			Data.PushBack(data);
		}

		// Проверяет, меньше ли ключ первого узла, чем ключ второго узла
		// Формальные параметры: узел node2
		// Входные данные: 2 узла
		// Выходные данные: True или False
		public bool IsLess(BNode<TKey, TData> node2) => key.CompareTo(node2.key) < 0;

		// Проверяет, больше ли ключ первого узла, чем ключ второго узла
		// Формальные параметры: узел node2
		// Входные данные: 2 узла
		// Выходные данные: True или False
		public bool IsMore(BNode<TKey, TData> node2) => key.CompareTo(node2.key) > 0;

		// Проверяет, равны ли ключи первого и второго узлов
		// Формальные параметры: узел node2
		// Входные данные: 2 узла
		// Выходные данные: True или False
		public bool IsEqual(BNode<TKey, TData> node2) => key.CompareTo(node2.key) == 0;
	}



	class BTree<TKey, TData> where TKey : IComparable
	{
		// Узел-корень дерева
		private BNode<TKey, TData> root;
		// Пустой узел-лист дерева
		private readonly BNode<TKey, TData> nil;
		public BNode<TKey, TData> Nil => nil;
		// Счётчик сравнений для операции поиска по диапазону
		public int ComparisonsNumber { get; private set; }
		// Список количества сравнений при поиске по диапазону
		public SingleLinkedList<int> ComparisonsList { get; private set; }

		// Конструктор дерева
		// Формальные параметры: пусто
		// Входные данные: узлы root и nil
		// Выходные данные: инициализация узла nil, root = nil
		public BTree()
		{
			nil = new BNode<TKey, TData>();
			nil.parent = nil;
			nil.left = nil;
			nil.right = nil;
			nil.key = default;
			root = nil;
		}

		// Удаляет все элементы дерева, освобождая память
		// Формальные параметры: пусто
		// Входные данные: дерево
		// Выходные данные: root = nil
		public void Clear()
		{
			Console.WriteLine("Clearing the tree.");
			while (root != Nil)
				Delete(root);
			Info();
		}

		// Нахождение узла с заданным ключом и значением в дереве
		// Формальные параметры: ключ key, значение data
		// Входные данные: дерево
		// Выходные данные: узел с заданным ключом и списком, содержащим значение
		public BNode<TKey, TData> Find(TKey key, TData data)
		{
			bool isFound = false;
			BNode<TKey, TData> temp = root;
			BNode<TKey, TData> node = new BNode<TKey, TData>(key);

			while (!isFound)
			{
				if (temp == Nil)
					break;
				if (node.IsLess(temp))
					temp = temp.left;
				else if (node.IsMore(temp))
					temp = temp.right;
				else if (temp.Data.Contains(data))
					isFound = true;
				else
					break;
			}

			if (isFound)
				return temp;
			else
				return Nil;
		}

		// Нахождение узла с заданным ключом в дереве
		// Формальные параметры: ключ key
		// Входные данные: дерево
		// Выходные данные: узел с заданным ключом
		public BNode<TKey, TData> Find(TKey key)
		{
			bool isFound = false;
			BNode<TKey, TData> temp = root;
			BNode<TKey, TData> node = new BNode<TKey, TData>(key);

			while (!isFound)
			{
				if (temp == Nil)
					break;
				if (node.IsLess(temp))
					temp = temp.left;
				else if (node.IsMore(temp))
					temp = temp.right;
				else
					isFound = true;
			}

			if (isFound)
				return temp;
			else
				return Nil;
		}

        //// Нахождение узла с заданным ключом в дереве
        //// Формальные параметры: ключ key
        //// Входные данные: дерево
        //// Выходные данные: узел с заданным ключом
        //public SingleLinkedList<BNode<TKey, TData>> SearchCat(TKey key)
        //{
        //	bool isFound = false;
        //	BNode<TKey, TData> temp = root;
        //	BNode<TKey, TData> node = new BNode<TKey, TData>(key);
        //	SingleLinkedList<BNode<TKey, TData>> result = new SingleLinkedList<BNode<TKey, TData>>();
        //	ComparisonsList = new SingleLinkedList<int>();
        //	ComparisonsNumber = 0;

        //	while (!isFound)
        //	{
        //		if (temp == Nil)
        //			break;
        //		if (node.IsLess(temp))
        //			temp = temp.left;
        //		else if (node.IsMore(temp))
        //			temp = temp.right;
        //		else
        //			isFound = true;
        //	}

        //	if (isFound)
        //	{
        //		PushLNR(temp, result);
        //		return result;
        //	}
        //	else
        //		return result;
        //}






        // Нахождение списка узлов, ключ которых лежит в заданнам диапазоне
        // Формальные параметры: нижняя граница поиска ключ keyFrom, верхняя граница поиска ключ keyTo
        // Входные данные: дерево, счётчик сравнений, список сравнений
        // Выходные данные: список узлов, ключ которых лежит в заданнам диапазоне, счётчик сравнений, список сравнений
        public SingleLinkedList<BNode<TKey, TData>> Search(TKey keyFrom, TKey keyTo)
        {
            SingleLinkedList<BNode<TKey, TData>> result = new SingleLinkedList<BNode<TKey, TData>>();
            ComparisonsList = new SingleLinkedList<int>();
            ComparisonsNumber = 0;

            if (keyFrom.CompareTo(keyTo) <= 0)
            {
                if (FindMaximum().key.CompareTo(keyFrom) >= 0 && FindMinimum().key.CompareTo(keyTo) <= 0)
                {
                    PushLNR(root, keyFrom, keyTo, result);
                }
            }

            return result;
        }

        //// Рекурсивное заполнение списка узлов, ключ которых лежит в заданном диапазоне,
        //// основанное на симметричном обходе, начиная с переданного узла
        //// Формальные параметры: нижняя граница поиска ключ keyFrom, верхняя граница поиска ключ keyTo,
        //// список узлов result
        //// Входные данные: дерево, счётчик сравнений, список сравнений
        //// Выходные данные: список узлов, ключ которых лежит в заданнам диапазоне,
        //// счётчик сравнений, список количества сравнений 
        //private void PushLNR(BNode<TKey, TData> current, 
        //	SingleLinkedList<BNode<TKey, TData>> result)
        //{
        //	if (current != Nil)
        //	{

        //		ComparisonsNumber++;

        //			ComparisonsList.PushBack(ComparisonsNumber);
        //			result.PushBack(current);

        //	}
        //}

        // Рекурсивное заполнение списка узлов, ключ которых лежит в заданном диапазоне,
        // основанное на симметричном обходе, начиная с переданного узла
        // Формальные параметры: нижняя граница поиска ключ keyFrom, верхняя граница поиска ключ keyTo,
        // список узлов result
        // Входные данные: дерево, счётчик сравнений, список сравнений
        // Выходные данные: список узлов, ключ которых лежит в заданнам диапазоне,
        // счётчик сравнений, список количества сравнений 
        private void PushLNR(BNode<TKey, TData> current, TKey keyFrom, TKey keyTo,
			SingleLinkedList<BNode<TKey, TData>> result)
		{
			if (current != Nil)
			{
				PushLNR(current.left, keyFrom, keyTo, result);
				ComparisonsNumber++;
				if (current.key.CompareTo(keyFrom) >= 0 && current.key.CompareTo(keyTo) <= 0)
				{
					ComparisonsList.PushBack(ComparisonsNumber);
					result.PushBack(current);
				}
				PushLNR(current.right, keyFrom, keyTo, result);
			}
		}

		// Нахождение узла с минимальным ключом в дереве
		// Формальные параметры: пусто
		// Входные данные: дерево
		// Выходные данные: узел с минимальным ключом
		public BNode<TKey, TData> FindMinimum()
		{
			BNode<TKey, TData> node = FindMinimum(root);
			Console.WriteLine("Minimal node {0} in the tree was found.", node.key);
			return node;
		}

		// Нахождение узла с минимальным ключом в поддереве
		// Формальные параметры: узел-корень node поддерева
		// Входные данные: дерево
		// Выходные данные: узел с минимальным ключом в поддереве
		public BNode<TKey, TData> FindMinimum(BNode<TKey, TData> node)
		{
			BNode<TKey, TData> temp = node;

			if (temp == Nil)
				return Nil;

			while (temp.left != Nil)
				temp = temp.left;

			return temp;
		}

		// Нахождение узла с максимальным ключом в дереве
		// Формальные параметры: пусто
		// Входные данные: дерево
		// Выходные данные: узел с максимальным ключом
		public BNode<TKey, TData> FindMaximum()
		{
			BNode<TKey, TData> node = FindMaximum(root);
			Console.WriteLine("Maximal node {0} in the tree was found.", node.key);
			return node;
		}

		// Нахождение узла с максимальным ключом в поддереве
		// Формальные параметры: узел-корень node поддерева
		// Входные данные: дерево
		// Выходные данные: узел с максимальным ключом в поддереве
		public BNode<TKey, TData> FindMaximum(BNode<TKey, TData> node)
		{
			BNode<TKey, TData> temp = node;

			if (temp == Nil)
				return Nil;

			while (temp.right != Nil)
				temp = temp.right;

			return temp;
		}

		// Выводит значения полей узлов дерева на экран с учётом связей
		// Формальные параметры: пусто
		// Входные данные: дерево
		// Выходные данные: значения узлов дерева в порядке прямого обхода КЛП
		public string Info()
		{
			string result = string.Empty;

			if (root == Nil)
			{
				result += "Дерево пусто.\n";
			}
			else
			{
				result += Info(root);
			}

			return result;
		}

		// Выводит значения полей узлов поддерева на экран с учётом связей
		// Формальные параметры: узел-корень поддерева
		// Входные данные: дерево
		// Выходные данные: значения узлов поддерева в порядке прямого обхода КЛП
		private string Info(BNode<TKey, TData> current)
		{
			string result = string.Empty;

			if (current != Nil)
			{
				result += $"Ключ: {current.key}\n";
				result += $"Индексы:\n{current.Data.ElementsInfo()}\n";
				result += Info(current.left);
				result += Info(current.right);
			}

			return result;
		}

        // Удаляет при нахождении узел из дерева по правилу удаления в бинарном дереве
        // или удаляет лишь значение data из списка значений узла
        // и возвращает список значений узла
        // Формальные параметры: ключ key, значение data
        // Входные данные: дерево
        // Выходные данные: дерево, удовлетворяющее свойствам КЧ дерева
        public SingleLinkedList<TData> Delete(TKey key, TData data)
        {
            BNode<TKey, TData> Z = Find(key, data);
            Delete(Z, data);
            return Z.Data;
        }

        //// Выводит значения полей узлов дерева на экран с учётом связей
        //// Формальные параметры: пусто
        //// Входные данные: дерево
        //// Выходные данные: значения узлов дерева по порядку
        //public string InfoLikeTree()
        //{
        //	string result = string.Empty;

        //	if (root == Nil)
        //	{
        //		result += "The tree is empty.\n";
        //	}
        //	else
        //	{
        //		result += InfoLikeTree(root, 0);
        //		result += "____________________________________\n";
        //	}

        //	return result;
        //}

        //// Выводит значения полей узлов поддерева на экран с учётом связей
        //// Формальные параметры: узел-корень поддерева, число пробелов n
        //// Входные данные: дерево
        //// Выходные данные: значения узлов поддерева по порядку
        //private string InfoLikeTree(BNode<TKey, TData> current, int n)
        //{
        //	string result = string.Empty;

        //	if (current != Nil)
        //	{
        //		result += InfoLikeTree(current.right, n + 1);

        //		for (int i = 0; i < n; i++)
        //			result += "	";
        //		result += $"{current.key} ";
        //		if /*(current.color == Colour.Black)*/
        //			result += $" (B, {current.Data.Info()})\n";
        //		else
        //			result += $" (R, {current.Data.Info()})\n";

        //		result += InfoLikeTree(current.left, n + 1);
        //	}

        //	return result;
        //}


        // Выводит ключи узлов согласно прямому обходу дерева
        // Формальные параметры: пусто
        // Входные данные: дерево
        // Выходные данные: значения узлов дерева в порядке КЛП
        public void TraversalNLR()
		{
			Console.WriteLine("Pre-order NLR");
			TraversalNLR(root);
			if (root == Nil)
				Console.WriteLine("Empty tree.");
			Console.WriteLine();
		}

		// Выводит ключи узлов согласно прямому обходу поддерева
		// Формальные параметры: узел-корень current поддерева
		// Входные данные: дерево
		// Выходные данные: значения узлов поддерева в порядке КЛП
		private void TraversalNLR(BNode<TKey, TData> current)
		{
			if (current != Nil)
			{
				Console.Write("{0} ", current.key);
				TraversalNLR(current.left);
				TraversalNLR(current.right);
			}
		}

		// Добавляет новый узел по ключу и значению в дерево по правилу вставки в бинарное дерево
		// Формальные параметры: ключ key, значение data
		// Входные данные: дерево
		// Выходные данные: дерево с новым узлом, удовлетворяющее свойствам КЧ дерева
		public void Insert(TKey key, TData data)
		{
			BNode<TKey, TData> Z = new BNode<TKey, TData>(key, data);
			BNode<TKey, TData> Y = Nil;
			BNode<TKey, TData> X = root;

			while (X != Nil)
			{
				Y = X;
				if (Z.IsEqual(X))
				{
					X.Data.PushBack(data);
					return;
				}
				else if (Z.IsLess(X))
					X = X.left;
				else
					X = X.right;
			}
			Z.parent = Y;

			if (Y == Nil)
				root = Z;
			else if (Z.IsLess(Y))
				Y.left = Z;
			else
				Y.right = Z;

			Z.left = Nil;
			Z.right = Nil;
		}

		// Меняет поддеревья местами
		// Формальные параметры: узел X, узел Y
		// Входные данные: дерево
		// Выходные данные: дерево с изменёнными связями
		private void Transplant(BNode<TKey, TData> X, BNode<TKey, TData> Y)
		{
			if (X.parent == Nil)
				root = Y;
			else if (X == X.parent.left)
				X.parent.left = Y;
			else
				X.parent.right = Y;

			Y.parent = X.parent;
		}

		// Удаляет при нахождении узел из дерева по правилу удаления в бинарном дереве
		// и возвращает список значений узла
		// Формальные параметры: ключ key
		// Входные данные: дерево
		// Выходные данные: дерево, удовлетворяющее свойствам КЧ дерева
		public SingleLinkedList<TData> Delete(TKey key)
		{
			BNode<TKey, TData> Z = Find(key);
			Delete(Z);
			return Z.Data;
		}

		// Удаляет узел из дерева по правилу удаления в бинарном дереве
		// Формальные параметры: узел Z дерева
		// Входные данные: дерево
		// Выходные данные: дерево, удовлетворяющее свойствам КЧ дерева
		public void Delete(BNode<TKey, TData> Z)
		{
			BNode<TKey, TData> Y = Z;
			BNode<TKey, TData> X = Nil;

            if (Z == Nil)
            {
                //Console.WriteLine("Nothing to delete!");
                return;
            }
            if (Z.left == Nil)
            {
                X = Z.right;
                Transplant(Z, Z.right);
            }
            else if (Z.right == Nil)
            {
                X = Z.left;
                Transplant(Z, Z.left);
            }
            else
            {
                Y = FindMaximum(Z.left);
                //Console.WriteLine("Minimum {0} was found.", Y.key);
                if (Y == Nil)
                {
                    //Console.WriteLine("Node does not have minimum.");
                    return;
                }
                X = Y.left;
                if (Y.parent == Z)
                    X.parent = Y;
                else
                {
                    Transplant(Y, Y.left);
                    Y.left = Z.left;
                    Y.left.parent = Y;
                }
                Transplant(Z, Y);

                Y.right = Z.right;
                Y.right.parent = Y;
            }

            //if (Z == Nil)
            //         {
            //             //Console.WriteLine("Nothing to delete!");
            //             return;
            //         }




        }

        // Удаляет узел из дерева по правилу удаления в бинарном дереве или удаляет лишь индекс data
        // Формальные параметры: узел Z дерева, значение data
        // Входные данные: дерево
        // Выходные данные: дерево, удовлетворяющее свойствам КЧ дерева
        public void Delete(BNode<TKey, TData> Z, TData data)
        {
            BNode<TKey, TData> Y = Z;
            BNode<TKey, TData> X = Nil;

            if (Z.Data.Count > 1)
            {
                Z.Data.Remove(data);
                return;
            }
            if (Z == Nil)
            {
                Console.WriteLine("Nothing to delete!");
                return;
            }
            if (Z.left == Nil)
            {
                X = Z.right;
                Transplant(Z, Z.right);
            }
            else if (Z.right == Nil)
            {
                X = Z.left;
                Transplant(Z, Z.left);
            }
            else
            {
                Y = FindMaximum(Z.left);
                Console.WriteLine("Minimum {0} was found.", Y.key);
                if (Y == Nil)
                {
                    Console.WriteLine("Node does not have minimum.");
                    return;
                }
                X = Y.left;
                if (Y.parent == Z)
                    X.parent = Y;
                else
                {
                    Transplant(Y, Y.left);
                    Y.left = Z.left;
                    Y.left.parent = Y;
                }
                Transplant(Z, Y);

                Y.right = Z.right;
                Y.right.parent = Y;

            }
        }
    }
}
