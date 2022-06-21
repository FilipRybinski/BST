using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BST
{
       
    public class BST
    {
        public class Node
        {
            public int value;
            public Node left, right;
            public Node(int value)
            {
                this.value = value;
                left = right = null;
            }
        }
        Node root;
        public BST()
        {
            root = null;
        }
        public void Delete(int value)
        {
            root= DeleteR(root, value);
        }
        private Node DeleteR(Node root, int value)
        {

            if (root == null)
                return root;

            if (value < root.value)
            {
                root.left = DeleteR(root.left, value);
            }
            if (value > root.value)
            {
                root.right = DeleteR(root.right, value);
            }
            else
            {

                if (root.left == null)
                    return root.right;
                if (root.right == null)
                    return root.left;


                root.value = Find(root.left);

                root.left = DeleteR(root.left, root.value);
            }
            return root;
        }
    
        private int Find(Node root)
        {
            int max = root.value;
            while (root.right != null)
            {
                max = root.right.value;
                root = root.right;
            }
            return max;
        }
        public Node Search(int value)
        {
            return SearchR(root, value);
        }
        private Node SearchR(Node root, int value)
        {
            if (root != null)
            {
                if (value < root.value) return SearchR(root.left, value);
                else if (value > root.value) return SearchR(root.right, value);
                else return root;
            }
            return root;

        }
        public void insert(int value)
        {
            root = insertR(root, value);
        }
        private Node insertR(Node root, int value)
        {
            if (root == null)
            {
                root = new Node(value);
                return root;
            }
            if (value < root.value)
                root.left = insertR(root.left, value);
            else if (value > root.value)
                root.right = insertR(root.right, value);
            return root;
        }
        public void RLR(Node root)
        {
            if (root == null)
            {
                return;
            }
            Console.Write(root.value+" ");
            RLR(root.left);
            RLR(root.right);
        }
        public void LRR(Node root)
        {
            if (root == null)
            {
                return;
            }
            LRR(root.left);
            Console.Write(root.value + " ");
            LRR(root.right);
        }
        public void LRightR(Node root)
        {
            if (root == null)
            {
                return;
            }
            LRightR(root.left);
            LRightR(root.right);
            Console.Write(root.value + " ");
        }

        public void RLR_OutTest3(Node root,StreamWriter sw)
        {
            if (root == null)
            {
                return;
            }
            sw.Write(root.value + " ");
            RLR_OutTest3(root.left,sw);
            RLR_OutTest3(root.right,sw);
        }
        public void Menu_main(BST tree)
        {

            int choice;
            Console.WriteLine("Wybierz interesująca Cię opcje");
            Console.Write("1.Zapisz elementy drzewa BST (wraz z wagami umieszczonymi w nawiasach) do pliku OutTest3.txt w kolejności KLP\n2.Dodaj element do drzewa BST\n3. Usuń element z drzewa BST\n4.Wypisz elementy drzewa BST\n5.Losowe Liczby\n6.Wypisz wylosowane liczby z pliku\n7.EXIT");
            do
            {
                do
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5 && choice != 6 && choice != 7);
                switch (choice)
                {
                    case 1:
                        using (StreamWriter sw = new StreamWriter("OutTest3.txt"))
                        {
                            RLR_OutTest3(root, sw);
                            sw.Close();
                        }
                        break;
                    case 2:
                        int value_insert;
                        bool if_number_insert;
                        Console.WriteLine("Wprowadz wartosc ktora chcesz umiescic w drzewie");
                        do { if_number_insert = int.TryParse(Console.ReadLine(), out value_insert); } while (!if_number_insert);
                        insert(value_insert);
                        break;
                    case 3:
                        int value_delete;
                        bool if_number_delete;
                        Console.WriteLine("Wprowadz wartosc ktora chcesz usunac z drzewa");
                        do { if_number_delete = int.TryParse(Console.ReadLine(), out value_delete); } while (!if_number_delete);
                        Delete(value_delete);
                        break;
                    case 4:
                        Console.Write("KLP ");
                        RLR(root);
                        Console.WriteLine();
                        Console.Write("LKP ");
                        LRR(root);
                        Console.WriteLine();
                        Console.Write("LPK ");
                        LRightR(root);
                        Console.WriteLine();
                        break;
                    case 5:
                        int count,i=1;
                        Random random = new Random();
                        bool if_number_count;
                        Console.WriteLine("Ile liczb chcesz wylosowac ?");
                        do { if_number_count = int.TryParse(Console.ReadLine(), out count); } while (!if_number_count);
                        using (StreamWriter sw = new StreamWriter("OutTest2.txt"))
                        {
                            do { int rnd = random.Next(0, 1000); sw.Write(rnd + " "); insert(rnd);  i++;  } while (i != count);
                            sw.Close();
                        }
                        break;
                    case 6:
                        using(StreamReader sr=new StreamReader("OutTest2.txt"))
                        {
                           Console.WriteLine(sr.ReadToEnd());
                        }
                        break;
                    case 7:
                        break;
                    

                }
                Console.WriteLine("Co dalej ..?");
            } while (choice != 7);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            BST tree = new BST();
            try
            {
                using (StreamReader sr = new StreamReader("In0207.txt"))
                {
                    string line;
                    string[] spliteline;
                    int result;
                    while ((line = sr.ReadLine()) != null)
                    {
                        spliteline = line.Split(' ');
                        foreach(var sl in spliteline)
                        {
                            result = Int32.Parse(sl);
                            tree.insert(result);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            tree.Menu_main(tree);
            
        }
    }
}
