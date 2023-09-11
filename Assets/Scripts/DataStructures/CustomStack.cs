using System;

[Serializable]
public class CustomStack<T> {

    private class Node {
        public T Data { get; set; }
        public Node Next { get; set; }
    }

    private Node Root { get; set; }
    private Node Last { get; set; }
    private int Size { get; set; }

    public CustomStack() {
        Root = null;
        Size = 0;
    }

    public CustomStack(T data) {
        Node newNode = GenerateNode(data);
        Root = newNode;
        Last = newNode;
        Size = 1;
    }

    public CustomStack(T[] dataArray) {
        Root = null;
        Size = 0;
        foreach (T data in dataArray) {
            PushFront(data);
        }
    }

    private Node GenerateNode(T data) {
        Node newNode = new Node {
            Data = data,
            Next = null
        };
        return newNode;
    }

    public void PushFront(T data) {
        Size++;
        if (Root == null) {
            Node newNode = GenerateNode(data);
            Root = newNode;
            Last = newNode;
            return;
        }
        Node tmp = GenerateNode(data);
        tmp.Next = Root;
        Root = tmp;
    }

    public bool IsEmpty() {
        return Root == null;
    }

    public T PopFront() {
        if (Root == null) {
            throw new InvalidOperationException("Stack is empty");
        }
        if (Root.Next == null) {
            Node tmp = Root;
            T data = tmp.Data;
            Root = null;
            Last = null;
            Size--;
            return data;
        }
        Node poppedNode = Root;
        T poppedData = poppedNode.Data;
        Root = Root.Next;
        Size--;
        return poppedData;
    }

    public T PopBack() {
        if (Root == null) {
            throw new InvalidOperationException("Stack is empty");
        }
        if (Root.Next == null) {
            Node tmp = Root;
            T data = tmp.Data;
            Root = null;
            Last = null;
            Size--;
            return data;
        }
        Node pIt = Root;
        while (pIt.Next.Next != null) {
            pIt = pIt.Next;
        }
        T poppedData = Last.Data;
        Last = null;
        pIt.Next = null;
        Size--;
        return poppedData;
    }

    public void Clear() {
        while (Root != null) {
            Node nextNode = Root.Next;
            Root = null;
            Root = nextNode;
        }
        Size = 0;
        Last = null;
    }

    public int Count() {
        return Size;
    }

    public T Peek() {
        if (Root == null) {
            throw new InvalidOperationException("Stack is empty");
        }
        if (Root.Next == null) {
            return Root.Data;
        }
        return Root.Data;
    }
}