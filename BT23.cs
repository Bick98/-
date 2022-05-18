using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace CW_ThoughtsOutLoud
{
    public class BT23
    {
        MainForm f1;
        public class Node
        {
            public ChainForBinary data = new ChainForBinary();
            public Node left;
            public Node right;
            public Node(App data)
            {
                this.data.Add(data);
            }
            
        }

        public static Node root;

        private readonly Node nil;
        public Node Nil => nil;

        public void Add(App data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (String.Compare(n.data.pBegin.data.My.Category, current.data.pBegin.data.My.Category) < 0)
            {
                current.left = RecursiveInsert(current.left, n);
            }
            else if (String.Compare(n.data.pBegin.data.My.Category, current.data.pBegin.data.My.Category) > 0)
            {
                current.right = RecursiveInsert(current.right, n);
            }
            else if (String.Compare(n.data.pBegin.data.My.Category, current.data.pBegin.data.My.Category) == 0) current.data.Add(n.data.pBegin.data);
            return current;
        }
        public void Delete(App target)
        {//and here
            root = Delete(root, target);
        }
        private Node Delete(Node current, App target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                //left subtree
                if (String.Compare(target.My.Category, current.data.pBegin.data.My.Category) < 0)
                {
                    current.left = Delete(current.left, target);
                }
                //right subtree
                else if (String.Compare(target.My.Category, current.data.pBegin.data.My.Category) > 0)
                {
                    current.right = Delete(current.right, target);

                }
                //if target is found
                else
                {
                    if (current.left != null)
                    {
                        //delete its inorder successor
                        parent = current.left;
                        while (parent.right != null)
                        {
                            parent = parent.right;
                        }
                        if (current.data.size > 1)
                        {
                            current.data.Delete(target);
                            return current;
                        }
                        else
                        {
                            current.data = parent.data;
                            current.left = Delete(current.left, parent.data.pBegin.data);

                        }
                    }
                    else
                    {   //if current.left != null
                        if (current.data.size > 1) current.data.Delete(target);
                        else return current.right;
                    }
                }
            }
            return current;
        }

        //public virtual void Contains(Node current, string value, ref int count, MainForm f)
        //{
        //    this.f1 = f;
        //    if (current != null)
        //    {
        //        count++;
        //        if (current.data.pBegin.data.My.Category == value)
        //        {
        //            for (int i = 0; i < f.ApplicationsGridView.Rows.Count; i++)
        //            {
        //                string l = f.ApplicationsGridView.Rows[i].Cells[2].Value.ToString();
        //                if (l == value) f.ApplicationsGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
        //            }
        //            MessageBox.Show($"{count} - сравнений");
        //        }
        //        if (String.Compare(value, current.data.pBegin.data.My.Category) <= 0) Contains(current.left, value, ref count, f);
        //        if (String.Compare(value, current.data.pBegin.data.My.Category) >= 0) Contains(current.right, value, ref count, f);
        //    }
        //}

        public void DisplayTree()
        {
            if (root == null)
            {
                Debug.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                current.data.Print();
                InOrderDisplayTree(current.right);
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        public int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        public string Info() //*************************************8
        {
            string result = string.Empty;

            if (root == nil)
            {
                result += "Дерево пусто.\n";
            }
            else
            {
                result += Info(root);
            }

            return result;
        }

        // Удаляет все элементы дерева, освобождая память ************************************
        // Формальные параметры: пусто
        // Входные данные: дерево
        // Выходные данные: root = nil
        public void Clear()
        {

            Console.WriteLine("Clearing the tree.");
            while (root != Nil)
            {
                root.data.Clear();
                //Delete(root.);
            }
            Info();
        }

        // Выводит значения полей узлов поддерева на экран с учётом связей*****************************
        // Формальные параметры: узел-корень поддерева
        // Входные данные: дерево
        // Выходные данные: значения узлов поддерева в порядке прямого обхода КЛП
        private string Info(Node current)
        {
            string result = string.Empty;

            if (current != Nil)
            {
                result += $"Ключ: {current.data}\n";
                //result += $"Индексы:\n{current.Data.ElementsInfo()}\n";
                result += Info(current.left);
                result += Info(current.right);
            }

            return result;
        }

        // Нахождение узла с заданным ключом в дереве
        // Формальные параметры: ключ key
        // Входные данные: дерево
        // Выходные данные: узел с заданным ключом
        public Node Find(App key)
        {
            bool isFound = false;
            Node temp = root;
            Node node = new Node(key);

            while (!isFound)
            {
                if (temp == Nil)
                    break;
                if (String.Compare(temp.data.pBegin.data.My.Category, node.data.pBegin.data.My.Category) < 0)
                    temp = temp.left;
                else if (String.Compare(temp.data.pBegin.data.My.Category, node.data.pBegin.data.My.Category) > 0)
                    temp = temp.right;
                else
                    isFound = true;
            }

            if (isFound)
                return temp;
            else
                return Nil;
        }

    }

}