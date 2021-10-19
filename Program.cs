using System;
using System.Diagnostics;
using System.IO;

namespace alg2 {  
     static class Backtracking {  
        static int N;  
        static void printBoard(int[, ] board) {  
            for (int i = 0; i < N; i++) {  
                for (int j = 0; j < N; j++) {  
                    Console.Write(board[i, j] + " ");  
                }  
                Console.Write("\n");  
            }  
        }  
        static Boolean toPlaceOrNotToPlace(int[, ] board, int row, int col) {  
            int i, j;  
            for (i = 0; i < col; i++) {  
                if (board[row, i] == 1) return false;  
            }  
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--) {  
                if (board[i, j] == 1) return false;  
            }  
            for (i = row, j = col; j >= 0 && i < N; i++, j--) {  
                if (board[i, j] == 1) return false;  
            }  
            return true;  
        }  
        static Boolean theBoardSolver(int[, ] board, int col) {  
            if (col >= N) return true;  
            for (int i = 0; i < N; i++) {  
                if (toPlaceOrNotToPlace(board, i, col)) {  
                    board[i, col] = 1;  
                    if (theBoardSolver(board, col + 1)) return true;  
                    // Backtracking is hella important in this one.  
                    board[i, col] = 0;  
                }  
            }  
            return false;  
        }

        public static void DoTask2() {
                Console.WriteLine("Введите размер поля:");
                string path2 = @"..\..\Data2.csv";
                int K = Convert.ToInt32(Console.ReadLine());
                
                for (N = 0; N < K; N++) {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    int[, ] board = new int[N, N];  
                    if (!theBoardSolver(board, 0)) {  
                        Console.WriteLine("Solution not found.");  
                    }  
                    //printBoard(board);  
                    stopwatch.Stop();
                    string time = (stopwatch.ElapsedTicks).ToString();
                    File.AppendAllText(path2, time + ";");
                }
            }
    }
     class GFG {
         internal class Node {
             public int data;
             public Node left, right;
         };

/* This function is here just to test buildTree() */
public static void printInorder(Node node) {
             if (node == null)
                 return;
             printInorder(node.left);
             Console.Write(node.data + " ");
             printInorder(node.right);
         }

// Recursively consubtree under given root using
// leftChil[] and rightchild
          public static Node buildCartesianTreeUtil(int root, int[] arr,
             int[] parent, int[] leftchild, int[] rightchild) {
             if (root == -1)
                 return null;

             // Create a new node with root's data
             Node temp = new Node();
             temp.data = arr[root];

             // Recursively conleft and right subtrees
             temp.left = buildCartesianTreeUtil(leftchild[root],
                 arr, parent, leftchild, rightchild);
             temp.right = buildCartesianTreeUtil(rightchild[root],
                 arr, parent, leftchild, rightchild);

             return temp;
         }

// A function to create the Cartesian Tree in O(N) time
public static Node buildCartesianTree(int[] arr, int n) {
             // Arrays to hold the index of parent, left-child,
             // right-child of each number in the input array
             int[] parent = new int[n];
             int[] leftchild = new int[n];
             int[] rightchild = new int[n];

             // Initialize all array values as -1
             memset(parent, -1);
             memset(leftchild, -1);
             memset(rightchild, -1);

             // 'root' and 'last' stores the index of the root and the
             // last processed of the Cartesian Tree.
             // Initially we take root of the Cartesian Tree as the
             // first element of the input array. This can change
             // according to the algorithm
             int root = 0, last;

             // Starting from the second element of the input array
             // to the last on scan across the elements, adding them
             // one at a time.
             for (int i = 1; i <= n - 1; i++) {
                 last = i - 1;
                 rightchild[i] = -1;

                 // Scan upward from the node's parent up to
                 // the root of the tree until a node is found
                 // whose value is greater than the current one
                 // This is the same as Step 2 mentioned in the
                 // algorithm
                 while (arr[last] <= arr[i] && last != root)
                     last = parent[last];

                 // arr[i] is the largest element yet; make it
                 // new root
                 if (arr[last] <= arr[i]) {
                     parent[root] = i;
                     leftchild[i] = root;
                     root = i;
                 }

                 // Just insert it
                 else if (rightchild[last] == -1) {
                     rightchild[last] = i;
                     parent[i] = last;
                     leftchild[i] = -1;
                 }

                 // Reconfigure links
                 else {
                     parent[rightchild[last]] = i;
                     leftchild[i] = rightchild[last];
                     rightchild[last] = i;
                     parent[i] = last;
                 }

             }

             // Since the root of the Cartesian Tree has no
             // parent, so we assign it -1
             parent[root] = -1;

             return (buildCartesianTreeUtil(root, arr, parent,
                 leftchild, rightchild));
         }

         static void memset(int[] arr, int value) {
             for (int i = 0; i < arr.Length; i++) {
                 arr[i] = value;
             }

         }
     }

     class Program {
         static void Main(string[] args) {
             //Backtracking.DoTask2();
             
             static void DoTask6() {
                 string path6 = @"..\..\Data7.csv";
                 int K = 1000;
                 int[] arr = new int[1000];

                 for (int i = 1; i < K; i++) {
                     Stopwatch stopwatch = new Stopwatch();
                     stopwatch.Start();
                     for (int j = 1; j <= i; j++) {
                         Random rnd = new Random();
                         arr[j] = rnd.Next(1, 500);

                         int n = arr.Length;
                         
                     
                         GFG.Node root = GFG.buildCartesianTree(arr, n);

                         /* Let us test the built tree by printing Inorder
                         traversal */
                         //Console.Write("Inorder traversal of the" +
                         //              " constructed tree : \n");
                         //GFG.printInorder(root);
                         
                     }
                     stopwatch.Stop();
                     string time = (stopwatch.ElapsedTicks).ToString();
                     File.AppendAllText(path6, time + ";");
                 }
             }
             
             DoTask6();
             
         }
     }
}